using LinkedEmailFinder.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LinkedEmailFinder.DataAccess.Repository
{
    public class GenericRepository<T> : IRepository<T> where T: class
    {
        private readonly LinkedInEmailFinder_DBContext _context;
        private readonly  DbSet<T> entities;
 

        public GenericRepository(LinkedInEmailFinder_DBContext context)
        {
            _context = context;
            if(_context != null)
            entities = _context.Set<T>();
        }
        public int Create(T t)
        {
            _context.Add(t);
          int result =   _context.SaveChanges();
            return result;

        }

        public int Delete(T t)
        {
            _context.Remove(t);
         var ret=   _context.SaveChanges();
            return ret;
        }

        public int Update(T t)
        {
            _context.Update(t);
           int ret =  _context.SaveChanges();
            return ret;
        }

        public IEnumerable<T> GetAll()
        {
            return entities.ToList();
        }

        public T GetByID(int id)
        {
            var ret = entities.Find(id);
            return ret;
        }
    }
}
