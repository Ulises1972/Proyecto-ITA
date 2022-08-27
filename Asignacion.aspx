<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Asignacion.aspx.cs" Inherits="TutoriasWeb.Asignacion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="col-lg-12 form-inline">
        <div class="row col-10 justify-content-center" style="margin-bottom:15px; margin-top:15px;">
            <div class=" col-6 form-inline justify-content-around"> 
                <asp:DropDownList ID="g_grado" runat="server" CssClass="form-control" OnSelectedIndexChanged="g_grado_SelectedIndexChanged" AutoPostBack="true" >
                    <asp:ListItem Text="Seleccionar grado" />
                    <asp:ListItem Text="1°" Value="0"/>
                    <asp:ListItem Text="2°" Value="1"/>
                    <asp:ListItem Text="3°" Value="2"/>
                </asp:DropDownList> 
                <asp:DropDownList ID="g_grupo" runat="server" CssClass="form-control" />
                <asp:Button ID="Btn_add" OnClick="Btn_add_Click" Text="Agregar" runat="server" CssClass="btn btn-primary"/>
            </div>
        </div>

        <div class="row col-12 justify-content-start">
            

            <asp:GridView ID="GridView1" Visible="false" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed table-responsive table-hover "  >
                <Columns>
                    <asp:BoundField DataField="No_control" HeaderText="No. Control" />
                    <asp:BoundField DataField="A_Paterno" HeaderText="Apellido Paterno" />
                    <asp:BoundField DataField="A_Materno" HeaderText="Apellido Materno" />
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre(s)" />
                    <asp:BoundField DataField="Semestre" HeaderText="Semestre" />
                    <asp:BoundField DataField="Carrera" HeaderText="Carrera" />
                    <asp:BoundField DataField="Tutoria" HeaderText="Tutoria" />
                    <asp:TemplateField ShowHeader="false">
                        <ItemTemplate>
                            <asp:CheckBox ID="checkBox" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
                    
                
        </div>
    </div>

</asp:Content>
