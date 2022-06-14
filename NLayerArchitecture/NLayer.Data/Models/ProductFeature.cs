using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Data.Models
{
    public class ProductFeature
    {
        [Key]
        [ForeignKey("Product")]
        public int Id { get; set; }

        public string Color { get; set; }

        public Product Product { get; set; }
    }
}
