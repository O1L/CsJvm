using CsJvm.Abstractions.Loader;
using CsJvm.Abstractions.VirtualMachine;
using CsJvm.Models;
using CsJvm.Models.Heap;
using CsJvm.VirtualMachine.Attributes;

namespace CsJvm.VirtualMachine.Natives.Java.Lang
{
    /// <summary>
    /// Java.lang.object native methods
    /// </summary>
    public class Object : INativeCall
    {
        /// <summary>
        /// Jar loader
        /// </summary>
        private readonly IJarLoader _loader;

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
        /// <param name="loader">Jar loader</param>
        /// <param name="runtime">Java runtime</param>
        /// <param name="heap">Java heap</param>
        public Object(IJarLoader loader, IJavaRuntime runtime, IJavaHeap heap)
        {
            _loader = loader;
            _runtime = runtime;
            _heap = heap;
        }

        [JavaNative("java/lang/Object", "registerNatives", "()V")]
        public void RegisterNatives(IJavaThread thread, string descriptor, params object[] args)
        {
            return;
        }

        [JavaNative("java/lang/Object", "getClass", "()Ljava/lang/Class;")]
        public void GetClass(IJavaThread thread, string descriptor, params object[] args)
        {
            if (args[0] is not HeapClassRef classRef)
                throw new InvalidOperationException($"{args[0].GetType()}");

            if (!_heap.TryGetClass(classRef, out var javaClass) || javaClass == null)
                throw new NullReferenceException(nameof(javaClass));

            if (!TryResolveClass("java/lang/Class", out var classClass) || classClass == null)
                throw new NullReferenceException(nameof(classClass));

            var classClassRef = _heap.CreateClassRef(classClass);

            thread.CurrentMethod.OperandStack.Push(classClassRef);
        }

        [JavaNative("java/lang/Object", "hashCode", "()I")]
        public void HashCode(IJavaThread thread, string descriptor, params object[] args)
        {
            var obj = args[0];
            if (obj == null)
                throw new NullReferenceException();

            var hashCode = obj.GetHashCode();
            thread.CurrentMethod.OperandStack.Push(hashCode);
        }

        [JavaNative("java/lang/Object", "clone", "()Ljava/lang/Object;")]
        public void Clone(IJavaThread thread, string descriptor, params object[] args) => throw new NotImplementedException();

        [JavaNative("java/lang/Object", "notify", "()V")]
        public void Notify(IJavaThread thread, string descriptor, params object[] args) => throw new NotImplementedException();

        [JavaNative("java/lang/Object", "notifyAll", "()V")]
        public void NotifyAll(IJavaThread thread, string descriptor, params object[] args) => throw new NotImplementedException();

        [JavaNative("java/lang/Object", "wait", "(J)V")]
        public void Wait(IJavaThread thread, string descriptor, params object[] args) => throw new NotImplementedException();

        /// <summary>
        /// Tries resolve java class
        /// </summary>
        /// <param name="className">Class name</param>
        /// <param name="javaClass">resolved class</param>
        private bool TryResolveClass(string className, out JavaClass? javaClass)
        {
            if (_loader.TryGetClass(className, out javaClass) || _runtime.TryGet(className, out javaClass))
                return true;

            javaClass = null;
            return false;
        }

    }
}
