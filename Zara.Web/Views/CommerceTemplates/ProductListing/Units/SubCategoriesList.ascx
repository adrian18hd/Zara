<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SubCategoriesList.ascx.cs" Inherits="Zara.Web.Views.CommerceTemplates.ProductListing.Units.SubCategoriesList" %>

<asp:Repeater runat="server" ID="rptSubCategories" ItemType="EPiServer.Commerce.Catalog.ContentTypes.NodeContent">
	<ItemTemplate>
		<p>
			<a href="<%#GetUrl(Item) %>"><%#Item.DisplayName ?? Item.Name %></a>
		</p>
	</ItemTemplate>
</asp:Repeater>