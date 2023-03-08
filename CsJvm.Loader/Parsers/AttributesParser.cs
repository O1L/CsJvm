using CsJvm.Loader.Extensions;
using CsJvm.Models.ClassFileFormat.Attributes;
using CsJvm.Models.ClassFileFormat.ConstantPool;

namespace CsJvm.Loader.Parsers
{
    /// <summary>
    /// Attributes Parser
    /// </summary>
    public static class AttributesParser
    {
        /// <summary>
        /// Gets parsed <see cref="AttributeInfo"/>
        /// </summary>
        /// <param name="reader">Binary reader</param>
        /// <param name="cpInfo">Constant pool info</param>
        /// <returns>Parsed <see cref="AttributeInfo"/></returns>
        public static AttributeInfo GetAttributeInfo(this BinaryReader reader, CpInfo[] cpInfo)
        {
            var attributeNameIndex = reader.ReadU2();
            var attributeLength = reader.ReadU4();

            if (cpInfo[attributeNameIndex] is not CONSTANT_Utf8Info info)
                throw new InvalidDataException();

            switch (info.Utf8String)
            {
                case PredefinedClassFileAttributes.ConstantValue:
                    {
                        var constantValueIndex = reader.ReadU2();

                        return new ConstantValueAttribute
                        {
                            AttributeNameIndex = attributeNameIndex,
                            AttributeLength = attributeLength,
                            ConstantValueIndex = constantValueIndex,
                            Name = info.Utf8String
                        };
                    }

                case PredefinedClassFileAttributes.Code:
                    {
                        var maxStack = reader.ReadU2();
                        var maxLocals = reader.ReadU2();
                        var codeLength = reader.ReadU4();

                        if (codeLength < 1)
                            throw new InvalidDataException("Bad code length!");

                        var code = new byte[codeLength];
                        for (uint i = 0; i < codeLength; i++)
                            code[i] = reader.ReadU1();

                        var exceptionTableLength = reader.ReadU2();
                        var exceptionTable = new ExceptionTable[exceptionTableLength];

                        for (var ex = 0; ex < exceptionTableLength; ex++)
                        {
                            exceptionTable[ex] = new ExceptionTable
                            {
                                StartPc = reader.ReadU2(),
                                EndPc = reader.ReadU2(),
                                HandlerPc = reader.ReadU2(),
                                CatchType = reader.ReadU2()
                            };
                        }

                        var attributesCount = reader.ReadU2();
                        var attributes = new AttributeInfo[attributesCount];

                        // recursion!
                        for (var attr = 0; attr < attributesCount; attr++)
                            attributes[attr] = reader.GetAttributeInfo(cpInfo);

                        return new CodeAttribute
                        {
                            AttributeNameIndex = attributeNameIndex,
                            AttributeLength = attributeLength,
                            MaxStack = maxStack,
                            MaxLocals = maxLocals,
                            CodeLength = codeLength,
                            Code = code,
                            ExceptionTableLength = exceptionTableLength,
                            ExceptionTable = exceptionTable,
                            AttributesCount = attributesCount,
                            Attributes = attributes,
                            Name = info.Utf8String
                        };
                    }

                case PredefinedClassFileAttributes.StackMapTable:
                    {
                        var numberOfEntries = reader.ReadU2();
                        var entries = new StackMapFrame[numberOfEntries];

                        for (var i = 0; i < numberOfEntries; i++)
                            entries[i] = GetStackMapFrame(reader);

                        return new StackMapTableAttribute
                        {
                            AttributeNameIndex = attributeNameIndex,
                            AttributeLength = attributeLength,
                            NumberOfEntries = numberOfEntries,
                            Entries = entries,
                            Name = info.Utf8String
                        };
                    }

                case PredefinedClassFileAttributes.Exceptions:
                    {
                        var numberOfExceptions = reader.ReadU2();
                        var exceptionIndexTable = new ushort[numberOfExceptions];

                        for (var i = 0; i < numberOfExceptions; i++)
                        {
                            var index = reader.ReadU2();

                            if (cpInfo[index] is not CONSTANT_ClassInfo)
                                throw new InvalidDataException("The entry must be a CONSTANT_ClassInfo structure");

                            exceptionIndexTable[i] = index;
                        }

                        return new ExceptionsAttribute
                        {
                            AttributeNameIndex = attributeNameIndex,
                            AttributeLength = attributeLength,
                            NumberOfExceptions = numberOfExceptions,
                            ExceptionIndexTable = exceptionIndexTable,
                            Name = info.Utf8String
                        };
                    }

                case PredefinedClassFileAttributes.InnerClasses:
                    {
                        var numberOfClasses = reader.ReadU2();
                        var classes = new Classes[numberOfClasses];

                        for (var i = 0; i < numberOfClasses; i++)
                        {
                            var innerClassInfoindex = reader.ReadU2();
                            if (cpInfo[innerClassInfoindex] is not CONSTANT_ClassInfo)
                                throw new InvalidDataException("The entry must be a CONSTANT_ClassInfo structure");

                            var outerClassInfoindex = reader.ReadU2();
                            if (outerClassInfoindex != 0 && cpInfo[outerClassInfoindex] is not CONSTANT_ClassInfo)
                                throw new InvalidDataException("The entry must be a CONSTANT_ClassInfo structure");

                            var innerNameIndex = reader.ReadU2();
                            if (innerNameIndex != 0 && cpInfo[innerNameIndex] is not CONSTANT_Utf8Info)
                                throw new InvalidDataException("The entry must be a CONSTANT_Utf8Info structure");

                            var innerClassAccessFlags = reader.ReadU2();

                            classes[i] = new Classes
                            {
                                InnerClassInfoIndex = innerClassInfoindex,
                                OuterClassInfoIndex = outerClassInfoindex,
                                InnerNameIndex = innerNameIndex,
                                InnerClassAccessFlags = innerClassAccessFlags
                            };
                        }

                        return new InnerClassesAttribute
                        {
                            AttributeNameIndex = attributeNameIndex,
                            AttributeLength = attributeLength,
                            NumberOfClasses = numberOfClasses,
                            Classes = classes,
                            Name = info.Utf8String
                        };
                    }

                case PredefinedClassFileAttributes.EnclosingMethod:
                    {
                        var classIndex = reader.ReadU2();
                        if (cpInfo[classIndex] is not CONSTANT_ClassInfo)
                            throw new InvalidDataException();

                        var methodIndex = reader.ReadU2();
                        if (methodIndex != 0 && cpInfo[methodIndex] is not CONSTANT_NameAndTypeInfo)
                            throw new InvalidDataException("The entry must be a CONSTANT_NameAndTypeInfo structure");

                        return new EnclosingMethodAttribute
                        {
                            AttributeNameIndex = attributeNameIndex,
                            AttributeLength = attributeLength,
                            ClassIndex = classIndex,
                            MethodIndex = methodIndex,
                            Name = info.Utf8String
                        };
                    }

                case PredefinedClassFileAttributes.Synthetic:
                    {
                        return new SyntheticAttribute
                        {
                            AttributeNameIndex = attributeNameIndex,
                            AttributeLength = attributeLength,
                            Name = info.Utf8String
                        };
                    }

                case PredefinedClassFileAttributes.Signature:
                    {
                        if (attributeLength != 2)
                            throw new InvalidDataException();

                        var signatureIndex = reader.ReadU2();
                        if (cpInfo[signatureIndex] is not CONSTANT_Utf8Info)
                            throw new InvalidDataException("The entry must be a CONSTANT_Utf8Info structure");

                        return new SignatureAttribute
                        {
                            AttributeNameIndex = attributeNameIndex,
                            AttributeLength = attributeLength,
                            SignatureIndex = signatureIndex,
                            Name = info.Utf8String
                        };
                    }

                case PredefinedClassFileAttributes.SourceFile:
                    {
                        if (attributeLength != 2)
                            throw new InvalidDataException("Invalid attribute length!");

                        var sourcefileIndex = reader.ReadU2();
                        if (cpInfo[sourcefileIndex] is not CONSTANT_Utf8Info)
                            throw new InvalidDataException("The entry must be a CONSTANT_Utf8Info structure");

                        return new SourceFileAttribute
                        {
                            AttributeNameIndex = attributeNameIndex,
                            AttributeLength = attributeLength,
                            SourceFileIndex = sourcefileIndex,
                            Name = info.Utf8String
                        };
                    }

                case PredefinedClassFileAttributes.SourceDebugExtension:
                    {
                        var debugExtension = new byte[attributeLength];
                        for (uint i = 0; i < attributeLength; i++)
                            debugExtension[i] = reader.ReadU1();

                        return new SourceDebugExtensionAttribute
                        {
                            AttributeNameIndex = attributeNameIndex,
                            AttributeLength = attributeLength,
                            DebugExtension = debugExtension,
                            Name = info.Utf8String
                        };
                    }

                case PredefinedClassFileAttributes.LineNumberTable:
                    {
                        var lineNumberTableLength = reader.ReadU2();
                        var lineNumberTable = new LineNumberTable[lineNumberTableLength];

                        for (var i = 0; i < lineNumberTableLength; i++)
                        {
                            lineNumberTable[i] = new LineNumberTable
                            {
                                StartPc = reader.ReadU2(),
                                LineNumber = reader.ReadU2()
                            };
                        }

                        return new LineNumberTableAttribute
                        {
                            AttributeNameIndex = attributeNameIndex,
                            AttributeLength = attributeLength,
                            LineNumberTableLength = lineNumberTableLength,
                            LineNumberTable = lineNumberTable,
                            Name = info.Utf8String
                        };
                    }

                case PredefinedClassFileAttributes.LocalVariableTable:
                    {
                        var localVariableTableLength = reader.ReadU2();
                        var localVariableTable = new LocalVariableTable[localVariableTableLength];

                        for (var i = 0; i < localVariableTableLength; i++)
                        {
                            var startPc = reader.ReadU2();
                            var length = reader.ReadU2();

                            var nameIndex = reader.ReadU2();
                            if (cpInfo[nameIndex] is not CONSTANT_Utf8Info)
                                throw new InvalidDataException("The entry must be a CONSTANT_Utf8Info structure");

                            var descriptorIndex = reader.ReadU2();
                            if (cpInfo[descriptorIndex] is not CONSTANT_Utf8Info)
                                throw new InvalidDataException("The entry must be a CONSTANT_Utf8Info structure");

                            var index = reader.ReadU2();

                            localVariableTable[i] = new LocalVariableTable
                            {
                                StartPc = startPc,
                                Length = length,
                                NameIndex = nameIndex,
                                DescriptorIndex = descriptorIndex,
                                Index = index
                            };
                        }

                        return new LocalVariableTableAttribute
                        {
                            AttributeNameIndex = attributeNameIndex,
                            AttributeLength = attributeLength,
                            LocalVariableTableLength = localVariableTableLength,
                            LocalVariableTable = localVariableTable,
                            Name = info.Utf8String
                        };
                    }

                case PredefinedClassFileAttributes.LocalVariableTypeTable:
                    {
                        var localVariableTypeTableLength = reader.ReadU2();
                        var localVariabletypeTable = new LocalVariableTypeTable[localVariableTypeTableLength];

                        for (var i = 0; i < localVariableTypeTableLength; i++)
                        {
                            var startPc = reader.ReadU2();
                            var length = reader.ReadU2();

                            var nameIndex = reader.ReadU2();
                            if (cpInfo[nameIndex] is not CONSTANT_Utf8Info)
                            {
                                throw new InvalidDataException("The entry must be a CONSTANT_Utf8Info structure");
                            }

                            var signatureIndex = reader.ReadU2();
                            if (cpInfo[signatureIndex] is not CONSTANT_Utf8Info)
                                throw new InvalidDataException("The entry must be a CONSTANT_Utf8Info structure");

                            var index = reader.ReadU2();

                            localVariabletypeTable[i] = new LocalVariableTypeTable
                            {
                                StartPc = startPc,
                                Length = length,
                                NameIndex = nameIndex,
                                SignatureIndex = signatureIndex,
                                Index = index
                            };
                        }

                        return new LocalVariableTypeTableAttribute
                        {
                            AttributeNameIndex = attributeNameIndex,
                            AttributeLength = attributeLength,
                            LocalVariableTypeTableLength = localVariableTypeTableLength,
                            LocalVariableTypeTable = localVariabletypeTable,
                            Name = info.Utf8String
                        };
                    }

                case PredefinedClassFileAttributes.Deprecated:
                    {
                        if (attributeLength != 0)
                            throw new InvalidDataException("Invalid attribute length!");

                        return new DeprecatedAttribute
                        {
                            AttributeNameIndex = attributeNameIndex,
                            AttributeLength = attributeLength,
                            Name = info.Utf8String
                        };
                    }

                case PredefinedClassFileAttributes.RuntimeVisibleAnnotations:
                    {
                        var numAnnotations = reader.ReadU2();
                        var annotations = new Annotation[numAnnotations];

                        for (var i = 0; i < numAnnotations; i++)
                            annotations[i] = GetAnnotation(reader, cpInfo);

                        return new RuntimeVisibleAnnotationsAttribute
                        {
                            AttributeNameIndex = attributeNameIndex,
                            AttributeLength = attributeLength,
                            NumAnnotations = numAnnotations,
                            Annotations = annotations,
                            Name = info.Utf8String
                        };
                    }

                case PredefinedClassFileAttributes.RuntimeInvisibleAnnotations:
                    {
                        var numAnnotations = reader.ReadU2();
                        var annotations = new Annotation[numAnnotations];

                        for (var i = 0; i < numAnnotations; i++)
                            annotations[i] = GetAnnotation(reader, cpInfo);

                        return new RuntimeInvisibleAnnotationsAttribute
                        {
                            AttributeNameIndex = attributeNameIndex,
                            AttributeLength = attributeLength,
                            NumAnnotations = numAnnotations,
                            Annotations = annotations,
                            Name = info.Utf8String
                        };
                    }

                case PredefinedClassFileAttributes.RuntimeVisibleParameterAnnotations:
                    {
                        var numParameters = reader.ReadU1();
                        var parameterAnnotations = new ParameterAnnotation[numParameters];

                        for (var i = 0; i < numParameters; i++)
                        {
                            var numAnnotations = reader.ReadU2();
                            var annotations = new Annotation[numAnnotations];

                            for (var j = 0; j < numAnnotations; j++)
                                annotations[j] = GetAnnotation(reader, cpInfo);

                            parameterAnnotations[i] = new ParameterAnnotation
                            {
                                NumAnnotations = numAnnotations,
                                Annotations = annotations
                            };
                        }

                        return new RuntimeVisibleParameterAnnotationsAttribute
                        {
                            AttributeNameIndex = attributeNameIndex,
                            AttributeLength = attributeLength,
                            NumParameters = numParameters,
                            ParameterAnnotations = parameterAnnotations,
                            Name = info.Utf8String
                        };
                    }

                case PredefinedClassFileAttributes.RuntimeInvisibleParameterAnnotations:
                    {
                        var numParameters = reader.ReadU1();
                        var parameterAnnotations = new ParameterAnnotation[numParameters];

                        for (var i = 0; i < numParameters; i++)
                        {
                            var numAnnotations = reader.ReadU2();
                            var annotations = new Annotation[numAnnotations];

                            for (var j = 0; j < numAnnotations; j++)
                                annotations[j] = GetAnnotation(reader, cpInfo);

                            parameterAnnotations[i] = new ParameterAnnotation
                            {
                                NumAnnotations = numAnnotations,
                                Annotations = annotations
                            };
                        }

                        return new RuntimeInvisibleParameterAnnotationsAttribute
                        {
                            AttributeNameIndex = attributeNameIndex,
                            AttributeLength = attributeLength,
                            NumParameters = numParameters,
                            ParameterAnnotations = parameterAnnotations,
                            Name = info.Utf8String
                        };
                    }

                case PredefinedClassFileAttributes.AnnotationDefault:
                    {
                        return new AnnotationDefaultAttribute
                        {
                            AttributeNameIndex = attributeNameIndex,
                            AttributeLength = attributeLength,
                            DefaultValue = GetElementValue(reader, cpInfo),
                            Name = info.Utf8String
                        };
                    }

                case PredefinedClassFileAttributes.BootstrapMethods:
                    {
                        var numBootstrapMethods = reader.ReadU2();
                        var bootstrapMethods = new BootstrapMethod[numBootstrapMethods];

                        for (var i = 0; i < numBootstrapMethods; i++)
                        {
                            var bootstrapMethodRef = reader.ReadU2();
                            var numBootstrapArguments = reader.ReadU2();
                            var bootstrapArguments = new ushort[numBootstrapArguments];

                            for (var j = 0; j < numBootstrapArguments; j++)
                                bootstrapArguments[j] = reader.ReadU2();

                            bootstrapMethods[i] = new BootstrapMethod
                            {
                                BootstrapMethodRef = bootstrapMethodRef,
                                NumBootstrapArguments = numBootstrapArguments,
                                BootstrapArguments = bootstrapArguments
                            };
                        }

                        return new BootstrapMethodsAttribute
                        {
                            AttributeNameIndex = attributeNameIndex,
                            AttributeLength = attributeLength,
                            NumBootstrapMethods = numBootstrapMethods,
                            BootstrapMethods = bootstrapMethods,
                            Name = info.Utf8String
                        };
                    }

                case PredefinedClassFileAttributes.RuntimeVisibleTypeAnnotations:
                case PredefinedClassFileAttributes.RuntimeInvisibleTypeAnnotations:
                case PredefinedClassFileAttributes.MethodParameters:
                case PredefinedClassFileAttributes.Module:
                case PredefinedClassFileAttributes.ModulePackages:
                case PredefinedClassFileAttributes.ModuleMainClass:
                    {

                        throw new ArgumentException(info.Utf8String);
                    }

                case PredefinedClassFileAttributes.NestHost:
                    {
                        var hostClassIndex = reader.ReadU2();

                        if (cpInfo[hostClassIndex] is not CONSTANT_ClassInfo)
                            throw new InvalidDataException("The entry must be a CONSTANT_ClassInfo structure");

                        return new NestHostAttribute
                        {
                            AttributeNameIndex = attributeNameIndex,
                            AttributeLength = attributeLength,
                            HostClassIndex = hostClassIndex,
                            Name = info.Utf8String
                        };
                    }

                case PredefinedClassFileAttributes.NestMembers:
                    {
                        var numberOfClasses = reader.ReadU2();
                        var classes = new ushort[numberOfClasses];

                        for (var j = 0; j < numberOfClasses; j++)
                        {
                            classes[j] = reader.ReadU2();

                            if (cpInfo[classes[j]] is not CONSTANT_ClassInfo)
                                throw new InvalidDataException("The entry must be a CONSTANT_ClassInfo structure");
                        }

                        return new NestMembersAttribute
                        {
                            AttributeNameIndex = attributeNameIndex,
                            AttributeLength = attributeLength,
                            NumberOfClasses = numberOfClasses,
                            Classes = classes,
                            Name = info.Utf8String
                        };
                    }

                default:
                    throw new ArgumentException(info.Utf8String);
            }
        }

