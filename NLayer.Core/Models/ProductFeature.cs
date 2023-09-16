using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Models
{
    public class ProductFeature
    {
        public int ID { get; set; }
        public string Color { get; set; }
        public int Height { get; set; }
        public int Widht { get; set; }


        public int ProductID { get; set; }
        public Product Product { get; set; }
    }
}
