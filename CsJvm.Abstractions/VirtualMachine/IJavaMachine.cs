namespace CsJvm.Abstractions.VirtualMachine
{
    /// <summary>
    /// Provides methods to run, pause, resume and stop Java Virtual machine
    /// </summary>
    public interface IJavaMachine : IDisposable
    {
        /// <summary>
        /// Main thread reference
        /// </summary>
        IJavaThread? MainThread { get; }


        /// <summary>
        /// Loads JAR file to execute
        /// </summary>
        /// <param name="path"></param>
        public Task<bool> LoadAsync(string path);

        /// <summary>
        /// Start main method execution
        /// </summary>
        public Task RunAsync();

        /// <summary>
        /// Pause / resume virtual machine
        /// </summary>
        public void PauseResume();

        /// <summary>
        /// Stops virtual machine
        /// </summary>
        public void Stop();

        /// <summary>
        /// Steps into one instruction
        /// </summary>
        public Task StepIntoAsync();

        /// <summary>
        /// Steps over one instruction
        /// </summary>
        public Task StepOverAsync();
    }
}
