using NLayer.Data.Interfaces;
using NLayer.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Data.Repositories
{
    public class ProductFeatureRepo : GenericRepo<ProductFeature> , IProductFeatureRepo
    {
        public ProductFeatureRepo(AppDbContext context) : base(context)
        {
        }
    }
}
