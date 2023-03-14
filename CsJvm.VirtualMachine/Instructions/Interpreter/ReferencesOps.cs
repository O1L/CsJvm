using CsJvm.Abstractions.VirtualMachine;
using CsJvm.Models;
using CsJvm.Models.ClassFileFormat;
using CsJvm.Models.ClassFileFormat.ConstantPool;
using CsJvm.Models.ClassFileFormat.Methods;
using CsJvm.Models.Heap;
using CsJvm.VirtualMachine.Attributes;
using CsJvm.VirtualMachine.Extensions;

namespace CsJvm.VirtualMachine.Instructions.Interpreter
{
    public partial class JvmInterpreter
    {
        [Opcode(0xb2, "getstatic")]
        public async Task GetStatic(IJavaThread thread)
        {
            var indexbyte1 = thread.CurrentMethod.Code[thread.ProgramCounter++];
            var indexbyte2 = thread.CurrentMethod.Code[thread.ProgramCounter++];
            var index = (indexbyte1 << 8) | indexbyte2;

            if (thread.CurrentMethod.CpInfo[index] is not CONSTANT_FieldrefInfo info)
                throw new InvalidOperationException("Must be a CONSTANT_FieldrefInfo structure");

            var (className, fieldName, descriptor) = GetFieldrefInfo(thread, info);
            var field = $"{fieldName}:{descriptor}";

            if (!_heap.TryGetStaticClass(className, out var javaClass) || javaClass == null)
            {
                // resolve class
                javaClass = await ResolveClass(className) ?? throw new InvalidOperationException($"Cannot resolve class {className}");

                // looking for field in the resolved class and super-classes
                while (javaClass != null && !javaClass.Fields.ContainsKey(field))
                {
                    javaClass = javaClass.SuperClass;
                }

                if (javaClass == null)
                    throw new InvalidOperationException($"Cannot find class {className}");

                _heap.TryCreateStaticClassRef(javaClass);
            }

            // run static ctor
            await RunStaticCtorAsync(javaClass, thread);

            if (javaClass == null || !javaClass.Fields.TryGetValue(field, out var value))
                throw new InvalidOperationException($"Cannot find field {field}");

            // return value to stack
            thread.CurrentMethod.OperandStack.Push(value);
        }

        [Opcode(0xb3, "putstatic")]
        public async Task PutStatic(IJavaThread thread)
        {
            var indexbyte1 = thread.CurrentMethod.Code[thread.ProgramCounter++];
            var indexbyte2 = thread.CurrentMethod.Code[thread.ProgramCounter++];
            var index = (indexbyte1 << 8) | indexbyte2;

            if (thread.CurrentMethod.CpInfo[index] is not CONSTANT_FieldrefInfo info)
                throw new InvalidOperationException("Must be a CONSTANT_FieldrefInfo structure");

            // get field info
            var (className, fieldName, descriptor) = GetFieldrefInfo(thread, info);
            var field = $"{fieldName}:{descriptor}";

            if (!_heap.TryGetStaticClass(className, out var javaClass) || javaClass == null)
            {
                // resolve class
                javaClass = await ResolveClass(className) ?? throw new InvalidOperationException($"Cannot resolve class {className}");

                // looking for field in the resolved class and super-classes
                while (javaClass != null && !javaClass.Fields.ContainsKey(field))
                {
                    javaClass = javaClass.SuperClass;
                }

                if (javaClass == null)
                    throw new InvalidOperationException($"Cannot find class {className}");

                _heap.TryCreateStaticClassRef(javaClass);
            }

            if (javaClass == null || !javaClass.Fields.ContainsKey(field))
                throw new InvalidOperationException($"Cannot find field {field}");

            // run static ctor
            await RunStaticCtorAsync(javaClass, thread);

            // get value from stack
            var value = thread.CurrentMethod.OperandStack.Pop();

            // set the field value
            javaClass.Fields[field] = value;
        }

