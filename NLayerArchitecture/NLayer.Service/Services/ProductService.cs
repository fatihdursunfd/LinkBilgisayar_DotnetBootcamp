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
    public class ProductService : IProductService
    {
        private readonly IProductRepo _productRepo;
        private readonly ICategoryRepo _categoryRepo;

        public ProductService(IProductRepo productRepo, ICategoryRepo categoryRepo)
        {
            _productRepo = productRepo;
            _categoryRepo = categoryRepo;
        }

        public void Add(ProductAddDto product)
        {
            Product p = new Product();
            p.Price = product.Price;    
            p.Name = product.Name;
            p.Stock = product.Stock;
            p.CategoryId = product.CategoryId;

            _productRepo.Add(p);
        }

        public Response<List<ProductGetDto>> GetAll()
        {
            var products =  _productRepo.GetAll();

            if (!products.Any())
            {
                return new Response<List<ProductGetDto>>()
                {
                    Data = null,
                    Errors = new List<string>() { "ürün mevcut değil" },
                    StatusCode = 404
                };
            }

            var model = products.Select(p => new ProductGetDto()
            {
                Name = p.Name,
                Price = p.Price,
                Stock = p.Stock,
                CategoryName = _categoryRepo.GetById(p.CategoryId).Name
            }).ToList();

            return new Response<List<ProductGetDto>>()
            {
                Data = model,
                Errors = null,
                StatusCode = 200
            };
        }

        public Response<ProductGetDto> GetById(int id)
        {
            var product =  _productRepo.GetById(id);

            if (product is null)
            {
                return new Response<ProductGetDto>()
                {
                    Data = null,
                    Errors = new List<string>() { "ürün mevcut değil" },
                    StatusCode = 404
                };
            }
            var model =  new ProductGetDto()
            {
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock,
                CategoryName = _categoryRepo.GetById(product.CategoryId).Name
            };

            return new Response<ProductGetDto>()
            {
                Data = model,
                Errors = null,
                StatusCode = 200
            };
        }

        public Response<List<ProductFullModel>> GetFull(int categoryId)
        {
            var productFull = _productRepo.GetProductFullModels(categoryId);
            if (productFull is null)
            {
                return new Response<List<ProductFullModel>>()
                {
                    Data = null,
                    Errors = new List<string>() { "ürün mevcut değil" },
                    StatusCode = 404
                };
            }
            return new Response<List<ProductFullModel>>()
            {
                Data = productFull,
                Errors = null,
                StatusCode = 200
            };
        }

        public List<ProductFullModel> GetProductWithFeature()
        {
            var productsWithFeature = _productRepo.GetProductWithFeature();
            return productsWithFeature;
        }

        public void Remove(int id)
        {
            var user = _productRepo.GetById(id);
            if (user != null)
                _productRepo.Remove(user);
        }
    }
}
