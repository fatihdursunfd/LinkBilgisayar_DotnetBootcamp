using NLayer.Data.Models;
using NLayer.Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Service.Interfaces
{
    public interface IProductFeatureService
    {
        ProductFeature GetById(int id);

        List<ProductFeature> GetAll();

        void Add(ProductFeatureAddDto pf);

        void Remove(int id);
    }
}