        /// <summary>
        /// Gets parsed <see cref="StackMapFrame"/>
        /// </summary>
        /// <param name="reader">Binary reader</param>
        /// <returns>Parsed <see cref="StackMapFrame"/> instance</returns>
        private static StackMapFrame GetStackMapFrame(BinaryReader reader)
        {
            var frameType = reader.ReadU1();
            var type = frameType switch
            {
                var n when n is >= 0 and <= 63 => SameTypes.SAME,
                var n when n is >= 64 and <= 127 => SameTypes.SAME_LOCALS_1_STACK_ITEM,
                247 => SameTypes.SAME_LOCALS_1_STACK_ITEM_EXTENDED,
                var n when n is >= 248 and <= 250 => SameTypes.CHOP,
                251 => SameTypes.SAME_FRAME_EXTENDED,
                var n when n is >= 252 and <= 254 => SameTypes.APPEND,
                255 => SameTypes.FULL_FRAME,

                _ => throw new ArgumentException($"Value {frameType} is not in range!")
            };

            switch (type)
            {
                case SameTypes.SAME:
                    {
                        return new FSameFrame
                        {
                            FrameType = frameType
                        };
                    }

                case SameTypes.SAME_LOCALS_1_STACK_ITEM:
                    {
                        var stack = new[] { GetVerificationTypeInfo(reader) };
                        return new SameLocals1StackItemFrame
                        {
                            FrameType = frameType,
                            Stack = stack
                        };
                    }

                case SameTypes.SAME_LOCALS_1_STACK_ITEM_EXTENDED:
                    {
                        var offsetDelta = reader.ReadU2();
                        var stack = new[] { GetVerificationTypeInfo(reader) };

                        return new SameLocals1StackItemFrameExtended
                        {
                            FrameType = frameType,
                            OffsetDelta = offsetDelta,
                            Stack = stack
                        };
                    }

                case SameTypes.CHOP:
                    {
                        var offsetDelta = reader.ReadU2();
                        return new ChopFrame
                        {
                            FrameType = frameType,
                            OffsetDelta = offsetDelta
                        };
                    }

                case SameTypes.SAME_FRAME_EXTENDED:
                    {
                        var offsetDelta = reader.ReadU2();
                        return new SameFrameExtended
                        {
                            FrameType = frameType,
                            OffsetDelta = offsetDelta
                        };
                    }

                case SameTypes.APPEND:
                    {
                        var offsetDelta = reader.ReadU2();
                        var stackSize = frameType - 251;
                        var stack = new VerificationTypeInfo[stackSize];

                        for (var i = 0; i < stackSize; i++)
                            stack[i] = GetVerificationTypeInfo(reader);

                        return new AppendFrame
                        {
                            FrameType = frameType,
                            OffsetDelta = offsetDelta,
                            Locals = stack
                        };
                    }

                case SameTypes.FULL_FRAME:
                    {
                        var offsetDelta = reader.ReadU2();

                        var numberOfLocals = reader.ReadU2();
                        var locals = new VerificationTypeInfo[numberOfLocals];
                        for (var i = 0; i < numberOfLocals; i++)
                            locals[i] = GetVerificationTypeInfo(reader);

                        var numberOfStackItems = reader.ReadU2();
                        var stack = new VerificationTypeInfo[numberOfStackItems];
                        for (var i = 0; i < numberOfStackItems; i++)
                            stack[i] = GetVerificationTypeInfo(reader);

                        return new FullFrame
                        {
                            FrameType = frameType,
                            OffsetDelta = offsetDelta,
                            NumberOfLocals = numberOfLocals,
                            Locals = locals,
                            NumberOfStackItems = numberOfStackItems,
                            Stack = stack
                        };
                    }

                default: throw new ArgumentException(nameof(type));
            }
        }

