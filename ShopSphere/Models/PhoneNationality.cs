using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSphere.Models
{
    public class PhoneNationality : ReactiveObject
    {
        private int max_digits;
        private int prefix;
        private string uri;
        public int MaxDigits { get => this.max_digits; set => this.RaiseAndSetIfChanged(ref this.max_digits , value); }
        public int Prefix { get => this.prefix; set => this.RaiseAndSetIfChanged(ref this.prefix, value)    ; }
        public string Uri { get => this.uri; set => this.RaiseAndSetIfChanged(ref this.uri , value); }
        public PhoneNationality(int maxDigits, int _prefix, string _uri)
        {
            MaxDigits = maxDigits;
            Prefix = _prefix;
            Uri = _uri;
        }
    }
}
