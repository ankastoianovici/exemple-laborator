using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using System.Linq;

namespace L3.Domain
{
    public record Pret
    {
        public int Value { get; }

        public Pret(int value)
        {
            Value = value;

        }

        public static Pret operator *(Pret a, Cantitate b) => new Pret((a.Value * b.Value));

        public override string ToString()
        {
            return $"{Value:0.##}";
        }
    }
}