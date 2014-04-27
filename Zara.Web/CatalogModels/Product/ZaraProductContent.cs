using System.ComponentModel.DataAnnotations;
using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Commerce.Catalog.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAnnotations;

namespace Zara.Web.CatalogModels.Product
{
	/// <summary>
	/// Represents the model for ZaraProduct meta class. Behind the scenes EPiServer creates the ZaraProduct meta class and
	/// creates meta fields for each property (if they are not already created).
	/// </summary>
	[CatalogContentType(DisplayName = "Zara Product", MetaClassName = "ZaraProduct",
		GUID = "654DC6F2-7622-45F6-A5EB-BEA9B9760EDC")]
	public class ZaraProductContent : ProductContent
	{
		[Display(Name = "Reference number")]
		public virtual string RefNumber { get; set; }

		[Display(Name = "Model height")]
		public virtual string Height { get; set; }

		[Display(Name = "Main Content")]
		[CultureSpecific]
		public virtual XhtmlString MainContent { get; set; }

		[Display(Name = "Composition")]
		[CultureSpecific]
		public virtual XhtmlString Composition { get; set; }
	}
}