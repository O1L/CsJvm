using CsJvm.Abstractions.Instructions;
using CsJvm.Abstractions.VirtualMachine;
using CsJvm.Models;
using CsJvm.Models.ClassFileFormat.ConstantPool;
using CsJvm.Models.Heap;

namespace CsJvm.VirtualMachine.Instructions.Interpreter
{
    /// <summary>
    /// Java virtual machine opcodes interpreter
    /// </summary>
    public partial class JvmInterpreter : IOpcodes
    {
        /// <summary>
        /// Jar executable loader
        /// </summary>
        private readonly IJavaExecutable _executable;

        /// <summary>
        /// Java runtime
        /// </summary>
        private readonly IJavaRuntime _runtime;

        /// <summary>
        /// Native methods
        /// </summary>
        private readonly INativeMethodsProvider _natives;

        /// <summary>
        /// Heap
        /// </summary>
        private readonly IJavaHeap _heap;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="loader">Jar loader</param>
        /// <param name="runtime">Java runtime</param>
        /// <param name="natives">Native methods</param>
        /// <param name="heap">Heap</param>
        public JvmInterpreter(IJavaExecutable executable, IJavaRuntime runtime, INativeMethodsProvider natives, IJavaHeap heap)
        {
            _executable = executable;
            _runtime = runtime;
            _natives = natives;
            _heap = heap;
        }

        /// <summary>
        /// Tries to resolve specified class by name 
        /// </summary>
        /// <param name="className">Class name</param>
        /// <param name="javaClass">Resolved class</param>
        /// <returns><see langword="true"></see> if success; otherwise <see langword="false"></see></returns>
        private async Task<JavaClass?> ResolveClass(string className)
        {
            var javaClass = await _executable.GetClassAsync(className);

            if (javaClass == null)
                javaClass = await _runtime.GetClassAsync(className);

            return javaClass;
        }

        /// <summary>
        /// Executes static constructor
        /// </summary>
        /// <param name="javaClass">Class to initialize</param>
        /// <param name="thread">Current thread</param>
        private static Task RunStaticCtorAsync(JavaClass javaClass, IJavaThread thread)
        {
            if (javaClass.StaticInitialized ||
                !javaClass.Methods.TryGetValue("<clinit>:()V", out var clinit) ||
                clinit == null || clinit.CodeAttribute == null)
                return Task.CompletedTask;

            javaClass.StaticInitialized = true;

            var clinitFrame = new Frame
            {
                Code = clinit.CodeAttribute.Code,
                CpInfo = javaClass.ConstantPool,
                LocalVariables = new object[clinit.CodeAttribute.MaxLocals],
                MethodName = $"{javaClass.Name}.<clinit>:()V"
            };

            var thread0 = new JavaThread(thread.Decoder, clinitFrame);
            return thread0.RunAsync();
        }

        private Task RunDefaultCtorAsync(JavaClass javaClass, IJavaThread thread, HeapClassRef classRef)
        {
            if (javaClass.DefaulthInitialized || !javaClass.Methods.TryGetValue("<init>:()V", out var init) || init == null)
                return Task.CompletedTask;

            javaClass.DefaulthInitialized = true;
            var frame = new Frame
            {
                Code = init.CodeAttribute!.Code,
                CpInfo = javaClass.ConstantPool,
                LocalVariables = new object[init.CodeAttribute.MaxLocals],
                MethodName = $"{javaClass.Name}.<init>:()V"
            };

            frame.LocalVariables[0] = classRef;

            var thread0 = new JavaThread(thread.Decoder, frame);
            return thread0.RunAsync();
        }

        /// <summary>
        /// Gets method info
        /// </summary>
        /// <param name="thread">Current thread</param>
        /// <param name="info"><see cref="CONSTANT_MethodrefInfo"/> structure</param>
        /// <returns>A tuple of class name, method name and method descriptor</returns>
        private static (string ClassName, string MethodName, string Descriptor) GetMethodrefInfo(IJavaThread thread, CONSTANT_MethodrefInfo info)
        {
            var classInfo = (CONSTANT_ClassInfo)thread.CurrentMethod.CpInfo[info.Classindex];
            var nameAndTypeInfo = (CONSTANT_NameAndTypeInfo)thread.CurrentMethod.CpInfo[info.NameAndTypeIndex];

            var className = ((CONSTANT_Utf8Info)thread.CurrentMethod.CpInfo[classInfo.NameIndex]).Utf8String;
            var methodName = ((CONSTANT_Utf8Info)thread.CurrentMethod.CpInfo[nameAndTypeInfo.NameIndex]).Utf8String;
            var descriptor = ((CONSTANT_Utf8Info)thread.CurrentMethod.CpInfo[nameAndTypeInfo.DescriptorIndex]).Utf8String;

            return (className, methodName, descriptor);
        }

        /// <summary>
        /// Gets field info
        /// </summary>
        /// <param name="thread">Current thread</param>
        /// <param name="info"><see cref="CONSTANT_FieldrefInfo"/> structure</param>
        /// <returns>A tuple of class name, field name and method descriptor</returns>
        private static (string ClassName, string FieldName, string Descriptor) GetFieldrefInfo(IJavaThread thread, CONSTANT_FieldrefInfo info)
        {
            var nameAndTypeInfo = (CONSTANT_NameAndTypeInfo)thread.CurrentMethod.CpInfo[info.NameAndTypeIndex];
            var classInfo = (CONSTANT_ClassInfo)thread.CurrentMethod.CpInfo[info.ClassIndex];

            var className = ((CONSTANT_Utf8Info)thread.CurrentMethod.CpInfo[classInfo.NameIndex]).Utf8String;
            var fieldName = ((CONSTANT_Utf8Info)thread.CurrentMethod.CpInfo[nameAndTypeInfo.NameIndex]).Utf8String;
            var descriptor = ((CONSTANT_Utf8Info)thread.CurrentMethod.CpInfo[nameAndTypeInfo.DescriptorIndex]).Utf8String;

            return (className, fieldName, descriptor);
        }

        /// <summary>
        /// Creates a new frame
        /// </summary>
        /// <param name="javaClass">Parent class</param>
        /// <param name="method">Method to create</param>
        /// <param name="args">Local variables</param>
        /// <param name="objectref">Object reference</param>
        /// <returns>A new created <see cref="Frame"/> that can be executed</returns>
        private static Frame CreateFrame(JavaClass javaClass, JavaMethod method, object?[] args, object? objectref)
        {
            // create new frame for method
            var frame = new Frame
            {
                Code = method.CodeAttribute!.Code,
                CpInfo = javaClass.ConstantPool,
                LocalVariables = new object[method.CodeAttribute.MaxLocals],
                MethodName = method.Name
            };

            var varIndex = 0;

            if (objectref != null)
            {
                // set 0-element of local variable to objectref
                frame.LocalVariables[0] = objectref;
                varIndex++;
            }

            for (var i = 0; i < args.Length; i++)
            {
                var value = args[i];
                frame.LocalVariables[varIndex++] = value;

                if (value is long || value is double)
                    varIndex++;
            }

            return frame;
        }
    }
}
