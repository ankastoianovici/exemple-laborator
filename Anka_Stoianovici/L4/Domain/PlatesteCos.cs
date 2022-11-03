using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using System.Linq;

namespace L4.Domain
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

        public record PlatesteCosFailed : IPlatesteCos
        {
            public string Reason { get; }
            internal PlatesteCosFailed(string reason)
            {
                Reason = reason;
            }
        }
    }
}
