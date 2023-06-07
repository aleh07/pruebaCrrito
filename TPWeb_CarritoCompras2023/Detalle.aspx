<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Detalle.aspx.cs" Inherits="TPWeb_CarritoCompras2023.Detalle" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
 <style>
  .custom-section {
    background-color: #413636;
     border-radius: 10px;
  }
  .custom-container h2 {
            color: #ffffff;
            font-size: 24px;
            font-weight: bold;
        }

        .custom-container ul li {
            color: black;
            font-size: 16px;
        }
</style>
    <section class="custom-section">
    <div id="carouselExample" class="carousel slide" data-ride="carousel">
        <div class="carousel-inner">
            <% for (int i = 0; i < listaImagenes.Count; i++)
                { %>
            <div class="carousel-item<%= i == 0 ? " active" : "" %>">
                <img src="<%= listaImagenes[i].Imagen %>" class="d-block mx-auto" style="object-fit: contain;" />
            </div>
            <% } %>
        </div>
        <a class="carousel-control-prev" href="#" role="button" data-slide="prev">
            <span class="carousel-control-prev-icon" aria-hidden="true"></span>
            <span class="sr-only">Anterior</span>
        </a>
        <a class="carousel-control-next" href="#" role="button" data-slide="next">
            <span class="carousel-control-next-icon" aria-hidden="true"></span>
            <span class="sr-only">Siguiente</span>
        </a>
    </div>

    <style>
        .carousel-item img {
            display: block;
            margin: 0 auto;
        }

        .carousel-control-prev,
        .carousel-control-next,
        .carousel-control-prev:visited,
        .carousel-control-next:visited {
            color: whitesmoke;
        }
    </style>

    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script>
        $(document).ready(function () {
            $('.carousel-control-prev').click(function () {
                $('.carousel').carousel('prev');
            });

            $('.carousel-control-next').click(function () {
                $('.carousel').carousel('next');
            });
        });
    </script>
    <style>
  .custom-container {
    margin-left: 4px;
    margin-right: 6px;
    max-width: 700px;
    background-color:#888888;
    border-radius: 10px;

  }

   </style>

    <div class="container custom-container mx-auto">
  <div class="row">
     <div class="col-md-12">
       <% 
           Dominio.Producto producto = productoSeleccionado;
           Dominio.Marca marca = marcaSeleccionada;
        %>
      <h2 class="TituloDetalle" >Características destacadas</h2>
      <ul class="list-group">
        <li class="list-group-item">Código "<%:producto.CodArtículo%>"</li>
        <li class="list-group-item">Nombre "<%:producto.Nombre%>"</li>
        <li class="list-group-item">Descripción "<%:producto.Descripción%>"</li>
        <li class="list-group-item">Marca "<%:producto.marca.Nombre%>"</li>
        <li class="list-group-item">Categoría "<%:producto.categoria.Nombre%>"</li>
      </ul>
    </div>
    
  </div>


        <%  %>

</div>
</section>
</asp:Content>
