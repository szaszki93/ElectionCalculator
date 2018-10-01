using ElectionCalculatorDataAccess;
using System.Collections.Generic;
using System.Linq;

namespace ElectionCalculatorService.Entity
{
    public class Result
    {
        public Result(List<Candidate> candidates, List<Vote> votes, List<Person> peselsWithoutVoteRight)
        {
            List<int> peselsWithoutRightHashCodes = peselsWithoutVoteRight.Select(x => x.Pesel.GetHashCode()).ToList();
            int numberOfAllVotes = votes.Count;

            var votesOfPeselWithRight = votes
                .Where(x => !peselsWithoutRightHashCodes.Contains(x.PeselHashCode))
                .ToList();

            NumberOfVotesWithoutRight = numberOfAllVotes - votesOfPeselWithRight.Count;
            NumberOfInvalidVotes = votesOfPeselWithRight.Count(x => x.CandidateIndex == null);
            NumberOfValidVotes = votesOfPeselWithRight.Count - NumberOfInvalidVotes;

            Results = candidates.Select(x => new CandidateResult()
            {
                Candidate = x,
                NumberOfVotes = votesOfPeselWithRight.Count(y => y.CandidateIndex == candidates.IndexOf(x))
            }).ToList();

            var parties = candidates.Select(x => x.Party).Distinct().ToList();

            PartiesResults = parties.Select(x => new PartiesResults()
            {
                Party = x,
                NumberOfVotes = Results.Where(y => y.Candidate.Party == x).Sum(z => z.NumberOfVotes)
            }).ToList();
        }

        public int NumberOfInvalidVotes { get; private set; }
        public int NumberOfValidVotes { get; private set; }
        public int NumberOfVotesWithoutRight { get; private set; }
        public List<PartiesResults> PartiesResults { get; private set; }
        public List<CandidateResult> Results { get; private set; }
    }
}