//// See https://aka.ms/new-console-template for more information
//using Microsoft.EntityFrameworkCore;
//using OMS.Data.Model;
//using OMSWeb.Data.Access.DAL;
//using System.Numerics;

//Console.WriteLine("Hello, World!");
//dbcontext db = new dbcontext();

//	Category cat1 = new Category { CategoryId = 1, CategoryName = "As"};
//	Category cat2 = new Category { CategoryId = 2, CategoryName = "Assss"};

//	// добавление
//	db.Categories.Add(cat1);
//	db.Categories.Add(cat2);
//	db.SaveChanges();   // сохранение изменений

//	var phones = db.Categories.ToList();
//	foreach (var p in phones)
//		Console.WriteLine("{0} - {1}", p.CategoryId, p.CategoryName);
