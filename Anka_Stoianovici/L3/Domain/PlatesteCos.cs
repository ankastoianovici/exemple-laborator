using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using System.Linq;

namespace L3.Domain
{

    [AsChoice]
    public static partial class PlatesteCos
    {
        public interface IPlatesteCos { }

        public record PlatesteCosSuccess : IPlatesteCos
        {
            public string Csv { get; }
            public DateTime PublishedDate { get; }

            internal PlatesteCosSuccess (string csv, DateTime publishedDate)
            {
                Csv = csv;
                PublishedDate = publishedDate;
            }
        }

        public record PlatesteCosFaild : IPlatesteCos
        {
            public string Reason { get; }
            internal PlatesteCosFaild(string reason)
            {
                Reason = reason;
            }
        }
    }
}
