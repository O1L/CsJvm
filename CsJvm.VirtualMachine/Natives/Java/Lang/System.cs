using CsJvm.Abstractions.VirtualMachine;
using CsJvm.VirtualMachine.Attributes;

namespace CsJvm.VirtualMachine.Natives.Java.Lang
{
    /// <summary>
    /// Java.lang.system native methods
    /// </summary>
    public class System : INativeCall
    {
        [JavaNative("java/lang/System", "registerNatives", "()V")]
        public void RegisterNatives(IJavaThread thread, string descriptor, params object[] args)
        {
            return;
        }

        [JavaNative("java/lang/System", "setIn0", "(Ljava/io/InputStream;)V")]
        public void SetIn0(IJavaThread thread, string descriptor, params object[] args) => throw new NotImplementedException();

        [JavaNative("java/lang/System", "setOut0", "(Ljava/io/PrintStream;)V")]
        public void SetOut0(IJavaThread thread, string descriptor, params object[] args) => throw new NotImplementedException();

        [JavaNative("java/lang/System", "setErr0", "(Ljava/io/PrintStream;)V")]
        public void SetErr0(IJavaThread thread, string descriptor, params object[] args) => throw new NotImplementedException();

        [JavaNative("java/lang/System", "currentTimeMillis", "()J")]
        public void CurrentTimeMillis(IJavaThread thread, string descriptor, params object[] args)
        {
            var ms = DateTime.UtcNow.Millisecond;
            thread.CurrentMethod.OperandStack.Push(ms);
        }

        [JavaNative("java/lang/System", "nanoTime", "()J")]
        public void NanoTime(IJavaThread thread, string descriptor, params object[] args)
        {
            var ticks = DateTime.UtcNow.Ticks;
            thread.CurrentMethod.OperandStack.Push(ticks);
        }

        [JavaNative("java/lang/System", "arraycopy", "(Ljava/lang/Object;ILjava/lang/Object;II)V")]
        public void ArrayCopy(IJavaThread thread, string descriptor, params object[] args) => throw new NotImplementedException();

        [JavaNative("java/lang/System", "identityHashCode", "(Ljava/lang/Object;)I")]
        public void IdentityHashCode(IJavaThread thread, string descriptor, params object[] args) => throw new NotImplementedException();

        [JavaNative("java/lang/System", "initProperties", "(Ljava/util/Properties;)Ljava/util/Properties;")]
        public void InitProperties(IJavaThread thread, string descriptor, params object[] args) => throw new NotImplementedException();

        [JavaNative("java/lang/System", "mapLibraryName", "(Ljava/lang/String;)Ljava/lang/String;")]
        public void MapLibraryName(IJavaThread thread, string descriptor, params object[] args) => throw new NotImplementedException();
    }
}
