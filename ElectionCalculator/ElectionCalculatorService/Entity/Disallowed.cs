using System.Collections.Generic;

namespace ElectionCalculatorService.Entity
{
    public class Disallowed
    {
        public List<Person> Person { get; set; }
        public string PublicationDate { get; set; }
    }
}