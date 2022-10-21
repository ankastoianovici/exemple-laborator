using CSharp.Choices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using System.Linq;

namespace L2.Domain
{
    [AsChoice]
    public static partial class StareCos
    {
        public interface IStareCos { }

        public record CosGol(IReadOnlyCollection<StareInvalid> ListaCos) : IStareCos;

        public record CosNevalidat(IReadOnlyCollection<StareInvalid> ListaCos, string reason) : IStareCos;

        public record CosValidat(IReadOnlyCollection<StareValid> ListaCos) : IStareCos;

        public record CosPlatit(IReadOnlyCollection<StareValid> ListaCos, DateTime PublishedDate) : IStareCos;
    }
}
