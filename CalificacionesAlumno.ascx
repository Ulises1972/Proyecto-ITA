<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CalificacionesAlumno.ascx.cs" Inherits="TutoriasWeb.CalificacionesAlumno1" %>


<script src="https://cdn.jsdelivr.net/npm/jquery@3.6.0/dist/jquery.slim.min.js"></script>
<script src="https://cdn.jsdelivr.net/npm/popper.js@1.16.1/dist/umd/popper.min.js"></script>
<script src="https://cdn.jsdelivr.net/npm/bootstrap@4.6.1/dist/js/bootstrap.bundle.min.js"></script>

<script type="text/javascript">

    $(document).ready(function () {
        $("#btn_a").click(function () {
            $("#mdl").modal();
            </div>
        </div>
    </div>
</asp:Content>
        });
    });

    function openModal() {
        $(document).ready(function () {
            $("#mdl").modal();
        })
    };
</script>


    <div class="col-lg-12 form-inline justify-content-center"  >
        <div style="width: 90%; margin-left:100px;">
        <div runat="server" id="add" >
            <button type="button" class="btn btn-primary" id="btn_a">Agregar</button>
        </div>

            <div class="row col-12 justify-content-start" width="350px">
                  <asp:GridView ID="GridViewAlumnos" styele="width:350px;" runat="server" AutoGenerateColumns="false"
                    CssClass="table table-striped table-hover table-bordered table-m text-center shadow rounded font "
                    HeaderStyle-BackColor="#003366"
                    HeaderStyle-ForeColor="White"
                    HeaderStyle-Font-Bold="true"
                    HeaderStyle-Height="40px"
                    ShowHeader="true"
                    OnRowCommand="GridViewAlumnos_RowCommand">

                    <headerstyle cssclass="rounded-top" />


                      <columns>

                          <asp:TemplateField>
                              <itemtemplate >
                                  <div style="position: absolute; left: 20px; margin-top: -10px;  border-top-left-radius: 12px;">
                                      <div>
                                          <asp:ImageButton
                                              CssClass="rounded-start "
                                              Style="margin-right: 80px;"
                                              Width="17px"
                                              CommandName="Eliminar"
                                              CommandArgument='<%# Eval("No_control") %>'
                                              runat="server"
                                              ImageUrl="https://cdn-icons-png.flaticon.com/512/1214/1214428.png"
                                              OnClientClick="return confirm('¿Eliminar este instituto?');" />
                                      </div>

                                      <asp:TemplateField HeaderText="Editar">
                                          <itemtemplate>
                                              <asp:ImageButton
                                                  Style="margin-right: 80px;"
                                                  Width="17px"
                                                  CommandName="Editar"
                                                  CommandArgument='<%# Eval("No_control") %>'
                                                  runat="server"
                                                  ImageUrl="https://cdn-icons-png.flaticon.com/512/1159/1159633.png" />
                                          </itemtemplate>
                                      </asp:TemplateField>
                                  </div>
                            </itemtemplate>
                          </asp:TemplateField>

                          <asp:BoundField DataField="No_control" HeaderText="No. Control" HeaderStyle-Width="10px" ItemStyle-Width="100px" />
                          <asp:BoundField DataField="NombreCompleto" HeaderText="Nombre Completo" />
                          <asp:BoundField DataField="Semestre" HeaderText="Semestre" HeaderStyle-Width="80px" ItemStyle-Width="10px" />


                          <asp:TemplateField HeaderText="Unidad 1">
                              <itemtemplate >
                                  <%# Eval("u1_1").Equals(Eval("u1_2")) ? Eval("u1_1") : (Eval("u1_1") + ", " + Eval("u1_2")) %>
                              </itemtemplate>
                               <ItemStyle Width="5px" />
                          </asp:TemplateField>
                          <asp:TemplateField HeaderText="Unidad 2">
                              <itemtemplate>
                                  <%# Eval("u2_1").Equals(Eval("u2_2")) ? Eval("u2_1") : (Eval("u2_1") + ", " + Eval("u2_2")) %>
                              </itemtemplate>
                              <ItemStyle Width="5px" />
                          </asp:TemplateField>
                          <asp:TemplateField HeaderText="Unidad 3">
                              <itemtemplate>
                                  <%# Eval("u3_1").Equals(Eval("u3_2")) ? Eval("u3_1") : (Eval("u3_1") + ", " + Eval("u3_2")) %>
                              </itemtemplate>
                              <ItemStyle Width="5px" />
                          </asp:TemplateField>
                          <asp:TemplateField HeaderText="Unidad 4">
                              <itemtemplate>
                                  <%# Eval("u4_1").Equals(Eval("u4_2")) ? Eval("u4_1") : (Eval("u4_1") + ", " + Eval("u4_2")) %>
                              </itemtemplate>
                              <ItemStyle Width="5px" />
                          </asp:TemplateField>
                           <asp:TemplateField HeaderText="Unidad 5">
                               <itemtemplate>
                                   <%# Eval("u5_1").Equals(Eval("u5_2")) ? Eval("u5_1") : (Eval("u5_1") + ", " + Eval("u5_2")) %>
                               </itemtemplate>
                               <ItemStyle Width="5px" />
                           </asp:TemplateField>
                          <asp:TemplateField HeaderText="Unidad 6">
                              <itemtemplate>
                                  <%# Eval("u6_1").Equals(Eval("u6_2")) ? Eval("u6_1") : (Eval("u6_1") + ", " + Eval("u6_2")) %>
                              </itemtemplate>
                              <ItemStyle Width="5px" />
                          </asp:TemplateField>

                          <asp:TemplateField HeaderText="Unidad 7">
                              <itemtemplate>
                                  <%# Eval("u7_1").Equals(Eval("u7_2")) ? Eval("u7_1") : (Eval("u7_1") + ", " + Eval("u7_2")) %>
                              </itemtemplate>
                              <itemstyle width="5px" />
                          </asp:TemplateField>

                          <asp:TemplateField HeaderText="Unidad 8">
                              <itemtemplate>
                                  <%# Eval("u8_1").Equals(Eval("u8_2")) ? Eval("u8_1") : (Eval("u8_1") + ", " + Eval("u8_2")) %>
                              </itemtemplate>
                              <itemstyle width="5px" />
                          </asp:TemplateField>
                       



                          <asp:BoundField DataField="Promedio" HeaderText="Promedio" DataFormatString="{0:N2}" />


                          
                    </columns>
                </asp:GridView>
            </div>
    </div>
    </div>


    <style>
        .custom-header th {
            background-color: #003366 !important;
            color: white !important;
            border-top-left-radius: 12px;
            border-top-right-radius: 12px;
        }
        .font{
            font-size:13px;
        }

        /* Opcional: mejora el borde redondeado visible */
        .table thead tr:first-child th:first-child {
            border-top-left-radius: 12px;
        }

        .table thead tr:first-child th:last-child {
            border-top-right-radius: 12px;
        }

        .rounded-start {
            border-top-left-radius: 12px;
            border-bottom-left-radius: 12px;
        }
       

        .rounded-end {
            border-top-right-radius: 12px;
            border-top-left-radius: 12px;
    </style>



    <!-- The Modal Materia_Alumno-->
    <div class="modal fade" id="mdl">
        <div class="modal-dialog">
            <div class="modal-content">

              

                <div class="modal-body">
                    <div class="col-12">
                        <div class="form-group">
                            <asp:Label ID="ID" Text="ID" runat="server" Visible="false" />
                            
                            <div class="form-group">
                                <label>Seleccionar Alumno</label>
                                <asp:DropDownList ID="No_COntrol" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                    <asp:ListItem Value="" />
                                </asp:DropDownList>
                            </div>
                            
                            <br />
                       <div class="form-group">
    <label>Calificaciones</label>
    
    <!-- Unidad 1 a 8 -->
    <div class="row">
        <div class="col-12 col-md-6 mb-2">
            <label>Unidad 1</label>
            <div class="form-row">
                <div class="col">
                    <label>1</label>
                    <asp:TextBox ID="TextBox" runat="server" CssClass="form-control text-uppercase" />
                </div>
                <div class="col">
                    <label>2</label>
                    <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control text-uppercase" />
                </div>
            </div>
        </div>
        <div class="col-12 col-md-6 mb-2">
            <label>Unidad 2</label>
            <div class="form-row">
                <div class="col">
                    <label>1</label>
                    <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control text-uppercase" />
                </div>
                <div class="col">
                    <label>2</label>
                    <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control text-uppercase" />
                </div>
            </div>
        </div>


        <div class="col-12 col-md-6 mb-2">
            <label>Unidad 3</label>
            <div class="form-row">
                <div class="col">
                    <label>1</label>
                    <asp:TextBox ID="TextBox4" runat="server" CssClass="form-control text-uppercase" />
                </div>
                <div class="col">
                    <label>2</label>
                    <asp:TextBox ID="TextBox5" runat="server" CssClass="form-control text-uppercase" />
                </div>
            </div>
        </div>

        <div class="col-12 col-md-6 mb-2">
            <label>Unidad 4</label>
            <div class="form-row">
                <div class="col">
                    <label>1</label>
                    <asp:TextBox ID="TextBox6" runat="server" CssClass="form-control text-uppercase" />
                </div>
                <div class="col">
                    <label>2</label>
                    <asp:TextBox ID="TextBox7" runat="server" CssClass="form-control text-uppercase" />
                </div>
            </div>
        </div>

        <div class="col-12 col-md-6 mb-2">
            <label>Unidad 5</label>
            <div class="form-row">
                <div class="col">
                    <label>1</label>
                    <asp:TextBox ID="TextBox8" runat="server" CssClass="form-control text-uppercase" />
                </div>
                <div class="col">
                    <label>2</label>
                    <asp:TextBox ID="TextBox9" runat="server" CssClass="form-control text-uppercase" />
                </div>
            </div>
        </div>

        <div class="col-12 col-md-6 mb-2">
            <label>Unidad 6</label>
            <div class="form-row">
                <div class="col">
                    <label>1</label>
                    <asp:TextBox ID="TextBox10" runat="server" CssClass="form-control text-uppercase" />
                </div>
                <div class="col">
                    <label>2</label>
                    <asp:TextBox ID="TextBox11" runat="server" CssClass="form-control text-uppercase" />
                </div>
            </div>
        </div>

        <div class="col-12 col-md-6 mb-2">
            <label>Unidad 7</label>
            <div class="form-row">
                <div class="col">
                    <label>1</label>
                    <asp:TextBox ID="TextBox12" runat="server" CssClass="form-control text-uppercase" />
                </div>
                <div class="col">
                    <label>2</label>
                    <asp:TextBox ID="TextBox13" runat="server" CssClass="form-control text-uppercase" />
                </div>
            </div>
        </div>

        <div class="col-12 col-md-6 mb-2">
            <label>Unidad 8</label>
            <div class="form-row">
                <div class="col">
                    <label>1</label>
                    <asp:TextBox ID="TextBox14" runat="server" CssClass="form-control text-uppercase" />
                </div>
                <div class="col">
                    <label>2</label>
                    <asp:TextBox ID="TextBox15" runat="server" CssClass="form-control text-uppercase" />
                </div>
            </div>
        </div>

    </div>
</div>
                            <div class="modal-footer justify-content-center">
                                <asp:Button Text="Cancelar" runat="server" ID="Button1" CssClass="btn btn-primary" Width="200" Visible="false" />
                                <asp:Button ID="Button2" Text="Agregar" runat="server" CssClass="btn btn-primary" Width="200" onClick="Button2_Click" />
                                <asp:Button ID="btnActualizar" Text="Actualizar" runat="server" CssClass="btn btn-success" Width="200" OnClick="btnActualizar_Click" Visible="false" /> 
                            </div>

                        </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
