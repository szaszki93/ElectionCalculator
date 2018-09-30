using System.ComponentModel;

namespace ElectionCalculatorView.Base
{
    public abstract class NotifyPropertyViewModel : INotifyPropertyChanged
    {
        public void OnPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}