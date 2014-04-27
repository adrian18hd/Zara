using System;
using Zara.Importer;
using Zara.Web.Views.BaseClasses;

namespace Zara.Web.Views.Pages.StartPage
{
	public partial class StartPage : TemplatePageBase<ContentTypes.StartPage>
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			CatalogEntryImport import = new CatalogEntryImport();
			import.CreateProducts();
		}
	}
}