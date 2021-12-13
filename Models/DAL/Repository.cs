using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using UsakUniversitesi.Models.DAL;

namespace WaiterApp.Models.DAL
{
    public class Repository : BaseRepository, IRepository
    {
        private bool _disposed;
        private int totalRowCount;

        public Repository(DbContext context)
        {
            this._context = context;
            this._contextReused = true;
        }
        public void Dispose()
        {
            DisposeObject(true);
            GC.SuppressFinalize(this);
        }
        ~Repository()
        {
            DisposeObject(false);
        }
        private void DisposeObject(bool disposing)
        {
            if (_disposed)
            {
                return;
            }
            if (disposing)
            {
                if (_context != null)
                {
                    _context.Dispose();
                }
                _disposed = true;
            }
        }
        public int GetTotalRowCount()
        {
            return totalRowCount;
        }
        public ObjectContext GetContext()
        {
            return GetObjectContext();
        }
        public DBWaiterAppEntities DbContext()
        {
            return _context as DBWaiterAppEntities;
        }
        public void ReleaseContext()
        {
            ReleaseObjectContextIfNotReused();
        }
        public long Count<T>() where T : class
        {
            try
            {
                DbContext context = GetDbContext();
                return context.Set<T>().Count();
            }
            catch
            {
                throw;
            }
            finally
            {
                ReleaseObjectContextIfNotReused();
            }
        }

        public long Count<T>(Expression<Func<T, bool>> expression) where T : class
        {
            try
            {
                DbContext context = GetDbContext();
                return context.Set<T>().Count(expression);
            }
            catch
            {
                throw;
            }
            finally
            {
                ReleaseObjectContextIfNotReused();
            }
        }

        public List<T> GetAll<T>() where T : class
        {
            try
            {
                DbContext context = GetDbContext();
                return context.Set<T>().ToList();
            }
            catch
            {
                throw;
            }
            finally
            {
                ReleaseObjectContextIfNotReused();
            }
        }

        public List<T> GetAll<T>(Expression<Func<T, bool>> expression) where T : class
        {
            try
            {
                DbContext context = GetDbContext();
                return context.Set<T>().Where(expression).ToList();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                ReleaseObjectContextIfNotReused();
            }
        }

        public List<T> GetAllPaged<T>(Expression<Func<T, object>> sortExpression, int maximumRows, int startRowIndex) where T : class
        {
            try
            {
                DbContext context = GetDbContext();
                return context.Set<T>()
                    .OrderBy(sortExpression)
                    .Skip(startRowIndex)
                    .Take(maximumRows)
                    .ToList();
            }
            catch
            {
                throw;
            }
            finally
            {
                ReleaseObjectContextIfNotReused();
            }
        }
        public List<T> GetAllPaged<T, TKeyType>(Expression<Func<T, bool>> expression, Expression<Func<T, TKeyType>> sortExpression, SortType sortType, int maximumRows, int startRowIndex) where T : class
        {
            try
            {
                List<T> list;
                DbContext context = GetDbContext();


                if (sortType == SortType.Ascending)
                {
                    list = context.Set<T>()
                        .OrderBy(sortExpression)
                        .Skip(startRowIndex)
                        .Take(maximumRows)
                        .ToList();
                }
                else
                {
                    list = context.Set<T>()
                        .OrderByDescending(sortExpression)
                        .Skip(startRowIndex)
                        .Take(maximumRows)
                        .ToList();
                }
                totalRowCount = (int)this.Count<T>(expression);
                return list;
            }
            catch
            {
                throw;
            }
            finally
            {
                ReleaseObjectContextIfNotReused();
            }
        }

        public T GetSingle<T>(Expression<Func<T, bool>> expression) where T : class
        {
            try
            {
                DbContext context = GetDbContext();
                return context.Set<T>().FirstOrDefault(expression);
            }
            catch
            {
                throw;
            }
            finally
            {
                ReleaseObjectContextIfNotReused();
            }
        }

        public int Add<T>(T entity) where T : class
        {
            try
            {
                DbContext context = GetDbContext();
                context.Set<T>().Add(entity);
                return context.SaveChanges();
            }
            catch
            {
                try
                {
                    GetDbContext().Set<T>().Remove(entity);
                    Detach(entity);
                    entity = null;
                }
                catch { }
                throw;
            }
            finally
            {
                ReleaseObjectContextIfNotReused();
            }
        }

        public void AddOnly<T>(T entity) where T : class
        {
            try
            {
                DbContext context = GetDbContext();
                context.Set<T>().Add(entity);
            }
            catch
            {
                try
                {
                    GetDbContext().Set<T>().Remove(entity);
                    Detach(entity);
                    entity = null;
                }
                catch { }
                throw;
            }
            finally
            {
                ReleaseObjectContextIfNotReused();
            }
        }

        public int Update<T>(T entity) where T : class
        {
            try
            {
                var context = GetDbContext();
                context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                return context.SaveChanges();
            }
            catch
            {
                Detach(entity);
                throw;
            }
            finally
            {
                ReleaseObjectContextIfNotReused();
            }
        }

        public int Delete<T>(T entity) where T : class
        {
            try
            {
                DbContext context = GetDbContext();
                context.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
                return context.SaveChanges();
            }
            catch
            {
                throw;
            }
            finally
            {
                ReleaseObjectContextIfNotReused();
            }
        }

        public void DeleteOnly<T>(T entity) where T : class
        {
            try
            {
                DbContext context = GetDbContext();
                context.Set<T>().Remove(entity);
            }
            catch
            {
                throw;
            }
            finally
            {
                ReleaseObjectContextIfNotReused();
            }
        }

        public int Save()
        {
            try
            {
                DbContext context = GetDbContext();
                int result = context.SaveChanges();
                return result;
            }
            catch
            {
                throw;
            }
            finally
            {
                ReleaseObjectContextIfNotReused();
            }
        }


        public void Detach(object entity)
        {
            GetObjectContext().Detach(entity);
        }

        public T GetByKey<T>(object keyValue) where T : class
        {
            DbContext context = GetDbContext();
            return context.Set<T>().Find(keyValue);

        }

        public int DeleteByKey<T>(object keyValue) where T : class
        {
            return Delete<T>(GetByKey<T>(keyValue));
        }

        public void DeleteOnlyByKey<T>(object keyValue) where T : class
        {
            DeleteOnly<T>(GetByKey<T>(keyValue));
        }
    }
}