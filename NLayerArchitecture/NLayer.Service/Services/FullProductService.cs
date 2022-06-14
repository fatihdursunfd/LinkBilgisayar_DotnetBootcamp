using NLayer.Data;
using NLayer.Data.Interfaces;
using NLayer.Data.Models;
using NLayer.Service.Dto;
using NLayer.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Service.Services
{
    public class FullProductService : IFullProductService
    {
        private readonly IProductRepo productRepo;
        private readonly ICategoryRepo categoryRepo;
        private readonly IProductFeatureRepo productFeatureRepo;
        private readonly UnitOfWork unitOfWork;

        public FullProductService(IProductFeatureRepo productFeatureRepo, 
                                  ICategoryRepo categoryRepo, 
                                  IProductRepo productRepo, 
                                  UnitOfWork unitOfWork)
        {
            this.productFeatureRepo = productFeatureRepo;
            this.categoryRepo = categoryRepo;
            this.productRepo = productRepo;
            this.unitOfWork = unitOfWork;
        }

        public void Add(FullProductAddDto fullProduct)
        {
            Category category = new Category() { Name = fullProduct.CategoryName };

            Product product = new Product()
            {
                Name = fullProduct.Name,
                Price = fullProduct.Price,
                Stock = fullProduct.Stock,
                Category = category
            };

            ProductFeature pf = new ProductFeature() { Color = fullProduct.Color };

            using (var transaction = unitOfWork.BeginTransaction())
            {
                categoryRepo.Add(category);
                
                productRepo.Add(product);
                productRepo.Save();

                pf.Id = product.Id;
                productFeatureRepo.Add(pf);

                transaction.Commit();
            }
        }
    }
}
