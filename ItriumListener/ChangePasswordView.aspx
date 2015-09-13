<%@ Page Title="" Language="C#" MasterPageFile="~/ItriumListener.Master" AutoEventWireup="true" CodeBehind="ChangePasswordView.aspx.cs" Inherits="ItriumListener.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
    .auto-style1 {
        width: 204px;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <br />
        <br />
    <div class="container">
  <h2>Изменть пароль пользователя</h2>
  <form role="form">
    <div class="form-group">
      <label for="txtOldPassword">Старый пароль:</label>
        <asp:TextBox ID="txtOldPassword" runat="server" class="form-control"></asp:TextBox>
    </div>
    <div class="form-group">
      <label for="txtNewPassword">Новый пароль:</label>
        <asp:TextBox ID="txtNewPassword" runat="server" class="form-control"></asp:TextBox>
    </div>
    <div class="form-group">
      <label for="txtConfirmPassword">Подтверждение пароля:</label>
      <asp:TextBox ID="txtConfirmPassword" runat="server" class="form-control"></asp:TextBox>
    </div>
      <asp:Button ID="btnChangePassword" class="btn btn-default" runat="server"
           OnClientClick="return callWaitItriumEvent();" Text="Изменить пароль" Width="164px" UseSubmitBehavior="False" />



      <div class="modal fade bs-example-modal-sm" id="myPleaseWait" tabindex="-1"
    role="dialog" aria-hidden="true" data-backdrop="static">
    <div class="modal-dialog modal-sm">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title">
                    <span class="glyphicon glyphicon-credit-card">
                    </span>Приложите карту к считывателю
                 </h4>
            </div>
            <div class="modal-body">
                <div class="progress">
                    <div class="progress-bar progress-bar-info
                    progress-bar-striped active"
                    style="width: 100%">
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
  </form>
        <script type="text/javascript">
            function callWaitItriumEvent() {
                $('#myPleaseWait').modal('show');
                //$.ajax({
                //    url: "test.html",
                //    success: function (data) {
                //        $('#myPleaseWait').modal('hide');
                //    },
                //    error: function () {
                //        $('#myPleaseWait').modal('hide');
                //        console.log('An error occurred');
                //    }
                //});
            }
            </script>

</div>
</asp:Content>
