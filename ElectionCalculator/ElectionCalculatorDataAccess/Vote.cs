using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectionCalculatorDataAccess
{
    public class Vote
    {
        public int? CandidateIndex { get; set; }

        [Key]
        public int Id { get; set; }

        [Index]
        public int PeselHashCode { get; set; }
    }
}