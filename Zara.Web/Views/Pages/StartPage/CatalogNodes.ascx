<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="CatalogNodes.ascx.cs" Inherits="Zara.Web.Views.Pages.StartPage.CatalogNodes" %>

<asp:Repeater runat="server" ID="rptTopLevelCatalogNodes"
	ItemType="EPiServer.Commerce.Catalog.ContentTypes.NodeContent">
	<ItemTemplate>
		<div class="col-lg-4">
			<a href="<%#GetUrl(Item) %>">
				<img runat="server" id="img" src="<%#GetCatalogNodeImageUrl(Item) %>" class="img-rounded img-responsive" height="200" alt="<%# Item.DisplayName ?? Item.Name%>" />
			</a>
			<h2 class="text-center">
				<%# Item.DisplayName ?? Item.Name%>
			</h2>
		</div>
	</ItemTemplate>
</asp:Repeater>
