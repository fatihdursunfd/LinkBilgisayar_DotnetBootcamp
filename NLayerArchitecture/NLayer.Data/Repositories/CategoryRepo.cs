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
    public class CategoryRepo : GenericRepo<Category>, ICategoryRepo
    {
        private readonly AppDbContext context;
        public CategoryRepo(AppDbContext context) : base(context)
        {
            this.context = context;
        }

        public List<CategoryCount> GetCategoryCounts()
        {
            return context.CategoryCounts.FromSqlInterpolated($"select * from get_product_count()").ToList();
        }
    }
}
