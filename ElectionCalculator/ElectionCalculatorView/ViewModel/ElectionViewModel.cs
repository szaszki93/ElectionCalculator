using ElectionCalculatorView.Base;
using ElectionCalculatorView.Model;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace ElectionCalculatorView.ViewModel
{
    public class ElectionViewModel : LogoutViewModel
    {
        private readonly string _pesel;

        public ElectionViewModel(MainWindowViewModel mainViewModel, string pesel) : base(mainViewModel)
        {
            VoteCmd = new RelayCommand(x => Vote());
            _pesel = pesel;
            LoadCandidates();
        }

        public List<CandidateModel> Candidates { get; set; }

        public ICommand VoteCmd { get; set; }

        private void LoadCandidates()
        {
            var candidates = mainViewModel.CandidateBusiness.GetCandidates();
            var models = candidates.Select(x => new CandidateModel() { Name = x.Name, Party = x.Party }).ToList();
            Candidates = models;
        }

        private void Vote()
        {
            int numberOfVotes = Candidates.Count(x => x.IsChecked);

            int? candidateIndex = numberOfVotes == 1
                    ? Candidates.IndexOf(Candidates.Single(x => x.IsChecked))
                    : (int?)null;

            mainViewModel.VoteBusiness.SaveVote(_pesel, candidateIndex);

            mainViewModel.OpenResultView();
        }
    }
}