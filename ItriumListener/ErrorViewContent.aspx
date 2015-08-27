<%@ Page Title="" Language="C#" MasterPageFile="~/ItriumListener.Master" AutoEventWireup="true" CodeBehind="ErrorViewContent.aspx.cs" Inherits="ItriumListener.ErrorViewContentForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br/>
    <br/>
    <br/>
    <div style="height: 250px">
        <asp:GridView ID="gvErrorData" runat="server" Height="222px" Width="95%" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed" Font-Bold="False" ShowHeaderWhenEmpty="True">
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
</asp:Content>
