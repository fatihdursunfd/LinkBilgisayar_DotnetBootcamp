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
    public class ProductFeatureService : IProductFeatureService
    {
        private readonly IProductFeatureRepo productFeatureRepo;

        public ProductFeatureService(IProductFeatureRepo productFeatureRepo)
        {
            this.productFeatureRepo = productFeatureRepo;
        }

        public void Add(ProductFeatureAddDto pf)
        {
            ProductFeature pfa = new ProductFeature();
            pfa.Color = pf.Color;
            pfa.Id = pf.Id;
            productFeatureRepo.Add(pfa); 
        }

        public List<ProductFeature> GetAll()
        {
            var pfs = productFeatureRepo.GetAll();
            return pfs;
        }

        public ProductFeature GetById(int id)
        {
            var pf = productFeatureRepo.GetById(id);
            return pf;
        }

        public void Remove(int id)
        {
            var pf = productFeatureRepo.GetById(id);
            if(pf != null)
                productFeatureRepo.Remove(pf);
        }
    }
}
