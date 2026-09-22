using ConsoleApp39.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp39.Models;

public class Product : Entity
{
    public string ProductName { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }

    //FK
    public int CategoryId { get; set; }

    //NP
    public Category Category { get; set; }
}
