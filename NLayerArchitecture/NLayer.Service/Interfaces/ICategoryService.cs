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
    public interface ICategoryService
    {
        void Add(CategoryAddDto category);

        Response<CategoryGetDto> GetById(int id);

        Response<List<CategoryGetDto>> GetAll();

        void Remove(int id);

        List<CategoryCount> GetCategoryCounts();
    }
}
