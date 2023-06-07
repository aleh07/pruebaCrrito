using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
   public class ImagenArticulo
    {
        public Int32 Id { get; set; }
         
       public Producto producto { get; set; }

        public Int32 IdProducto { get; set; }
        public string Imagen { get; set; }
        
    }
}
