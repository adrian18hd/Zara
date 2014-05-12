<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="CartItems.ascx.cs" Inherits="Zara.Web.Views.Pages.ShoppingCart.Units.CartItems" %>

<asp:Panel ID="pnlWorkflowWarnings" runat="server" CssClass="alert alert-danger">
	<ul class="list-unstyled">
		<asp:Literal runat="server" ID="ltWarningList"></asp:Literal>
	</ul>
</asp:Panel>

<form method="POST">
	<asp:Repeater runat="server" ID="rptLineItems" ItemType="Mediachase.Commerce.Orders.LineItem">
		<HeaderTemplate>
			<table class="table table-bordered table-hover">
				<tr>
					<th>Item name</th>
					<th>Quantity</th>
					<th>Unit Price</th>
					<th>Final Price</th>
				</tr>
		</HeaderTemplate>
		<ItemTemplate>
			<tr>
				<td>
					<input type="hidden" name="lineItemId" value="<%#Item.LineItemId %>" />
					<a href="<%#GetLineItemUrl(Item) %>">
						<%#Item.DisplayName %>
					</a>
					<small>Color: <%#LineItemColor(Item) %></small>
					<small>Size: <%#LineItemSize(Item) %></small>
				</td>
				<td>
					<input type="number" value="<%#Item.Quantity.ToString("N0") %>" name="quantity" />
				</td>
				<td><%#Item.PlacedPrice.ToString("C2") %></td>
				<td><%#Item.ExtendedPrice.ToString("C2") %></td>
			</tr>
		</ItemTemplate>
		<FooterTemplate>
			</table>
		</FooterTemplate>
	</asp:Repeater>
	<input type="submit" name="UpdateItems" value="Update" class="btn btn-default" />
</form>
