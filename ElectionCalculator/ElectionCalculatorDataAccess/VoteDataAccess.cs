using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectionCalculatorDataAccess
{
    public class VoteDataAccess
    {
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