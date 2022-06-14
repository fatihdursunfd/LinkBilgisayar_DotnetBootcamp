using NLayer.Data.Interfaces;
using NLayer.Data.Models;
using NLayer.Service.Dto;
using NLayer.Service.Interfaces;
using NLayer.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Service.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepo _categoryRepo;

        public CategoryService(ICategoryRepo categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        public void Add(CategoryAddDto category)
        {
            var c = new Category() { Name = category.Name };
            _categoryRepo.Add(c);
        }

        public Response<CategoryGetDto> GetById(int id)
        {
            var category = _categoryRepo.GetById(id);
            if(category is null)
            {
                return new Response<CategoryGetDto>()
                {
                    Data = null,
                    Errors = new List<string>() { "ürün mevcut değil" },
                    StatusCode = 404
                };
            }

            var model =  new CategoryGetDto { Name = category.Name };

            return new Response<CategoryGetDto>()
            {
                Data = model,
                Errors = null,
                StatusCode = 200
            };
        }

        public Response<List<CategoryGetDto>> GetAll()
        {
            var categories = _categoryRepo.GetAll();
            if (!categories.Any())
            {
                return new Response<List<CategoryGetDto>>()
                {
                    Data = null,
                    Errors = new List<string>() { "ürün mevcut değil" },
                    StatusCode = 404
                };
            }
            var model = categories.Select(x => new CategoryGetDto() { Name = x.Name }).ToList();

            return new Response<List<CategoryGetDto>>()
            {
                Data = model,
                Errors = null,
                StatusCode = 200
            };
        }

        public void Remove(int id)
        {
            var category = _categoryRepo.GetById(id);
            if (category is not null)
                _categoryRepo.Remove(category);
        }

        public List<CategoryCount> GetCategoryCounts()
        {
            var categories = _categoryRepo.GetCategoryCounts();
            return categories;
        }
    }
}
