using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class carritoclass
    {
        public List<ItemsCarrito> lista { set; get; }
        public decimal totalCarrito(carritoclass carrito)
        {
            decimal total = 0;
            foreach (ItemsCarrito item in carrito.lista)
            {

                total +=Convert.ToDecimal( item.Producto.Precio * item.Cantidad);
            }
            return total;
        }

    }
}
