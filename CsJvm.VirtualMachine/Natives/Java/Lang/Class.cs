using CsJvm.Abstractions.VirtualMachine;
using CsJvm.Models;
using CsJvm.Models.Heap;
using CsJvm.VirtualMachine.Attributes;

namespace CsJvm.VirtualMachine.Natives.Java.Lang
{
    /// <summary>
    /// Java.lang.class native methods
    /// </summary>
    public class Class : INativeCall
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
        /// Java heap
        /// </summary>
        private readonly IJavaHeap _heap;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="executable">Executable jar loader</param>
        /// <param name="runtime">Java runtime</param>
        /// <param name="heap">Java heap</param>
        public Class(IJavaExecutable executable, IJavaRuntime runtime, IJavaHeap heap)
        {
            _executable = executable;
            _runtime = runtime;
            _heap = heap;
        }

        [JavaNative("java/lang/Class", "registerNatives", "()V")]
        public void RegisterNatives(IJavaThread thread, string descriptor, params object[] args)
        {
            return;
        }

        [JavaNative("java/lang/Class", "forName0", "(Ljava/lang/String;ZLjava/lang/ClassLoader;Ljava/lang/Class;)Ljava/lang/Class;")]
        public void ForName0(IJavaThread thread, string descriptor, params object[] args) => throw new NotImplementedException();

        [JavaNative("java/lang/Class", "isInstance", "(Ljava/lang/Object;)Z")]
        public void IsInstance(IJavaThread thread, string descriptor, params object[] args) => throw new NotImplementedException();

        [JavaNative("java/lang/Class", "isInterface", "()Z")]
        public void IsInterface(IJavaThread thread, string descriptor, params object[] args) => throw new NotImplementedException();

        [JavaNative("java/lang/Class", "isArray", "()Z")]
        public void IsArray(IJavaThread thread, string descriptor, params object[] args) => throw new NotImplementedException();//var arg = args[0];//frame.OperandStack.Push(arg.GetType().IsArray);

        [JavaNative("java/lang/Class", "isPrimitive", "()Z")]
        public void IsPrimitive(IJavaThread thread, string descriptor, params object[] args) => throw new NotImplementedException();//var arg = args[0];//frame.OperandStack.Push(arg.GetType().IsPrimitive);

        [JavaNative("java/lang/Class", "getName0", "()Ljava/lang/String;")]
        public void GetName0(IJavaThread thread, string descriptor, params object[] args)
        {
            if (args[0] is not HeapClassRef classRef)
                throw new InvalidOperationException($"{args[0].GetType()}");

            if (!_heap.TryGetClass(classRef, out var javaClass) || javaClass == null)
                throw new NullReferenceException(nameof(javaClass));

            if (!TryResolveClass("java/lang/String", out var stringClass) || stringClass == null)
                throw new NullReferenceException(nameof(stringClass));

            // TODO
            stringClass.Fields["value:[C"] = classRef.Name.ToCharArray();
            var stringClassRef = _heap.CreateClassRef(stringClass);

            thread.CurrentMethod.OperandStack.Push(stringClassRef);

        }

        [JavaNative("java/lang/Class", "getSuperclass", "()Ljava/lang/Class;")]
        public void GgetSuperclass(IJavaThread thread, string descriptor, params object[] args) => throw new NotImplementedException();

        [JavaNative("java/lang/Class", "getInterfaces0", "()[Ljava/lang/Class;")]
        public void GetInterfaces0(IJavaThread thread, string descriptor, params object[] args) => throw new NotImplementedException();

        [JavaNative("java/lang/Class", "getComponentType", "()Ljava/lang/Class;")]
        public void GetComponentType(IJavaThread thread, string descriptor, params object[] args) => throw new NotImplementedException();

        [JavaNative("java/lang/Class", "getModifiers", "()I")]
        public void GetModifiers(IJavaThread thread, string descriptor, params object[] args) => throw new NotImplementedException();

