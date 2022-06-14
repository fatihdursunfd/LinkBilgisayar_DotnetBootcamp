using Microsoft.EntityFrameworkCore;
using NLayer.Data.Interfaces;
using NLayer.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Data.Repositories
{
    public class ProductRepo : GenericRepo<Product>, IProductRepo
    {
        private readonly AppDbContext context;
        public ProductRepo(AppDbContext context) : base(context)
        {
            this.context = context;
        }

        public List<ProductFullModel> GetProductFullModels(int categoryId)
        {
            var productFull = context.ProductFullModels.FromSqlInterpolated($"exec sp_get_product_full_by_category_ıd {categoryId}").ToList();
            return productFull;
        }

        public List<ProductFullModel> GetProductWithFeature()
        {
            var productsWithFeature = context.ProductFullModels.ToList();
            return productsWithFeature;
        }
    }
}
    