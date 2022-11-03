using CSharp.Choices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using System.Linq;

namespace L4.Domain
{
    [AsChoice]
      public static partial class StareCos
    {
        public interface IStareCos { }
        public record CosGol : IStareCos
        {
            public CosGol(IReadOnlyCollection<StareInvalid> listaCos)
            {
                StareList = listaCos;
            }

            public IReadOnlyCollection<StareInvalid> StareList { get; }
        }


        public record CosNevalidat : IStareCos
        {
            internal CosNevalidat(IReadOnlyCollection<StareInvalid> listaCos, string reason)
            {
                ListaCos = listaCos;
                Reason = reason;
            }

            public IReadOnlyCollection<StareInvalid> ListaCos { get; }
            public string Reason { get; }
        }


        public record CosValidat : IStareCos
        {
            internal CosValidat(IReadOnlyCollection<StareValid> listaCos)
            {
                ListaCos = listaCos;
            }

            public IReadOnlyCollection<StareValid> ListaCos { get; }
        }



        public record CosCalculat : IStareCos
        {
            internal CosCalculat(IReadOnlyCollection<Calcul> listaCos)
            {
                ListaCos = listaCos;
            }

            public IReadOnlyCollection<Calcul> ListaCos { get; }
        }


        public record CosPlatit : IStareCos
        {
            internal CosPlatit(IReadOnlyCollection<Calcul> listaCos, string csv, DateTime publishedDate)
            {
                ListaCos = listaCos;
                PublishedDate = publishedDate;
                Csv = csv;
            }

            public IReadOnlyCollection<Calcul> ListaCos { get; }
            public DateTime PublishedDate { get; }
            public string Csv { get; }
        }
    }
}
