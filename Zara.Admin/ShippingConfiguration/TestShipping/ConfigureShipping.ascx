<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ConfigureShipping.ascx.cs" Inherits="EPiServer.Commerce.Sample.ShippingConfiguration.TestShipping.ConfigureShipping" %>

<table>

	<tr>
		<td>
			<asp:Label runat="server" ID="lblKey" AssociatedControlID="txtKey">Key:</asp:Label>
		</td>
		<td>
			<asp:TextBox runat="server" ID="txtKey"></asp:TextBox>
		</td>
		<td>
			<asp:RequiredFieldValidator runat="server" ID="rqdKey" ValidationGroup="Fedex" ControlToValidate="txtKey"></asp:RequiredFieldValidator>
		</td>
	</tr>
	<tr>
		<td>
			<asp:Label runat="server" ID="lblPassword" AssociatedControlID="txtPassword">Password:</asp:Label>
		</td>
		<td>
			<asp:TextBox runat="server" ID="txtPassword"></asp:TextBox>
		</td>
		<td>
			<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ValidationGroup="Fedex" ControlToValidate="txtPassword"></asp:RequiredFieldValidator>
		</td>
	</tr>
</table>
