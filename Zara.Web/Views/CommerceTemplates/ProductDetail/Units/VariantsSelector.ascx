<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="VariantsSelector.ascx.cs" Inherits="Zara.Web.Views.CommerceTemplates.ProductDetail.Units.VariantsSelector" %>

<form role="form" method="POST">
	<div class="form-group">
		<label>Color: </label>
		<asp:Repeater runat="server" ID="rptColors" ItemType="System.String">
			<HeaderTemplate>
				<select class="form-control" name="color">
			</HeaderTemplate>
			<ItemTemplate>
				<option><%#Item %></option>
			</ItemTemplate>
			<FooterTemplate>
				</select>
			</FooterTemplate>
		</asp:Repeater>
	</div>
	<div class="form-group">
		<label>Size: </label>
		<asp:Repeater runat="server" ID="rptSizes" ItemType="System.String">
			<HeaderTemplate>
				<select class="form-control" name="size">
			</HeaderTemplate>
			<ItemTemplate>
				<option><%#Item %></option>
			</ItemTemplate>
			<FooterTemplate>
				</select>
			</FooterTemplate>
		</asp:Repeater>
	</div>

	<div class="form-group">
		<span>Quantity:</span>
		<input class="form-control" type="number" name="quantity" required />
	</div>
	<div class="form-group">
		<label>
			Price:
		</label>
		<span>
			<%=Price %>
		</span>
	</div>
	<div class="form-group">
		<input type="submit" name="addToCart" value="Add to cart" class="btn btn-default" />
	</div>
</form>

<asp:Panel CssClass="alert alert-danger" runat="server" ID="pnlError">
	<asp:Label runat="server" ID="lblError"></asp:Label>
</asp:Panel>
<asp:Panel CssClass="alert alert-success" runat="server" ID="pnlSuccess">
	<asp:Label runat="server" ID="lblSuccess"></asp:Label>
</asp:Panel>