        [Opcode(0xb4, "getfield")]
        public Task GetField(IJavaThread thread)
        {
            var indexbyte1 = thread.CurrentMethod.Code[thread.ProgramCounter++];
            var indexbyte2 = thread.CurrentMethod.Code[thread.ProgramCounter++];
            var index = (indexbyte1 << 8) | indexbyte2;

            if (thread.CurrentMethod.CpInfo[index] is not CONSTANT_FieldrefInfo info)
                throw new InvalidOperationException("Must be a CONSTANT_FieldrefInfo structure");

            // get field info
            var (className, fieldName, descriptor) = GetFieldrefInfo(thread, info);
            var field = $"{fieldName}:{descriptor}";

            var objectref = thread.CurrentMethod.OperandStack.Pop();

            if (objectref == null || objectref is not HeapClassRef heapClassRef)
                throw new NullReferenceException(nameof(objectref));

            if (!_heap.TryGetClass(heapClassRef, out var javaClass) || javaClass == null)
                throw new InvalidOperationException("Cannot find object in the heap!");

            while (!javaClass.Fields.ContainsKey(field) && javaClass.SuperClass != null)
                javaClass = javaClass.SuperClass;

            if (!javaClass.Fields.TryGetValue(field, out var value))
                throw new InvalidOperationException($"Cannot find field {field}");

            thread.CurrentMethod.OperandStack.Push(value);

            return Task.CompletedTask;
        }

        [Opcode(0xb5, "putfield")]
        public Task PutField(IJavaThread thread)
        {
            var indexbyte1 = thread.CurrentMethod.Code[thread.ProgramCounter++];
            var indexbyte2 = thread.CurrentMethod.Code[thread.ProgramCounter++];
            var index = (indexbyte1 << 8) | indexbyte2;

            if (thread.CurrentMethod.CpInfo[index] is not CONSTANT_FieldrefInfo info)
                throw new InvalidOperationException("Must be a CONSTANT_FieldrefInfo structure");

            // get field info
            var (className, fieldName, descriptor) = GetFieldrefInfo(thread, info);
            var field = $"{fieldName}:{descriptor}";

            var value = thread.CurrentMethod.OperandStack.Pop();
            var objectref = thread.CurrentMethod.OperandStack.Pop();

            if (objectref == null || objectref is not HeapClassRef heapClassRef)
                throw new NullReferenceException(nameof(objectref));

            JavaClass? javaClass = null;
            var reverse = false;

            // WTF (fixme!)
            if (heapClassRef.Name == className || className == "java/lang/AbstractStringBuilder")
            {
                if (!_heap.TryGetClass(heapClassRef, out javaClass) || javaClass == null)
                    throw new InvalidOperationException("Cannot find object in the heap!");
            }
            else
            {
                if (!_heap.TryGetClass((HeapClassRef)value!, out javaClass) || javaClass == null)
                    throw new InvalidOperationException("Cannot find object in the heap!");

                reverse = true;
            }

            // check superclasses
            if (!javaClass.Fields.ContainsKey(field))
            {
                var superClass = javaClass.SuperClass;
                while (superClass != null && !superClass.Fields.ContainsKey(field))
                    superClass = superClass.SuperClass;

                if (superClass == null)
                    throw new InvalidOperationException($"Cannot find field {field}");

                superClass.Fields[field] = reverse ? objectref : value;
                return Task.CompletedTask;
            }

            javaClass.Fields[field] = reverse ? objectref : value;
            return Task.CompletedTask;
        }

