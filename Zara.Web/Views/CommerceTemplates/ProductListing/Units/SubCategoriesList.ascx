<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="SubCategoriesList.ascx.cs" Inherits="Zara.Web.Views.CommerceTemplates.ProductListing.Units.SubCategoriesList" %>


<div class="panel panel-default">
	<div class="panel-heading">
		<h4>Sub-categories</h4>
	</div>
	<div class="panel-body">
		<asp:Repeater runat="server" ID="rptSubCategories" ItemType="EPiServer.Commerce.Catalog.ContentTypes.NodeContent">
			<ItemTemplate>
				<p>
					<a href="<%#GetUrl(Item) %>"><%#Item.DisplayName ?? Item.Name %></a>
				</p>
			</ItemTemplate>
		</asp:Repeater>
		<asp:Literal runat="server" ID="ltNoSubcategories">
			<em>-- no subcategories --</em>
		</asp:Literal>
	</div>
</div>
