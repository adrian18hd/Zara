using System;
using EPiServer;
using EPiServer.Core;
using Mediachase.Commerce.Orders;
using Mediachase.Commerce.Website.Helpers;
using Zara.Web.Views.Pages.StartPage.ContentTypes;

namespace Zara.Web.Views
{
	public partial class MasterPage : System.Web.UI.MasterPage
	{
		private readonly StartPage _startPage = DataFactory.Instance.GetPage(ContentReference.StartPage) as StartPage;
		private readonly CartHelper _cartHelper = new CartHelper(Cart.DefaultName);

		protected string ShoppingCartPageLink
		{
			get
			{
				if (_startPage == null)
					return String.Empty;
				if (_startPage.Settings != null && _startPage.Settings.ShoppingCartPage != null)
					return DataFactory.Instance.GetPage(_startPage.Settings.ShoppingCartPage).LinkURL;
				return String.Empty;
			}
		}

		protected int NumberOfItemsInCart
		{
			get { return Convert.ToInt32(_cartHelper.GetTotalItemCount()); }
		}

		protected override void OnLoad(EventArgs e)
		{
		}
	}
}