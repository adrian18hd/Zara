using System;
using System.Collections.Generic;
using EPiServer.Commerce.Catalog.ContentTypes;
using Zara.Web.Views.BaseClasses;

namespace Zara.Web.Views.CommerceTemplates.ProductListing.Units
{
	public partial class SubCategoriesList : RendererControlBase<NodeContent>
	{
		protected override void OnLoad(EventArgs e)
		{
			IEnumerable<NodeContent> nodes = ContentLoader.GetChildren<NodeContent>(CurrentData.ContentLink);
			rptSubCategories.DataSource = nodes;
			rptSubCategories.DataBind();
		}
	}
}