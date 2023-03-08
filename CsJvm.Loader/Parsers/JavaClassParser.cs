using CsJvm.Loader.Extensions;
using CsJvm.Models;
using CsJvm.Models.ClassFileFormat.Attributes;
using CsJvm.Models.ClassFileFormat.ConstantPool;
using CsJvm.Models.ClassFileFormat.Methods;

namespace CsJvm.Loader.Parsers
{
    /// <summary>
    /// JavaClass Parser
    /// </summary>
    public static class JavaClassParser
    {
        /// <summary>
        /// Parses methods
        /// </summary>
        /// <param name="javaClass">JavaClass instance</param>
        /// <param name="classFile">ClassFile data</param>
        public static JavaClass ParseMethods(this JavaClass javaClass)
        {
            foreach (var info in javaClass.ClassFile.Methods)
            {
                var methodName = $"{info.Name}:{info.Descriptor}";

                // check native methods first
                if (info.AccessFlags.HasFlag(MethodAccessAndPropertyFlags.ACC_NATIVE))
                {
                    javaClass.Methods[methodName] = new JavaMethod
                    {
                        Name = methodName,
                        AccessFlags = info.AccessFlags,
                        CodeAttribute = null
                    };
                }

                foreach (var attr in info.Attributes)
                {
                    switch (attr)
                    {
                        case CodeAttribute codeAttribute:
                            {
                                javaClass.Methods[methodName] = new JavaMethod
                                {
                                    Name = methodName,
                                    AccessFlags = info.AccessFlags,
                                    CodeAttribute = codeAttribute
                                };
                            }
                            break;

                        case DeprecatedAttribute deprAttr:
                            {

                            }
                            break;

                        case RuntimeVisibleAnnotationsAttribute rvaAttr:
                            {

                            }
                            break;

                        case ExceptionsAttribute excAttr:
                            {

                            }
                            break;

                        case SignatureAttribute signatureAttr:
                            {
                                if (javaClass.ConstantPool[signatureAttr.SignatureIndex] is not CONSTANT_Utf8Info utf8Info)
                                    throw new InvalidDataException("The entry must be a CONSTANT_Utf8Info structure");
                            }
                            break;

                        case RuntimeInvisibleAnnotationsAttribute riaAttr:
                            {

                            }
                            break;

                        case RuntimeInvisibleParameterAnnotationsAttribute ripaAttr:
                            {

                            }
                            break;

                        case AnnotationDefaultAttribute adAttr:
                            {

                            }
                            break;

                        case SyntheticAttribute syntAttr:
                            {

                            }
                            break;

                        default:
                            throw new NotImplementedException(attr.Name);

                    }
                }
            }

            return javaClass;
        }


        /// <summary>
        /// Loads fields
        /// </summary>
        /// <param name="javaClass">JavaClass instance</param>
        public static JavaClass ParseFields(this JavaClass javaClass)
        {
            foreach (var field in javaClass.ClassFile.Fields)
            {
                var fieldFullName = $"{field.Name}:{field.Descriptor}";
                javaClass.Fields[fieldFullName] = field.Descriptor.GetDefaultValue();

                // fill atributes
                for (var i = 0; i < field.AttributesCount; i++)
                {
                    switch (field.Attributes[i])
                    {
                        case ConstantValueAttribute constAttr:
                            {
                                var attrIndex = constAttr.ConstantValueIndex;

                                javaClass.Fields[fieldFullName] = javaClass.ClassFile.ConstantPool[attrIndex] switch
                                {
                                    CONSTANT_IntegerInfo constInt => constInt.Value,
                                    CONSTANT_FloatInfo constFloat => constFloat.Value,
                                    CONSTANT_LongInfo constLong => constLong.Value,
                                    CONSTANT_DoubleInfo constDouble => constDouble.Value,
                                    CONSTANT_StringInfo constString => ((CONSTANT_Utf8Info)javaClass.ConstantPool[constString.StringIndex]).Utf8String.ToCharArray(),
                                    _ => throw new InvalidOperationException($"Unknown constant type at index: {attrIndex}"),
                                };
                                break;
                            }

                        case SignatureAttribute signatureAttr:
                            javaClass.Fields[fieldFullName] = ((CONSTANT_Utf8Info)javaClass.ConstantPool[signatureAttr.SignatureIndex]).Utf8String;
                            break;

                        // TODO
                        case DeprecatedAttribute deprAttr:
                        case RuntimeVisibleAnnotationsAttribute rvaAttr:
                        case RuntimeInvisibleAnnotationsAttribute riaAttr:
                        case SyntheticAttribute synthAttr:
                            break;

                        default:
                            throw new NotImplementedException(field.Attributes[i].Name);
                    }
                }
            }

            return javaClass;
        }

