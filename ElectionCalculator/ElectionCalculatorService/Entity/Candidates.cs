﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectionCalculatorService.Entity
{
    public class Candidates
    {
        public List<Candidate> Candidate { get; set; }
        public string PublicationDate { get; set; }
    }
}