using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindConsole.Models
{
    class Format
    {

        public int validateInt(string input)
        {
            int output;
            do
            {
                if (!int.TryParse(input, out output))
                {
                    Console.Write("    Please enter a numeric value: ");
                    input = Console.ReadLine();
                }
                else if ((Convert.ToDouble(input)) <= 0)
                {
                    Console.Write("    Please enter a positive value: ");
                    input = Console.ReadLine();
                }
                else
                {
                    output = int.Parse(input);
                }
            } while ((!int.TryParse(input, out output)) || ((int.Parse(input)) <= 0));

            return output;
        }

        public int validateIntZero(string input)
        {
            int output;
            do
            {
                if (!int.TryParse(input, out output))
                {
                    Console.Write("    Please enter a numeric value, or select 0 to cancel: ");
                    input = Console.ReadLine();
                }
                else if ((Convert.ToDouble(input)) < 0)
                {
                    Console.Write("    Please enter a value greater than 0: ");
                    input = Console.ReadLine();
                }
                else
                {
                    output = int.Parse(input);
                }
            } while ((!int.TryParse(input, out output)) || ((int.Parse(input)) < 0));

            return output;
        }
        

        public string ViewProductsFormat()
        {
            return "    {0,-4}\t{1,-50}";
        }
        public string ViewProdFormat()
        {
            return "    {0,-50}\t{1,-20}";
        }
        public string ViewCategoriesFormat()
        {
            return "    {0,-4}\t{1,-20}\t{2,-50}";
        }
        public string ViewCatProdFormat()
        {
            return "    {0,-4}\t{1,-20}\t{2,-60}\t{3,-10}\t{4,-30}";
        }

        public string ViewAllProductsFormat()
        {
            return "    {0,-4}\t{1,-50}\t{2,-30}\t{3,-30}\t{4,-30}\t{5,-30}\t{6,-30}\t{7,-30}\t{8,-10}";
        }

        public string ViewCategoryFormat()
        {
            return "    {0,-4}\t{1,-50}";
        }

        public string ViewSupplierFormat()
        {
            return "    {0,-4}\t{1,-50}\t{2,-20}\t{3,-20}";
        }

        public void MainMenu()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n    ---------------------------------------------------------------------------------------------\n" +
                "    Welcome to Matts NorthWind Traders!\n" +
                "    ---------------------------------------------------------------------------------------------\n");
            Console.ResetColor();
            Console.WriteLine("\n    1) Add Category");
            Console.WriteLine("    2) Edit Category");
            Console.WriteLine("    3) View Category");
            Console.WriteLine("    4) Add Product");
            Console.WriteLine("    5) Edit Product");
            Console.WriteLine("    6) View Products");
            Console.WriteLine("    7) Display Category and related products");
            Console.WriteLine("    8) Display all Categories and their related products");
            Console.WriteLine("    0) Quit");
            Console.Write("    ");

        }

        public void EditPProductHeader()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n    ---------------------------------------------------------------------------------------------\n" +
                "    Edit Product\n" +
                "    ---------------------------------------------------------------------------------------------\n");
            Console.WriteLine(ViewProductsFormat(), "Product ID", "Product Name");
            Console.WriteLine(ViewProductsFormat(), "------", "------------------------------------");
            Console.ResetColor();
        }

        public void AddProductHeader()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n    ---------------------------------------------------------------------------------------------\n" +
                "    Add Product\n" +
                "    ---------------------------------------------------------------------------------------------\n");
            Console.ResetColor();
        }

        public void ViewSuppliersHeaderShort()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(ViewSupplierFormat(), "Supplier ID", "Company Name", "City", "Country");
            Console.WriteLine(ViewSupplierFormat(), "------", "------------------------------------", "-------------------", "-------------------");
            Console.ResetColor();
        }

        public void ViewAllProductsHeader()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n    ---------------------------------------------------------------------------------------------\n" +
                "    View Products\n" +
                "    ---------------------------------------------------------------------------------------------\n");
            Console.ResetColor();
        }

        public void AddCategoryHeader()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n    ---------------------------------------------------------------------------------------------\n" +
                "    Add Category\n" +
                "    ---------------------------------------------------------------------------------------------\n");
            Console.ResetColor();
        }
        public void EditCategoryHeader()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n    ---------------------------------------------------------------------------------------------\n" +
                "    Edit Category\n" +
                "    ---------------------------------------------------------------------------------------------\n");
            Console.WriteLine(ViewCategoriesFormat(), "CategoryID", "Category Name", "Category Description");
            Console.WriteLine(ViewCategoriesFormat(), "------", "--------------------", "------------------------------------");
            Console.ResetColor();
        }

        public void ViewAllCategoryHeader()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n    ---------------------------------------------------------------------------------------------\n" +
                "    View Categories\n" +
                "    ---------------------------------------------------------------------------------------------\n");
            Console.WriteLine(ViewCategoriesFormat(), "CategoryID", "Category Name", "Category Description");
            Console.WriteLine(ViewCategoriesFormat(), "------", "--------------------", "------------------------------------");
            Console.ResetColor();
        }
        public void ViewCatProdHeader()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n    ---------------------------------------------------------------------------------------------\n" +
                "    View Categories and related Products\n" +
                "    ---------------------------------------------------------------------------------------------\n");
            Console.WriteLine(ViewCatProdFormat(), "CategoryID", "Category Name", "Category Description", "ProductID", "Product Name");
            Console.WriteLine(ViewCatProdFormat(), "------", "--------------------", "------------------------------------", "------", "--------------------");
            Console.ResetColor();
        }

        public void ViewCategoryHeader()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n    ---------------------------------------------------------------------------------------------\n" +
                "    View Categories\n" +
                "    ---------------------------------------------------------------------------------------------\n");
            Console.ResetColor();
        }
    }
}
