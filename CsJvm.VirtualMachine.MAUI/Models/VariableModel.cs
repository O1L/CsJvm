using CommunityToolkit.Mvvm.ComponentModel;

namespace CsJvm.VirtualMachine.MAUI.Models
{
    public class VariableModel : ObservableObject
    {
        private int _index;
        private string _typeName = string.Empty;
        private string _value = string.Empty;

        public int Index
        {
            get => _index;
            set
            {
                _index = value;
                OnPropertyChanged();
            }
        }

        public string TypeName
        {
            get => _typeName;
            set
            {
                _typeName = value;
                OnPropertyChanged();
            }
        }

        public string Value
        {
            get => _value;
            set
            {
                _value = value;
                OnPropertyChanged();
            }
        }
    }
}
