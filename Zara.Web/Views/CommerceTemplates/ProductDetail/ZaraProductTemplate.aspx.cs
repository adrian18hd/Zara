using System;
using System.Linq;
using EPiServer.Framework.DataAnnotations;
using Zara.Web.CatalogModels.Product;
using Zara.Web.Views.BaseClasses;

namespace Zara.Web.Views.CommerceTemplates.ProductDetail
{
	[TemplateDescriptor(Default = true)]
	public partial class ZaraProductTemplate : RendererBase<ZaraProductContent>
	{
	}
}