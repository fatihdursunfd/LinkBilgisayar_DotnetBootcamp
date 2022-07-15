using Assessment.Core.Repositories;
using Assessment.Core.Services;
using Assessment.Core.UnitOfWorks;
using Assessment.Service.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.Service.Services
{
    public class Service<T> : IService<T> where T : class
    {
        private readonly IGenericRepo<T> genericRepo;
        private readonly IUnitOfWork unitOfWork;

        public Service(IUnitOfWork unitOfWork, IGenericRepo<T> genericRepo)
        {
            this.unitOfWork = unitOfWork;
            this.genericRepo = genericRepo;
        }

        public async Task<T> AddAsync(T entity)
        {
            await genericRepo.AddAsync(entity);
            await unitOfWork.CommitAsync();
            return entity;
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            await genericRepo.AddRangeAsync(entities);
            await unitOfWork.CommitAsync();
            return entities;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await genericRepo.GetAll().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var response =  await genericRepo.GetByIdAsync(id);
            if (response is null)
                throw new NotFoundException($"{typeof(T).Name}({id}) not found");

            return response;    
        }

        public async Task RemoveAsync(T entity)
        {
            genericRepo.Remove(entity);
            await unitOfWork.CommitAsync();
        }

        public async Task RemoveAllAsync(IEnumerable<T> entities)
        {
            genericRepo.RemoveAll(entities);
            await unitOfWork.CommitAsync();
        }

        public async Task SaveChangesAsync()
        {
            await unitOfWork.CommitAsync();
        }

        public async Task<T> UpdateAsync(T entity)
        {
            genericRepo.Update(entity);
            await unitOfWork.CommitAsync();
            return entity;
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return genericRepo.Where(expression);
        }
    }
}
