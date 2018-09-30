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

        public ResultBusiness(ExternalService externalService)
        {
            _externalService = externalService;
        }
    }
}