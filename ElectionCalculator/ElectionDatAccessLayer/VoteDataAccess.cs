using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectionCalculatorDataAccess
{
    public class VoteDataAccess
    {
        public List<Vote> GetVotes()
        {
            using (var db = new ElectionContext())
            {
                var result = db.Votes.ToList();
                return result;
            }
        }

        public bool IsThatPeselJustVote(int peselHashCode)
        {
            using (var db = new ElectionContext())
            {
                bool result = db.Votes.Any(x => x.PeselHashCode == peselHashCode);
                return result;
            }
        }

        public void SaveVote(Vote vote)
        {
            using (var db = new ElectionContext())
            {
                db.Votes.Add(vote);
                db.SaveChanges();
            }
        }
    }
}