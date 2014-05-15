<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterPage.Master" AutoEventWireup="false" CodeBehind="Checkout.aspx.cs" Inherits="Zara.Web.Views.Pages.Checkout.Checkout" %>

<%@ Register TagPrefix="zara" TagName="PaymentMethod" Src="Units/PaymentMethod.ascx" %>
<%@ Register TagPrefix="zara" TagName="ShippingMethod" Src="Units/ShippingMethod.ascx" %>
<%@ Register TagPrefix="zara" TagName="CartSummary" Src="~/Views/Pages/ShoppingCart/Units/CartSummary.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

	<div class="row">
		<div class="col-lg-4">
			<address>
				<strong>Billing Address.</strong><br />
				4134 Del Rey Avenue<br />
				Marina del Rey, CA 90292<br />
				<abbr title="Phone">P:</abbr>
				(123) 456-7890
			</address>
		</div>
		<div class="col-lg-4">
			<address>
				<strong>Shipping Address.</strong><br />
				4134 Del Rey Avenue<br />
				Marina del Rey, CA 90292<br />
				<abbr title="Phone">P:</abbr>
				(123) 456-7890
			</address>
		</div>
		<div class="col-lg-4">
			<form method="POST">
				<zara:PaymentMethod ID="paymentMethod" runat="server" />
				<zara:ShippingMethod ID="shippingMethod" runat="server" />
				<zara:CartSummary runat="server" />
				<asp:Panel runat="server" ID="pnlPlaceOrder">
					<input type="submit" value="Place Order" name="PlaceOrder" class="btn btn-default" />
				</asp:Panel>
			</form>
		</div>
		<div class="clearfix"></div>
	</div>

</asp:Content>
