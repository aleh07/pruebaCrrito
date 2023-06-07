using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace TPWeb_CarritoCompras2023
{
    public partial class Detalle : System.Web.UI.Page
    {
        
        public List<Producto> listaproducto { get; set; }
        public Producto productoSeleccionado { get; set; }

        public Marca marcaSeleccionada { get; set; }
        public List<ImagenArticulo> listaImagenes { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                string ID = Request.QueryString["id"];
                int id = Int32.Parse(ID);

                ProductoNegocio negocio = new ProductoNegocio();

                productoSeleccionado = negocio.ObtenerProducto(id);
                Session.Add("producto", productoSeleccionado);

                Int32 IdArt = Int32.Parse(Request.QueryString["id"]);
                List<ImagenArticulo> Lista = negocio.listarImgArt(IdArt);
                listaImagenes = Lista;

                MarcaNegocio Marca = new MarcaNegocio();
                marcaSeleccionada = Marca.obtenerMarca(id);
                Session.Add("Marca", marcaSeleccionada);


            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                throw;
            }


        }

        public string obtenerUrl(int id)
        {
            try
            {
                ProductoNegocio negocio = new ProductoNegocio();
                List<ImagenArticulo> ListaImagenProducto = negocio.listarImgArt(id);
                return ListaImagenProducto[0].Imagen;



            }
            catch (Exception)
            {
                string url = "https://efectocolibri.com/wp-content/uploads/2021/01/placeholder.png";

                return url;
            }


        }

    }
}