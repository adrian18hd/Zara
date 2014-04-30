using System.ComponentModel.DataAnnotations;
using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Commerce.Catalog.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAnnotations;

namespace Zara.Web.CatalogModels.Product
{
	/// <summary>
	///     Represents the model for ZaraVariant meta class. Behind the scenes EPiServer creates the ZaraVariant meta class and
	///     creates meta fields for each property (if they are not already created).
	/// </summary>
	[CatalogContentType(DisplayName = "Zara Variant", MetaClassName = "ZaraVariant",
		GUID = "46AC87F0-D65F-4430-960E-B1ADB7581583")]
	public class ZaraVariantContent : VariationContent
	{
		[Display(Name = "Main Content")]
		[CultureSpecific]
		public virtual XhtmlString MainContent { get; set; }

		[Display(Name = "Color")]
		[CultureSpecific]
		public virtual string Color { get; set; }

		[Display(Name = "Size")]
		[CultureSpecific]
		public virtual string Size { get; set; }

		public virtual string NewField { get; set; }
	}
}