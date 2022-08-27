<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TutoriasWeb.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <script src="https://cdn.jsdelivr.net/npm/jquery@3.6.0/dist/jquery.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/popper.js@1.16.1/dist/umd/popper.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.6.1/dist/js/bootstrap.bundle.min.js"></script>
    <script type="text/javascript">
        function openModal() {
            $(document).ready(function () {
                $("#mdl").modal();
            })
        };
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    
    <div> 
        <div class="container py-4">
            <div class="row justify-content-center">
                <div class="col-md-6 col-lg-5 mb-5 mb-lg-0">
                    <div class="card">
                        <div class="card-header bg-color-grey text-1 text-uppercase text-center">
                            Iniciar Sesión
                        </div>
                        <div class="card-body">
                            <div class="row">
                                <div class="form-group col">
                                    <label class="form-label text-color-dark text-3">Usuario</label>
                                    <input runat="server" type="text" id="in_user" size="20" maxlength="20" class="form-control form-control-lg text-4" autocomplete="off" required="required"/> 
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group col">
                                    <label class="form-label text-color-dark text-3">Clave de Acceso</label>
                                    <input runat="server" type="password" size="10" maxlength="20" id="in_pass" class="form-control form-control-lg text-4" autocomplete="off" /> 
                                </div>
                            </div>

                            <div class="row justify-content-end">
                                    <a href="#" class="se">Recuperar Clave de Acceso</a>
                            </div>

                            <div class="row justify-content-center">
                                <div class="col-8 btn">
                                    <asp:Button runat="server" id="btn_login" OnClick="btn_login_Click" Text="Ingresar" CssClass="btn btn-primary btn-block" ></asp:Button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row col-12 justify-content-center">
                    <asp:Button Text="Generar PDF" runat="server" ID="pdf" CssClass="btn btn-primary" OnClick="pdf_Click" CausesValidation="false"/>
                </div>
            </div>
        </div>
    </div>

    
    <!-- The Modal-->
          <div class="modal fade" id="mdl">
            <div class="modal-dialog">
              <div class="modal-content">
      
                <div class="modal-header">
                  <h4 class="modal-title">Crear Contraseña</h4>
                  <button type="button" class="close" data-dismiss="modal">×</button>
                </div>
        
                <div class="modal-body">
                  <div class="col-12">
                      <div class="form-group">
                          <div>
                              <label>Contraseña</label>
                              <asp:TextBox ID="pass" runat="server" CssClass="form-control" />
                          </div>
                          <div>
                              <label>Confirma Contraseña</label>
                              <asp:TextBox ID="pass2" runat="server" CssClass="form-control" />
                          </div>
                      </div>
                  </div>
                </div>
        
                <div class="modal-footer justify-content-center">
                  <asp:Button ID="Btn_addPass" Text="Aceptar" runat="server" OnClick="Btn_addPass_Click" CssClass="btn btn-primary" Width="200"/>
                </div>
        
              </div>
            </div>
          </div>



</asp:Content>
