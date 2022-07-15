using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.Core.DTOs
{
    public class CommercialTransactionDto 
    {
        public string Job { get; set; }

        public double Price { get; set; }

        public DateTime JobDate { get; set; }

        public int CustomerId { get; set; }
    }
}
