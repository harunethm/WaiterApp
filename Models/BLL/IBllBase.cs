using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WaiterApp.Models.DAL;

namespace WaiterApp.Models.BLL
{
    public interface IBllBase<T> where T : class
    {
        IRepository RepositoryOlustur();
        bool Add(T entity);
        bool Delete(T entity);
        bool Update(T entity);
        long Count();
        int GetTotalRowCount();
        List<T> GetAll();
        List<T> GetAll(Expression<Func<T, bool>> expression);
        T GetSingle(Expression<Func<T, bool>> expression);
        T GetByKey(object keyValue);
        bool DeleteByKey(object keyValue);
        void AddOnly(T entity);
        void DeleteOnly(T entity);
        void UpdateOnly(T entity);
        void DeleteOnlyByKey(object keyValue);
        int Save();
    }
}
