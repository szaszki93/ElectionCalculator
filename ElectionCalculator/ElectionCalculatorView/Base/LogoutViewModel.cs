using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ElectionCalculatorView.ViewModel;

namespace ElectionCalculatorView.Base
{
    public abstract class LogoutViewModel : BaseViewModel
    {
        protected LogoutViewModel(MainWindowViewModel mainViewModel) : base(mainViewModel)
        {
            LogoutCmd = new RelayCommand(x => Logout());
        }

        public ICommand LogoutCmd { get; set; }

        private void Logout()
        {
            mainViewModel.OpenLoginView();
        }
    }
}