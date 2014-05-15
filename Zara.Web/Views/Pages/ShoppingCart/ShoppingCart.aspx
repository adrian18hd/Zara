<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterPage.Master" AutoEventWireup="false" CodeBehind="ShoppingCart.aspx.cs" Inherits="Zara.Web.Views.Pages.ShoppingCart.ShoppingCart" %>
<%@ Register tagPrefix="zara" tagName="CartItems" src="Units/CartItems.ascx" %>
<%@ Register tagPrefix="zara" tagName="CartSummary" src="Units/CartSummary.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<asp:Label runat="server" CssClass="text-center text-info" ID="lblEmptyShoppingCartMessage">
		<EPiServer:Property runat="server" PropertyName="EmptyShoppingCartMessage"></EPiServer:Property>
	</asp:Label>
	<zara:CartItems ID="cartItems" runat="server"/>
	<div class="pull-right">
		<zara:CartSummary runat="server" />
		<asp:PlaceHolder runat="server" ID="navCheckout">
			<a href="<%=CheckoutPageLink %>" class="btn-link">Checkout</a>
		</asp:PlaceHolder>
	</div>
	<div class="clearfix"></div>
</asp:Content>
