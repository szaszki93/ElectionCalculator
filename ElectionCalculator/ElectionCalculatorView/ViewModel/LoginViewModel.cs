using ElectionCalculatorView.Base;
using System.Windows.Input;

namespace ElectionCalculatorView.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        private string _FirstName = null;

        private string _LastName = null;

        private string _Pesel = null;

        public LoginViewModel(MainWindowViewModel mainViewModel) : base(mainViewModel)
        {
            LoginCmd = new RelayCommand(x => Login());
        }

        public string FirstName
        {
            get
            {
                return _FirstName;
            }
            set
            {
                _FirstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }

        public string LastName
        {
            get
            {
                return _LastName;
            }
            set
            {
                _LastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }

        public ICommand LoginCmd { get; set; }

        public string Pesel
        {
            get
            {
                return _Pesel;
            }
            set
            {
                _Pesel = value;
                OnPropertyChanged(nameof(Pesel));
            }
        }

        private void Login()
        {
            if (string.IsNullOrEmpty(Pesel)) { return; }

            mainViewModel.OpenElectionView(Pesel);
        }
    }
}