<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ErrorViewForm.aspx.cs" Inherits="ItriumListener.ErrorViewForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="height: 250px">
    
        ТАБЛИЦА ОШИБОК<asp:GridView ID="gvErrorData" runat="server" Height="222px" Width="766px">
            <Columns>
                <asp:BoundField DataField="errorDate" HeaderText="Дата" />
            </Columns>
        </asp:GridView>
    
        </div>
    </form>
</body>
</html>
