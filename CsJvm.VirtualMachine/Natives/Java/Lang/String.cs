using CsJvm.Abstractions.VirtualMachine;
using CsJvm.VirtualMachine.Attributes;

namespace CsJvm.VirtualMachine.Natives.Java.Lang
{
    /// <summary>
    /// Java.lang.string native methods
    /// </summary>
    public class String : INativeCall
    {
        [JavaNative("java/lang/String", "intern", "()Ljava/lang/String;")]
        public void Intern(IJavaThread thread, string descriptor, params object[] args) => throw new NotImplementedException();

        [JavaNative("java/lang/String", "intern2", "()Ljava/lang/String;")]
        public void Intern2(IJavaThread thread, string descriptor, params object[] args) => throw new NotImplementedException();
    }
}
