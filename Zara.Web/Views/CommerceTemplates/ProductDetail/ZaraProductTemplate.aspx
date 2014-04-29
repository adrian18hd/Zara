<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterPage.Master" AutoEventWireup="false" CodeBehind="ZaraProductTemplate.aspx.cs" Inherits="Zara.Web.Views.CommerceTemplates.ProductDetail.ZaraProductTemplate" %>

<%@ Register TagPrefix="zara" TagName="VariantsSelector" Src="Units/VariantsSelector.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<h1>
		<EPiServer:Property runat="server" PropertyName="DisplayName"></EPiServer:Property></h1>
	<div class="col-lg-8">
		<EPiServer:Property runat="server" PropertyName="MainContent"></EPiServer:Property>
	</div>
	<div class="col-lg-4">
		<p><strong>Ref #: </strong><EPiServer:Property runat="server" PropertyName="RefNumber"></EPiServer:Property></p>
		<p><strong>Model Height: </strong><EPiServer:Property runat="server" PropertyName="Height"></EPiServer:Property></p>
		<hr/>
		<zara:VariantsSelector ID="variantsSelector" runat="server" />
	</div>
</asp:Content>
