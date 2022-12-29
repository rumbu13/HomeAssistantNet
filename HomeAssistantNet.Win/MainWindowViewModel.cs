using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HomeAssistantNet.Win
{
    internal class MainWindowViewModel : INotifyPropertyChanged
    {
        

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        private bool isConnected;
        public bool IsConnected
        {
            get => isConnected;
            set => SetField(ref isConnected, value);
        }

        private bool isConnecting;
        public bool IsConnecting
        {
            get => isConnected;
            set => SetField(ref isConnected, value);
        }

        private bool isNotConnected;
        public bool IsNotConnected
        {
            get => isNotConnected;
            set => SetField(ref isNotConnected, value);
        }


    }
}
