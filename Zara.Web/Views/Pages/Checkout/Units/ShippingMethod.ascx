<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="ShippingMethod.ascx.cs" Inherits="Zara.Web.Views.Pages.Checkout.Units.ShippingMethod" %>

<asp:Repeater runat="server" ID="rptShippingMethods" ItemType="Mediachase.Commerce.Orders.Dto.ShippingMethodDto.ShippingMethodRow">
	<ItemTemplate>
		<label>
			<%#Item.DisplayName %> - <%# GetShippingCost(Item).ToString("C2") %>
			<input
				type="radio"
				name="shippingMethod"
				value="<%#Item.ShippingMethodId %>"
				<%# (IsSelected(Item.ShippingMethodId) ? "checked = 'checked'" : "" )%> />
		</label>
	</ItemTemplate>
</asp:Repeater>
<input type="submit" name="Apply" value="Apply shipping"/>
