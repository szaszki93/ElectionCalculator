using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.CSharp.RuntimeBinder;
using System.Web.Helpers;
using System.Text;
using System.Threading.Tasks;
using ElectionCalculatorService.Models;
using Newtonsoft.Json;

namespace ElectionCalculatorService
{
    public class ExternalService
    {
        private const string CANDIDATES_WEB_PATH = "http://webtask.future-processing.com:8069/candidates";

        public List<Candidate> GetCandidates()
        {
            WebClient client = new WebClient();
            string downloadedString = client.DownloadString(CANDIDATES_WEB_PATH);

            var dyn = JsonConvert.DeserializeObject<RootObject>(downloadedString);

            Candidates candidates = dyn.Candidates;

            return candidates.Candidate;
        }
    }
}