<%@ Page Title="" Language="C#" MasterPageFile="~/ItriumListener.Master" AutoEventWireup="true" CodeBehind="ItriumEventsViewContent.aspx.cs" Inherits="ItriumListener.ItriumEventsViewForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br/>
    <br/>
    <br/>
    <div style="height: 250px">
        <asp:GridView ID="gvItriumEventsData" runat="server" Height="222px" Width="766px" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed" Font-Bold="False" ShowHeaderWhenEmpty="True">
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="id">
                    <ItemStyle Width="50px"/>
                </asp:BoundField>
                <asp:BoundField DataField="dateTime" HeaderText="Дата"/>
                <asp:BoundField DataField="typeName" HeaderText="Тип"/>
                <asp:BoundField DataField="credentialHolder" HeaderText="Сотрудник"/>
                <asp:BoundField DataField="clockNumber" HeaderText="Момент"/>
            </Columns>
            <PagerSettings FirstPageText="First" LastPageText="Last"/>
        </asp:GridView>
    </div>
</asp:Content>
