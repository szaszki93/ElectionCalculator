using ElectionCalculatorService.Entity;
using ElectionCalculatorView.Base;
using System.Windows.Input;

namespace ElectionCalculatorView.ViewModel
{
    public class GraphViewModel : LogoutViewModel
    {
        public GraphViewModel(MainWindowViewModel mainViewModel, Result data) : base(mainViewModel)
        {
            BackCmd = new RelayCommand(x => Back());
            Data = data;
        }

        public ICommand BackCmd { get; set; }
        public Result Data { get; private set; }

        private void Back()
        {
            mainViewModel.OpenResultView(Data);
        }
    }
}