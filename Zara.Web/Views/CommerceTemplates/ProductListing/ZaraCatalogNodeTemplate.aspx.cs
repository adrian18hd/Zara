using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using EPiServer;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.Framework.DataAnnotations;
using EPiServer.Web;
using EPiServer.Web.WebControls;
using Zara.Web.CatalogModels.Category;
using Zara.Web.Views.BaseClasses;

namespace Zara.Web.Views.CommerceTemplates.ProductListing
{
	[TemplateDescriptor(Default = true, Path = "~/Views/CommerceTemplates/ProductListing/ZaraCatalogNodeTemplate.aspx")]
	public partial class ZaraCatalogNodeTemplate : RendererBase<ZaraCategoryContent>
	{
	}
}