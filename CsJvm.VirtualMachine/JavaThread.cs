using CsJvm.Abstractions.VirtualMachine;
using CsJvm.Models;
using CsJvm.VirtualMachine.Extensions;

namespace CsJvm.VirtualMachine
{
    /// <summary>
    /// The Java Virtual Machine thread
    /// </summary>
    public class JavaThread : IJavaThread
    {
        /// <summary>
        /// Decoder execution mode
        /// </summary>
        private enum StepMode
        {
            /// <summary>
            /// Run the code
            /// </summary>
            Run,

            /// <summary>
            /// Step into
            /// </summary>
            Into,

            /// <summary>
            /// Step over
            /// </summary>
            Over
        }

        /// <summary>
        /// Opcodes decoder
        /// </summary>
        private readonly IOpcodesDecoder _decoder;

        /// <summary>
        /// Each Java Virtual Machine thread has a private Java Virtual Machine stack, created at the same time as the thread
        /// </summary>
        private readonly Stack<Frame> _frameStack = new();

        /// <summary>
        /// Invoking methods program counters stack
        /// </summary>
        private readonly Stack<uint> _programCounters = new();

        /// <summary>
        /// Native methods stack
        /// </summary>
        private readonly Stack<NativeMethod> _nativeMethods = new();

        /// <summary>
        /// Execution engine step mode
        /// </summary>
        private StepMode _stepMode = StepMode.Run;

        /// <inheritdoc/>
        public string Name { get; set; } = string.Empty;

        /// <inheritdoc/>
        public uint ProgramCounter { get; set; }

        /// <inheritdoc/>
        public Frame CurrentMethod { get; private set; }

        /// <inheritdoc/>
        public Frame PreviousMethod => _frameStack.Peek();

        /// <inheritdoc/>
        public int FrameSize => _frameStack.Count;

        /// <inheritdoc/>
        public IOpcodesDecoder Decoder => _decoder;

        /// <summary>
        /// Current executing native method
        /// </summary>
        public NativeMethod? CurrentNativeMethod { get; private set; }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="decoder">opcodes decoder</param>
        /// <param name="method">Method to run</param>
        public JavaThread(IOpcodesDecoder decoder, Frame method)
        {
            _decoder = decoder;
            CurrentMethod = method;
        }

        /// <inheritdoc/>
        public void PushFrame(Frame frame)
        {
            if (CurrentMethod != null)
            {
                // store current method in the stack
                _frameStack.Push(CurrentMethod.Clone());

                // store current program counter
                _programCounters.Push(ProgramCounter);

                // reset program counter
                ProgramCounter = 0;
            }

            // make the new method current
            CurrentMethod = frame;

            // immediately execute the method
            switch (_stepMode)
            {
                case StepMode.Run:
                    Run();
                    break;

                case StepMode.Into:
                    StepInto();
                    break;

                case StepMode.Over:
                    RunAndRestore();
                    break;

                default: throw new InvalidOperationException($"Unknown step mode: {_stepMode}");
            }
        }

        /// <inheritdoc/>
        public void PushNative(NativeMethod nativeMethod)
        {
            if (CurrentNativeMethod != null)
            {
                // store current method in the stack
                _nativeMethods.Push(CurrentNativeMethod.Clone());
            }

            // make the new method current
            CurrentNativeMethod = nativeMethod;

            // immediately execute the method
            RunNative();
        }

        /// <inheritdoc/>
        public void Run()
        {
            if (CurrentMethod == null)
                return;

            while (ProgramCounter < CurrentMethod.Code.Length)
            {
                _decoder.Execute(this);
            }

            // the current method execution is done, restore previous method and continue
            if (_frameStack.TryPop(out var prevFrame))
            {
                // restore program counter
                ProgramCounter = _programCounters.Pop();

                // restore previous method
                CurrentMethod = prevFrame;

                // continue execution
                Run();
            }
        }

        /// <inheritdoc/>
        public void Stop() => throw new NotImplementedException();

        /// <inheritdoc/>
        public void StepInto()
        {
            _stepMode = StepMode.Into;

            if (CurrentMethod == null)
                return;

            if (ProgramCounter < CurrentMethod.Code.Length)
            {
                _decoder.Execute(this);
                return;
            }

            // the current method execution is done, restore previous method and continue
            if (_frameStack.TryPop(out var prevFrame))
            {
                // restore program counter
                ProgramCounter = _programCounters.Pop();

                // restore previous method
                CurrentMethod = prevFrame;

                // continue execution
                StepInto();
            }
        }

        /// <inheritdoc/>
        public void StepOver()
        {
            _stepMode = StepMode.Over;

            if (CurrentMethod == null)
                return;

            if (ProgramCounter < CurrentMethod.Code.Length)
            {
                _decoder.Execute(this);
                return;
            }

            // the current method execution is done, restore previous method and continue
            if (_frameStack.TryPop(out var prevFrame))
            {
                // restore program counter
                ProgramCounter = _programCounters.Pop();

                // restore previous method
                CurrentMethod = prevFrame;

                // continue execution
                StepOver();
            }
        }

        /// <summary>
        /// Runs native method
        /// </summary>
        private void RunNative()
        {
            if (CurrentNativeMethod == null || CurrentNativeMethod.Method == null)
                return;

            CurrentNativeMethod.Method.Invoke(CurrentNativeMethod.Instance, new object[] { this, CurrentNativeMethod.Descriptor, CurrentNativeMethod.Args });

            // the current method execution is done, restore previous method and continue
            if (_nativeMethods.TryPop(out var prevNative))
            {
                // restore previous method
                CurrentNativeMethod = prevNative;

                // continue execution
                RunNative();
            }
        }

        /// <summary>
        /// Runs current method and restores previous frame
        /// </summary>
        private void RunAndRestore()
        {
            while (ProgramCounter < CurrentMethod.Code.Length)
            {
                _decoder.Execute(this);
            }

            // the current method execution is done, restore previous method and continue
            if (_frameStack.TryPop(out var prevFrame))
            {
                // restore program counter
                ProgramCounter = _programCounters.Pop();

                // restore previous method
                CurrentMethod = prevFrame;
            }
        }
    }
}
