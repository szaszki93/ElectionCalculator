﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectionCalculatorService.Models
{
    public class Vote
    {
        public int? CandidateIndex { get; set; }
        public int PeselHashCode { get; set; }
    }
}