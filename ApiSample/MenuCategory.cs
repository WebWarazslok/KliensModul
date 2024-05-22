using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiSample
{
    public class MenuCategory
    {
        public int MenuProductsID { get; set; }     
        public string Category { get; set; }    
        public string Day { get; set; }     
        public int OptionsID { get; set; }    
        public decimal Price { get; set; }     
        public string ProductName { get; set; }     
        public int Quantity { get; set; }     
        public int SKU { get; set; }
    }
}
