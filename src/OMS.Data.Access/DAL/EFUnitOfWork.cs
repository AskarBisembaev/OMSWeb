using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OMS.Data.Model;
using OMSWeb.Data.Access.DAL;

namespace OMS.Data.Access.DAL
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private DbContext _context;

        public EFUnitOfWork(DbContext context)
        {
            _context = context;
        }

        public DbContext Context => _context;


        public void Add<T>(T obj)
            where T : class
        {
            var set = _context.Set<T>();
            set.Add(obj);
        }

        public void Update<T>(T obj)
            where T : class
        {
            var set = _context.Set<T>();
            set.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }

        void IUnitOfWork.Remove<T>(T obj)
        {
            var set = _context.Set<T>();
            set.Remove(obj);
        }

		public IQueryable<T> Query<T>()
		  where T : class
		{
			return _context.Set<T>();
		}

		public async Task CommitAsync()
		{
			await _context.SaveChangesAsync();
		}

		public void Dispose()
        {
            _context = null;
        }

		public IEnumerable GetAll<T>(T obj) where T : class
		{
			throw new NotImplementedException();
		}

		public void Get<T>(T obj) where T : class
		{
			var set = _context.Set<T>();
			set.Find(obj);
		}
	}
}

	#region 1
	//public class EFUnitOfWork : IDisposable
	//{
	//       private dbcontext db = new dbcontext();
	//       private CategoryUoW categoryUoW;
	//       private ProductUoW productUoW;

	//       public CategoryUoW Categories
	//       {
	//           get
	//           {
	//               return categoryUoW;
	//           }
	//       }

	//       public ProductUoW Products
	//       {
	//           get
	//           {
	//               return productUoW;
	//           }
	//       }

	//       public void Save()
	//       {
	//           db.SaveChanges();
	//       }

	//       private bool disposed = false;

	//       public virtual void Dispose(bool disposing)
	//       {
	//           if (!this.disposed)
	//           {
	//               if (disposing)
	//               {
	//                   db.Dispose();
	//               }
	//               this.disposed = true;
	//           }
	//       }

	//       public void Dispose()
	//       {
	//           Dispose(true);
	//           GC.SuppressFinalize(this);
	//       }
	//   }
	#endregion