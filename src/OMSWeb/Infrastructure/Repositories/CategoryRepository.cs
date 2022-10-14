using Castle.Core.Resource;
using Microsoft.EntityFrameworkCore;
using OMS.Data.Model;
using OMSWeb.Data.Access.DAL;

namespace OMSWeb.Infrastructure.Repositories
{
	public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
	{
		private readonly DbSet<Category> _category;

		public CategoryRepository(dbcontext dbContext) : base(dbContext)
		{
			_category = dbContext.Set<Category>();
		}
	}
}
