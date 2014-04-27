using System.Linq;
using EPiServer.ServiceLocation;
using Mediachase.Commerce.Catalog;
using Mediachase.Commerce.Catalog.Dto;

namespace Zara.Importer
{
	public static class ImportManager
	{
		public static int GetDefaultCatalogId()
		{
			CatalogDto catalogDto = ServiceLocator.Current.GetInstance<ICatalogSystem>().GetCatalogDto();
			return catalogDto.Catalog.First().CatalogId;
		}

		public static void Import()
		{
			CatalogImport catalogImport = new CatalogImport();
			catalogImport.CreateZaraCatalog();
			CatalogNodeImport catalogNodeImport = new CatalogNodeImport();
			catalogNodeImport.CreateCatalogNodeStructure();
			CatalogEntryImport catalogEntryImport = new CatalogEntryImport();
			catalogEntryImport.CreateProducts();
		}
	}
}