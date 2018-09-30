using ElectionCalculatorView.ViewModel;

namespace ElectionCalculatorView.Base
{
    public abstract class BaseViewModel : NotifyPropertyViewModel
    {
        protected readonly MainWindowViewModel mainViewModel;

        protected BaseViewModel(MainWindowViewModel mainViewModel)
        {
            this.mainViewModel = mainViewModel;
        }
    }
}