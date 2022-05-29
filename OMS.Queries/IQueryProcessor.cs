using OMS.Api.Models.Category;
using OMS.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Queries
{
	public interface IQueryProcessor
	{
		IQueryable<Category> Get();
		Category Get(int id);
		Task<Category> Create(CreateCategoryModel model);
		Task<Category> Update(int id, UpdateCategoryModel model);
		Task Delete(int id);
	}
}
