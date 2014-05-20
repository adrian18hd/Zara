using System;
using Mediachase.Commerce.Core;
using Mediachase.Commerce.Engine;
using Mediachase.Commerce.Orders;
using Mediachase.Commerce.Orders.Dto;
using Mediachase.Commerce.Orders.Managers;
using Mediachase.Commerce.Website.Helpers;
using Zara.Web.Views.BaseClasses;
using Zara.Web.Views.Pages.Checkout.ContentTypes;

namespace Zara.Web.Views.Pages.Checkout
{
	public partial class Checkout : TemplatePageBase<CheckoutPage>
	{
		private readonly CartHelper _cartHelper = new CartHelper(Cart.DefaultName);

		protected override void OnLoad(EventArgs e)
		{
			_cartHelper.Cart.RunWorkflow("CartValidate");
			_cartHelper.Cart.AcceptChanges();
			if (Request["PlaceOrder"] != null)
			{
				PlaceOrder();
			}
			else if (Request["Apply"] != null)
			{
				ApplyCheckoutItemsToCart();
			}
			pnlPlaceOrder.Visible = CartHasAllObjectsPopulated();
		}

		private bool CartHasAllObjectsPopulated()
		{
			return _cartHelper.Cart.OrderAddresses.Count == 2 && _cartHelper.OrderForm.Shipments.Count == 1;
		}

		private void ApplyCheckoutItemsToCart()
		{
			_cartHelper.Reset();
			_cartHelper.Cart.AcceptChanges();
			OrderAddress shippingAddress = GetAddress(true);
			OrderAddress billingAddress = GetAddress(false);
			_cartHelper.Cart.OrderAddresses.Add(shippingAddress);
			_cartHelper.Cart.OrderAddresses.Add(billingAddress);

			Guid shippingMethodId;
			if (!Guid.TryParse(Request["shippingMethod"], out shippingMethodId))
				throw new ApplicationException("Please select a shipping method first");
			ShippingMethodDto shippingMethodDto = ShippingManager.GetShippingMethod(shippingMethodId);
			ShippingMethodDto.ShippingMethodRow shippingMethodRow = shippingMethodDto.ShippingMethod[0];

			Shipment shipment = new Shipment
			{
				ShippingMethodId = shippingMethodId,
				ShippingMethodName = shippingMethodRow.Name,
				ShippingAddressId = shippingAddress.Name
			};
			int lineItemIndex = 0;
			foreach (LineItem lineItem in _cartHelper.LineItems)
			{
				shipment.AddLineItemIndex(lineItemIndex, lineItem.Quantity);
				lineItemIndex++;
			}
			_cartHelper.OrderForm.Shipments.Add(shipment);
			WorkflowResults workflowResults = _cartHelper.Cart.RunWorkflow("CartPrepare");
			_cartHelper.Cart.AcceptChanges();
		}

		private void PlaceOrder()
		{
			PaymentMethodDto paymentMethod = PaymentManager.GetPaymentMethodBySystemName("TestPayment",
				SiteContext.Current.LanguageName);
			if (paymentMethod == null || paymentMethod.PaymentMethod == null || paymentMethod.PaymentMethod.Count == 0)
				throw new ApplicationException("No TestPayment payment found");

			Payment payment = new CreditCardPayment
			{
				PaymentMethodId = paymentMethod.PaymentMethod[0].PaymentMethodId,
				PaymentMethodName = paymentMethod.PaymentMethod[0].Name,
				Amount = _cartHelper.Cart.Total,
				BillingAddressId = "BillingAddress",
				CardType = "Visa",
				CreditCardNumber = "4111111111111111",
				CreditCardSecurityCode = "123",
				CustomerName = "Test Owner",
				ExpirationMonth = 12,
				ExpirationYear = DateTime.UtcNow.AddYears(1).Year,
				Status = PaymentStatus.Pending.ToString(),
				TransactionType = TransactionType.Authorization.ToString()
			};
			_cartHelper.OrderForm.Payments.Add(payment);
			_cartHelper.Cart.RunWorkflow("CartCheckout");
			_cartHelper.Cart.AcceptChanges();

			var po = _cartHelper.Cart.SaveAsPurchaseOrder();
			_cartHelper.Cart.Delete();
			_cartHelper.Cart.AcceptChanges();
			Response.Write(po.TrackingNumber);
			Response.End();
		}

		private OrderAddress GetAddress(bool shipping)
		{
			OrderAddress orderAddress = new OrderAddress();
			orderAddress.City = "Marina del Rey";
			orderAddress.CountryCode = "USA";
			orderAddress.CountryName = "United States of America";
			orderAddress.DaytimePhoneNumber = "1234567890";
			orderAddress.FirstName = "Test";
			orderAddress.LastName = "Test";
			orderAddress.Line1 = "4134 Del Rey ave.";
			orderAddress.PostalCode = "90292";
			orderAddress.State = "CA";
			orderAddress.Name = shipping ? "ShippingAddress" : "BillingAddress";
			return orderAddress;
		}
	}
}