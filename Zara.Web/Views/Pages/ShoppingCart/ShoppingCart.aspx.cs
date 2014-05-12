using System;
using Mediachase.Commerce.Orders;
using Mediachase.Commerce.Website.Helpers;
using Zara.Web.Views.BaseClasses;
using Zara.Web.Views.Pages.ShoppingCart.ContentTypes;

namespace Zara.Web.Views.Pages.ShoppingCart
{
	public partial class ShoppingCart : TemplatePageBase<ShoppingCartPage>
	{
		private readonly CartHelper _cartHelper = new CartHelper(Cart.DefaultName);

		protected bool CartIsEmpty
		{
			get { return _cartHelper.IsEmpty; }
		}

		protected override void OnLoad(EventArgs e)
		{
			lblEmptyShoppingCartMessage.Visible = CartIsEmpty;
		}
	}
}