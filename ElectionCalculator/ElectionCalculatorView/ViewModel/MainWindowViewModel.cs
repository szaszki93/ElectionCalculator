using ElectionCalculatorService;
using ElectionCalculatorService.Entity;
using ElectionCalculatorView.Base;

namespace ElectionCalculatorView.ViewModel
{
    public class MainWindowViewModel : NotifyPropertyViewModel
    {
        private BaseViewModel _currentViewModel;

        public MainWindowViewModel()
        {
            CreateServices();

            OpenLoginView();
        }

        public CandidateService CandidateService { get; private set; }

        public BaseViewModel CurrentViewModel
        {
            get
            {
                return _currentViewModel;
            }
            set
            {
                if (_currentViewModel != value)
                {
                    _currentViewModel = value;
                    OnPropertyChanged(nameof(CurrentViewModel));
                }
            }
        }

        public ResultService ResultService { get; private set; }
        public VoteService VoteService { get; private set; }

        public void OpenElectionView(Person person)
        {
            CurrentViewModel = new ElectionViewModel(this, person);
        }

        public void OpenGraphView(Result data)
        {
            CurrentViewModel = new GraphViewModel(this, data);
        }

        public void OpenLoginView()
        {
            CurrentViewModel = new LoginViewModel(this);
        }

        public void OpenResultView()
        {
            CurrentViewModel = new ResultViewModel(this);
        }

        public void OpenResultView(Result data)
        {
            CurrentViewModel = new ResultViewModel(this, data);
        }

        private void CreateServices()
        {
            var externalService = new ExternalService();
            CandidateService = new CandidateService(externalService);
            VoteService = new VoteService();
            ResultService = new ResultService(externalService, VoteService, CandidateService);
        }
    }
}