using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using System.Linq;
using static LanguageExt.Prelude;
using LanguageExt;

namespace L4.Domain
{
    public record Adresa
    {
        public string Value { get; }
        public Adresa (string value)
        {
            Value=value;
        }
         public static Option<Adresa> TryParse(string pretStr)
        {
            return Some<Adresa>(new(pretStr));
        }
        public override string ToString()
        {
            return Value;
        }
    }
    
}
