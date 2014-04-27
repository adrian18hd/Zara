<%@ Page Language="c#" Inherits="Zara.Web.Views.CommerceTemplates.ProductListing.ZaraCatalogNodeTemplate" CodeBehind="ZaraCatalogNodeTemplate.aspx.cs" MasterPageFile="~/Views/MasterPage.Master" %>

<%@ Register TagPrefix="zara" TagName="SubCategories" Src="Units/SubCategoriesList.ascx" %>
<%@ Register TagPrefix="zara" TagName="EntriesList" Src="Units/EntriesList.ascx" %>

<asp:Content ContentPlaceHolderID="MainContent" ID="MainContent" runat="server">
	<h1><%=CurrentContent.DisplayName %></h1>

	<zara:SubCategories ID="subCategories" runat="server" />

	<zara:EntriesList ID="entries" runat="server" />
</asp:Content>
