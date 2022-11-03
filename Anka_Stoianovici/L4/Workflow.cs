using L4.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using System.Linq;
using LanguageExt;
using static L4.Domain.PlatesteCos;
using static L4.Domain.StareCos;
using static L4.OperatiiCos;

namespace L4
{
    class Workflow
    {
        public async Task<IPlatesteCos> Execute(CommandPlateste command, Func<CodProdus, TryAsync<bool>> checkProduct)
        {
            CosGol cosGol = new CosGol(command.IntrareCos);
            IStareCos cos = await ValideazaCos(checkProduct, cosGol);
            cos = CalculateFinalCos(cos);
            cos = PlatesteCos(cos);

            return cos.Match(
                    whenCosGol: cosGol => new PlatesteCosFailed("Unexpected unvalidated state") as PlatesteCos,
                    whenCosNevalidat: cosNevalidat => new PlatesteCosFailed(cosNevalidat.Reason),
                    whenCosValidat: cosValidat => new PlatesteCosFailed("Unexpected validated state"),
                    whenCosCalculat: CosCalculat => new PlatesteCosFailed("Unexpected calculated state"),
                    whenCosPlatit: cosPlatit => new PlatesteCosSuccess(cosPlatit.Csv, cosPlatit.PublishedDate)
                );
        }
    }
}
