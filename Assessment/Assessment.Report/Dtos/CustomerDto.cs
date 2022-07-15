using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.Reporting.Dtos
{
    public class CustomerDto
    {
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public int Count { get; set; }
        public double TotalPrice { get; set; }

    }
}
