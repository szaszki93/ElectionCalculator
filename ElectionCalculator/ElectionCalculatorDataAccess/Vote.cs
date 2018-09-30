using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectionCalculatorDataAccess
{
    public class Vote
    {
        public int? CandidateIndex { get; set; }

        [Index]
        public int PeselHashCode { get; set; }
    }
}