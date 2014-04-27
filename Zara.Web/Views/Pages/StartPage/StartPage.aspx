<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterPage.Master" AutoEventWireup="true" CodeBehind="StartPage.aspx.cs" Inherits="Zara.Web.Views.Pages.StartPage.StartPage" %>

<%@ Register TagName="CatalogNodes" TagPrefix="zara" Src="CatalogNodes.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<div class="jumbotron">
		<EPiServer:Property runat="server" PropertyName="MainBlock"></EPiServer:Property>
	</div>
	<div class="row">
		<zara:CatalogNodes runat="server" />
	</div>
</asp:Content>
