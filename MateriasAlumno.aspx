<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MateriasAlumno.aspx.cs" Inherits="TutoriasWeb.MateriasAlumno" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .item{
            margin-top:10px;
            padding-left:20px;
            align-items:start;
            align-content:baseline;
        }
    </style>
    <script type="text/javascript">
        function Confirmacion() {

            var seleccion = confirm("¿Seguro que quiere finalizar? una vez finalizado no pueden haber cambios");
            /*
            if (seleccion)
                alert("se acepto el mensaje");
            else
                alert("NO se acepto el mensaje");
                */

            //usado para que no haga postback el boton de asp.net cuando 
            //no se acepte el confirm
            return seleccion;

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server" >
    <div class="row col-12 justify-content-center">
        <div class="item form-inline">
            <label>Semestre:</label>
            <asp:DropDownList runat="server" ID="ddSemestre" OnSelectedIndexChanged="ddSemestre_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" >
                <asp:ListItem Text="Selecciona un semetre" Value="" />    
                <asp:ListItem Text="1" />     
                <asp:ListItem Text="2" />     
                <asp:ListItem Text="3" />     
                <asp:ListItem Text="4" />     
                <asp:ListItem Text="5" />     
                <asp:ListItem Text="6" />     
                <asp:ListItem Text="7" />     
                <asp:ListItem Text="8" />     
                <asp:ListItem Text="9" />    
            </asp:DropDownList>                     
        </div>   
        <div class="row col-12 justify-content-around" runat="server" id="tablas" visible="false">
            <div>
                <div class="row col-12 justify-content-center">
                    <label>Materias a elegir</label>
                </div>
                <asp:GridView ID="gvMaterias" runat="server" OnSelectedIndexChanging="gvMaterias_SelectedIndexChanging" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed table-responsive table-hover " OnRowCommand="gvMaterias_RowCommand" >
                    <Columns>                        
                        <asp:BoundField DataField="ID" HeaderText="ID" Visible="false" />
                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                        <asp:BoundField DataField="Nombre_Corto" HeaderText="Clave" />
                        <asp:TemplateField ShowHeader="false">
                            <ItemTemplate>
                                <asp:ImageButton CommandArgument='<%# Eval("ID") %>' ID="add" runat="server" CommandName="add" Width="25px" ImageUrl="data:image/png;base64,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"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                    </Columns>
                </asp:GridView>                
            </div>
            <div>
                <div class="row col-12 justify-content-center">
                    <label>Materias a cursar</label>
                </div>
                <asp:GridView ID="gvSeleccionadas" runat="server" OnRowCommand="gvSeleccionadas_RowCommand" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed table-responsive table-hover "  >
                    <Columns>                        
                        <asp:BoundField DataField="ID" HeaderText="ID" />
                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                        <asp:BoundField DataField="Nombre_Corto" HeaderText="Clave" />
                        <asp:TemplateField ShowHeader="false">
                            <ItemTemplate>
                                <asp:ImageButton CommandArgument='<%# Eval("ID") %>' ID="delete" runat="server" CommandName="delete" Width="25px" ImageUrl="data:image/png;base64,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"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>

        <div class="row col-12 justify-content-center">
            <asp:Button Text="Aceptar" runat="server" ID="btnAceptar" OnClientClick="Confirmacion();" OnClick="btnAceptar_Click" CssClass="btn btn-primary" Visible="false"/>
        </div>
        
        
         
        
        
        
    </div>
    
</asp:Content>
