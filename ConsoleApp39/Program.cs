using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;
using SqlConnection = Microsoft.Data.SqlClient.SqlConnection;
using SqlDataAdapter = Microsoft.Data.SqlClient.SqlDataAdapter;

namespace ConsoleApp39
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            string cs = @"Data Source=JAVID\SQLEXPRESS;Initial Catalog=Dapper;Integrated Security=True;Connect Timeout=30;TrustServerCertificate=True;";
            using var con = new SqlConnection(cs);
            var sql = string.Empty;

            sql = "SELECT * FROM Products";
            var collection = (await con.QueryAsync(sql)).ToList();

            int select1 = 0;
            List<string> options = new List<string> { "Add Product", "Edit Product", "Remove Product", "Clear List" };

            bool isRun = true;

            while (isRun)
            {
                Console.Clear();
                for (int i = 0; i < options.Count; i++)
                {
                    if (select1 == i)
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine($">>    {options[i]}");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine($"    {options[i]}");
                    }
                }
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Escape)
                {
                    isRun = false;
                }
                else if (key.Key == ConsoleKey.DownArrow)
                {
                    select1++;
                    if (select1 >= options.Count)
                    {
                        select1 = 0;
                    }
                }
                else if (key.Key == ConsoleKey.UpArrow)
                {
                    select1--;

                    if (select1 < 0)
                    {
                        select1 = options.Count - 1;
                    }
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    if (select1 == 0)
                    {
                        Console.Clear();
                        Console.WriteLine("\u001b[32m=== ADD A NEW PRODUCT ===");

                        Console.Write("\u001b[32mProduct name: \u001b[0m");
                        string productname = Console.ReadLine();

                        decimal price;
                        Console.Write("\u001b[32mPrice: \u001b[0m");
                        while (!decimal.TryParse(Console.ReadLine(), out price) || price < 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("The price cannot be empty.");
                            Console.ResetColor();
                            Console.Write("Price: ");
                        }

                        int stock;
                        Console.Write("\u001b[32mStock: \u001b[0m");
                        while(!int.TryParse(Console.ReadLine(), out stock) || stock < 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("The stock cannot be empty.");
                            Console.ResetColor();
                            Console.Write("Stock: ");
                        }

                        int catId;
                        Console.Write("\u001b[32mCategory Id: \u001b[0m");
                        while (!int.TryParse(Console.ReadLine(), out catId) || (catId < 1 || catId > 5))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("The category Id cannot be empty.");
                            Console.ResetColor();
                            Console.Write("Category Id: ");
                        }

                        var insertSql = "INSERT INTO Products (ProductName, Price, Stock, CategoryId) VALUES (@ProductName, @Price, @Stock, @CategoryId)";

                        if (!string.IsNullOrWhiteSpace(productname))
                        {

                            int rows = await con.ExecuteAsync(insertSql, new
                            {
                                ProductName = productname,
                                Price = price,
                                Stock = stock,
                                CategoryId = catId
                            });

                            if (rows > 0)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("\nAdded succesfully");
                                Console.ResetColor();

                                collection = (await con.QueryAsync(sql)).ToList();

                                Console.ReadKey(true);
                            }

                            Console.WriteLine("\nPress any key to continue.");
                            Console.ReadKey(true);
                        }
                        else
                        {
                            Console.ForegroundColor= ConsoleColor.Red;
                            Console.WriteLine("\nThe product name cannot be empty.");
                            Console.ResetColor();
                            Console.ReadKey(true);
                        }
                    }

                    else if (select1 == 1)
                    {
                        int select2 = 0;
                        bool isRun2 = true;
                        if (collection.Count == 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("The are no product to edit.");
                            Console.ResetColor();
                            Console.WriteLine("\nPress any key to continue.");
                            Console.ReadKey(true);

                            isRun2 = false;
                        }
                        else
                        {
                            while (isRun2)
                            {
                                Console.Clear();

                                for (int i = 0; i < collection.Count; i++)
                                {
                                    if (select2 == i)
                                    {
                                        Console.BackgroundColor = ConsoleColor.White;
                                        Console.ForegroundColor = ConsoleColor.Black;
                                        Console.WriteLine($">>    {collection[i].Id}  {collection[i].ProductName}");
                                        Console.ResetColor();
                                    }

                                    else
                                    {
                                        Console.WriteLine($"    {collection[i].Id} {collection[i].ProductName}");
                                    }

                                }
                                ConsoleKeyInfo key2 = Console.ReadKey(true);
                                if (key2.Key == ConsoleKey.Escape)
                                {
                                    isRun2 = false;
                                }
                                else if (key2.Key == ConsoleKey.DownArrow)
                                {
                                    select2++;
                                    if (select2 >= collection.Count)
                                    {
                                        select2 = 0;
                                    }
                                }
                                else if (key2.Key == ConsoleKey.UpArrow)
                                {
                                    select2--;

                                    if (select2 < 0)
                                    {
                                        select2 = collection.Count - 1;
                                    }
                                }
                                else if (key2.Key == ConsoleKey.Enter)
                                {
                                    Console.Clear();

                                    List<string> columns = new List<string>();
                                    string id = "Id";
                                    var query = "SELECT * FROM Products";

                                    using SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                                    DataTable table = new DataTable();
                                    adapter.Fill(table);

                                    foreach (DataColumn Dcol in table.Columns)
                                    {
                                        if (Dcol.ColumnName != id)
                                        {
                                            columns.Add(Dcol.ColumnName);
                                        }
                                    }

                                    int select3 = 0;
                                    bool isRun3 = true;


                                    while (isRun3)
                                    {
                                        Console.Clear();
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        Console.WriteLine($"=== Update column (ID: {collection[select2].Id}) ===\n");
                                        Console.ResetColor();
                                        for (int i = 0; i < columns.Count; i++)
                                        {
                                            if (select3 == i)
                                            {
                                                Console.BackgroundColor = ConsoleColor.White;
                                                Console.ForegroundColor = ConsoleColor.Black;
                                                Console.WriteLine($">>    {columns[i]}");
                                                Console.ResetColor();
                                            }

                                            else
                                            {
                                                Console.WriteLine($"    {columns[i]}");
                                            }

                                        }
                                        ConsoleKeyInfo key3 = Console.ReadKey(true);
                                        if (key3.Key == ConsoleKey.Escape)
                                        {
                                            isRun3 = false; ;
                                        }
                                        else if (key3.Key == ConsoleKey.DownArrow)
                                        {
                                            select3++;
                                            if (select3 >= columns.Count)
                                            {
                                                select3 = 0;
                                            }
                                        }
                                        else if (key3.Key == ConsoleKey.UpArrow)
                                        {
                                            select3--;

                                            if (select3 < 0)
                                            {
                                                select3 = columns.Count - 1;
                                            }
                                        }
                                        else if (key3.Key == ConsoleKey.Enter)
                                        {
                                            Console.Clear();

                                            string col = columns[select3];

                                            Console.Write("New val: ");
                                            string newcol = Console.ReadLine();


                                            string update = $"UPDATE Products SET {col} = @newcoll WHERE Id = @Id";

                                            var paramets = new
                                            {
                                                newcoll = newcol,
                                                Id = collection[select2].Id
                                            };

                                            int rows = await con.ExecuteAsync(update, paramets);

                                            if (rows > 0)
                                            {
                                                Console.ForegroundColor = ConsoleColor.Green;
                                                Console.WriteLine("\nUpdated successfully.");
                                                Console.ResetColor(); collection = (await con.QueryAsync(sql)).ToList();
                                            }
                                            else
                                            {
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.WriteLine("The are no product to remove.");
                                                Console.ResetColor();
                                            }

                                            Console.WriteLine("\nPress any key to continue.");
                                            Console.ReadKey(true);
                                        }
                                    }
                                }
                            }
                        }
                    }

                    else if (select1 == 2)
                    {
                        int select4 = 0;
                        bool isRun5 = true;

                        if (collection.Count == 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("The are no product to remove.");
                            Console.ResetColor();
                            Console.WriteLine("\nPress any key to contiune");
                            Console.ReadKey(true);

                            isRun5 = false;

                        }
                        else
                        {

                            while (isRun5)
                            {
                                Console.Clear();

                                for (int i = 0; i < collection.Count; i++)
                                {
                                    if (select4 == i)
                                    {
                                        Console.BackgroundColor = ConsoleColor.White;
                                        Console.ForegroundColor = ConsoleColor.Black;
                                        Console.WriteLine($">>    {collection[i].Id}  {collection[i].ProductName}");
                                        Console.ResetColor();
                                    }

                                    else
                                    {
                                        Console.WriteLine($"    {collection[i].Id} {collection[i].ProductName}");
                                    }

                                }
                                ConsoleKeyInfo key4 = Console.ReadKey(true);
                                if (key4.Key == ConsoleKey.Escape)
                                {
                                    isRun5 = false;
                                }
                                else if (key4.Key == ConsoleKey.DownArrow)
                                {
                                    select4++;
                                    if (select4 >= collection.Count)
                                    {
                                        select4 = 0;
                                    }
                                }
                                else if (key4.Key == ConsoleKey.UpArrow)
                                {
                                    select4--;

                                    if (select4 < 0)
                                    {
                                        select4 = collection.Count - 1;
                                    }
                                }
                                else if (key4.Key == ConsoleKey.Enter)
                                {


                                    string delete = "DELETE FROM Products WHERE Id = @Id";

                                    var param = new
                                    {
                                        Id = collection[select4].Id
                                    };

                                    int rows = await con.ExecuteAsync(delete, param);

                                    if (rows > 0)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        Console.WriteLine("\nDeleteed successfully.");

                                        Console.ResetColor();
                                        collection = (await con.QueryAsync(sql)).ToList();
                                    }

                                    Console.WriteLine("\nPress any key to continue.");
                                    Console.ReadKey(true);
                                }
                            }
                        }
                    }

                    else if (select1 == 3)
                    {
                        string clearList = "DELETE FROM Products";

                        int rows = await con.ExecuteAsync(clearList);

                        if (rows > 0)
                        {
                            collection = (await con.QueryAsync(sql)).ToList();

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("All products deleted successfully.");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nThere are no products to clear.");
                            Console.ResetColor();
                        }

                        Console.WriteLine("\nPress any key to continue.");
                        Console.ReadKey(true);
                    }
                }

            }

        }
    }
}