        [Opcode(0xb6, "invokevirtual")]
        public Task InvokeVirtual(IJavaThread thread)
        {
            var indexbyte1 = thread.CurrentMethod.Code[thread.ProgramCounter++];
            var indexbyte2 = thread.CurrentMethod.Code[thread.ProgramCounter++];
            var index = (indexbyte1 << 8) | indexbyte2;

            var info = thread.CurrentMethod.CpInfo[index];
            if (info is not CONSTANT_MethodrefInfo methodrefinfo)
                throw new NotImplementedException($"{info.GetType()}");

            (var className, var methodName, var descriptor) = GetMethodrefInfo(thread, methodrefinfo);

            // construct full method name
            var key = $"{methodName}:{descriptor}";

            /*if (!TryResolveClass(className, out var javaClass) || javaClass == null)
                throw new InvalidOperationException($"Cannot resolve class {className}");

            // looking for method in the resolved class and super-classes
            while (javaClass != null && !javaClass.Methods.ContainsKey(key))
            {
                javaClass = javaClass.SuperClass;
            }

            if (javaClass == null || !javaClass.Methods.TryGetValue(key, out var method) || method == null)
                throw new InvalidOperationException($"Cannot find method {key}");*/

            // calculate arguments count
            var argsCount = descriptor.GetArgsCount();

            // pop args from stack
            var args = thread.CurrentMethod.OperandStack.PopCountOf(argsCount);

            // pop objectref
            var objectref = thread.CurrentMethod.OperandStack.Pop();

            // check the object ref
            if (objectref == null || objectref is not HeapClassRef heapClassRef)
                throw new NullReferenceException(nameof(objectref));

            if (!_heap.TryGetClass(heapClassRef, out var javaClass) || javaClass == null)
                throw new InvalidOperationException($"Cannot find class {className}");

            JavaClass? superClass = null;
            if (!javaClass.Methods.TryGetValue(key, out var method) || method == null)
            {
                superClass = javaClass.SuperClass;

                while (superClass != null && !superClass.Methods.TryGetValue(key, out method))
                    superClass = superClass.SuperClass;

                if (superClass == null || method == null)
                    throw new InvalidOperationException($"Cannot find method {key}");
            }

            if (method.AccessFlags.HasFlag(MethodAccessAndPropertyFlags.ACC_NATIVE))
            {
                args = new object[] { objectref }.Concat(args).ToArray();

                if (!_natives.TryGetMethod(className, methodName, descriptor, args!, out var nativeMethod) || nativeMethod == null)
                    throw new NotImplementedException($"Native {className}.{key}");

                thread.PushNative(nativeMethod);
            }

            //if (heapClassRef.Name != className)
            //    objectref = _heap.CreateClassRef(javaClass);

            // create new method frame
            var frame = CreateFrame(javaClass, method, args, objectref);

            // run it
            return thread.PushFrameAsync(frame);
        }

        [Opcode(0xb7, "invokespecial")]
        public async Task InvokeSpecial(IJavaThread thread)
        {
            var indexbyte1 = thread.CurrentMethod.Code[thread.ProgramCounter++];
            var indexbyte2 = thread.CurrentMethod.Code[thread.ProgramCounter++];
            var index = (indexbyte1 << 8) | indexbyte2;

            var info = thread.CurrentMethod.CpInfo[index];
            if (info is not CONSTANT_MethodrefInfo methodrefinfo)
                throw new NotImplementedException($"{info.GetType()}");

            (var className, var methodName, var descriptor) = GetMethodrefInfo(thread, methodrefinfo);

            // construct full method name
            var key = $"{methodName}:{descriptor}";

            // calculate arguments count
            var argsCount = descriptor.GetArgsCount();

            // pop args from stack
            var args = thread.CurrentMethod.OperandStack.PopCountOf(argsCount)!;

            // pop objectref
            var objectref = (HeapClassRef)thread.CurrentMethod.OperandStack.Pop()!;

            // check the object ref
            if (objectref == default)
                throw new NullReferenceException(nameof(objectref));

            /*if (!_heap.TryGetClass(objectref, out var javaClass) || javaClass == null)
                 throw new NullReferenceException($"{nameof(javaClass)} is not found");

             JavaClass? superClass = null;
             if (!javaClass.Methods.TryGetValue(key, out var method) || method == null)
             {
                 superClass = javaClass.SuperClass;

                 while (superClass != null && !superClass.Methods.TryGetValue(key, out method))
                     superClass = superClass.SuperClass;

                 if (superClass == null || method == null)
                     throw new InvalidOperationException($"Cannot find method {key}");
             }*/

            var javaClass = await ResolveClass(className) ?? throw new InvalidOperationException($"Cannot resolve class {className}");

            // looking for method in the resolved class and super-classes
            while (javaClass != null && !javaClass.Methods.ContainsKey(key))
            {
                javaClass = javaClass.SuperClass;
            }

            if (javaClass == null || !javaClass.Methods.TryGetValue(key, out var method) || method == null)
                throw new InvalidOperationException($"Cannot find method {key}");

            if (method.AccessFlags.HasFlag(MethodAccessAndPropertyFlags.ACC_NATIVE))
            {
                args = new object[] { objectref }.Concat(args).ToArray();

                if (!_natives.TryGetMethod(className, methodName, descriptor, args!, out var nativeMethod) || nativeMethod == null)
                    throw new NotImplementedException($"Native {className}.{key}");

                thread.PushNative(nativeMethod);
                return;
            }

            // create new method frame
            var frame = CreateFrame(javaClass, method, args, objectref);

            /*if (key == "<init>:()V")
            {
                var thread0 = new JavaThread(thread.Decoder, frame);
                thread0.Run();
            }
            else
                thread.PushFrame(frame);*/

            // run it
            await thread.PushFrameAsync(frame);

            //var thread0 = new JavaThread(thread.Decoder, frame);
            //thread0.Run();

            // restore
            //thread.RestoreFrame();
            //thread.Run();
        }

