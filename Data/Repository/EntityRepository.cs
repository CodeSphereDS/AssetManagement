using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Model;
using Data.Session;
using NHibernate;

namespace Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private ISession _session;
        public Repository(ISession session) { _session = session; }

        public void Add(T obj)
        {          
                _session.Save(obj);                    
        }

        public void Delete(T obj)
        {
            _session.BeginTransaction();
            _session.Delete(obj);
            _session.Transaction.Commit();

        }

        public void SaveOrUpdate(T obj)
        {
            _session.SaveOrUpdate(obj);
        }

        public void Update(T obj)
        {
            _session.Update(obj);
        }
        
        public T GetID(int userID)
        {
            return _session.Get<T>(userID);
        }

      
        public IEnumerable<T> GetList(T obj)
        {
            return _session.CreateCriteria<T>().List<T>();
        }

       
    }
}
