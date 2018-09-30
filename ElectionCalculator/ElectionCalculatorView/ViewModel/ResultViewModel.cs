using ElectionCalculatorView.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ElectionCalculatorView.ViewModel
{
    public class ResultViewModel : LogoutViewModel
    {
        public ResultViewModel(MainWindowViewModel mainViewModel) : base(mainViewModel)
        {
            ShowGraphCmd = new RelayCommand(x => ShowGraph());
        }

        private void ShowGraph()
        {
            mainViewModel.OpenGraphView();
        }

        public ICommand ShowGraphCmd { get; set; }
    }
}