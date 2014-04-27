using System;
using System.Linq;
using EPiServer.ServiceLocation;
using Mediachase.Commerce.Catalog;
using Mediachase.Commerce.Catalog.Dto;
using Mediachase.Commerce.Core;
using Mediachase.Commerce.Storage;
using Mediachase.MetaDataPlus;
using Mediachase.MetaDataPlus.Configurator;

namespace Zara.Importer
{
	public class CatalogNodeImport
	{
		private readonly ICatalogSystem _catalogSystem;
		private readonly int _catalogId;
		private readonly Guid _applicationId;
		private const string MetaClassName = "ZaraCatalogNode";
		private readonly MetaDataContext _metaDataContext;

		public CatalogNodeImport()
		{
			_catalogSystem = ServiceLocator.Current.GetInstance<ICatalogSystem>();
			_applicationId = AppContext.Current.ApplicationId;
			_catalogId = ImportManager.GetDefaultCatalogId();
			_metaDataContext = CatalogContext.MetaDataContext;
		}

		public void CreateCatalogNodeStructure()
		{
			//check to see if we have any catalog node
			if (_catalogSystem.GetCatalogNodesDto(_catalogId).CatalogNode.Any())
				return;
			/*
			 
			 * Barbati - Men
				* Sacouri - Coats
				* Pantaloni - Pants
			 * Femei - Women
				* Paltoane - Overcoats
				* Rochii - Dresses
			 
			 */

			int menCatalogNodeId = CreateCatalogNode("Men", "Men", "Barbati", "Men", 0);
			CreateCatalogNode("Coats", "Coats", "Sacouri", "Coats", menCatalogNodeId);
			CreateCatalogNode("Pants", "Pants", "Pantaloni", "Pants", menCatalogNodeId);
			
			int womenCatalogNodeId = CreateCatalogNode("Women", "Women", "Femei", "Women", 0);
			CreateCatalogNode("Overcoats", "Overcoats", "Paltoane", "Overcoats", womenCatalogNodeId);
			CreateCatalogNode("Dresses", "Dresses", "Rochii", "Dresses", womenCatalogNodeId);
		}

		private int CreateCatalogNode(string name, string enDisplayName, string swDisplayName, string code,
			int parentCatalogNodeId)
		{
			CatalogNodeDto dto = new CatalogNodeDto();
			CatalogNodeDto.CatalogNodeRow newCatalogNodeRow = dto.CatalogNode.NewCatalogNodeRow();

			//all nodes will have the same meta class: ZaraCatalogNode
			//load the meta class
			MetaClass metaClass = MetaClass.Load(_metaDataContext, MetaClassName);

			//set the catalog node properties
			newCatalogNodeRow.ApplicationId = _applicationId;
			newCatalogNodeRow.CatalogId = _catalogId;
			newCatalogNodeRow.Code = code;
			newCatalogNodeRow.Name = name;
			newCatalogNodeRow.EndDate = DateTime.UtcNow.AddYears(10);
			newCatalogNodeRow.IsActive = true;
			newCatalogNodeRow.MetaClassId = metaClass.Id;
			newCatalogNodeRow.ParentNodeId = parentCatalogNodeId;
			newCatalogNodeRow.SortOrder = 0;
			newCatalogNodeRow.StartDate = DateTime.UtcNow;
			dto.CatalogNode.AddCatalogNodeRow(newCatalogNodeRow);
			//save catalog node to database.
			//note that CatalogItemSeo is populated automatically
			_catalogSystem.SaveCatalogNode(dto);

			//after the node is saved in the database we create the meta class records that will go in the table CatalogNodeEx_ZaraCatalogNode
			//create meta class record for the saved catalog node
			_metaDataContext.UseCurrentThreadCulture = false;
			//we set the values in both languages en and sw
			_metaDataContext.Language = "en";
			MetaObject metaObject = new MetaObject(metaClass, newCatalogNodeRow.CatalogNodeId);
			MetaHelper.SetMetaFieldValue(_metaDataContext, metaObject, "DisplayName", new object[] {enDisplayName});
			metaObject.AcceptChanges(_metaDataContext);

			_metaDataContext.Language = "sv";
			MetaHelper.SetMetaFieldValue(_metaDataContext, metaObject, "DisplayName", new object[] {swDisplayName});
			metaObject.AcceptChanges(_metaDataContext);

			_metaDataContext.UseCurrentThreadCulture = true;

			//return the saved catalog node id
			return newCatalogNodeRow.CatalogNodeId;
		}
	}
}