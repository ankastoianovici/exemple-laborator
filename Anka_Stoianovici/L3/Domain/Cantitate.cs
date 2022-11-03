using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using System.Linq;

namespace L3.Domain
{
    public record Cantitate
    {
        public int Value { get; }

        public Cantitate(int value)
        {
            Value = value; 
        }
        public override string ToString()
        {
            return $"{Value:0.##}";
        }
    }
}
