using ElectionCalculatorService.Entity;
using ElectionCalculatorView.Base;
using ElectionCalculatorView.Model;
using ElectionCalculatorView.Resource;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace ElectionCalculatorView.ViewModel
{
    public class ElectionViewModel : LogoutViewModel
    {
        private readonly Person _person;

        public ElectionViewModel(MainWindowViewModel mainViewModel, Person person) : base(mainViewModel)
        {
            VoteCmd = new RelayCommand(x => Vote());
            _person = person;
            LoadCandidates();
        }

        public List<CandidateModel> Candidates { get; set; }

        public ICommand VoteCmd { get; set; }

        private void LoadCandidates()
        {
            var candidates = mainViewModel.CandidateService.GetCandidates();
            var models = candidates.Select(x => new CandidateModel() { Name = x.Name, Party = x.Party }).ToList();
            Candidates = models;
        }

        private void Vote()
        {
            var answer = MessageBox.Show(
                   Language.AreYouVote,
                   Language.Question,
                   MessageBoxButton.YesNo,
                   MessageBoxImage.Question);

            if (answer == MessageBoxResult.No) { return; }

            int numberOfVotes = Candidates.Count(x => x.IsChecked);

            int? candidateIndex = numberOfVotes == 1
                    ? Candidates.IndexOf(Candidates.Single(x => x.IsChecked))
                    : (int?)null;

            mainViewModel.VoteService.SaveVote(_person, candidateIndex);

            mainViewModel.OpenResultView();
        }
    }
}