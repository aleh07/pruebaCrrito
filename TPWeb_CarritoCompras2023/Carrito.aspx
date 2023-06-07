<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="Carrito.aspx.cs" Inherits="TPWeb_CarritoCompras2023.Carrito" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
     <table class="table table-striped">
  
    <thead>
     <tr>
      <th scope="col">Nombre</th>
      <th scope="col">Precio</th>
      <th scope="col">Cantidad</th>
    </tr>
       </thead>
    

     <tbody>
        
        <asp:Repeater runat="server" ID="repetidorCarrito">
            <ItemTemplate>
                     <tr>
                     <th><p><%#Eval("Producto.Nombre")%></p></th>
                     <th> <p><%#Eval("Producto.Precio")%></p></th>
                     <th>
                  <asp:TextBox TextMode="Number" runat="server" OnTextChanged="txtCantidad_TextChanged" text='<%#Eval("Cantidad")%>' ID="txtCantidad" min="1"/>
                        <asp:Button Text="Agregar" CssClass="btn btn-primary"  ID="btnAgregar" AutoPostBack="true" OnClick="btnAgregar_Click"  CommandArgument='<%#Eval("Producto.Id")%>' runat="server" />
                    
                    <asp:Button Text="Eliminar" CssClass="btn btn-danger"  ID="btnEliminar" AutoPostBack="true" OnClick="btnEliminar_Click" CommandArgument='<%#Eval("Producto.Id")%>' runat="server" />
                 </th>
                </tr>
            </ItemTemplate> 
        </asp:Repeater>
          
    </tbody>
    </table>
<p>Total: <asp:Label ID="lblTotal" runat="server" OnLoad="lblTotal_Load" /></p>
         
   
</asp:Content>
