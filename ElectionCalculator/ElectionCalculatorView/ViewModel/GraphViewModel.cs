using ElectionCalculatorService.Entity;
using ElectionCalculatorView.Base;
using ElectionCalculatorView.Model;
using ElectionCalculatorView.Resource;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace ElectionCalculatorView.ViewModel
{
    public class GraphViewModel : LogoutViewModel
    {
        public GraphViewModel(MainWindowViewModel mainViewModel, Result data) : base(mainViewModel)
        {
            BackCmd = new RelayCommand(x => Back());
            Data = data;

            SetOverrallResults();
        }

        public ICommand BackCmd { get; set; }

        public Result Data { get; private set; }

        public List<OverrallResult> OverrallResults { get; private set; }

        private void Back()
        {
            mainViewModel.OpenResultView(Data);
        }

        private void SetOverrallResults()
        {
            OverrallResults = new List<OverrallResult>()
            {
                new OverrallResult(){ TypeVoteName=Language.NumberOfValidVotes, NumberOfVotes=Data.NumberOfValidVotes},
                new OverrallResult(){ TypeVoteName=Language.NumberOfInvalidVotes, NumberOfVotes=Data.NumberOfInvalidVotes},
                new OverrallResult(){ TypeVoteName=Language.NumberOfVotesWithoutRight, NumberOfVotes=Data.NumberOfVotesWithoutRight}
            };
        }
    }
}