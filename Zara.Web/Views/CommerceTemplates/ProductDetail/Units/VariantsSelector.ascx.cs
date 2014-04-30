using System;
using System.Collections.Generic;
using System.Linq;
using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Commerce.Catalog.Linking;
using Zara.Web.CatalogModels.Product;
using Zara.Web.Views.BaseClasses;

namespace Zara.Web.Views.CommerceTemplates.ProductDetail.Units
{
	public partial class VariantsSelector : RendererControlBase<ProductContent>
	{
		private List<ZaraVariantContent> _variants;

		protected List<ZaraVariantContent> Variants
		{
			get
			{
				if (_variants == null)
				{
					_variants = new List<ZaraVariantContent>();
					IEnumerable<Relation> variantRelations = CurrentData.GetVariantRelations();
					foreach (Relation variantRelation in variantRelations)
					{
						ZaraVariantContent variantContent = ContentLoader.Get<ZaraVariantContent>(variantRelation.Target);
						if (variantContent != null)
							_variants.Add(variantContent);
					}
				}
				return _variants;
			}
		}

		protected List<string> Colors
		{
			get { return Variants.Select(c => c.Color).Distinct().ToList(); }
		}

		protected List<string> Sizes
		{
			get { return Variants.Select(c => c.Size).Distinct().ToList(); }
		}

		protected override void OnLoad(EventArgs e)
		{
			if (Variants == null || Variants.Count == 0)
			{
				Visible = false;
				return;
			}

			BindColorsAndSizes();

			//Get all relations for the current product
			//IEnumerable<Association> associations = LinksRepository.GetAssociations(CurrentData.ContentLink).Where(a=>a.Group.Name == "YouMightAlsoLike");
			//var emtry = ContentLoader.Get<EntryContentBase>(associations.First().Target);
		}

		private void BindColorsAndSizes()
		{
			rptColors.DataSource = Colors;
			rptColors.DataBind();

			rptSizes.DataSource = Sizes;
			rptSizes.DataBind();
		}
	}
}