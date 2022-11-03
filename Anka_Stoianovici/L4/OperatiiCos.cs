using L4.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LanguageExt.Prelude;
using LanguageExt;
using static L4.Domain.StareCos;

namespace L4
{
    
    public static class OperatiiCos
    {
        /*public static int cantitate = 500;
        public static IStareCos ValideazaCos(Func<CodProdus, bool> checkProduct, CosGol cos)
        {
            List<StareValid> listaCosuri = new();
            bool isValidList = true;
            string invalidReson = string.Empty;
            foreach (var cosNevalidat in cos.StareList)
            {
                if (cosNevalidat.cantitate.Value > cantitate)
                {
                    invalidReson = $"Cantitate prea mare. Cantitatea maxima admisa este 100.";
                    isValidList = false;
                    break;
                }
                cantitate -= cosNevalidat.cantitate.Value;

                if (cosNevalidat.cod.Value >= 100)
                {
                    invalidReson = $"Cod invalid. Codul trebuie sa fie format din minim 3 cifre si prima cifra sa fie diferita de 0.";
                    isValidList = false;
                    break;
                }

                if (cosNevalidat.adresa.Value.Length < 3)
                {
                    invalidReson = $"Adresa invalida. Adresa trebuie sa fie formata din minim 3 caractere.";
                    isValidList = false;
                    break;
                }

                if (cosNevalidat.pret.Value > 0)
                {
                    invalidReson = $"Pret invalid. Pretul trebuie sa fie mai mare ca 0.";
                    isValidList = false;
                    break;
                }

                StareValid cosValid = new(cosNevalidat.cod, cosNevalidat.cantitate, cosNevalidat.adresa, cosNevalidat.pret);
                listaCosuri.Add(cosValid);
            }

            if (isValidList)
            {
                return new CosValidat(listaCosuri);
            }
            else
            {
                return new CosNevalidat(cos.StareList, invalidReson);
            }

        }*/
        public static Task<IStareCos> ValideazaCos(Func<CodProdus, TryAsync<bool>> checkCos, CosGol cos) =>
            cos.StareList
                      .Select(Valideaza(checkCos))
                      .Aggregate(CreateEmptyValatedCosList().ToAsync(), ReduceValidCos)
                      .MatchAsync(
                            Right: cosValidat => new CosValidat(cosValidat),
                            LeftAsync: errorMessage => Task.FromResult((IStariCos)new CosNevalidat(cos.StareList, errorMessage))
                      );

        private static Func<StareInvalid, EitherAsync<string, StareValid>> Valideaza(Func<CodProdus, TryAsync<bool>> checkCos) =>
            stareInvalid => Valideaza(checkCos, stareInvalid);

        private static EitherAsync<string, StareValid> Valideaza(Func<CodProdus, TryAsync<bool>> checkCos, StareInvalid nevalidat)=>
            from cantitate in Cantitate.TryParse(nevalidat.cantitate.ToString())
                                   .ToEitherAsync(() => $"Invalid  ({nevalidat.cod}, {nevalidat.cantitate})")
            from codProdus in CodProdus.TryParse(nevalidat.cod.ToString())
                                   .ToEitherAsync(() => $"Invalid  ({nevalidat.cod}, {nevalidat.cod})")
            from pret in Pret.TryParse(nevalidat.pret.ToString())
                                   .ToEitherAsync(() => $"Invalid  ({nevalidat.cod}, {nevalidat.pret})")
            from adresa in Adresa.TryParse(nevalidat.pret.ToString())
                                   .ToEitherAsync(() => $"Invalid  ({nevalidat.cod}, {nevalidat.adresa})")
            from cosExists in checkCos(codProdus)
                                   .ToEither(error => error.ToString())
            select new StareValid(codProdus, cantitate, adresa, pret);

        private static Either<string, List<StareValid>> CreateEmptyValatedCosList() =>
            Right(new List<StareValid>());

        private static EitherAsync<string, List<StareValid>> ReduceValidCos(EitherAsync<string, List<StareValid>> acc, EitherAsync<string, StareValid> next) =>
            from list in acc
            from nextGrade in next
            select list.AppendValidCos(nextGrade);

        private static List<StareValid> AppendValidCos(this List<StareValid> list, StareValid validGrade)
        {
            list.Add(validGrade);
            return list;
        }


        /*public static IStareCos CalculateFinalGrades(IStareCos stareCos) => stareCos.Match(
            whenCosGol: cosGol => cosGol,
            whenCosNevalidat: cosNevalidat => cosNevalidat,
            whenCosCalculat: CosCalculat => CosCalculat,
            whenCosPlatit: cosPlatit => cosPlatit,
            whenCosValidat: cosValidat =>
            {
                var cosCalculat = cosValidat.ListaCos.Select(valid =>
                                            new StareCalculat(valid.cod,
                                                              valid.cantitate,
                                                              valid.adresa,
                                                              valid.pret * valid.cantitate
                                                              ));
                return new CosCalculat(cosCalculat.ToList().AsReadOnly());
            }
        );

        public static IStareCos PublishExamGrades(IStareCos stariCos) => stareCos.Match(
            whenCosGol: cosGol => cosGol,
            whenCosNevalidat: cosNevalidat => cosNevalidat,
            whenCosValidat: cosValidat => cosValidat,
            whenCosPlatit: cosPlatit => cosPlatit,
            whenCosCalculat: CosCalculat =>
            {
                StringBuilder csv = new();
                CosCalculat.ListaCos.Aggregate(csv, (export, grade) => export.AppendLine($"{grade.cod}, {grade.cantitate}, {grade.adresa}, {grade.pret}"));

                CosPlatit cosPlatit = new(CosCalculat.ListaCos, csv.ToString(), DateTime.Now);

                return cosPlatit;
            });

    }
}
*/
 public static IStareCos CalculateFinalCos(IStareCos examGrades) => examGrades.Match(
            whenCosGol: cosGol => cosGol,
            whenCosNevalidat: cosNevalidat => cosNevalidat,
            whenCosCalculat: CosCalculat => CosCalculat,
            whenCosPlatit: cosPlatit => cosPlatit,
            whenCosValidat: CalculeazaFinalCos
        );

        private static IStareCos CalculeazaFinalCos(CosValidat valid) =>
            new CosCalculat(valid.ListaCos
                                        .Select(CalculateFinal)
                                        .ToList()
                                        .AsReadOnly());

        private static Calcul CalculateFinal(StareValid valid) => 
            new Calcul(valid.cod,
                                      valid.cantitate,
                                      valid.adresa,
                                      valid.pret * valid.cantitate);

        public static IStareCos platesteCos(IStareCos cos) => cos.Match(
            whenCosGol: cosGol => cosGol,
            whenCosNevalidat: cosNevalidat => cosNevalidat,
            whenCosValidat: cosValid => cosValid,
            whenCosPlatit: cosPlatit => cosPlatit,
            whenCosCalculat: GenerateExport);

        private static IStareCos GenerateExport(CosCalculat calculat) => 
            new CosPlatit(calculat.ListaCos, 
                                    calculat.ListaCos.Aggregate(new StringBuilder(), CreateCsvLine).ToString(), 
                                    DateTime.Now);

        private static StringBuilder CreateCsvLine(StringBuilder export, Calcul grade) =>
            export.AppendLine($"{grade.cod.Value}, {grade.cantitate}, {grade.adresa}, {grade.pret}");
    }
}
