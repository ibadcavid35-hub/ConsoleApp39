using ConsoleApp39.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp39.Models;

public class Category : Entity
{
    public string CategoryName { get; set; }

    //NP
    public ICollection<Product> Products { get; set; }
}
