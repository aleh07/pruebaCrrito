using Dominio;
using System;
using System.Collections.Generic;

namespace Negocio
{
    public class ProductoNegocio
    {
        //FUNCIONES PARA PRODUCTOS
        public List<Producto> listar()
        {
            List<Producto> lista = new List<Producto>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT A.id,A.codigo, A.Nombre, A.descripcion,A.IdMArca,A.IdCategoria,M.descripcion as marca , C.descripcion as categoria, a.precio from ARTICULOS A, MARCAS M, CATEGORIAS C  where A.IdMarca = M.Id and C.Id = A.IdCategoria ");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Producto aux = new Producto();
                    aux.Id = (Int32)datos.Lector["Id"];
                    if (!(datos.Lector.IsDBNull(datos.lector.GetOrdinal("codigo"))))
                        aux.CodArtículo = (string)datos.Lector["codigo"];
                    if (!(datos.Lector.IsDBNull(datos.lector.GetOrdinal("Nombre"))))
                        aux.Nombre = (string)datos.Lector["Nombre"];
                    if (!(datos.Lector.IsDBNull(datos.lector.GetOrdinal("Descripcion"))))
                        aux.Descripción = (string)datos.Lector["Descripcion"];
                    if (!(datos.Lector.IsDBNull(datos.lector.GetOrdinal("precio"))))
                    {
                        decimal DosDecimal;
                        DosDecimal = (decimal)datos.Lector["precio"];
                        aux.Precio = float.Parse(DosDecimal.ToString("0.00"));
                    }
                    aux.marca = new Marca();
                    aux.marca.Id = (Int32)datos.Lector["IdMarca"];
                    if (!(datos.Lector.IsDBNull(datos.lector.GetOrdinal("marca"))))
                        aux.marca.Nombre = (string)datos.Lector["marca"];
                    aux.categoria = new Categoria();
                    if (!(datos.Lector.IsDBNull(datos.lector.GetOrdinal("categoria"))))
                        aux.categoria.Nombre = (string)datos.Lector["categoria"];
                    aux.categoria.Id = (Int32)datos.Lector["IdCategoria"];
                    aux.Estado = true;
                    lista.Add(aux);
                }

