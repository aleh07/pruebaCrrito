<%@ Page Title="Inicio" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TPWeb_CarritoCompras2023._Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
     <asp:UpdatePanel ID="updatePanel" runat="server">
        <ContentTemplate>
           <div class="row">
            <div class="mb-3">
             <asp:TextBox runat="server" ID="txtFiltro" class="btn btn-outline-success" AutoPostBack="true" OnTextChanged="filtro_textChanged" />
             <asp:Label Text="Buscar por Nombre" runat="server" />
            </div>
          </div>
          <div class ="col-6" style="display:flex; flex-direction:column;justify-content:flex-end">
          <br />
           <div class="mb-3">
            <asp:checkbox Text="Filtro Avanzado"  class="btn btn-outline-success"  ID="chkAvanzado" runat="server" AutoPostBack="true" OnCheckedChanged="chkAvanzado_ChekedChanged" />
           </div>
        </div>
            <%if (filtroAvanzado)
                {%>

               <div class="row">
                <div class="col-3">
                    <div class="mb-3">
                        <asp:Label Text="Buscar por" runat="server" />
                        <asp:DropDownList runat="server" class="btn btn-outline-success" ID="ddlCampo" AutoPostBack="true">
                            <asp:ListItem Text="Nombre" />
                            <asp:ListItem Text="Marca" />
                            <asp:ListItem Text="Codigo" />
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-3">
                    <div class="mb-3">
                        <asp:Label Text="Criterio" runat="server" />
                        <asp:DropDownList runat="server" class="btn btn-outline-success" ID="ddlCriterio" AutoPostBack="true">
                            <asp:ListItem Text="Contiene" />
                            <asp:ListItem Text="Comienza con" />
                            <asp:ListItem Text="Termina con" />
                        </asp:DropDownList>
                    </div>
                </div>
            
                <div class="col-3">
                    <div class="mb-3">
                        <asp:Label Text="Filtro" runat="server" />
                        <asp:TextBox runat="server" class="btn btn-outline-success" ID="txtFiltroAvanzado" />
                    </div>
                </div>
                <div class="col-3">
                    <div class="mb-3">
                        <asp:Button Text="Buscar Producto" runat="server" CssClass="btn btn-success" ID="btnBuscar" OnClick="BtnBuscar_clik" />
                    </div>
                </div>
                </div>
        
                <div class="row row-cols-1 row-cols-md-3 g-4">
                    <% 
                        foreach (Dominio.Producto producto in listaproducto)
                        {
                    %>
                    
                    <div class="col">
                    <div class="shadow-lg p-3 mb-5 bg-body-tertiary rounded">
                       <div class="cards"  >                        
                            <img src="<%:obtenerUrl(producto.Id)%>" class="card-img-top" alt=".Imagen del producto">
                            <div class="card-body">
                                <div class="card-info">
                                <h5 class="card-title"><%:producto.Nombre%></h5>
                                <p class="card-text"> $<%:producto.Precio %></p>
                                 </div>
                                 <div class="columnas">
                                  <a href="Detalle.aspx?id=<%:producto.Id %>" class="btn btn-success">Ver Detalle</a>
                                  <a href="Default.aspx?id=<%:producto.Id %>" class="btn btn-success">Carrito</a>                           
                              </div>
                             </div>
                            </div>
                        </div>
                    </div>
                  
                    <%  }%>
               </div>
            <%}%>    
    <%else
      {
    %>
    <div class="row row-cols-1 row-cols-md-3 g-4">
        <% 
            foreach (Dominio.Producto producto in listaproducto)
            {
        %> 
        
        <div class="col">
           <div class="shadow-lg p-3 mb-5 bg-body-tertiary rounded">
            <div class="card">
                <img src="<%:obtenerUrl(producto.Id)%>" class="card-img-top" alt=".Imagen del producto">
                <div class="card-body">
                    <h5 class="card-title"><%:producto.Nombre%></h5>
                    <p class="card-text"> $<%:producto.Precio %></p>
                     <div class="columnas">
                    <a href="Detalle.aspx?id=<%:producto.Id %>" class="btn btn-success">Ver Detalle</a>
                    <a href="Default.aspx?id=<%:producto.Id %>" class="btn btn-success">Carrito</a>
                    </div>
                </div>
            </div>
            </div>
         </div>
          <%}%>
     </div>
    <%}%>    
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
