using Microsoft.EntityFrameworkCore;
using OMS.Data.Model;
using OMSWeb.Data.Access.DAL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Data.Access.DAL
{
	public interface IUnitOfWork : IDisposable 
	{
		IEnumerable GetAll<T>(T obj) where T : class;
		void Get<T>(T obj) where T : class;
		void Add<T>(T obj) where T : class;
		void Update<T>(T obj) where T : class;
		void Remove<T>(T obj) where T : class;
		Task CommitAsync();
		IQueryable<T> Query<T>() where T : class;
	}




	//public class CategoryUoW : IUnitOfWork
	//{
	//	private dbcontext db;
	//	public CategoryUoW(dbcontext context)
	//	{
	//		this.db = context;
	//	}

	//	public IEnumerable<Category> GetAll()
	//	{
	//		return db.Categories;
	//	}

	//	public Category Get(T id)
	//	{
	//		return db.Categories.Find(id);
	//	}

	//	public void Add(Category category)
	//	{
	//		db.Categories.Add(category);
	//	}

	//	public void Update(Category category)
	//	{
	//		db.Entry(category).State = EntityState.Modified;
	//	}

	//	public void Remove(int id)
	//	{
	//		Category category = db.Categories.Find(id);
	//		if(category != null)
	//			db.Categories.Remove(category);
	//	}

	//	public void Dispose()
	//	{
	//		db = null;
	//	}

	//	public IEnumerable GetAll<T>(T obj) where T : class
	//	{
	//		return db.Categories;
	//	}

	//	public Category? Get<T>(int id) where T : class
	//	{
	//		return db.Categories.Find(id);
	//	}

	//	public void Add<T>(Category category) where T : class
	//	{
	//		db.Categories.Add(category);
	//	}

	//	public void Update<T>(Category category) where T : class
	//	{
	//		db.Entry(category).State = EntityState.Modified;
	//	}

	//	public void Remove<T>(int id) where T : class
	//	{
	//		Category category = db.Categories.Find(id);
	//		if (category != null)
	//			db.Categories.Remove(category);
	//	}
	//}


	//public class ProductUoW : IUnitOfWork<Product>
	//{
	//	private dbcontext db;
	//	public ProductUoW(dbcontext context)
	//	{
	//		this.db = context;
	//	}

	//	public IEnumerable<Product> GetAll()
	//	{
	//		return db.Products;
	//	}

	//	public Product Get(int id)
	//	{
	//		return db.Products.Find(id);
	//	}

	//	public void Add(Product product)
	//	{
	//		db.Products.Add(product);
	//	}

	//	public void Update(Product product)
	//	{
	//		db.Entry(product).State = EntityState.Modified;
	//	}

	//	public void Remove(int id)
	//	{
	//		Product product = db.Products.Find(id);
	//		if (product != null)
	//			db.Products.Remove(product);
	//	}

	//	public void Dispose()
	//	{
	//		db = null;
	//	}


	//}

}