        /// <summary>
        /// Gets parsed <see cref="VerificationTypeInfo"/>
        /// </summary>
        /// <param name="reader">Binary reader</param>
        /// <returns>Parsed <see cref="VerificationTypeInfo"/> instance</returns>
        private static VerificationTypeInfo GetVerificationTypeInfo(BinaryReader reader)
        {
            var tag = reader.ReadU1();

            return (VariableTags)tag switch
            {
                VariableTags.ITEM_Top => new TopVariableInfo { Tag = tag },
                VariableTags.ITEM_Integer => new IntegerVariableInfo { Tag = tag },
                VariableTags.ITEM_Float => new FloatVariableInfo { Tag = tag },
                VariableTags.ITEM_Double => new DoubleVariableInfo { Tag = tag },
                VariableTags.ITEM_Long => new LongVariableInfo { Tag = tag },
                VariableTags.ITEM_Null => new NullVariableInfo { Tag = tag },
                VariableTags.ITEM_UninitializedThis => new UninitializedThisVariableInfo { Tag = tag },
                VariableTags.ITEM_Object => new ObjectVariableInfo { Tag = tag, CPoolIndex = reader.ReadU2() },
                VariableTags.ITEM_Uninitialized => new UninitializedVariableInfo { Tag = tag, Offset = reader.ReadU2() },
                _ => throw new ArgumentException(nameof(tag))
            };
        }

