using L3.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using System.Linq;
using static L3.Domain.PlatesteCos;
using static L3.Domain.StareCos;
using static L3.OperatiiCos;

namespace L3
{
    class Workflow
    {
        public IPlatesteCos Execute(Plateste command, Func<CodProdus, bool> checkProduct)
        {
            CosGol cosGol = new CosGol(command.IntrareCos);
            IStareCos cos = ValideazaCos(checkProduct, cosGol);
            cos = CalculateFinalGrades(cos);
            cos = PublishExamGrades(cos);

            return cos.Match(
                    whenCosGol: cosGol => new PlatesteCosFaild("Unexpected unvalidated state") as PlatesteCos,
                    whenCosNevalidat: cosNevalidat => new PlatesteCosFaild(cosNevalidat.Reason),
                    whenCosValidat: cosValidat => new PlatesteCosFaild("Unexpected validated state"),
                    whenCosCalculat: CosCalculat => new PlatesteCosFaild("Unexpected calculated state"),
                    whenCosPlatit: cosPlatit => new PlatesteCosSuccess(cosPlatit.Csv, cosPlatit.PublishedDate)
                );
        }
    }
}
