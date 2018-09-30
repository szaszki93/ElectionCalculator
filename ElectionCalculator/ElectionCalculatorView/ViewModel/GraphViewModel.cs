using ElectionCalculatorView.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ElectionCalculatorView.ViewModel
{
    public class GraphViewModel : LogoutViewModel
    {
        public GraphViewModel(MainWindowViewModel mainViewModel) : base(mainViewModel)
        {
            BackCmd = new RelayCommand(x => Back());
        }

        public ICommand BackCmd { get; set; }

        private void Back()
        {
            mainViewModel.OpenResultView();
        }
    }
}