using System;
using EPiServer.Commerce.Catalog.ContentTypes;
using Zara.Web.CatalogModels.Product;
using Zara.Web.Views.BaseClasses;

namespace Zara.Web.Views.CommerceTemplates.ProductListing.Units
{
	public partial class SimpleProduct : RendererControlBase<ProductContent>
	{
		public ZaraProductContent ZaraProductContent { get; set; }

		protected override void OnLoad(EventArgs e)
		{
			if (ZaraProductContent == null)
			{
				Visible = false;
				return;
			}
		}
	}
}