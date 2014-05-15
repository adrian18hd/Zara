using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EPiServer.Core;
using EPiServer.DataAnnotations;
using Zara.Web.Business;

namespace Zara.Web.Views.Pages.StartPage.ContentTypes
{
	[ContentType(
		GUID = "2441459E-517F-43DF-9E06-3DF8A201BC4F",
		AvailableInEditMode = false,
		DisplayName = "Commerce Settings",
		Description = "Commerce settings for the site",
		GroupName = Constants.ContentGroups.Commerce,
		Order = 1
		)]
	public class CommerceSettingsBlock : BlockData
	{
		public virtual PageReference ShoppingCartPage { get; set; }

		public virtual PageReference CheckoutPage { get; set; }
	}
}