using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using System.Linq;

namespace L2.Domain
{
    public record Adresa
    {
        public string Value { get; }
        public Adresa (string value)
        {
            Value=value;
        }
        public override string ToString()
        {
            return Value;
        }
    }
    
}
