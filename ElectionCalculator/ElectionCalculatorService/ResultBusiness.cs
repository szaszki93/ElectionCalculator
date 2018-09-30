using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectionCalculatorService
{
    public class ResultBusiness
    {
        private readonly ExternalService _externalService;
        private readonly VoteBusiness _voteBusiness;

        public ResultBusiness(ExternalService externalService, VoteBusiness voteBusiness)
        {
            _voteBusiness = voteBusiness;
            _externalService = externalService;
        }
    }
}