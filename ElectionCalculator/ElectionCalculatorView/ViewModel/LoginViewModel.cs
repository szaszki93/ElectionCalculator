using ElectionCalculatorView.Base;
using ElectionCalculatorView.Helpers;
using System;
using System.Windows;
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

            HideErrors();
        }

        public string FirstName
        {
            get => _FirstName;
            set
            {
                _FirstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }

        public Visibility IsFirstNameErrorVisible { get; set; }
        public Visibility IsLastNameErrorVisible { get; set; }
        public Visibility IsPeselErrorVisible { get; set; }

        public string LastName
        {
            get => _LastName;
            set
            {
                _LastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }

        public ICommand LoginCmd { get; set; }

        public string Pesel
        {
            get => _Pesel;
            set
            {
                _Pesel = value;
                OnPropertyChanged(nameof(Pesel));
            }
        }

        private bool HaveEighteenYears()
        {
            DateTime bornDate = PeselValidator.GetDateFromPesel(Pesel).Value;

            return DateTime.Today >= bornDate.AddYears(18);
        }

        private bool HaveError()
        {
            HideErrors();

            bool hasError = false;

            if (string.IsNullOrEmpty(FirstName))
            {
                IsFirstNameErrorVisible = Visibility.Visible;
                OnPropertyChanged(nameof(IsFirstNameErrorVisible));
                hasError = true;
            }

            if (string.IsNullOrEmpty(LastName))
            {
                IsLastNameErrorVisible = Visibility.Visible;
                OnPropertyChanged(nameof(IsLastNameErrorVisible));
                hasError = true;
            }

            if (!PeselValidator.IsPeselValid(Pesel))
            {
                IsPeselErrorVisible = Visibility.Visible;
                OnPropertyChanged(nameof(IsPeselErrorVisible));
                hasError = true;
            }

            return hasError;
        }

        private void HideErrors()
        {
            IsFirstNameErrorVisible = Visibility.Collapsed;
            OnPropertyChanged(nameof(IsFirstNameErrorVisible));

            IsLastNameErrorVisible = Visibility.Collapsed;
            OnPropertyChanged(nameof(IsLastNameErrorVisible));

            IsPeselErrorVisible = Visibility.Collapsed;
            OnPropertyChanged(nameof(IsPeselErrorVisible));
        }

        private void Login()
        {
            if (Pesel == "a") { mainViewModel.OpenResultView(); }

            if (HaveError()) { return; }

            if (!HaveEighteenYears())
            {
                MessageBox.Show(
                    "You cannot login because you don't have eighteen years.",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);

                return;
            }

            if (mainViewModel.VoteBusiness.IsThatPeselJustVote(Pesel))
            {
                MessageBox.Show(
                    "You cannot login because you have already voted.",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);

                return;
            }

            mainViewModel.OpenElectionView(Pesel);
        }
    }
}