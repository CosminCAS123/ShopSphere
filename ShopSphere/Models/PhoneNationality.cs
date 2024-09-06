using ReactiveUI;
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
      public string Prefix { get; set; }
      public string Uri { get; set; }
        public PhoneNationality(int maxDigits, string _prefix, string _uri)
        {
            MaxDigits = maxDigits;
            Prefix = _prefix;
            Uri = _uri;
        }
    }
}
