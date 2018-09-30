using ElectionCalculatorDataAccess;
using ElectionCalculatorService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectionCalculatorService
{
    public class VoteBusiness
    {
        private readonly VoteDataAccess _dataAccess;

        public VoteBusiness()
        {
            _dataAccess = new VoteDataAccess();
        }

        public bool IsThatPeselJustVote(string pesel)
        {
            bool result = _dataAccess.IsThatPeselJustVote(pesel.GetHashCode());

            return result;
        }

        public void SaveVote(string pesel, int? candidateIndex)
        {
            if (IsThatPeselJustVote(pesel))
            {
                return;
            }

            var vote = new Vote()
            {
                PeselHashCode = pesel.GetHashCode(),
                CandidateIndex = candidateIndex
            };

            _dataAccess.SaveVote(vote);
        }
    }
}