        [Opcode(0xb8, "invokestatic")]
        public async Task InvokeStatic(IJavaThread thread)
        {
            var indexbyte1 = thread.CurrentMethod.Code[thread.ProgramCounter++];
            var indexbyte2 = thread.CurrentMethod.Code[thread.ProgramCounter++];
            var index = (indexbyte1 << 8) | indexbyte2;

            var info = thread.CurrentMethod.CpInfo[index];
            if (info is not CONSTANT_MethodrefInfo methodrefinfo)
                throw new NotImplementedException($"{info.GetType()}");

            (var className, var methodName, var descriptor) = GetMethodrefInfo(thread, methodrefinfo);

            // calculate arguments count
            var argsCount = descriptor.GetArgsCount();

            // get args from stack
            var args = thread.CurrentMethod.OperandStack.PopCountOf(argsCount);

            // get full method name
            var key = $"{methodName}:{descriptor}";

            if (!_heap.TryGetStaticClass(className, out var javaClass) || javaClass == null)
            {
                // resolve class
                javaClass = await ResolveClass(className) ?? throw new InvalidOperationException($"Cannot resolve class {className}");

                // looking for field in the resolved class and super-classes
                while (javaClass != null && !javaClass.Methods.ContainsKey(key))
                {
                    javaClass = javaClass.SuperClass;
                }

                if (javaClass == null)
                    throw new InvalidOperationException($"Cannot find class {className}");

                _heap.TryCreateStaticClassRef(javaClass);
            }

            if (!javaClass.Methods.TryGetValue(key, out var method) || method == null)
                throw new InvalidOperationException($"Cannot find method {key}");

            // check the method access flags
            if (!method.AccessFlags.HasFlag(MethodAccessAndPropertyFlags.ACC_STATIC) || method.AccessFlags.HasFlag(MethodAccessAndPropertyFlags.ACC_ABSTRACT))
                throw new InvalidOperationException($"Bad invokestatic call for method {key}");

            if (method.AccessFlags.HasFlag(MethodAccessAndPropertyFlags.ACC_SYNCHRONIZED))
                throw new NotImplementedException("ACC_SYNCHRONIZED");

            if (method.AccessFlags.HasFlag(MethodAccessAndPropertyFlags.ACC_NATIVE))
            {
                if (!_natives.TryGetMethod(className, methodName, descriptor, args!, out var nativeMethod) || nativeMethod == null)
                    throw new NotImplementedException($"Native {className}.{key}");

                thread.PushNative(nativeMethod);
                return;
            }


            var frame = CreateFrame(javaClass, method, args, null);
            await thread.PushFrameAsync(frame);

            // restore
            //thread.RestoreFrame();
            //thread.Run();
        }

        [Opcode(0xb9, "invokeinterface")]
        public Task InvokeInterface(IJavaThread thread) => throw new NotImplementedException();

        [Opcode(0xba, "invokedynamic")]
        public Task InvokeDynamic(IJavaThread thread) => throw new NotImplementedException();

        [Opcode(0xbb, "new")]
        public async Task New(IJavaThread thread)
        {
            var indexbyte1 = thread.CurrentMethod.Code[thread.ProgramCounter++];
            var indexbyte2 = thread.CurrentMethod.Code[thread.ProgramCounter++];
            var index = (indexbyte1 << 8) | indexbyte2;

            var info = thread.CurrentMethod.CpInfo[index];

            if (info is not CONSTANT_ClassInfo classInfo)
                throw new NotImplementedException($"{info.GetType()}");

            var className = ((CONSTANT_Utf8Info)thread.CurrentMethod.CpInfo[classInfo.NameIndex]).Utf8String;

            var javaClass = await ResolveClass(className) ?? throw new InvalidOperationException($"Cannot resolve class {className}");

            //RunStaticCtor(javaClass, thread);

            var objectref = _heap.CreateClassRef(javaClass);
            thread.CurrentMethod.OperandStack.Push(objectref);
        }

