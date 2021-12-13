using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace WaiterApp.Models.DAL
{
    public class BaseRepository
    {
        internal DbContext _context;
        internal bool _contextReused;

        public DbContext GetDbContext()
        {
            if (!_contextReused)
            {
                DbContext entities = new DBWaiterAppEntities();
                return entities;
            }
            return _context;
        }
        public ObjectContext GetObjectContext()
        {
            ObjectContext objectContext = ((IObjectContextAdapter)GetDbContext()).ObjectContext;
            return objectContext;
        }
        public void ReleaseObjectContextIfNotReused()
        {
            try
            {
                if (!_contextReused)
                {
                    ReleaseObjectContext();
                }
            }
            catch
            {
                throw;

            }
        }
        public void ReleaseObjectContext()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
            _contextReused = false;
        }
    }
}