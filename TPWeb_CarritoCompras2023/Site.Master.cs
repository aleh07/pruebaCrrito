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
    public partial class SiteMaster : MasterPage
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
                    }

                    Session["idArtCarrito"] = ""; // Reiniciar el ID de artículo en la sesión
                    Session["carrito"] = carrito;
                }

                //repetidorCarrito.DataSource = carrito.lista;
                //repetidorCarrito.DataBind();

                //lblTotal.Text = carrito.totalCarrito(carrito).ToString();


                if (!IsPostBack)
                {

                    if (Session["ItemCount"] == null)
                    {
                        Session["ItemCount"] = 0;
                    }
                }

                string a = Convert.ToString(Session["items"]);
                if (string.IsNullOrEmpty(a))
                {
                    Session["a"] = 0;
                }
                else
                {
                    int c = Convert.ToInt32(Session["items"]);
                    int d = Convert.ToInt32(Session["a"]);
                    int cantItems = d + c;


                    if (c > 0)
                    {
                        if (Session["ItemCount"] != null)
                        {
                            int itemCount;
                            if (int.TryParse(Session["ItemCount"].ToString(), out itemCount))
                            {
                                itemCount += c;
                                Session["ItemCount"] = itemCount;
                            }
                            else
                            {
                                // Manejo del error de conversión
                            }
                        }
                        else
                        {
                            Session["ItemCount"] = c;
                        }
                        Session["items"] = 0;
                    }

                    LblItems.Text = Session["ItemCount"].ToString();
                    Session["a"] = cantItems;

                }

            }
            catch (Exception ex)
            {

                Session.Add("error", ex);
                throw;
            }
        }
    }

}
