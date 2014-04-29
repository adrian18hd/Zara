<%@ Page Language="c#" Inherits="Zara.Web.Views.CommerceTemplates.ProductListing.ZaraCatalogNodeTemplate" CodeBehind="ZaraCatalogNodeTemplate.aspx.cs" MasterPageFile="~/Views/MasterPage.Master" %>

<%@ Register TagPrefix="zara" TagName="SubCategories" Src="Units/SubCategoriesList.ascx" %>
<%@ Register TagPrefix="zara" TagName="EntriesList" Src="Units/EntriesList.ascx" %>

<asp:Content ContentPlaceHolderID="MainContent" ID="MainContent" runat="server">
	<h1><%=CurrentContent.DisplayName %></h1>
	<div class="col-lg-3">
		<zara:SubCategories ID="subCategories" runat="server" />
	</div>

	<div class="col-lg-9">
		<zara:EntriesList ID="entries" runat="server" />
	</div>
</asp:Content>
