using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs
{
    public abstract class BaseDTO
    {
        public int ID { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
