<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="VariantsSelector.ascx.cs" Inherits="Zara.Web.Views.CommerceTemplates.ProductDetail.Units.VariantsSelector" %>


<asp:Repeater runat="server" ID="rptColors" ItemType="System.String">
	<HeaderTemplate>
		<select name="color">
	</HeaderTemplate>
	<ItemTemplate>
		<option><%#Item %></option>
	</ItemTemplate>
	<FooterTemplate>
		</select>
	</FooterTemplate>
</asp:Repeater>

<asp:Repeater runat="server" ID="rptSizes" ItemType="System.String">
	<HeaderTemplate>
		<select name="size">
	</HeaderTemplate>
	<ItemTemplate>
		<option><%#Item %></option>
	</ItemTemplate>
	<FooterTemplate>
		</select>
	</FooterTemplate>
</asp:Repeater>