        /// <summary>
        /// Parses attributes
        /// </summary>
        /// <param name="javaClass">JavaClass instance</param>
        public static JavaClass ParseAttributes(this JavaClass javaClass)
        {
            javaClass.Attributes = javaClass.ClassFile.Attributes;

            foreach (var attr in javaClass.ClassFile.Attributes)
            {
                // get all BootstrapMethods_attribute's from class
                switch (attr)
                {
                    case BootstrapMethodsAttribute bootstrapAttr:
                        {
                            foreach (var method in bootstrapAttr.BootstrapMethods)
                            {
                                var methodInfo = (CONSTANT_MethodHandleInfo)javaClass.ConstantPool[method.BootstrapMethodRef];
                                var key = string.Empty;

                                switch ((BytecodeBehaviorsForMethodHandles)methodInfo.ReferenceKind)
                                {
                                    case BytecodeBehaviorsForMethodHandles.REF_getField:
                                    case BytecodeBehaviorsForMethodHandles.REF_getStatic:
                                    case BytecodeBehaviorsForMethodHandles.REF_putField:
                                    case BytecodeBehaviorsForMethodHandles.REF_putStatic:
                                        {
                                            var fieldrefInfo = (CONSTANT_FieldrefInfo)javaClass.ConstantPool[methodInfo.ReferenceIndex];

                                            var classInfo = (CONSTANT_ClassInfo)javaClass.ConstantPool[fieldrefInfo.ClassIndex];
                                            var nameAndTypeInfo = (CONSTANT_NameAndTypeInfo)javaClass.ConstantPool[fieldrefInfo.NameAndTypeIndex];

                                            var dynClassName = ((CONSTANT_Utf8Info)javaClass.ConstantPool[classInfo.NameIndex]).Utf8String;
                                            var methodName = ((CONSTANT_Utf8Info)javaClass.ConstantPool[nameAndTypeInfo.NameIndex]).Utf8String;
                                            var decriptorName = ((CONSTANT_Utf8Info)javaClass.ConstantPool[nameAndTypeInfo.DescriptorIndex]).Utf8String;

                                            key = $"{dynClassName}.{methodName}:{decriptorName}";
                                        }
                                        break;

                                    case BytecodeBehaviorsForMethodHandles.REF_invokeVirtual:
                                    case BytecodeBehaviorsForMethodHandles.REF_newInvokeSpecial:
                                        {
                                            var methodrefInfo = (CONSTANT_MethodrefInfo)javaClass.ConstantPool[methodInfo.ReferenceIndex];

                                            var classInfo = (CONSTANT_ClassInfo)javaClass.ConstantPool[methodrefInfo.Classindex];
                                            var nameAndTypeInfo = (CONSTANT_NameAndTypeInfo)javaClass.ConstantPool[methodrefInfo.NameAndTypeIndex];

                                            var dynClassName = ((CONSTANT_Utf8Info)javaClass.ConstantPool[classInfo.NameIndex]).Utf8String;
                                            var methodName = ((CONSTANT_Utf8Info)javaClass.ConstantPool[nameAndTypeInfo.NameIndex]).Utf8String;
                                            var decriptorName = ((CONSTANT_Utf8Info)javaClass.ConstantPool[nameAndTypeInfo.DescriptorIndex]).Utf8String;

                                            key = $"{dynClassName}.{methodName}:{decriptorName}";
                                        }
                                        break;

                                    case BytecodeBehaviorsForMethodHandles.REF_invokeStatic:
                                    case BytecodeBehaviorsForMethodHandles.REF_invokeSpecial:
                                        {
                                            switch (javaClass.ConstantPool[methodInfo.ReferenceIndex])
                                            {
                                                case CONSTANT_MethodrefInfo methodrefInfo:
                                                    {
                                                        var classInfo = (CONSTANT_ClassInfo)javaClass.ConstantPool[methodrefInfo.Classindex];
                                                        var nameAndTypeInfo = (CONSTANT_NameAndTypeInfo)javaClass.ConstantPool[methodrefInfo.NameAndTypeIndex];

                                                        var dynClassName = ((CONSTANT_Utf8Info)javaClass.ConstantPool[classInfo.NameIndex]).Utf8String;
                                                        var methodName = ((CONSTANT_Utf8Info)javaClass.ConstantPool[nameAndTypeInfo.NameIndex]).Utf8String;
                                                        var decriptorName = ((CONSTANT_Utf8Info)javaClass.ConstantPool[nameAndTypeInfo.DescriptorIndex]).Utf8String;

                                                        key = $"{dynClassName}.{methodName}:{decriptorName}";
                                                        break;
                                                    }

                                                case CONSTANT_InterfaceMethodrefInfo interfaceMethodrefInfo:
                                                    {
                                                        var classInfo = (CONSTANT_ClassInfo)javaClass.ConstantPool[interfaceMethodrefInfo.ClassIndex];
                                                        var nameAndTypeInfo = (CONSTANT_NameAndTypeInfo)javaClass.ConstantPool[interfaceMethodrefInfo.NameAndTypeIndex];

                                                        var dynClassName = ((CONSTANT_Utf8Info)javaClass.ConstantPool[classInfo.NameIndex]).Utf8String;
                                                        var methodName = ((CONSTANT_Utf8Info)javaClass.ConstantPool[nameAndTypeInfo.NameIndex]).Utf8String;
                                                        var decriptorName = ((CONSTANT_Utf8Info)javaClass.ConstantPool[nameAndTypeInfo.DescriptorIndex]).Utf8String;

                                                        key = $"{dynClassName}.{methodName}:{decriptorName}";
                                                        break;
                                                    }

                                                default:
                                                    throw new InvalidDataException($"Unknown bootstrap method info: {javaClass.ConstantPool[methodInfo.ReferenceIndex].GetType()}");
                                            }
                                        }
                                        break;

                                    case BytecodeBehaviorsForMethodHandles.REF_invokeInterface:
                                        {
                                            var interfaceMethodrefInfo = (CONSTANT_InterfaceMethodrefInfo)javaClass.ConstantPool[methodInfo.ReferenceIndex];

                                            var classInfo = (CONSTANT_ClassInfo)javaClass.ConstantPool[interfaceMethodrefInfo.ClassIndex];
                                            var nameAndTypeInfo = (CONSTANT_NameAndTypeInfo)javaClass.ConstantPool[interfaceMethodrefInfo.NameAndTypeIndex];

                                            var dynClassName = ((CONSTANT_Utf8Info)javaClass.ConstantPool[classInfo.NameIndex]).Utf8String;
                                            var methodName = ((CONSTANT_Utf8Info)javaClass.ConstantPool[nameAndTypeInfo.NameIndex]).Utf8String;
                                            var decriptorName = ((CONSTANT_Utf8Info)javaClass.ConstantPool[nameAndTypeInfo.DescriptorIndex]).Utf8String;

                                            key = $"{dynClassName}.{methodName}:{decriptorName}";
                                        }
                                        break;

                                    default:
                                        throw new InvalidDataException($"Unknown bootstrap method handle: {methodInfo.ReferenceKind}");
                                }

                                javaClass.BootstrapMethods[key] = method;
                            }

                            break;
                        }

                    case InnerClassesAttribute innerClassAttr:
                        {
                            foreach (var innerClass in innerClassAttr.Classes)
                            {
                                var icNameIndex = ((CONSTANT_ClassInfo)javaClass.ConstantPool[innerClass.InnerClassInfoIndex]);
                                var icName = ((CONSTANT_Utf8Info)javaClass.ConstantPool[icNameIndex.NameIndex]).Utf8String;

                                javaClass.InnerClasses.Add(icName);

                                if (innerClass.OuterClassInfoIndex != 0)
                                {
                                    var ocNameIndex = (CONSTANT_ClassInfo)javaClass.ConstantPool[innerClass.OuterClassInfoIndex];
                                    var ocName = ((CONSTANT_Utf8Info)javaClass.ConstantPool[ocNameIndex.NameIndex]).Utf8String;
                                    javaClass.OuterClasses.Add(ocName);
                                }

                                if (innerClass.InnerNameIndex != 0)
                                {
                                    var innerName = ((CONSTANT_Utf8Info)javaClass.ConstantPool[innerClass.InnerNameIndex]).Utf8String;
                                    //javaClass.InnerClasses.Add(innerName);
                                }
                            }

                            break;
                        }

                    case SourceFileAttribute sourceAttr:
                        {
                            var sourceName = ((CONSTANT_Utf8Info)javaClass.ConstantPool[sourceAttr.SourceFileIndex]).Utf8String;
                            break;
                        }

                    case SignatureAttribute signAttr:
                        {
                            var signature = ((CONSTANT_Utf8Info)javaClass.ConstantPool[signAttr.SignatureIndex]).Utf8String;
                            break;
                        }

                    case DeprecatedAttribute deprAttr:
                        {
                            var index = ((CONSTANT_Utf8Info)javaClass.ConstantPool[deprAttr.AttributeNameIndex]).Utf8String;
                            break;
                        }

                    case EnclosingMethodAttribute enclAttr:
                        {
                            var index = ((CONSTANT_Utf8Info)javaClass.ConstantPool[enclAttr.AttributeNameIndex]).Utf8String;
                            break;
                        }

                    case RuntimeVisibleAnnotationsAttribute rvaAttr:
                        {
                            var index = ((CONSTANT_Utf8Info)javaClass.ConstantPool[rvaAttr.AttributeNameIndex]).Utf8String;
                            break;
                        }

                    case NestMembersAttribute nestMembers:
                        {
                            var index = nestMembers.AttributeNameIndex;
                            break;
                        }

                    default:
                        throw new NotImplementedException($"Unknown attribute: {attr.GetType()}");
                }
            }

            return javaClass;
        }

        /// <summary>
        /// Parses super-class
        /// </summary>
        /// <param name="javaClass">JavaClass instance</param>
        /// <param name="classFile">ClassFile data</param>
        public static JavaClass ParseSuperclass(this JavaClass javaClass)
        {
            if (javaClass.ClassFile.SuperClass != 0)
            {
                var classInfo = (CONSTANT_ClassInfo)javaClass.ConstantPool[javaClass.ClassFile.SuperClass];
                var superClassName = ((CONSTANT_Utf8Info)javaClass.ConstantPool[classInfo.NameIndex])!.Utf8String;

                // fake .class file here, will be overwritten on superclass loading routine
                javaClass.SuperClass = new JavaClass(superClassName, javaClass.ClassFile);
            }

            return javaClass;
        }
    }
}
