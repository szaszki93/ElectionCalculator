using ElectionCalculatorService.Entity;
using System.Collections.Generic;
using System.Linq;

namespace ElectionCalculatorService
{
    public class CandidateService
    {
        private readonly ExternalService _externalService;
        private List<Candidate> _candidates;

        public CandidateService(ExternalService externalService)
        {
            _externalService = externalService;
        }

        public List<Candidate> GetCandidates()
        {
            if (_candidates == null)
            {
                _candidates = _externalService.GetCandidates().OrderBy(x => x.Name).ToList();
            }

            return _candidates;
        }
    }
}