using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using NLog;
using NorthwindConsole.Models;

namespace NorthwindConsole
{
    class MainClass
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public static void Main(string[] args)
        {
            logger.Info("Program started");
            try
            {
                Format f = new Format();
                int choice;
                do
                {
                    

                    f.MainMenu();

                    choice = f.validateInt(Console.ReadLine());
                    Console.Clear();
                    logger.Info($"Option {choice} selected");

                    //Add Category
                    if (choice == 1)
                    {
                        Category category = new Category();

                        Console.Clear();
                        f.AddCategoryHeader();
                        Console.Write("    Enter Category Name: ");
                        category.CategoryName = Console.ReadLine();

                        Console.Clear();
                        f.AddCategoryHeader();
                        Console.Write("    Enter the Category Description: ");
                        category.Description = Console.ReadLine();

                        ValidationContext context = new ValidationContext(category, null, null);
                        List<ValidationResult> results = new List<ValidationResult>();

                        var isValid = Validator.TryValidateObject(category, context, results, true);
                        if (isValid)
                        {
                            var db = new NorthwindContext();
                            // check for unique name
                            if (db.Categories.Any(c => c.CategoryName == category.CategoryName))
                            {
                                // generate validation error
                                isValid = false;
                                results.Add(new ValidationResult("Name exists", new string[] { "CategoryName" }));
                            }
                            else
                            {
                                logger.Info("Validation passed");
                                Console.Clear();
                                db.AddCategory(category);
                                f.AddCategoryHeader();
                                Console.Write("\n    Category Added Successfully! Press any key to return to the Main Menu: ");
                                Console.ReadKey();
                                Console.Clear();
                            }
                        }
                        if (!isValid)
                        {
                            foreach (var result in results)
                            {
                                logger.Error($"{result.MemberNames.First()} : {result.ErrorMessage}");

                            }
                        }
                    }

                    //Edit Category
                    else if (choice == 2)
                    {
                        var db = new NorthwindContext();
                        var query = db.Categories.OrderBy(b => b.CategoryId);


                        Console.Clear();
                        f.EditCategoryHeader();

                        var category = ShowCategory(db);
                        if (category != null)
                        {
                            Category UpdatedCategory = InputCategroy(db);
                            if (UpdatedCategory != null)
                            {
                                UpdatedCategory.CategoryId = category.CategoryId;
                                db.EditCategory(UpdatedCategory);
                                Console.WriteLine("    Category Successfully Updated!");
                                // logger.Info("Post (id: {postid}) updated", UpdatedPost.PostId);
                            }
                        }
                        Console.Write("\n    Press any key to continue: ");
                        Console.ReadKey();
                        Console.Clear();
                    }

                    //View Category
                    else if (choice == 3)
                    {
                        var db = new NorthwindContext();

                        Console.Clear();
                        f.ViewCategoryHeader();

                        Console.ResetColor();
                        Console.WriteLine("\n    1) View All Categories");
                        Console.WriteLine("    2) View Categories and their related Products");
                        Console.WriteLine("    3) View a specific Category and its related Products");
                        Console.Write("    ");
                        int all = f.validateInt(Console.ReadLine());
                        do
                        {
                            
                            if (all > 3)
                            {
                                Console.Write("    Please choose from 1 or 3 ");
                                all = f.validateInt(Console.ReadLine());
                            }

                        } while (all > 3);
                        
                        if (all == 1)
                        {
                            var categories = db.Categories.OrderBy(b => b.CategoryId);
                            Console.Clear();
                            f.ViewAllCategoryHeader();
                            foreach (Category p in categories)
                            {
                                Console.WriteLine(f.ViewCategoriesFormat(), p.CategoryId, p.CategoryName, p.Description);
                            }

                            Console.ResetColor();
                            Console.Write("\n    Press any key to continue: ");
                            Console.ReadKey();
                            Console.Clear();
                        }
                        else if (all == 2)
                        {
                            {
                                var query = from p in db.Products.OrderBy(b => b.CategoryId)
                                            join c in db.Categories on p.CategoryId equals c.CategoryId
                                            where p.Discontinued == false
                                            select new
                                            {
                                                prodid = p.ProductID,
                                                prodname = p.ProductName,
                                                catid = c.CategoryId,
                                                catname = c.CategoryName,
                                                catdesc = c.Description
                                            };

                                Console.Clear();
                                f.ViewCatProdHeader();
                                foreach (var p in query.OrderBy(b => b.catid))
                                {

                              
                                    Console.WriteLine(f.ViewCatProdFormat(), p.catid, p.catname, p.catdesc, p.prodid, p.prodname);
                                }
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine(f.ViewCatProdFormat(), "------", "--------------------", "------------------------------------", "------", "--------------------");
                                Console.ResetColor();
                            }
                            Console.ResetColor();
                            Console.Write("\n    Press any key to continue: ");
                            Console.ReadKey();
                            Console.Clear();
                        }
                        else if (all == 3)
                        {
                            var categories = db.Categories.OrderBy(b => b.CategoryId);
                            Console.Clear();
                            f.ViewAllCategoryHeader();
                            foreach (Category p in categories)
                            {
                                Console.WriteLine(f.ViewCategoriesFormat(), p.CategoryId, p.CategoryName, p.Description);
                            }

                            Console.ResetColor();
                            Console.Write("\n    Select the Category ID to view its related Products: ");
                            int cid = f.validateInt(Console.ReadLine());

                            
                            {
                                var query = from p in db.Products.OrderBy(b => b.CategoryId)
                                            join c in db.Categories on p.CategoryId equals c.CategoryId
                                            where p.Discontinued == false
                                          //  && p.CategoryId == cid
                                            select new
                                            {
                                                prodid = p.ProductID,
                                                prodname = p.ProductName,
                                                catid = c.CategoryId,
                                                catname = c.CategoryName,
                                                catdesc = c.Description
                                            };

                                Console.Clear();
                                f.ViewCatProdHeader();
                                foreach (var p in query.OrderBy(b => b.catid))
                                {
                                    if(p.catid == cid)
                                    {
                                        Console.WriteLine(f.ViewCatProdFormat(), p.catid, p.catname, p.catdesc, p.prodid, p.prodname);
                                    }

                                    
                                }
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine(f.ViewCatProdFormat(), "------", "--------------------", "------------------------------------", "------", "--------------------");
                                Console.ResetColor();
                            }
                            Console.ResetColor();
                            Console.Write("\n    Press any key to continue: ");
                            Console.ReadKey();
                            Console.Clear();
                        }

                    }
            


                    //Add Product
                    else if (choice == 4) 
                    {

                        var db = new NorthwindContext();
                        Product product = new Product();
                        ValidationContext context = new ValidationContext(product, null, null);
                        List<ValidationResult> results = new List<ValidationResult>();

                        Console.Clear();
                        f.AddProductHeader();
                        Console.Write("    Enter Product Name: ");
                        product.ProductName = Console.ReadLine();

                        Console.Clear();
                        f.AddProductHeader();
                        Console.Write("    Enter Product Quantity per unit: ");
                        product.QuantityPerUnit = Console.ReadLine();

                        Console.Clear();
                        f.AddProductHeader();
                        Console.WriteLine("    Enter the Category ID. Choose from the available Categories:\n");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine(f.ViewCategoryFormat(), "Category ID", "Category");
                        Console.WriteLine(f.ViewCategoryFormat(), "------", "------------------------------------");

                        var categories = db.Categories.OrderBy(b => b.CategoryId);
                        foreach (Category p in categories)
                        {
                            Console.WriteLine(f.ViewCategoryFormat(), p.CategoryId, p.CategoryName);
                        }
                        Console.WriteLine(f.ViewCategoryFormat(), "------", "------------------------------------");
                        Console.ResetColor();
                        Console.Write("    ");
                        product.CategoryId = f.validateInt(Console.ReadLine());

                        Console.Clear();
                        f.AddProductHeader();

                        Console.WriteLine("    Enter the Product Supplier ID. Choose from the available Suppliers: \n");
                        
                        f.ViewSuppliersHeaderShort();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        var suppliers = db.Suppliers.OrderBy(b => b.SupplierId);
                        foreach (Supplier p in suppliers)
                        {
                            Console.WriteLine(f.ViewSupplierFormat(), p.SupplierId, p.CompanyName, p.City, p.Country);
                        }
                        Console.WriteLine(f.ViewSupplierFormat(), "------", "------------------------------------", "-------------------", "-------------------");
                        Console.Write("    ");
                        Console.ResetColor();
                        product.SupplierId = f.validateInt(Console.ReadLine());

                        Console.Clear();
                        f.AddProductHeader();
                        Console.Write("    Enter Product Price: ");
                        product.UnitPrice = f.validateInt(Console.ReadLine());

                        Console.Clear();
                        f.AddProductHeader();
                        Console.Write("    Enter Number of units to add to inventory: ");
                        product.UnitsInStock = Int16.Parse(Console.ReadLine());

                        Console.Clear();
                        f.AddProductHeader();
                        Console.Write("    What level of inventory should we re-order this product: ");
                        product.ReorderLevel = Int16.Parse(Console.ReadLine());

                        product.UnitsOnOrder = 0;
                        product.Discontinued = false;


                        var isValid = Validator.TryValidateObject(product, context, results, true);
                        if (isValid)
                        {
                            
                            // check for unique name
                            if (db.Products.Any(p => p.ProductName == product.ProductName))
                            {
                                // generate validation error
                                isValid = false;
                                results.Add(new ValidationResult("Name exists", new string[] { "ProductName" }));
                            }
                            else
                            {
                                //logger.Info("Validation passed");
                                // TODO: save category to db
                                db.AddProduct(product);
                                Console.Clear();
                                f.AddProductHeader();
                                Console.Write("\n    Product Added Successfully! Press any key to return to the Main Menu: ");
                                Console.ReadKey();
                                Console.Clear();
                            }
                        }
                        if (!isValid)
                        {
                            foreach (var result in results)
                            {
                                logger.Error($"{result.MemberNames.First()} : {result.ErrorMessage}");
                            }
                        }
                    }

                    //Edit Product
                    else if (choice == 5) 
                    {
                        var db = new NorthwindContext();
                        var query = db.Products.OrderBy(b => b.ProductID);


                        Console.Clear();
                        f.EditPProductHeader();

                        var product = ShowProduct(db);
                        if (product != null)
                        {
                            Product UpdatedProduct = InputProduct(db);
                            if (UpdatedProduct != null)
                            {
                                UpdatedProduct.ProductID = product.ProductID;
                                db.EditProduct(UpdatedProduct);
                                Console.WriteLine("\n    Product Successfully Updated!");
                                // logger.Info("Post (id: {postid}) updated", UpdatedPost.PostId);
                            }
                        }
                        Console.Write("\n    Press any key to continue: ");
                        Console.ReadKey();
                        Console.Clear();
                    }

                    //View Product
                    else if (choice == 6) 
                    {
                        var db = new NorthwindContext();
                       
                        Console.Clear();
                        f.ViewAllProductsHeader();

                        string all;
                        do
                        {
                            Console.Write("\n    View All Products? Y/N: ");
                            all = Console.ReadLine().ToUpper();
                        } while ((all != "Y") && (all != "N"));

                        if (all == "Y")
                        {
                            Console.Clear();
                            f.ViewAllProductsHeader();

                            string disc;
                            do
                            {
                                Console.Write("\n    Include Discontinued Products? Y/N: ");
                                disc = Console.ReadLine().ToUpper();
                            } while ((disc != "Y") && (disc != "N"));

                            if (disc == "N")
                            {
                                Console.Clear();
                                f.ViewAllProductsHeader();
                                var products = db.Products.OrderBy(b => b.ProductID);
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine(f.ViewProdFormat(), "Product Name", "Status");
                                Console.WriteLine(f.ViewProdFormat(), "------------------------------------", "-------------------");
                                Console.ResetColor();
                                foreach (Product p in products)
                                {
                                    if (p.Discontinued == false)
                                    {
                                        Console.WriteLine("    " + p.ProductName);
                                    }
                                }
                            }
                            else
                            {
                                Console.Clear();
                                f.ViewAllProductsHeader();
                                var products = db.Products.OrderBy(b => b.ProductID);
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine(f.ViewProdFormat(), "Product Name", "Status");
                                Console.WriteLine(f.ViewProdFormat(), "------------------------------------", "-------------------");
                                Console.ResetColor();

                                string status;

                                foreach (Product p in products)
                                {
                                    if (p.Discontinued == false)
                                    {
                                        status = " ";
                                    }
                                    else
                                    {
                                        status = "Discontinued";
                                    }
                                    Console.WriteLine(f.ViewProdFormat(), p.ProductName, status);
                                }
                            }
                            Console.ResetColor();
                            Console.Write("\n    Press any key to continue: ");
                            Console.ReadKey();
                            Console.Clear();
                        }
                        else
                        {
                            Console.Clear();
                            f.ViewAllProductsHeader();
                            var products = db.Products.OrderBy(b => b.ProductID);
                            Console.WriteLine(f.ViewProductsFormat(), "Product ID", "Product Name");
                            Console.WriteLine(f.ViewProductsFormat(), "------", "------------------------------------");
                            foreach (Product p in products)
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine(f.ViewProductsFormat(), p.ProductID, p.ProductName);
                            }
                            Console.ResetColor();
                            
                            Console.Write("\n    Select the Product ID to view details: ");
                            int prodid = f.validateInt(Console.ReadLine());
                            do
                            {
                                if (db.Products.Any(a => a.ProductID == prodid))
                                {
                                    var query = from p in db.Products
                                                join c in db.Categories on p.CategoryId equals c.CategoryId
                                                join s in db.Suppliers on p.SupplierId equals s.SupplierId
                                                select new
                                                {
                                                    prodid = p.ProductID,
                                                    prodname = p.ProductName,
                                                    catid = c.CategoryId,
                                                    catname = c.CategoryName,
                                                    catdesc = c.Description,
                                                    qpa = p.QuantityPerUnit,
                                                    price = p.UnitPrice,
                                                    instock = p.UnitsInStock,
                                                    onorder = p.UnitsOnOrder,
                                                    reorder = p.ReorderLevel,
                                                    supplier = s.CompanyName
                                                };

                                    Console.Clear();
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.WriteLine("\n    ---------------------------------------------------------------------------------------------\n" +
                                        "    View Product Details \n" +
                                        "    ---------------------------------------------------------------------------------------------\n");
                                    Console.ResetColor();

                                    foreach (var p in query)
                                    {
                                        if (p.prodid == prodid)
                                        {

                                            Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("              Product ID: "); Console.ResetColor(); Console.Write(p.prodid);
                                            Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("\n            Product Name: "); Console.ResetColor(); Console.Write(p.prodname);
                                            Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("\n                Category: "); Console.ResetColor(); Console.Write(p.catname);
                                            Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("\n    Category Description: "); Console.ResetColor(); Console.Write(p.catname);
                                            Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("\n                Supplier: "); Console.ResetColor(); Console.Write(p.supplier);
                                            Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("\n                     QPA: "); Console.ResetColor(); Console.Write(p.qpa);
                                            Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("\n              Unit Price: "); Console.ResetColor(); Console.Write(String.Format("{0:C0}", Convert.ToInt32(p.price)));
                                            Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("\n          Units in Stock: "); Console.ResetColor(); Console.Write(p.instock);
                                            Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("\n          Units on Order: "); Console.ResetColor(); Console.Write(p.onorder);
                                            Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("\n           Reorder Level: "); Console.ResetColor(); Console.Write(p.reorder);
                                            prodid = 0;
                                        }
                                    }
                                }
                                else if (db.Products.Any(a => a.ProductID != prodid))
                                {
                                    Console.WriteLine("    Please choose a valid Product ID, or enter 0 to cancel: \n");
                                    prodid = f.validateIntZero(Console.ReadLine());
                                }
                            } while ((prodid != 0) && (db.Products.Any(b => b.ProductID != prodid)));

                            Console.Write("\n\n    Press any key to continue: ");
                            Console.ReadKey();
                            Console.Clear();
                        }
                    }


                    // Display Category and related products
                    else if (choice == 7) 
                    {
                        var db = new NorthwindContext();
                        var query = db.Categories.OrderBy(p => p.CategoryId);

                        Console.WriteLine("Select the category whose products you want to display:");
                        foreach (var item in query)
                        {
                            Console.WriteLine($"{item.CategoryId}) {item.CategoryName}");
                        }
                        int id = int.Parse(Console.ReadLine());
                        Console.Clear();
                        logger.Info($"CategoryId {id} selected");
                        Category category = db.Categories.FirstOrDefault(c => c.CategoryId == id);
                        Console.WriteLine($"{category.CategoryName} - {category.Description}");
                        foreach(Product p in category.Products)
                        {
                            Console.WriteLine(p.ProductName);
                        }
                    }

                    // Display all Categories and their related products
                    else if (choice == 8)
                    {
                        var db = new NorthwindContext();
                        var query = db.Categories.Include("Products").OrderBy(p => p.CategoryId);
                        foreach(var item in query)
                        {
                            Console.WriteLine($"{item.CategoryName}");
                            foreach(Product p in item.Products)
                            {
                                Console.WriteLine($"\t{p.ProductName}");
                            }
                        }
                    }
                    Console.WriteLine();

                } while (choice != 0);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
            logger.Info("Program ended");
        }

