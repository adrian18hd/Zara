using System;
using EPiServer.Commerce.Catalog.ContentTypes;
using Mediachase.Commerce.Core;
using Mediachase.Commerce.Orders.Dto;
using Mediachase.Commerce.Orders.Managers;
using Zara.Web.Views.BaseClasses;

namespace Zara.Web.Views.Pages.Checkout.Units
{
	public partial class PaymentMethod : RendererControlBase<CatalogContentBase>
	{
		public Guid PaymentMethodId { get; set; }
		public string PaymentMethodName { get; set; }

		protected override void OnLoad(EventArgs e)
		{
			PaymentMethodDto paymentMethodDto = PaymentManager.GetPaymentMethodBySystemName("TestPayment",
				SiteContext.Current.LanguageName);
			if (paymentMethodDto.PaymentMethod.Count == 0)
				throw new ApplicationException("You must define a payment method with the 'TestPayment' SystemKeyword");
			PaymentMethodId = paymentMethodDto.PaymentMethod[0].PaymentMethodId;
			PaymentMethodName = paymentMethodDto.PaymentMethod[0].Name;
		}
	}
}