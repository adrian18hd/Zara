using System;
using System.Web.UI;
using EPiServer;
using EPiServer.Core;
using Mediachase.Commerce.Orders;
using Mediachase.Commerce.Website.Helpers;
using Zara.Web.Views.BaseClasses;
using Zara.Web.Views.Pages.ShoppingCart.ContentTypes;

namespace Zara.Web.Views.Pages.ShoppingCart
{
	public partial class ShoppingCart : TemplatePageBase<ShoppingCartPage>
	{
		readonly CartHelper _cartHelper = new CartHelper(Cart.DefaultName);

		protected override void OnLoad(EventArgs e)
		{
			_cartHelper.Reset();
			_cartHelper.Cart.TaxTotal = 0;
			_cartHelper.Cart.HandlingTotal = 0;
			_cartHelper.OrderForm.TaxTotal = 0;
			_cartHelper.Cart.AcceptChanges();
		}

		protected override void Render(HtmlTextWriter writer)
		{
			lblEmptyShoppingCartMessage.Visible = _cartHelper.IsEmpty;
			navCheckout.Visible = !_cartHelper.IsEmpty;
			base.Render(writer);
		}

		public string CheckoutPageLink
		{
			get
			{
				StartPage.ContentTypes.StartPage startPage =
					DataFactory.Instance.GetPage(ContentReference.StartPage) as StartPage.ContentTypes.StartPage;
				if (startPage != null && startPage.Settings.CheckoutPage != null)
				{
					return DataFactory.Instance.GetPage(startPage.Settings.CheckoutPage).LinkURL;
				}
				return string.Empty;
			}
		}
	}
}