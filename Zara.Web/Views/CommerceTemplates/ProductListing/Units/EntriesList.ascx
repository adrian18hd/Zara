<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EntriesList.ascx.cs" Inherits="Zara.Web.Views.CommerceTemplates.ProductListing.Units.EntriesList" %>

<%@ Register TagPrefix="zara" TagName="SimpleProduct" Src="SimpleProduct.ascx" %>

<div class="panel panel-info">
	<div class="panel-heading">
		<h4>Products</h4>
	</div>
	<div class="panel-body">
		<asp:Repeater runat="server" ID="rptEntries" ItemType="Zara.Web.CatalogModels.Product.ZaraProductContent">
			<ItemTemplate>
				<zara:SimpleProduct ZaraProductContent="<%#Item %>" runat="server" />
			</ItemTemplate>
		</asp:Repeater>

		<asp:Literal ID="ltNoProducts" runat="server">
	<div class="jumbotron">
		No products in current category!
	</div>
		</asp:Literal>
	</div>
</div>
