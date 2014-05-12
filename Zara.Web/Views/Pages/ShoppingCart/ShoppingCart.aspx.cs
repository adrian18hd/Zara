using System;
using System.Web.UI;
using Mediachase.Commerce.Orders;
using Mediachase.Commerce.Website.Helpers;
using Zara.Web.Views.BaseClasses;
using Zara.Web.Views.Pages.ShoppingCart.ContentTypes;

namespace Zara.Web.Views.Pages.ShoppingCart
{
	public partial class ShoppingCart : TemplatePageBase<ShoppingCartPage>
	{
		protected override void Render(HtmlTextWriter writer)
		{
			CartHelper cartHelper = new CartHelper(Cart.DefaultName);
			lblEmptyShoppingCartMessage.Visible = cartHelper.IsEmpty;
			base.Render(writer);
		}
	}
}