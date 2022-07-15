using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.Core.Models
{
    public class CommercialTransaction : BaseEntity
    {
        public string Job { get; set; }

        public double Price { get; set; }

        public DateTime JobDate { get; set; }

        public int CustomerId { get; set; }

        public Customer Customer { get; set; }

    }
}
