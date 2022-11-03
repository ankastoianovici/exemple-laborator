using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using System.Linq;
using static LanguageExt.Prelude;
using LanguageExt;


namespace L4.Domain
{
    public record Pret
    {
        public decimal Value { get; }

        public Pret(decimal value)
        {
            Value = value;

        }
        public static Option <Pret> TryParse(string pretStr)
        {
            if(decimal.TryParse(pretStr, out decimal numericPret) && IsValid(numericPret))
            {
                return Some<Pret>(new(numericPret));
            }
            else
            {
                return None;
            }
        }
        private static bool IsValid(decimal numericGrade) => numericGrade > 0 && numericGrade <= 10;

        public static Pret operator *(Pret a, Cantitate b) => new Pret((a.Value * b.Value));

        public override string ToString()
        {
            return $"{Value:0.##}";
        }
    }
}