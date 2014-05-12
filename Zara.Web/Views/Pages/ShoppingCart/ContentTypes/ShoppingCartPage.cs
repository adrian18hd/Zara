using System.ComponentModel.DataAnnotations;
using EPiServer.DataAnnotations;
using Zara.Web.Views.BaseClasses;

namespace Zara.Web.Views.Pages.ShoppingCart.ContentTypes
{
	[ContentType(DisplayName = "Shopping Cart Page", GUID = "29b1cd40-c9ef-4a4c-bc4b-7fc1d27ca19a", Description = "")]
	public class ShoppingCartPage : SitePageDataBase
	{
		[CultureSpecific]
		[Display(Name = "Empty shopping cart message")]
		public virtual string EmptyShoppingCartMessage { get; set; }
	}
}