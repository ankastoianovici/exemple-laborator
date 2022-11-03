using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using System.Linq;
using static L3.Domain.StareCos;

namespace L3.Domain
{
    public record Plateste
    {
        public Plateste(IReadOnlyCollection<StareInvalid> intrareCos)
        {
            IntrareCos=intrareCos;
        }
        public IReadOnlyCollection<StareInvalid> IntrareCos {get; }
    }
}