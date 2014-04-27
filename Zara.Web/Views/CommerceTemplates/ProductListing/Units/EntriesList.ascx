<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EntriesList.ascx.cs" Inherits="Zara.Web.Views.CommerceTemplates.ProductListing.Units.EntriesList" %>

<%@ Register TagPrefix="zara" TagName="SimpleProduct" Src="SimpleProduct.ascx" %>

<div class="row">
	
<asp:Repeater runat="server" ID="rptEntries" ItemType="Zara.Web.CatalogModels.Product.ZaraProductContent">
	<ItemTemplate>
		<zara:SimpleProduct ZaraProductContent="<%#Item %>" runat="server" />
	</ItemTemplate>
</asp:Repeater>
</div>