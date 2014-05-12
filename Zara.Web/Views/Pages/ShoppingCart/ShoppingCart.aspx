<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterPage.Master" AutoEventWireup="false" CodeBehind="ShoppingCart.aspx.cs" Inherits="Zara.Web.Views.Pages.ShoppingCart.ShoppingCart" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<asp:Label runat="server" CssClass="text-center text-info" ID="lblEmptyShoppingCartMessage">
		<EPiServer:Property runat="server" PropertyName="EmptyShoppingCartMessage"></EPiServer:Property>
	</asp:Label>
</asp:Content>
