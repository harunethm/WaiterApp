using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UsakUniversitesi.Models.DAL;

namespace WaiterApp.Models.DAL
{
    public interface IRepository : IDisposable
    {
        int Add<T>(T entity) where T : class;
        void AddOnly<T>(T entity) where T : class;

        long Count<T>() where T : class;
        long Count<T>(Expression<Func<T, bool>> expression) where T : class;

        int Delete<T>(T entity) where T : class;
        int DeleteByKey<T>(object keyValue) where T : class;

        void DeleteOnly<T>(T entity) where T : class;
        void DeleteOnlyByKey<T>(object keyValue) where T : class;

        List<T> GetAll<T>() where T : class;
        List<T> GetAll<T>(Expression<Func<T, bool>> expression) where T : class;

        int GetTotalRowCount();
        List<T> GetAllPaged<T>(Expression<Func<T, object>> sortExpression, int maximumRows, int startRowIndex) where T : class;
        List<T> GetAllPaged<T, TKeyType>(Expression<Func<T, bool>> expression, Expression<Func<T, TKeyType>> sortExpression, SortType sortType, int maximumRows, int startRowIndex) where T : class;

        T GetSingle<T>(Expression<Func<T, bool>> expression) where T : class;

        int Update<T>(T entity) where T : class;

        int Save();

        ObjectContext GetContext();
        DBWaiterAppEntities DbContext();

        void Detach(object entity);
        T GetByKey<T>(object keyValue) where T : class;
    }
}
