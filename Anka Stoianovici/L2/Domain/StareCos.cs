using CSharp.Choices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using System.Linq;

namespace L2.Domain
{
    [AsChoice]
    public static partial class StariCos
    {
        public interface IStariCos { }

        public record CosGol(IReadOnlyCollection<StareInvalid> ListaCos) : IStariCos;

        public record CosNevalidat(IReadOnlyCollection<StareInvalid> ListaCos, string reason) : IStariCos;

        public record CosValidat(IReadOnlyCollection<StareValid> ListaCos) : IStariCos;

        public record CosPlatit(IReadOnlyCollection<StareValid> ListaCos, DateTime PublishedDate) : IStariCos;
    }
}
