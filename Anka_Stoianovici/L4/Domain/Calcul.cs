using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using System.Linq;

namespace L4.Domain
{
    public record Calcul(CodProdus cod, Cantitate cantitate, Adresa adresa, Pret pret);
}
