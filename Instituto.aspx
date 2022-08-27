<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Instituto.aspx.cs" Inherits="TutoriasWeb.Instituto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://cdn.jsdelivr.net/npm/jquery@3.6.0/dist/jquery.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/popper.js@1.16.1/dist/umd/popper.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.6.1/dist/js/bootstrap.bundle.min.js"></script>

    <script type="text/javascript">

        $(document).ready(function () {
            $("#btn_a").click(function () {
                $("#mdl").modal();
            });
        });

        function openModal() {
            $(document).ready(function () {
                $("#mdl").modal();
            })
        };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="col-lg-12 form-inline">
        <div runat="server" id="add">
            <button type="button" class="btn btn-primary" id="btn_a">Agregar</button>
        </div>
                            
        <div class="row col-12 justify-content-start">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" OnRowCommand="GridView1_RowCommand" CssClass="table table-bordered table-condensed table-responsive table-hover "  >
                <Columns>
                    <asp:BoundField DataField="ID" HeaderText="" />
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                    <asp:TemplateField ShowHeader="false">
                        <ItemTemplate>
                            <asp:Image  runat="server" Width="200"  ImageUrl='<%# Eval("Logo") %>'/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <a href='<%# Eval("Sitio") %>'  rel="stylesheet" ><%# Eval("Sitio") %></a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="false">
                        <ItemTemplate>
                            <asp:ImageButton  Width="30px" CommandName="Editar"  CommandArgument='<%# Eval("ID") %>'  runat="server"  ImageUrl="data:image/jpeg;base64,/9j/4AAQSkZJRgABAQAAAQABAAD/2wCEAAkGBxAQEA8QEBAQDxAPDQ0QEA8PDw8NDw8QFRIWFhURFRcYHSggGBolGxUVITEhJSkrLi4uFx8zODMtNygtLjcBCgoKDg0OGhAQGi0lHyYtLTA3LSstLS0vKy0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tKy0tLS0tLS0tN//AABEIAOEA4QMBIgACEQEDEQH/xAAcAAEAAQUBAQAAAAAAAAAAAAAAAwECBAUGBwj/xAA9EAACAQIDBgMECAUDBQAAAAAAAQIDEQQhUQUGEjFBYRMVcQcigcEUMkJikaGx0SNDUnLhM4LwJFNjksL/xAAaAQEAAgMBAAAAAAAAAAAAAAAAAwQCBQYB/8QAKxEAAgIBBAEDAgYDAAAAAAAAAAECAxEEEiExBRMiQWGhFDJCUYGRFSND/9oADAMBAAIRAxEAPwD3EAAAAAAAAAAAAAAAAAAAAFCjKnE+0feb6NT+jUpWr14NykudGjyc3o3ml8dDGUtqyySmqVs1CPZBvF7Q40pzpYWEarg3GdacrUVJc1G31ra8jS4H2lYji99YerHrGDdOXwd3c8yxWJ43ZXUI5Rj21fchTKbuk3wdNX43TxhhrL/c+ltg7eoYyHFSl7ytx0pZVKb7rTvyNoj5r2VtmpRnGanKE4/VqRdpR7PVdj1vdff6nWUaeKcaU3ZRrJ2pVH0Tb+o3o8syeu5S4ZqdX4yVXur5X3R3JUtTRcTmrAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABZUqKKcnkkrsA1+8O2KeDw9SvUd+GPuwWcpybSjFL1aXxPnreLatSvVqTqO9SrLiqvOy/ppr7qR7RtGu60nKSvF5KLzSjoef7zbhuXFWwfN3csPJ8+8H0f3WQXRk1wbTxt9VUmpdv5OATLky2rTlCTjOLjKLtKMlaSejRRMqNHQqWeUSJmRh8TKHLk+cXyZiplx4Zpnou6G/dShwwlerRVr0pP8AiU1rBvmuz/I9Y2TtahiqaqUJqcXzWalF6STzTPmSMms1kze7C3iq4eopwm6c8lxrOMlpOPJomruceGa3V+Nhb7ocS+zPowqclutvtRxXDTq2o13yV/4dXvCWvZ5+vM6y5cjJS5RzttU6pbZrDKgA9IwAAAAAAAAAAAAAAAAAAAAAAAChrdsVlw+HzvZvsZmLrqEW+vRas52tVbbb5so6zUemsR7J6Ktzy+iNRRXwdPwIKky6hjbO0uWq5or0eSjnbZ/ZLZpn3E1m8O62Hx0ffXh1UvdrQXvrtJfaXZnlG8G7uJwM+GtG8G3wVoXdOf7Psz3yMFJJpp36rkR4nBwqQdOrCM4SVnGS4otGwlXGayjPTa2yl7Xyj5xuXJnfb2ezidLirYLiq0828O3xVafX3H9uPrn6nn/JtPJptNPJprmmVZRcXydBTqIWrMWSIqmWJlUzAsZMzC4yUMucf6X8tD1L2fb7TlWp4TES44VYtUKsn78Zr+VPW/R/rfLyNM2u7PG8XhIw+t9KpuPrdX/JGdcnGSwV9XTC2p7j6WRUtRcbA5AAAAAAAAAAAAAAAAAAAAAAFsnZXfQqzVbWxX2F/ufyIrrVXByZnCDm8IxMfiuOXZcl8zXVJF1SZjVJHNai5ybbNpCCisIpUkY8mVnIslka6csk6RJhsbOk7xeXWL5M6HB7QhWWWUusXz+GpykmUjJp3Ts11WTJ9J5KzTvHcf2IrtPGxfU7OEkjl98NxcPjr1IfwMTb/Uivdqdqi6+qzXczcFtNyyqc19vX1NlDFW55r/nI6qjUVamGYs13+zTzyuD5+21sbEYOo6deDg8+GXOE1rF9TBTPojamz6GLpOnWhGrTl0fOL1i+cX3R5LvXuLWwnFVocVegs3lerTX3kvrLujGdTXRutL5CNntnwzk0z1P2QbqtyW0KyskpRw0X1bydX5L1b0OK3F3bltHFRp5+DTtOvNdIXygu8uXweh9FYahGnCMIJRhCKjGKySSySMqa+csj8lq8L0o/PZKipQqWjQgAAAAAAAAAAAAAAAAAAAsqTUU28klc8bxyCHH4lU43+0+S+ZzdWpe7eb1JsbiXOTb5dOyMGpM5/W6nfLjo2VFW1fUtqTMapMuqTI4K77I085ZeEW0hFdSOoyWbMebIJvHBki1hK5QyqFOyu+b5EcI7mZN4Lox4Vb8S6niHHutCyTIpMtRulU8weCOUFJYZsaWK6xfqjMp4hSyeT06Poc9xNO6djp9gbPclGrUWXOMddJM6Tx3kvxHskuTW6jT+lynwZ+w9k0sNGfh04U3Vm6lTgio3k0ld262RswVNsVW2+yhUAHgAAAAAAAAAAAAAAAAKAA0u18Zd8CeS592Zu08XwRsvrS5du5zdSZq9fqdq2It6erL3MpUmY1SZdUmY1SRzllhsYoO7dl1JmrKwo07K75v9Ck2YJYWWZEVRmPIkqMshG7sitLl4RkiTD07u/RGTJlVGytoWSZYUdqMW8kcmRSZfJlKNGVSUYRV5Sdl+/oRNOT2o9ykuTN2Fs7x6ma/hwacu/wB34nbxjbJZJGNs/Bxo04wj0Wb6t6mWdh4/RrT14+X2ae+52Sz8AAGwIAAAAAAAAAAAAAAAAAAARV6qhFyfJIkOf2tjeOXCn7sfzepX1N6qhn5JKq3OWDExeIc5OT6/kuiMKcy6pMxqkzlbrXJts20IpLCKVJFcNTu+J8ly7sjpwc5WXxeiM5pJWWSRXrjue59GbeCybMabJqjMebFsgkRSZl4elwq75v8AJEWFpXfE+S5d2ZUmY1Q/Uz2T+CyTIZMvkyKbE2EWSZ1W7ezPDj4s1781kusY/uzV7v7N8WfHJe5B/wDtLT0OwRuvEaL/ALT/AIKOru/Qv5BUA6I14AAAAAAAAAAAAAAAAAAKFSDF1lTi5PpyWrPJNJZZ6lngw9sY3gXAvrSWfZHO1JkmJrOUnJ5tvMxKkjl9bqnZJv4NpRVsiW1JmPKV8itSRk7Pofbl/tXzNYk7JYRZ6RNh6PBHu+f7FKjJajMebLUsRWEYLkimyKMHJ2/H0LpmVRp8K7vn+xXUd8voZ5witklZdCOTL5MhkySTxwYosky7CYaVWcYR5t5vol1ZHI67YOzvBheS/iTzl91dIkui0r1FuPhdkd9vpx+pnYTDRpQjCKsoq3r3ZOUKnXxiorC6NQ3nsAAyPAAAAAAAAAAAAAAAAAAAavbksoLVt/gv8m0NPtuXvQWkW/xf+CDUvFbJKvzo1NSipdnqa3EwcXZr4m4SE6SkrSV1oaG/SqxccM2ELNvZpMLQ8SVuizl6G2lkrLklkXUsMoK0eV7vUiqMrQo9GPPZK57iKozHmyWoyOEOJ26dSCbbeDNcFcNS+0/h+5NJlzI5MzxtWDzsjkyKTL5Ml2fg3WqKK5c5PRESi7JKMe2G1FZZn7u7O45eLJe7F+4n1lr6I6hFlGkoRUYqySSSJDrdJplRWors1FtjslkAAtEYAAAAAAAAAAAAAAAAAAAABQ0m1ner6RS+fzN2aHHO9Sfrb8Crqn7ME1P5iBIvSKJF8UUUWGyqRFWwylmsn+TJ0i5I9cFJYZ4pNdGjrU5J2as/1JoQ4Vbr1NvOmpc1fv1RgYrDSjnzWunqa+eldb3LksRtUuGYsmQyZfJkUmUpsmRbZtpLNt2S7nX7IwKowS+1LOT76eiNbu9s/wDnSX9i/wDo6A3ni9HsXqy7ZQ1V257UVABuCmAAAAAAAAAAAAAAAAAAAAAAAAUOequ8pPWTN3javBTqT/opzlnksk2eG7K9scW19Kwrinnx0J8Vlq4yt+pW1EHJLBLVJJ8nq6Rekabd7ebB4+N8NXjOSS4qUr060P7oPP48u5u0inta7J85CRekUii9IzSMWVSL0iiRekZpGLZr8Zs1Szhk/wCno/2MLZ2z5VKnDJNRhnO/5L4nSU6LfPIyYwSIv8dCc1N/0ZfiJKO0RikklySKlQbNLBVAAPQAAAAAAAAAAAAAAAAAAAAAAAAaHfvE+FszH1Fzhg67XS74HZHye6dj6a9sGI4Nj4rWo6FJLXjqxT/K/wCB82zgAY1KpOnKM6cpQnB3jODcZRfZo9a3B9qXE4YbaMkm7Rhi8oxb0q6f3HlE4EM4mMoKS5PVJo+uY55rP5l6R5/7F6m0Z4Z0cVQqRw9NL6NiKvuycf8At2ebS6P4HqFOgo93qVlS8krmjHpUG+yMqnSS/ckBYjWkRN5AAMzwAAAAAAAAAAAAAAAAAAAAAAAAAFGwCoIZYiK6kcsdFdQDgvbrX4dn0Ka/m46ndfdjTqS/XhPCJxPevabsartKOFjQnTSpVKrqeJJx+soqMlra0vxMfdncXZ+Ecalb/q66s1KokqMH92Hzd/gAeX7sbgY7aDUqdPwqN/8AXrpwhb7q5y+B7Fuh7LsDgOGpNfS8Ss3WrRShF/8Ajp5qPq7vudKtqwSsrJLklkkRy2zHUA3CRU0vncdR53HUA3QNJ53HUjntxAG/FznfPUPPkAdFcHO+fIefIA6IHPx24iVbbjqAbsGl87jqPO46gG6BpVtqOpKtrw1ANqDWLa0NSSO0oaoAzwYkcdF9SWOJi+oBMCilcqAAAACHEJ2yJgAc1i6VS+VzX1aNXudk6a0LHh46AHESpVe5C6VTud08HHRFv0CGiAOGlTqdyJ059zvJbOhoR+Vw0AOH8Ofcp4c+53PlcNCj2VDQA4bhl3LXGR2z2PHQp5NDQA4rgkOCR2vk0NB5NDQA4rgkOCR2vk0NB5NDQA4pRkVtLudp5NDQqtjw0AOMVOfcr4c+520dlQ0LvK4aAHDqnPuSRp1O52nlcNC6OzoLoAcWqVTuSwpVe52X0CGiL44OOgByNOlV7mfhaVW/U6FYaOhIqSXQAhwidlcySiRUAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA//Z"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                        
                </Columns>
            </asp:GridView>
        </div>
    </div>

    

    <!-- The Modal Maestro-->
          <div class="modal fade" id="mdl">
            <div class="modal-dialog">
              <div class="modal-content">
      
                <div class="modal-header">
                  <h4 class="modal-title">Agregar Tutor</h4>
                  <button type="button" class="close" data-dismiss="modal">×</button>
                </div>
        
                <div class="modal-body">
                  <div class="col-12">
                      <div class="form-group">
                          <asp:Label ID="ID" Text="ID" runat="server" Visible="false"/>
                          <div>
                              <label>Nombre</label>
                              <asp:TextBox ID="nombre" runat="server" CssClass="form-control text-uppercase" AutoCompleteType="Disabled" />
                          </div>
                          <div>
                              <label>Logo(URL)</label>
                              <asp:TextBox ID="logo" runat="server" CssClass="form-control text-uppercase" AutoCompleteType="Disabled" />
                          </div>
                          <div>
                              <label>Sitio(URL)</label>
                              <asp:TextBox ID="sitio" runat="server" CssClass="form-control text-uppercase" AutoCompleteType="Disabled" />
                          </div>
                      </div>
                  </div>
                </div>
                <div class="modal-footer justify-content-center">
                    <asp:Button Text="Cancelar" runat="server" ID="Btn_cancel" CssClass="btn btn-primary" Width="200" OnClick="Btn_cancel_Click" Visible="false"/>
                    <asp:Button ID="Btn_addInst" Text="Agregar" runat="server" OnClick="Btn_addInst_Click" CssClass="btn btn-primary" Width="200"/>
                </div>
        
              </div>
            </div>
          </div>
</asp:Content>
