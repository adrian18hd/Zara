<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="CartSummary.ascx.cs" Inherits="Zara.Web.Views.Pages.ShoppingCart.Units.CartSummary" %>

<dl class="dl-horizontal">
	<dt>Sub Total:</dt>
	<dd><asp:Label runat="server" ID="lblSubTotal"></asp:Label></dd>
	<dt>Discount Total: </dt>
	<dd><asp:Label runat="server" ID="lblDiscountTotal"></asp:Label></dd>
	<dt>Shipping Total: </dt>
	<dd><asp:Label runat="server" ID="lblShippingTotal"></asp:Label></dd>
	<dt>Tax Total</dt>
	<dd><asp:Label runat="server" ID="lblTaxTotal"></asp:Label></dd>
	<dt>Total</dt>
	<dd><asp:Label runat="server" ID="lblTotal"></asp:Label></dd>
</dl>