        /// <summary>
        /// Gets parsed <see cref="Annotation"/>
        /// </summary>
        /// <param name="reader">Binary reader</param>
        /// <param name="cpInfo">Constant pool info</param>
        /// <returns>Parsed <see cref="Annotation"/> instance</returns>
        private static Annotation GetAnnotation(BinaryReader reader, CpInfo[] cpInfo)
        {
            var typeIndex = reader.ReadU2();
            if (cpInfo[typeIndex] is not CONSTANT_Utf8Info)
                throw new InvalidDataException("The entry must be a CONSTANT_Utf8Info structure");

            var numElementValuePairs = reader.ReadU2();
            var elementValuepairs = new ElementValuePairs[numElementValuePairs];
            for (var i = 0; i < numElementValuePairs; i++)
            {
                var elementNameIndex = reader.ReadU2();
                if (cpInfo[elementNameIndex] is not CONSTANT_Utf8Info)
                    throw new InvalidDataException("The entry must be a CONSTANT_Utf8Info structure");

                var value = GetElementValue(reader, cpInfo);

                elementValuepairs[i] = new ElementValuePairs
                {
                    ElementNameIndex = elementNameIndex,
                    Value = value
                };
            }

            return new Annotation
            {
                TypeIndex = typeIndex,
                NumElementValuePairs = numElementValuePairs,
                ElementValuePairs = elementValuepairs
            };
        }

