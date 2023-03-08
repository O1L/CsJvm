using CsJvm.Models;

namespace CsJvm.Abstractions.VirtualMachine
{
    /// <summary>
    /// Represents the Java Virtual Machine thread. The Java Virtual Machine can support many threads of execution at once. 
    /// </summary>
    public interface IJavaThread
    {
        /// <summary>
        /// Current thread name
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Each Java Virtual Machine thread has its own pc (program counter) register
        /// </summary>
        uint ProgramCounter { get; set; }

        /// <summary>
        /// Current frames stack size (excluding executing method)
        /// </summary>
        int FrameSize { get; }

        /// <summary>
        /// Each Java Virtual Machine thread is executing the code of a single method, namely the current method for that thread
        /// </summary>
        Frame CurrentMethod { get; }

        /// <summary>
        /// Previous executed method
        /// </summary>
        Frame PreviousMethod { get; }

        /// <summary>
        /// Opcodes decoder reference
        /// </summary>
        IOpcodesDecoder Decoder { get; }

        /// <summary>
        /// Pushes new method and immediately run execution
        /// </summary>
        /// <param name="frame">Method to run</param>
        void PushFrame(Frame frame);

        /// <summary>
        /// Pushes new native method and immediately run execution
        /// </summary>
        /// <param name="nativeMethod">Method to run</param>
        void PushNative(NativeMethod nativeMethod);

        /// <summary>
        /// Runs thread
        /// </summary>
        void Run();

        /// <summary>
        /// Stops thread
        /// </summary>
        void Stop();

        /// <summary>
        /// Steps into one instruction
        /// </summary>
        public void StepInto();

        /// <summary>
        /// Steps over one instruction
        /// </summary>
        public void StepOver();
    }
}