        [Opcode(0xbc, "newarray")]
        public Task NewArray(IJavaThread thread)
        {
            var atype = (ArrayTypes)thread.CurrentMethod.Code[thread.ProgramCounter++];
            var count = (int)thread.CurrentMethod.OperandStack.Pop()!;

            if (count < 0)
                throw new Exception("NegativeArraySizeException");

            Array result = atype switch
            {
                ArrayTypes.T_BOOLEAN => new byte[count],
                ArrayTypes.T_CHAR => new char[count],
                ArrayTypes.T_FLOAT => new float[count],
                ArrayTypes.T_DOUBLE => new double[count],
                ArrayTypes.T_BYTE => new byte[count],
                ArrayTypes.T_SHORT => new short[count],
                ArrayTypes.T_INT => new int[count],
                ArrayTypes.T_LONG => new long[count],

                _ => throw new InvalidOperationException($"Unknown array type: {atype}")
            };

            var arrayref = _heap.CreateArrayRef(result, atype, count);
            thread.CurrentMethod.OperandStack.Push(arrayref);

            return Task.CompletedTask;
        }

        [Opcode(0xbd, "anewarray")]
        public async Task AnewArray(IJavaThread thread)
        {
            var count = (int)thread.CurrentMethod.OperandStack.Pop()!;
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            var indexbyte1 = thread.CurrentMethod.Code[thread.ProgramCounter++];
            var indexbyte2 = thread.CurrentMethod.Code[thread.ProgramCounter++];
            var index = (indexbyte1 << 8) | indexbyte2;

            var info = thread.CurrentMethod.CpInfo[index];
            if (info is not CONSTANT_ClassInfo classInfo)
                throw new NotImplementedException($"{info.GetType()}");

            var className = ((CONSTANT_Utf8Info)thread.CurrentMethod.CpInfo[classInfo.NameIndex]).Utf8String;
            var javaClass = await ResolveClass(className) ?? throw new InvalidOperationException($"Cannot resolve class {className}");

            Array array = new HeapClassRef[count];
            var arrayref = _heap.CreateClassArrayRef(array, count);

            thread.CurrentMethod.OperandStack.Push(arrayref);
        }

        [Opcode(0xbe, "arraylength")]
        public Task ArrayLength(IJavaThread thread)
        {
            var arrayref = thread.CurrentMethod.OperandStack.Pop();
            if (arrayref == null)
                throw new NullReferenceException(nameof(arrayref));

            var length = arrayref switch
            {
                HeapArrayRef arrayRef => (_heap.TryGetArray(arrayRef, out var array) && array != null) ? array.Length : throw new NullReferenceException(nameof(array)),
                HeapClassArrayRef classArrayRef => (_heap.TryGetClassArray(classArrayRef, out var array) && array != null) ? array.Length : throw new NullReferenceException(nameof(array)),
                _ => throw new Exception()
            };

            thread.CurrentMethod.OperandStack.Push(length);

            return Task.CompletedTask;
        }

        [Opcode(0xbf, "athrow")]
        public Task Athrow(IJavaThread thread) => throw new NotImplementedException();

        [Opcode(0xc0, "checkcast")]
        public Task CheckCast(IJavaThread thread) => throw new NotImplementedException();

        [Opcode(0xc1, "instanceof")]
        public Task InstanceOf(IJavaThread thread) => throw new NotImplementedException();

        [Opcode(0xc2, "monitorenter")]
        public Task MonitorEnter(IJavaThread thread)
        {
            var objectref = thread.CurrentMethod.OperandStack.Pop();
            if (objectref == null)
                throw new NullReferenceException(nameof(objectref));

            return Task.CompletedTask;
        }

        [Opcode(0xc3, "monitorexit")]
        public Task MonitorExit(IJavaThread thread)
        {
            var objectref = thread.CurrentMethod.OperandStack.Pop();
            if (objectref == null)
                throw new NullReferenceException(nameof(objectref));

            return Task.CompletedTask;
        }
    }
}
