using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System;

namespace NorthwindConsole.Models
{
    public class NorthwindContext : DbContext
    {
        public NorthwindContext() : base("name=NorthwindContext") { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }

        public void AddCategory(Category category)
        {
            this.Categories.Add(category);
            this.SaveChanges();
        }

        public void AddProduct(Product product)
        {
            this.Products.Add(product);
            this.SaveChanges();
        }
        public void EditProduct(Product UpdatedProduct)
        {
            Product product = this.Products.Find(UpdatedProduct.ProductID);
            product.ProductName = UpdatedProduct.ProductName;
            this.SaveChanges();
        }

        public void EditCategory(Category UpdatedCategory)
        {
            Category category = this.Categories.Find(UpdatedCategory.CategoryId);
            category.CategoryName = UpdatedCategory.CategoryName;
            category.Description = UpdatedCategory.Description;
            this.SaveChanges();
        }



    }
}
