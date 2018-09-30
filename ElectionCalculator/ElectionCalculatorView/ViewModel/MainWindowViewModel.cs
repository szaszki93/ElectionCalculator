using ElectionCalculatorService;
using ElectionCalculatorView.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ElectionCalculatorView.ViewModel
{
    public class MainWindowViewModel : NotifyPropertyViewModel
    {
        private BaseViewModel _currentViewModel;

        public MainWindowViewModel()
        {
            CreateBusinesses();

            OpenLoginView();
        }

        public CandidateBusiness CandidateBusiness { get; private set; }

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

        public ResultBusiness ResultBusiness { get; private set; }
        public VoteBusiness VoteBusiness { get; private set; }

        public void OpenElectionView(string pesel)
        {
            CurrentViewModel = new ElectionViewModel(this, pesel);
        }

        public void OpenGraphView()
        {
            CurrentViewModel = new GraphViewModel(this);
        }

        public void OpenLoginView()
        {
            CurrentViewModel = new LoginViewModel(this);
        }

        public void OpenResultView()
        {
            CurrentViewModel = new ResultViewModel(this);
        }

        private void CreateBusinesses()
        {
            var externalService = new ExternalService();
            CandidateBusiness = new CandidateBusiness(externalService);
            ResultBusiness = new ResultBusiness(externalService);
            VoteBusiness = new VoteBusiness();
        }
    }
}