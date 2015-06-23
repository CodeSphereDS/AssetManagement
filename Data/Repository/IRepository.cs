using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Model;

namespace Data.Repository
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetList(T obj);
        void Add(T obj);
        void Delete(T obj);
        void SaveOrUpdate(T obj);
        void Update(T obj);

    }

}
