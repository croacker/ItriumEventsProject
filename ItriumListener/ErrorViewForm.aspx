<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ErrorViewForm.aspx.cs" Inherits="ItriumListener.ErrorViewForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Список ошибок</title>
    <link href="Content/css/bootstrap.min.css" rel="stylesheet" type="text/css"/>
</head>
<body>
    <form id="frmErrorsView" runat="server">
        <div style="height: 250px">
            <asp:GridView ID="gvErrorData" runat="server" Height="222px" Width="95%" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed" Caption="Список ошибок" Font-Bold="False" ShowHeaderWhenEmpty="True">
                <Columns>
                    <asp:BoundField DataField="ID" HeaderText="id">
                        <ItemStyle Width="50px"/>
                    </asp:BoundField>
                    <asp:BoundField DataField="errorDate" HeaderText="Дата"/>
                    <asp:BoundField DataField="title" HeaderText="Тема"/>
                    <asp:BoundField DataField="msg" HeaderText="Сообщение"/>
                </Columns>
                <PagerSettings FirstPageText="First" LastPageText="Last"/>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
