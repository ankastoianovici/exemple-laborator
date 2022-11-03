using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using System.Linq;
using static L4.Domain.StareCos;

namespace L4.Domain
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