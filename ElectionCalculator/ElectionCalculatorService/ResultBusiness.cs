using ElectionCalculatorService.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectionCalculatorService
{
    public class ResultBusiness
    {
        private readonly CandidateBusiness _candidateBusiness;
        private readonly ExternalService _externalService;
        private readonly VoteBusiness _voteBusiness;

        public ResultBusiness(ExternalService externalService, VoteBusiness voteBusiness, CandidateBusiness candidateBusiness)
        {
            _voteBusiness = voteBusiness;
            _externalService = externalService;
            _candidateBusiness = candidateBusiness;
        }

        public Result GetResult()
        {
            var votes = _voteBusiness.GetVotes();
            var candidates = _candidateBusiness.GetCandidates();
            var peselsWithoutRight = _externalService.GetPeselsWithoutRight();

            var result = new Result(candidates, votes, peselsWithoutRight);

            return result;
        }
    }
}