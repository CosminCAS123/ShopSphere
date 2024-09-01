using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSphere.Models
{
    public class PhoneNationality
    {
        public int MaxDigits;
        public int Prefix;
        public string Uri;
        public PhoneNationality(int maxDigits, int prefix, string uri)
        {
            MaxDigits = maxDigits;
            Prefix = prefix;
            Uri = uri;
        }
    }
}
