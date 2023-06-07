using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
   public class ItemsCarrito
    {
        public int Cantidad { get; set; }
        public Producto Producto { get; set; }
        public decimal SubTotal { get; set; }

    }
}
