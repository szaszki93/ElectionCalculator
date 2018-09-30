using ElectionCalculatorView.Base;
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