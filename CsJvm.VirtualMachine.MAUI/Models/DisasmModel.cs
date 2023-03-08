using CommunityToolkit.Mvvm.ComponentModel;
using CsJvm.Models.Disasm;

namespace CsJvm.VirtualMachine.MAUI.Models
{
    public class DisasmModel : ObservableObject
    {
        private uint _currentPc;
        private uint _breakPoint;
        private string _methodName = string.Empty;
        private string _prevMethodName = string.Empty;
        private int _framesCount;

        private OpcodeInfo[] _opcodes = Array.Empty<OpcodeInfo>();
        private VariableModel[] _operandStack = Array.Empty<VariableModel>();
        private VariableModel[] _localVariables = Array.Empty<VariableModel>();

        public uint CurrentPc
        {
            get => _currentPc;
            set
            {
                _currentPc = value;
                OnPropertyChanged();
            }
        }

        public uint BreakPoint
        {
            get => _breakPoint;
            set
            {
                _breakPoint = value;
                OnPropertyChanged();
            }
        }

        public OpcodeInfo[] Opcodes
        {
            get => _opcodes;
            set
            {
                _opcodes = value;
                OnPropertyChanged();
            }
        }

        public string MethodName
        {
            get => _methodName;
            set
            {
                _methodName = value;
                OnPropertyChanged();
            }
        }

        public string PreviousMethodName
        {
            get => _prevMethodName;
            set
            {
                _prevMethodName = value;
                OnPropertyChanged();
            }
        }

        public int FramesCount
        {
            get => _framesCount;
            set
            {
                _framesCount = value;
                OnPropertyChanged();
            }
        }

        public VariableModel[] OperandStack
        {
            get => _operandStack;
            set
            {
                _operandStack = value;
                OnPropertyChanged();
            }
        }

        public VariableModel[] LocalVariables
        {
            get => _localVariables;
            set
            {
                _localVariables = value;
                OnPropertyChanged();
            }
        }

        public OpcodeInfo CurrentOpcode => Opcodes.FirstOrDefault(x => x.ProgramCounter == CurrentPc);
    }
}
