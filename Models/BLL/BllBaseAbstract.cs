using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using WaiterApp.Models.DAL;

namespace WaiterApp.Models.BLL
{
    [System.ComponentModel.DataObject]
    public abstract class BllBaseAbstract<T> : IBllBase<T> where T : class
    {
        public BllBaseAbstract()
        {
        }

        public IRepository Repository;
        public BllBaseAbstract(IRepository rep)
        {
            Repository = rep;
        }

        internal IRepository repository
        {
            get
            {
                if (Repository == null)
                    return BllOrtak.repository;

                else
                    return Repository;
            }
        }

        public IRepository RepositoryOlustur()
        {
            Repository = new Repository(new DBWaiterAppEntities());
            return Repository;
        }

        public void BaglantiKapat()
        {
            try
            {
                repository.GetContext().Connection.Close();
            }
            catch
            {
            }
            Repository = null;
        }

        public virtual int GetTotalRowCount()
        {
            return repository.GetTotalRowCount();
        }

        public virtual bool Add(T entity)
        {
            return repository.Add<T>(entity) > 0;
        }

        public virtual bool Delete(T entity)
        {
            return repository.Delete<T>(entity) > 0;
        }

        public virtual bool Update(T entity)
        {
            return repository.Update<T>(entity) > 0;
        }

        public virtual long Count()
        {
            return repository.Count<T>();
        }

        public List<T> GetAll(Expression<Func<T, bool>> expression)
        {
            return repository.GetAll<T>(expression) as List<T>;
        }

        public T GetSingle(Expression<Func<T, bool>> expression)
        {
            return repository.GetSingle<T>(expression) as T;
        }

        public virtual List<T> GetAll()
        {
            return repository.GetAll<T>();
        }

        public virtual List<T> GetByKeyList(object keyValue)
        {
            var ent = repository.GetByKey<T>(keyValue);
            List<T> result = new List<T>();
            result.Add(ent);
            return result;
        }

        public virtual T GetByKey(object keyValue)
        {
            return repository.GetByKey<T>(keyValue);
        }

        public virtual bool DeleteByKey(object keyValue)
        {
            return repository.DeleteByKey<T>(keyValue) > 0;
        }

        public virtual void AddOnly(T entity)
        {
            repository.AddOnly<T>(entity);
        }

        public virtual void DeleteOnly(T entity)
        {
            repository.DeleteOnly<T>(entity);
        }

        public virtual void UpdateOnly(T entity)
        {
            repository.Update<T>(entity);
        }

        public virtual void DeleteOnlyByKey(object keyValue)
        {
            repository.DeleteOnlyByKey<T>(keyValue);
        }

        public int Save()
        {
            return repository.Save();
        }

    }
}