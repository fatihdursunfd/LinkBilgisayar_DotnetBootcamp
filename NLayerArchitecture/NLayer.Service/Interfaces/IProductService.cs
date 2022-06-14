using NLayer.Data.Models;
using NLayer.Service.Dto;
using NLayer.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Service.Interfaces
{
    public interface IProductService
    {
        void Add(ProductAddDto product);

        void Remove(int id);

        Response<ProductGetDto> GetById(int id);

        Response<List<ProductGetDto>> GetAll();

        Response<List<ProductFullModel>> GetFull(int categoryId);

        List<ProductFullModel> GetProductWithFeature();

    }
}
