using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.Core.Models
{
    public class Customer : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string City { get; set; }
        public string ImageName { get; set; }
        public List<CommercialTransaction> CommercialTransactions { get; set; } = new List<CommercialTransaction>();

    }
}
    