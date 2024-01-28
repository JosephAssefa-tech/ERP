using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excellerent.EppConfiguration.Presentation.Resource.Dtos
{
    public class Iso
    {
        public string iso2 { get; set; }
        public string iso3 { get; set; }
    }

    public class Country : Iso
    {
        public string country { get; set; }
    }

    public class CountryAndCity : Iso
    {
        public string country { get; set; }
        public List<string> cities { get; set; }
    }

    public class State
    {
        public string name { get; set; }
        public string state_code { get; set; }
    }

    public class CountryAndState : Iso
    {
        public string name { get; set; }
        public List<State> states { get; set; }
    }

    public class CountryAndCode
    {
        public string name { get; set; }
        public string code { get; set; }
        public string dial_code { get; set; }
    }
}