                return lista;

            }



            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public List<Producto> listarConSP() 
        {
            List<Producto> lista = new List<Producto>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearProcedimiento("StoredListar");
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Producto aux = new Producto();
                    aux.Id = (Int32)datos.Lector["Id"];
                    if (!(datos.Lector.IsDBNull(datos.lector.GetOrdinal("codigo"))))
                        aux.CodArtículo = (string)datos.Lector["codigo"];
                    if (!(datos.Lector.IsDBNull(datos.lector.GetOrdinal("Nombre"))))
                        aux.Nombre = (string)datos.Lector["Nombre"];
                    if (!(datos.Lector.IsDBNull(datos.lector.GetOrdinal("Descripcion"))))
                        aux.Descripción = (string)datos.Lector["Descripcion"];
                    if (!(datos.Lector.IsDBNull(datos.lector.GetOrdinal("precio"))))
                    {
                        decimal DosDecimal;
                        DosDecimal = (decimal)datos.Lector["precio"];
                        aux.Precio = float.Parse(DosDecimal.ToString("0.00"));
                    }
                    aux.marca = new Marca();
                    aux.marca.Id = (Int32)datos.Lector["IdMarca"];
                    if (!(datos.Lector.IsDBNull(datos.lector.GetOrdinal("marca"))))
                        aux.marca.Nombre = (string)datos.Lector["marca"];
                    aux.categoria = new Categoria();
                    if (!(datos.Lector.IsDBNull(datos.lector.GetOrdinal("categoria"))))
                        aux.categoria.Nombre = (string)datos.Lector["categoria"];
                    aux.categoria.Id = (Int32)datos.Lector["IdCategoria"];

                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Int32 UltimoId()
        {

            Int32 UltimoId = new Int32();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(" select MAX(id) as IdArticulo from articulos");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Int32 aux = new Int32();

                    aux = (Int32)datos.Lector["IdArticulo"];
                    UltimoId = aux;

                }
                return UltimoId;


            }



            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public void agregar(Producto nuevo, ImagenArticulo ImagenNueva)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("Insert into ARTICULOS (Codigo, Nombre, Descripcion,IdMarca, IdCategoria,Precio)values('" + nuevo.CodArtículo + "', '" + nuevo.Nombre + "', '" + nuevo.Descripción + "', @idMarca, @idCategoria, " + nuevo.Precio + ")");
                datos.setearParametro("@idMarca", nuevo.marca.Id);
                datos.setearParametro("@idCategoria", nuevo.categoria.Id);
                datos.ejectutarAccion();
                datos.cerrarConexion();
                datos.setearConsulta("INSERT INTO IMAGENES (IdArticulo,ImagenUrl)values(@IdArt,@ImagenUrl)");
                datos.setearParametro("@ImagenUrl", ImagenNueva.Imagen);
                datos.setearParametro("@IdArt", ImagenNueva.IdProducto);
                datos.ejectutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public void EliminarFisico(Int32 Id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("delete from articulos where id = @id");
                datos.setearParametro("@id", Id);
                datos.ejectutarAccion();
                datos.cerrarConexion();
                datos.setearConsulta("delete from IMAGENES where IdArticulo = @idart");
                datos.setearParametro("@idart", Id);
                datos.ejectutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public void Modificar(Producto producto)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE  Articulos set Codigo =@codigo, Nombre =@nombre, Descripcion=@descripcion, IdMarca= @idMarca, IdCategoria =@idCategoria, Precio = @precio where id = @id");
                datos.setearParametro("@codigo", producto.CodArtículo);
                datos.setearParametro("@nombre", producto.Nombre);
                datos.setearParametro("@descripcion", producto.Descripción);
                datos.setearParametro("@idMarca", producto.marca.Id);
                datos.setearParametro("@idCategoria", producto.categoria.Id);
                datos.setearParametro("@precio", producto.Precio);
                datos.setearParametro("@id", producto.Id);


                datos.ejectutarAccion();
            }


            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }

        }
        public List<Producto> filtrar(string campo, string criterio, string filtro)
        {
            List<Producto> lista = new List<Producto>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = "SELECT A.id,A.codigo, A.Nombre, A.descripcion,A.IdMArca,A.IdCategoria,M.descripcion as marca , C.descripcion as categoria, a.precio from ARTICULOS A, MARCAS M, CATEGORIAS C  where A.IdMarca = M.Id and C.Id = A.IdCategoria and ";

                switch (campo)
                {
                    case "Codigo":
                        switch (criterio)
                        {
                            case "Comienza con":
                                consulta += "A.codigo like'" + filtro + "%'";
                                break;

                            case "Termina con":
                                consulta += "A.codigo like '%" + filtro + "'";
                                break;
                            case "Contiene":
                                consulta += "A.codigo like'%" + filtro + "%'";
                                break;
                        }
                        break;
                    case "Nombre":
                        switch (criterio)
                        {
                            case "Comienza con":
                                consulta += "A.Nombre like'" + filtro + "%'";
                                break;

                            case "Termina con":
                                consulta += "A.Nombre like'%" + filtro + "'";
                                break;
                            case "Contiene":
                                consulta += "A.Nombre like'%" + filtro + "%'";
                                break;
                        }
                        break;
                    case "Marca":
                        switch (criterio)
                        {
                            case "Comienza con":
                                consulta += "M.descripcion like'" + filtro + "%'";
                                break;
                            case "Termina con":
                                consulta += "M.descripcion like'%" + filtro + "'";
                                break;
                            case "Contiene":
                                consulta += "M.descripcion like'%" + filtro + "%'";
                                break;
                        }
                        break;
                    case "Categoria":
                        switch (criterio)
                        {
                            case "Comienza con":
                                consulta += "C.descripcion like'" + filtro + "%'";
                                break;
                            case "Termina con":
                                consulta += "C.descripcion like'%" + filtro + "'";
                                break;
                            case "Contiene":
                                consulta += "C.descripcion like'%" + filtro + "%'";
                                break;
                        }
                        break;
                }
                datos.setearConsulta(consulta);
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Producto aux = new Producto();
                    aux.Id = (Int32)datos.Lector["Id"];
                    if (!(datos.Lector.IsDBNull(datos.lector.GetOrdinal("codigo"))))
                        aux.CodArtículo = (string)datos.Lector["codigo"];
                    if (!(datos.Lector.IsDBNull(datos.lector.GetOrdinal("Nombre"))))
                        aux.Nombre = (string)datos.Lector["Nombre"];
                    if (!(datos.Lector.IsDBNull(datos.lector.GetOrdinal("Descripcion"))))
                        aux.Descripción = (string)datos.Lector["Descripcion"];
                    if (!(datos.Lector.IsDBNull(datos.lector.GetOrdinal("precio"))))
                    {
                        decimal DosDecimal;
                        DosDecimal = (decimal)datos.Lector["precio"];
                        aux.Precio = float.Parse(DosDecimal.ToString("0.00"));
                    }
                    aux.marca = new Marca();
                    aux.marca.Id = (Int32)datos.Lector["IdMarca"];
                    if (!(datos.Lector.IsDBNull(datos.lector.GetOrdinal("marca"))))
                        aux.marca.Nombre = (string)datos.Lector["marca"];
                    aux.categoria = new Categoria();
                    if (!(datos.Lector.IsDBNull(datos.lector.GetOrdinal("categoria"))))
                        aux.categoria.Nombre = (string)datos.Lector["categoria"];
                    aux.categoria.Id = (Int32)datos.Lector["IdCategoria"];

                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public Producto ObtenerProducto(Int32 id)
        {
            Producto aux = new Producto();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT A.Id,A.codigo, A.Nombre, A.descripcion,A.IdMArca,A.IdCategoria,M.descripcion as marca , C.descripcion as categoria, a.precio from ARTICULOS A, MARCAS M, CATEGORIAS C  where A.IdMarca = M.Id and C.Id = A.IdCategoria and A.Id =" + id);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {

                   
                    aux.Id = (Int32)datos.Lector["Id"];
                    if (!(datos.Lector.IsDBNull(datos.lector.GetOrdinal("codigo"))))
                        aux.CodArtículo = (string)datos.Lector["codigo"];
                    if (!(datos.Lector.IsDBNull(datos.lector.GetOrdinal("Nombre"))))
                        aux.Nombre = (string)datos.Lector["Nombre"];
                    if (!(datos.Lector.IsDBNull(datos.lector.GetOrdinal("Descripcion"))))
                        aux.Descripción = (string)datos.Lector["Descripcion"];
                    if (!(datos.Lector.IsDBNull(datos.lector.GetOrdinal("precio"))))
                    {
                        decimal DosDecimal;
                        DosDecimal = (decimal)datos.Lector["precio"];
                        aux.Precio = float.Parse(DosDecimal.ToString("0.00"));
                    }
                    aux.marca = new Marca();
                    aux.marca.Id = (Int32)datos.Lector["IdMarca"];
                    if (!(datos.Lector.IsDBNull(datos.lector.GetOrdinal("marca"))))
                        aux.marca.Nombre = (string)datos.Lector["marca"];
                    aux.categoria = new Categoria();
                    if (!(datos.Lector.IsDBNull(datos.lector.GetOrdinal("categoria"))))
                        aux.categoria.Nombre = (string)datos.Lector["categoria"];
                    aux.categoria.Id = (Int32)datos.Lector["IdCategoria"];



                }

                return aux;

            }



            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }




        //FUNCIONES PARA IMAGENES
        public List<ImagenArticulo> listarImgArt(Int32 id)
        {
            List<ImagenArticulo> lista = new List<ImagenArticulo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("select  i.id as IMG ,a.Codigo, i.IdArticulo , i.ImagenUrl from imagenes as i inner join ARTICULOS as a on a.id = i.IdArticulo  where a.id=" + id);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    ImagenArticulo aux = new ImagenArticulo();
                    aux.producto = new Producto();
                    aux.Id = (Int32)datos.Lector["IMG"];
                    aux.producto.Id = (Int32)datos.Lector["IdArticulo"];
                    aux.producto.Nombre = (string)datos.Lector["Codigo"];
                    aux.Imagen = (string)datos.Lector["ImagenUrl"];


                    lista.Add(aux);
                }

                return lista;

            }



            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public List<ImagenArticulo> listarImagenArticuloConSP(Int32 id)
        {
            List<ImagenArticulo> lista = new List<ImagenArticulo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearProcedimiento("StoredListarImagen");

                while (datos.Lector.Read())
                {
                    ImagenArticulo aux = new ImagenArticulo();
                    aux.producto = new Producto();
                    aux.Id = (Int32)datos.Lector["IMG"];
                    aux.producto.Id = (Int32)datos.Lector["IdArticulo"];
                    aux.producto.Nombre = (string)datos.Lector["Codigo"];
                    aux.Imagen = (string)datos.Lector["ImagenUrl"];


                    lista.Add(aux);
                }

                return lista;

            }



            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public void agregarImg(ImagenArticulo ImagenNueva)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {

                datos.setearConsulta("INSERT INTO IMAGENES (IdArticulo,ImagenUrl)values(@IdArt,@ImagenUrl)");
                datos.setearParametro("@ImagenUrl", ImagenNueva.Imagen);
                datos.setearParametro("@IdArt", ImagenNueva.IdProducto);
                datos.ejectutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public void EliminarFisicoImg(Int32 Id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {

                datos.setearConsulta("delete from IMAGENES where id = @id");
                datos.setearParametro("@id", Id);
                datos.ejectutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public void ModificarImg(ImagenArticulo imagenModificar)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {

                datos.setearConsulta("update IMAGENES set ImagenUrl =@Url where Id =@Id and IdArticulo =@IdArt ");
                datos.setearParametro("@Url", imagenModificar.Imagen);
                datos.setearParametro("@Id", imagenModificar.Id);
                datos.setearParametro("@IdArt", imagenModificar.IdProducto);

                datos.ejectutarAccion();
            }


            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }

        }

   
    }
}

