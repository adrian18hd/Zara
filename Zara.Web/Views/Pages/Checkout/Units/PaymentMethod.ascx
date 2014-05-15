<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="PaymentMethod.ascx.cs" Inherits="Zara.Web.Views.Pages.Checkout.Units.PaymentMethod" %>

<div class="well">
	<div class="form-group">
		<label class="control-label">Card owner: </label>
		<input class="form-control" type="text" value="Test Owner" readonly="readonly" />
	</div>
	<div class="form-group">
		<label class="control-label">Card Type: Visa</label>
	</div>
	<div class="form-group">
		<label class="control-label">Card number: </label>
		<input class="form-control" type="text" value="4111111111111111" readonly="readonly"/>
	</div>
	<div class="form-group">
		<label class="control-label">CVV: </label>
		<input class="form-control" type="text" value="CVV" readonly="readonly" />
	</div>
</div>