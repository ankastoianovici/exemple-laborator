using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using System.Linq;
using static LanguageExt.Prelude;
using LanguageExt;

namespace L4.Domain
{
    public record CodProdus
    {
        public decimal Value { get; }

        public CodProdus(decimal value)
        {
            Value = value;

        }
        public static Option<CodProdus> TryParse(string codStr)
        {
            if(decimal.TryParse(codStr, out decimal numericCod) && IsValid(numericCod))
            {
                return Some<CodProdus>(new(numericCod));
            }
            else
            {
                return None;
            }
        }
        private static bool IsValid(decimal numericGrade) => numericGrade > 0 && numericGrade <= 10;


        public override string ToString()
        {
            return $"{Value:0.##}";
        }
    }
}
