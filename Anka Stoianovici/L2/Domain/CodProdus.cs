using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using System.Linq;

namespace L2.Domain
{
    public record CodProdus
    {
        public int Value { get; }

        public CodProdus(int value)
        {
            Value = value;

        }


        public override string ToString()
        {
            return $"{Value:0.##}";
        }
    }
}