        /// <summary>
        /// Gets parsed <see cref="ElementValue"/>
        /// </summary>
        /// <param name="reader">Binary reader</param>
        /// <param name="cpInfo">Constant pool info</param>
        /// <returns>Parsed <see cref="ElementValue"/> instance</returns>
        private static ElementValue GetElementValue(BinaryReader reader, CpInfo[] cpInfo)
        {
            var tag = reader.ReadU1();
            Value value;
            switch ((char)tag)
            {
                case 'B':
                case 'C':
                case 'D':
                case 'F':
                case 'I':
                case 'J':
                case 'S':
                case 'Z':
                case 's':
                    {
                        value = new ConstValueIndex
                        {
                            ValueIndex = reader.ReadU2()
                        };
                    }
                    break;

                case 'e':
                    {
                        var typeNameIndex = reader.ReadU2();
                        if (cpInfo[typeNameIndex] is not CONSTANT_Utf8Info)
                            throw new InvalidDataException("The entry must be a CONSTANT_Utf8Info structure");

                        var constNameIndex = reader.ReadU2();
                        if (cpInfo[constNameIndex] is not CONSTANT_Utf8Info)
                            throw new InvalidDataException("The entry must be a CONSTANT_Utf8Info structure");

                        value = new EnumConstValue
                        {
                            TypeNameIndex = typeNameIndex,
                            ConstNameIndex = constNameIndex
                        };
                    }
                    break;

                case 'c':
                    {
                        var classInfoIndex = reader.ReadU2();
                        if (cpInfo[classInfoIndex] is not CONSTANT_Utf8Info)
                            throw new InvalidDataException("The entry must be a CONSTANT_Utf8Info structure");

                        value = new ClassInfoIndex
                        {
                            InfoIndex = classInfoIndex
                        };
                    }
                    break;

                case '@':
                    {
                        value = new AnnotationValue
                        {
                            Value = GetAnnotation(reader, cpInfo)
                        };
                    }
                    break;

                case '[':
                    {
                        var numValues = reader.ReadU2();
                        var values = new ElementValue[numValues];

                        // recursion!
                        for (var i = 0; i < numValues; i++)
                            values[i] = GetElementValue(reader, cpInfo);

                        value = new ArrayValue
                        {
                            NumValues = numValues,
                            Values = values
                        };
                    }
                    break;

                default: throw new InvalidDataException();
            }

            return new ElementValue
            {
                Tag = tag,
                Value = value
            };
        }
    }
}
