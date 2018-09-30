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
        public void SaveVote(string pesel, int? candidateIndex)
        {
            var vote = new Vote()
            {
                PeselHashCode = pesel.GetHashCode(),
                CandidateIndex = candidateIndex
            };
        }
    }
}