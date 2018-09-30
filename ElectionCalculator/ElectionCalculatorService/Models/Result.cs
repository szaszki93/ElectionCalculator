using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectionCalculatorService.Models
{
    public class Result
    {
        public Candidate Candidate { get; set; }

        public int NumberOfVotes { get; set; }
    }
}