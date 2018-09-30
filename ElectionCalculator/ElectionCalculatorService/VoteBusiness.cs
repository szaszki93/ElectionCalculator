using ElectionCalculatorDataAccess;
using System.Collections.Generic;

namespace ElectionCalculatorService
{
    public class VoteBusiness
    {
        private readonly VoteDataAccess _dataAccess;

        public VoteBusiness()
        {
            _dataAccess = new VoteDataAccess();
        }

        public List<Vote> GetVotes()
        {
            return _dataAccess.GetVotes();
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