<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SimpleProduct.ascx.cs" Inherits="Zara.Web.Views.CommerceTemplates.ProductListing.Units.SimpleProduct" %>

<div class="col-lg-3">
	<div class="thumbnail">
		<h5><%=ZaraProductContent.DisplayName ?? ZaraProductContent.Name %></h5>
		<hr/>
		Price: <%=Price.ToString("C2") %>
		<div class="text-center">
			<a class="btn btn-info" href="<%=GetUrl(ZaraProductContent) %>">Details</a>
		</div>
	</div>
</div>
