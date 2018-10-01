using ElectionCalculatorDataAccess;
using ElectionCalculatorService.Entity;
using System.Collections.Generic;

namespace ElectionCalculatorService
{
    public class VoteService
    {
        private readonly VoteDataAccess _dataAccess;

        public VoteService()
        {
            _dataAccess = new VoteDataAccess();
        }

        public List<Vote> GetVotes()
        {
            return _dataAccess.GetVotes();
        }

        public bool IsPersonJustVote(Person person)
        {
            bool result = IsPersonJustVote(person.Pesel.GetHashCode());

            return result;
        }

        public void SaveVote(Person person, int? candidateIndex)
        {
            int peselHashCode = person.Pesel.GetHashCode();

            if (IsPersonJustVote(peselHashCode)) { return; }

            var vote = new Vote()
            {
                PeselHashCode = peselHashCode,
                CandidateIndex = candidateIndex
            };

            _dataAccess.SaveVote(vote);
        }

        private bool IsPersonJustVote(int peselHashCode)
        {
            bool result = _dataAccess.IsThatPeselJustVote(peselHashCode);

            return result;
        }
    }
}