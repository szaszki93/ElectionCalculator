using ElectionCalculatorService.Entity;

namespace ElectionCalculatorService
{
    public class ResultService
    {
        private readonly CandidateService _candidateService;
        private readonly ExternalService _externalService;
        private readonly VoteService _voteService;

        public ResultService(ExternalService externalService, VoteService voteService, CandidateService candidateService)
        {
            _voteService = voteService;
            _externalService = externalService;
            _candidateService = candidateService;
        }

        public Result GetResult()
        {
            var votes = _voteService.GetVotes();
            var candidates = _candidateService.GetCandidates();
            var peselsWithoutRight = _externalService.GetPeselsWithoutRight();

            var result = new Result(candidates, votes, peselsWithoutRight);

            return result;
        }
    }
}