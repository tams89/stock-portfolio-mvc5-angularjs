using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM.Models
{
    /// <summary>
    /// Tick Model
    /// </summary>
    public class Tick : EntityBase<Tick>
    {
        public Guid Id { get; set; }

        public DateTime Date { get; set; }

        public DateTime Time { get; set; }

        public decimal? Open { get; set; }

        public decimal? High { get; set; }

        public decimal? Low { get; set; }

        public decimal? Close { get; set; }

        public decimal? Volume { get; set; }
    }
}