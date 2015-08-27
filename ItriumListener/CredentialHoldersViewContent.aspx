<%@ Page Title="" Language="C#" MasterPageFile="~/ItriumListener.Master" AutoEventWireup="true" CodeBehind="CredentialHoldersViewContent.aspx.cs" Inherits="ItriumListener.CredentialHoldersViewForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br/>
    <br/>
    <br/>
    <div style="height: 250px">
        <asp:GridView ID="gvCredentialHoldersData" runat="server" Height="222px" Width="95%" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed" Font-Bold="False" ShowHeaderWhenEmpty="True">
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="id">
                    <ItemStyle Width="50px"/>
                </asp:BoundField>
                <asp:BoundField DataField="name" HeaderText="Имя"/>
            </Columns>
            <PagerSettings FirstPageText="First" LastPageText="Last"/>
        </asp:GridView>
    </div>
</asp:Content>
