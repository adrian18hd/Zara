<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SimpleProduct.ascx.cs" Inherits="Zara.Web.Views.CommerceTemplates.ProductListing.Units.SimpleProduct" %>

<div class="col-lg-3">
	<div class="thumbnail">
		<img data-src="holder.js/100%x180" width="100" height="180" />
		<h3><%=ZaraProductContent.DisplayName ?? ZaraProductContent.Name %></h3>
		<dl class="dl-horizontal">
			<dt>
				Ref#: 
			</dt>
			<dd>
				<%=ZaraProductContent.RefNumber %>
			</dd>
			<dt>
				Model Height:
			</dt>
			<dd>
				<%=ZaraProductContent.Height %>
			</dd>
		</dl>
		<div class="text-center">
			<a class="btn btn-info" href="<%=GetUrl(ZaraProductContent) %>">Details</a>
		</div>
	</div>
</div>
