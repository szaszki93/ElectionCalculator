using System.Collections.Generic;
using System.Net;
using System.Text;
using ElectionCalculatorService.Entity;
using Newtonsoft.Json;

namespace ElectionCalculatorService
{
    public class ExternalService
    {
        private const string BLOCKED_WEB_PATH = "http://webtask.future-processing.com:8069/blocked";
        private const string CANDIDATES_WEB_PATH = "http://webtask.future-processing.com:8069/candidates";

        public List<Candidate> GetCandidates()
        {
            WebClient client = new WebClient
            {
                Encoding = Encoding.UTF8
            };

            string downloadedString = client.DownloadString(CANDIDATES_WEB_PATH);
            var rootObject = JsonConvert.DeserializeObject<RootObjectCandidates>(downloadedString);

            Candidates candidates = rootObject.Candidates;
            return candidates.Candidate;
        }

        public List<Person> GetPeselsWithoutRight()
        {
            WebClient client = new WebClient
            {
                Encoding = Encoding.UTF8
            };

            string downloadedString = client.DownloadString(BLOCKED_WEB_PATH);
            var rootObject = JsonConvert.DeserializeObject<RootObjectBlocked>(downloadedString);

            var result = rootObject.Disallowed.Person;
            return result;
        }
    }
}