        public static Product InputProduct(NorthwindContext db)
        {
            Console.Clear();
            Product product = new Product();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n    ---------------------------------------------------------------------------------------------\n" +
                "    Edit Product\n" +
                "    ---------------------------------------------------------------------------------------------\n");
            Console.ResetColor();
            
            Console.Write("\n    Enter the New Product Name: ");
            product.ProductName = Console.ReadLine();
            Console.Write("    Enter the New Product QPA: ");
            product.QuantityPerUnit = Console.ReadLine();

            ValidationContext context = new ValidationContext(product, null, null);
            List<ValidationResult> results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(product, context, results, true);
            if (isValid)
            {
                return product;
            }
            else
            {
                foreach (var result in results)
                {
                    logger.Error($"{result.MemberNames.First()} : {result.ErrorMessage}");
                }
            }
            return null;
        }

        public static Category InputCategroy(NorthwindContext db)
        {
            Category category = new Category();
            Console.Write("\n    Enter the new Category Name: ");
            category.CategoryName = Console.ReadLine();
            Console.Write("\n    Enter the new Description: ");
            category.Description = Console.ReadLine();

            ValidationContext context = new ValidationContext(category, null, null);
            List<ValidationResult> results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(category, context, results, true);
            if (isValid)
            {
                return category;
            }
            else
            {
                foreach (var result in results)
                {
                    logger.Error($"{result.MemberNames.First()} : {result.ErrorMessage}");
                }
            }
            return null;
        }


        public static Product ShowProduct(NorthwindContext db)
        {
            Format f = new Format();
            var products = db.Products.OrderBy(b => b.ProductID);
            foreach (Product p in products)
            {
                Console.WriteLine(f.ViewProductsFormat(), p.ProductID, p.ProductName);
            }
            Console.Write("\n    Choose the Product ID: ");
            int productid = f.validateInt(Console.ReadLine());
            do
            {
                if (db.Products.Any(b => b.ProductID == productid))
                {
                    Product product = db.Products.FirstOrDefault(p => p.ProductID == productid);
                    return product;
                }
                else if (db.Products.Any(b => b.ProductID != productid))
                {
                    //logger.Error("There are no Blogs saved with that Id");
                    Console.Write("    Please choose a valid product ID, or enter 0 to cancel: \n");
                    productid = f.validateIntZero(Console.ReadLine());
                }
            } while ((productid != 0) && (db.Products.Any(b => b.ProductID != productid)));

            return null;
        }

        public static Category ShowCategory(NorthwindContext db)
        {
            Format f = new Format();
            var category = db.Categories.OrderBy(b => b.CategoryId);
            foreach (Category p in category)
            {
                Console.WriteLine(f.ViewCategoriesFormat(), p.CategoryId, p.CategoryName, p.Description);
            }
            Console.Write("\n    Choose the Category ID: ");
            int categoryid = f.validateInt(Console.ReadLine());
            do
            {
                if (db.Categories.Any(b => b.CategoryId == categoryid))
                {
                    Category ctg = db.Categories.FirstOrDefault(p => p.CategoryId == categoryid);
                    return ctg;
                }
                else if (db.Categories.Any(b => b.CategoryId != categoryid))
                {
                    //logger.Error("There are no Blogs saved with that Id");
                    Console.WriteLine("    Please choose a valid Category ID, or enter 0 to cancel: \n");
                    categoryid = f.validateIntZero(Console.ReadLine());
                }
            } while ((categoryid != 0) && (db.Categories.Any(b => b.CategoryId != categoryid)));

            return null;
        }

    }
}
