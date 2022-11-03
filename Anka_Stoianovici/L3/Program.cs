
/*Implementați workflow-ul pentru plasarea unei comenzi și realizați o aplicații consolă care să apeleze workflow-ul creat. Workflow-ul trebuie să conțină următoarele operații:

validarea datelor de intrare si conversia la tipurile de date corecte
verificarea existenței produsului pe baza codului de produs
verificarea stocului
verificarea adresei de livrare
calcularea prețului Workflow-ul se va finaliza cu generarea unui eveniment.*/

using L3.Domain;
using System;
using System.Collections.Generic;
using static L3.Domain.StareCos;

namespace L3
{
    class Program
    {
        private static readonly Random random = new Random();

        static void Main(string[] args)
        {
            var listaCosuri = citesteLista().ToArray();
            /*CosGol cosNevalidat = new(listaCosuri);
            IStareCos result = ValideazaCos(cosNevalidat);*/
            CommandPlateste command = new(listaCosuri);
            Workflow workflow = new Workflow();
            var result = workflow.Execute(command, (registrationNumber) => true);
            result.Match(
                /*whenCosGol: unvalidatedResult => cosNevalidat,
                whenCosPlatit: publishedResult => publishedResult,
                whenCosNevalidat: invalidResult => invalidResult,
                whenCosValidat: validatedResult => PublishExamGrades(validatedResult)*/
                whenPlatesteCosFaild: @event =>
                    {
                        Console.WriteLine($"Publish failed: {@event.Reason}");
                        return @event;
                    },
                    whenPlatesteCosSuccess: @event =>
                    {
                        Console.WriteLine($"Publish succeeded.");
                        Console.WriteLine(@event.Csv);
                        return @event;
                    }
            );

            Console.WriteLine("Hello World!");
        }

        private static List<StareInvalid> citesteLista()
        {
            List <StareInvalid> listaCosuri = new();
            do
            {
                var cod = ReadValue("Cod Produs: ");
                if (string.IsNullOrEmpty(cod))
                {
                    break;
                }

                var cantitate = ReadValue("Cantitate: ");
                if (string.IsNullOrEmpty(cantitate))
                {
                    break;
                }

                var adresa = ReadValue("Adresa: ");
                if (string.IsNullOrEmpty(adresa))
                {
                    break;
                }

                var pret = ReadValue("Pret: ");
                if (string.IsNullOrEmpty(pret))
                {
                    break;
                }

                CodProdus obCod = new CodProdus(Int32.Parse(cod));
                Cantitate obCantitate = new Cantitate(Int32.Parse(cantitate));
                Adresa obAdresa = new Adresa(adresa);
                Pret obPret = new Pret(Int32.Parse(pret));
                listaCosuri.Add(new (obCod, obCantitate, obAdresa,obPret));
            } while (true);
            return listaCosuri;
        }

        /*private static IStareCos ValideazaCos(CosGol cosInvalid) =>
            random.Next(100) > 50 ?
            new CosNevalidat(new List<StareInvalid>(), "Random errror")
            : new CosValidat(new List<StareValid>());

        private static IStareCos PublishExamGrades(CosValidat cosValidat) =>
            new CosPlatit(new List<StareValid>(), DateTime.Now);*/

        private static string? ReadValue(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine();
        }
    }
}
