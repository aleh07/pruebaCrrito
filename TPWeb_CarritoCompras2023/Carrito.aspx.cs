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
    public partial class Carrito : System.Web.UI.Page
    {
        public List<ItemsCarrito> Listacarrito { set; get; }
        public carritoclass carrito = new carritoclass();
        Producto producto = new Producto();
        ItemsCarrito item = new ItemsCarrito();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {


                string id = Convert.ToString(Session["idArtCarrito"]);
                carrito = (carritoclass)Session["carrito"];
                if (carrito == null) carrito = new carritoclass();
                if (carrito.lista == null) carrito.lista = new List<ItemsCarrito>();

                if (id != "")
                {
                    ItemsCarrito item = carrito.lista.Find(x => x.Producto.Id.ToString() == id);
                    if (item == null)
                    {
                        List<Producto> listado = (List<Producto>)Session["Listaproducto"];
                        producto = listado.Find(x => x.Id.ToString() == id);
                        item = new ItemsCarrito(); // Crear una nueva instancia de ItemsCarrito
                        item.SubTotal = Convert.ToDecimal(producto.Precio);
                        item.Producto = producto;
                        item.Cantidad = 1;
                        carrito.lista.Add(item);
                    }
                    else
                    {
                        //item.SubTotal += item.SubTotal;
                        item.Cantidad++;
                        Session["carrito"] = carrito;
                    }
                    Session["idArtCarrito"] = ""; // Reiniciar el ID de artículo en la sesión
                    Session["carrito"] = carrito;
                }

                repetidorCarrito.DataSource = carrito.lista;
                repetidorCarrito.DataBind();

                lblTotal.Text = carrito.totalCarrito(carrito).ToString();

            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                throw;
            }

        }


        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                var cantidadTextBox = ((Button)sender).Parent.FindControl("txtCantidad") as TextBox;
                var cantidad = cantidadTextBox.Text;
                if (!string.IsNullOrEmpty(cantidad))
                {
                    var argument = ((Button)sender).CommandArgument;
                    carrito = (carritoclass)Session["carrito"];
                    ItemsCarrito item1 = carrito.lista.Find(x => x.Producto.Id.ToString() == argument);
                    item1.Cantidad = int.Parse(cantidad);
                    Session["carrito"] = carrito;
                    repetidorCarrito.DataSource = carrito.lista;
                    repetidorCarrito.DataBind();
                    lblTotal.Text = carrito.totalCarrito(carrito).ToString();
                }

            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                throw;
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                var argument = ((Button)sender).CommandArgument;
                carrito = (carritoclass)Session["carrito"];
                ItemsCarrito item1 = carrito.lista.Find(x => x.Producto.Id.ToString() == argument);
                carrito.lista.Remove(item1);
                Session["carrito"] = carrito;

                repetidorCarrito.DataSource = carrito.lista;
                repetidorCarrito.DataBind();
                lblTotal.Text = carrito.totalCarrito(carrito).ToString();

                // Actualizar el valor de "ItemCount" en la sesión
                int itemCount = carrito.lista.Sum(x => x.Cantidad);
                Session["ItemCount"] = itemCount.ToString();
            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                throw;
            }
        }
        protected void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var cantidad = ((TextBox)sender).Text;
                //lblTotal.Text = cantidad;
            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                throw;
            }
        }
        protected void lblTotal_Load(object sender, EventArgs e)
        {

        }

    }
}