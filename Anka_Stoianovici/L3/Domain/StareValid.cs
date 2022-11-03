using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using System.Linq;

namespace L3.Domain
{
    public record StareValid(CodProdus cod, Cantitate cantitate, Adresa adresa, Pret pret);
}
