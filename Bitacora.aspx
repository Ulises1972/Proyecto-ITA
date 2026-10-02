<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Bitacora.aspx.cs" Inherits="TutoriasWeb.Bitacora" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="col-lg-12 form-inline">

        <div class="row col-10 justify-content-center" style="margin-bottom:15px; margin-top:15px;">
            <div class=" col-6 form-inline justify-content-around">
                <asp:TextBox runat="server" CssClass="form-control" Width="400"></asp:TextBox>
                <asp:Button runat="server" Text="Buscar" CssClass="btn btn-primary" Width="100"/>
            </div>
        </div>
            
                
        <div class="row col-12 justify-content-start">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed table-responsive table-hover "  >
                <Columns>
                    <asp:BoundField DataField="USUARIO" HeaderText="USUARIO" />
                    <asp:BoundField DataField="TABLA" HeaderText="TABLA" />
                    <asp:BoundField DataField="ACCION" HeaderText="ACCION" />
                    <asp:BoundField DataField="REGISTRO" HeaderText="REGISTRO" />
                    <asp:BoundField DataField="NOMBRE" HeaderText="NOMBRE" />
                    <asp:BoundField DataField="DESCRIPCION" HeaderText="DESCRIPCION" />
                    <asp:BoundField DataField="FECHA_HORA" HeaderText="FECHA/HORA" />
                </Columns>
            </asp:GridView>
        </div>
    </div>

    

</asp:Content>
