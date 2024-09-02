using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSphere.Models
{
    public class PhoneNationality
    {
        public int MaxDigits { get; set; }
        public int Prefix { get; set; }
        public string Uri { get; set; }
        public PhoneNationality(int maxDigits, int prefix, string uri)
        {
            MaxDigits = maxDigits;
            Prefix = prefix;
            Uri = uri;
        }
    }
}
