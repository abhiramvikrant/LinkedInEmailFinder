using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace  LinkedEmailFinder.DataAccess.Repository
{
    public interface IRepository<T> where T: class
    {
        IEnumerable<T> GetAll();
        T GetByID(int id);
        int Create(T t);

        int Update(T t);
       

        int Delete(T t);

    }
}
