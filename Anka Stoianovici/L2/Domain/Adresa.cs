using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
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