using System;
using System.Collections.Generic;
using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Commerce.Catalog.Linking;
using Zara.Web.CatalogModels.Product;
using Zara.Web.Views.BaseClasses;

namespace Zara.Web.Views.CommerceTemplates.ProductListing.Units
{
	public partial class EntriesList : RendererControlBase<NodeContent>
	{
		protected override void OnLoad(EventArgs e)
		{
			IEnumerable<ProductContent> entries = ContentLoader.GetChildren<ProductContent>(CurrentData.ContentLink);
			//foreach (ProductContent productContent in entries)
			//{
				//ZaraProductContent testProdContent = productContent as ZaraProductContent;
				//IEnumerable<Relation> variantRelations = testProdContent.GetVariantRelations();
				//foreach (Relation variantRelation in variantRelations)
				//{
				//	VariationContent variationContent = ContentLoader.Get<VariationContent>(variantRelation.Target);
				//	ZaraVariantContent zaraVaraint = variationContent as ZaraVariantContent;
				//	var entry = zaraVaraint.LoadEntry();
				//	ContentLoader
				//}
			//}
			rptEntries.DataSource = entries;
			rptEntries.DataBind();
		}
	}
}