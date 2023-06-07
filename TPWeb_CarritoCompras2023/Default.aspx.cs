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
    public partial class _Default : Page
    {
        public List<Producto> listaproducto { get; set; }
        public List<Producto> listacarrito { get; set; }

        public bool filtroAvanzado { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {

            ProductoNegocio negocio = new ProductoNegocio();
            try
            {


                listaproducto = negocio.listar();
                listacarrito = listaproducto;
                Session.Add("Listaproducto", listaproducto);
                filtroAvanzado = chkAvanzado.Checked;

                if (Request.QueryString["id"] != null)
                {
                    Int32 IdArt = Int32.Parse(Request.QueryString["id"]);
                    Session.Add("idArtCarrito", IdArt);

                    Session.Add("items", 1);


                }

            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                throw;
            }


        }

        public string obtenerUrl(Int32 idarticulo)
        {
            try
            {
                ProductoNegocio negocio = new ProductoNegocio();


                List<ImagenArticulo> ListaImagenProducto = negocio.listarImgArt(idarticulo);
                return ListaImagenProducto[0].Imagen;

            }
            catch (Exception)
            {
                string url = "https://efectocolibri.com/wp-content/uploads/2021/01/placeholder.png";

                return url;
            }


        }
        protected void filtro_textChanged(object sender, EventArgs e)
        {
            try
            {

                listaproducto = (List<Producto>)Session["Listaproducto"];
                listaproducto = listaproducto.FindAll(x => x.Nombre.ToUpper().Contains(txtFiltro.Text.ToUpper()));


            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                throw;
            }


        }
        protected void chkAvanzado_ChekedChanged(object sender, EventArgs e)
        {
            try
            {


                filtroAvanzado = chkAvanzado.Checked;
                txtFiltro.Enabled = !filtroAvanzado;

            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                throw;
            }


        }

        protected void BtnBuscar_clik(object sender, EventArgs e)
        {
            try
            {


                ProductoNegocio negocio = new ProductoNegocio();
                listaproducto = negocio.filtrar(ddlCampo.SelectedItem.ToString(), ddlCriterio.SelectedItem.ToString(), txtFiltroAvanzado.Text);

            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                throw;
            }
        }
    }
}