        [JavaNative("java/lang/Class", "getSigners", "()[Ljava/lang/Object;")]
        public void GetSigners(IJavaThread thread, string descriptor, params object[] args) => throw new NotImplementedException();

        [JavaNative("java/lang/Class", "setSigners", ":([Ljava/lang/Object;)V")]
        public void SetSigners(IJavaThread thread, string descriptor, params object[] args) => throw new NotImplementedException();

        [JavaNative("java/lang/Class", "getEnclosingMethod0", "()[Ljava/lang/Object;")]
        public void GetEnclosingMethod0(IJavaThread thread, string descriptor, params object[] args) => throw new NotImplementedException();

        [JavaNative("java/lang/Class", "getDeclaringClass0", "()Ljava/lang/Class;")]
        public void GetDeclaringClass0(IJavaThread thread, string descriptor, params object[] args) => throw new NotImplementedException();

        [JavaNative("java/lang/Class", "getProtectionDomain0", "()Ljava/security/ProtectionDomain;")]
        public void GetProtectionDomain0(IJavaThread thread, string descriptor, params object[] args) => throw new NotImplementedException();

        [JavaNative("java/lang/Class", "getPrimitiveClass", "(Ljava/lang/String;)Ljava/lang/Class;")]
        public void GetPrimitiveClass(IJavaThread thread, string descriptor, params object[] args) => throw new NotImplementedException();

        [JavaNative("java/lang/Class", "getGenericSignature0", "()Ljava/lang/String;")]
        public void GgetGenericSignature0(IJavaThread thread, string descriptor, params object[] args) => throw new NotImplementedException();

        [JavaNative("java/lang/Class", "getRawAnnotations", "()[B")]
        public void getRawAnnotations(IJavaThread thread, string descriptor, params object[] args) => throw new NotImplementedException();

        [JavaNative("java/lang/Class", "getRawTypeAnnotations", ":()[B")]
        public void GetRawTypeAnnotations(IJavaThread thread, string descriptor, params object[] args) => throw new NotImplementedException();

        [JavaNative("java/lang/Class", "getConstantPool", "()Lsun/reflect/ConstantPool;")]
        public void GetConstantPool(IJavaThread thread, string descriptor, params object[] args) => throw new NotImplementedException();

        [JavaNative("java/lang/Class", "getDeclaredFields0", "(Z)[Ljava/lang/reflect/Field;")]
        public void GetDeclaredFields0(IJavaThread thread, string descriptor, params object[] args) => throw new NotImplementedException();

        [JavaNative("java/lang/Class", "getDeclaredMethods0", "(Z)[Ljava/lang/reflect/Method;")]
        public void GetDeclaredMethods0(IJavaThread thread, string descriptor, params object[] args) => throw new NotImplementedException();

        [JavaNative("java/lang/Class", "getDeclaredConstructors0", "(Z)[Ljava/lang/reflect/Constructor;")]
        public void GetDeclaredConstructors0(IJavaThread thread, string descriptor, params object[] args) => throw new NotImplementedException();

        [JavaNative("java/lang/Class", "getDeclaredClasses0", "()[Ljava/lang/Class;")]
        public void GetDeclaredClasses0(IJavaThread thread, string descriptor, params object[] args) => throw new NotImplementedException();

        [JavaNative("java/lang/Class", "desiredAssertionStatus0", "(Ljava/lang/Class;)Z")]
        public void DesiredAssertionStatus0(IJavaThread thread, string descriptor, params object[] args) => throw new NotImplementedException();

        /// <summary>
        /// Tries resolve java class
        /// </summary>
        /// <param name="className">Class name</param>
        /// <param name="javaClass">resolved class</param>
        private bool TryResolveClass(string className, out JavaClass? javaClass)
        {
            /*if (_executable.TryGet(className, out javaClass) || _runtime.TryGet(className, out javaClass))
                return true;*/

            javaClass = null;
            return false;
        }
    }
}
