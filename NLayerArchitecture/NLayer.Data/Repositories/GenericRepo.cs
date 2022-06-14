using Microsoft.EntityFrameworkCore;
using NLayer.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Data.Repositories
{
    public class GenericRepo<T> : IGenericRepo<T> where T : class
    {
        private readonly DbSet<T> _dbSet;

        private readonly AppDbContext _context;

        public GenericRepo(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public void Remove(T obj)
        {
            _dbSet.Remove(obj);
            Save();
        }

        public List<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public void Add(T obj)
        {
            _dbSet.Add(obj);
            Save();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
