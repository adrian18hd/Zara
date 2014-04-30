using System;
using System.Collections.Generic;
using System.Linq;
using EPiServer.ServiceLocation;
using Mediachase.Commerce;
using Mediachase.Commerce.Catalog;
using Mediachase.Commerce.Catalog.Dto;
using Mediachase.Commerce.Catalog.Objects;
using Mediachase.Commerce.Core;
using Mediachase.Commerce.Inventory;
using Mediachase.Commerce.Pricing;
using Mediachase.Commerce.Storage;
using Mediachase.MetaDataPlus;
using Mediachase.MetaDataPlus.Configurator;

namespace Zara.Importer
{
	public class CatalogEntryImport
	{
		private const string ProductMetaClassName = "ZaraProduct";
		private const string VariantMetaClassName = "ZaraVariant";
		private const string ParentCatalogNodeCode = "Overcoats";
		private const string WarehouseCode = "default";

		private readonly ICatalogSystem _catalogSystem;
		private readonly int _catalogId;
		private readonly CatalogNode _parentCatalogNode;
		private readonly Guid _applicationId;

		private readonly MetaDataContext _metaDataContext;
		private readonly IWarehouseInventoryService _warehouseInventoryService;
		private readonly IPriceService _priceService;

		public CatalogEntryImport()
		{
			_catalogSystem = ServiceLocator.Current.GetInstance<ICatalogSystem>();
			_applicationId = AppContext.Current.ApplicationId;
			_catalogId = ImportManager.GetDefaultCatalogId();
			_metaDataContext = CatalogContext.MetaDataContext;
			_parentCatalogNode = _catalogSystem.GetCatalogNode(ParentCatalogNodeCode);
			_warehouseInventoryService = ServiceLocator.Current.GetInstance<IWarehouseInventoryService>();
			_priceService = ServiceLocator.Current.GetInstance<IPriceService>();
		}

		public void CreateProducts()
		{
			CatalogEntryDto entryDto = _catalogSystem.GetCatalogEntriesDto(_catalogId, _parentCatalogNode.CatalogNodeId);
			if (entryDto.CatalogEntry.Any()) return;

			int prod1Id = CreateProductEntry("2496128", "JACQUARD COAT WITH POCKETS", "2496128", "178 CM");
			CreateVariants(prod1Id, "2496128", "JACQUARD COAT WITH POCKETS", new List<string> {"Light Green", "Blue"},
				new List<string> {"XS", "S", "M", "L", "XL"}, 479);
		}

		private int CreateProductEntry(string code, string name, string refNumber, string height)
		{
			MetaClass metaClass = MetaClass.Load(_metaDataContext, ProductMetaClassName);

			CatalogEntryDto entryDto = new CatalogEntryDto();
			CatalogEntryDto.CatalogEntryRow productRow = entryDto.CatalogEntry.NewCatalogEntryRow();
			PopulateEntryRow(name, code, metaClass.Id, EntryType.Product, ref productRow);

			//add row to dataset
			entryDto.CatalogEntry.AddCatalogEntryRow(productRow);
			//save entry
			_catalogSystem.SaveCatalogEntry(entryDto);

			_metaDataContext.UseCurrentThreadCulture = false;
			_metaDataContext.Language = "en";

			//set meta data properties
			MetaObject metaObject = new MetaObject(metaClass, productRow.CatalogEntryId);
			//DisplayName meta field is associated automatically by EPiServer to every catalog meta class
			MetaHelper.SetMetaFieldValue(_metaDataContext, metaObject, "DisplayName", new object[] {name});
			MetaHelper.SetMetaFieldValue(_metaDataContext, metaObject, "RefNumber", new object[] {refNumber});
			MetaHelper.SetMetaFieldValue(_metaDataContext, metaObject, "Height", new object[] {height});
			metaObject.AcceptChanges(_metaDataContext);

			_metaDataContext.Language = "sv";
			MetaHelper.SetMetaFieldValue(_metaDataContext, metaObject, "DisplayName", new object[] { name });
			MetaHelper.SetMetaFieldValue(_metaDataContext, metaObject, "RefNumber", new object[] { refNumber });
			MetaHelper.SetMetaFieldValue(_metaDataContext, metaObject, "Height", new object[] { height });
			metaObject.AcceptChanges(_metaDataContext);

			_metaDataContext.UseCurrentThreadCulture = true;

			//add the entry to the category
			AddEntryToCategory(productRow);
			//return entry id
			return productRow.CatalogEntryId;
		}

		private void CreateVariants(int productId, string code, string name, List<string> colors, List<string> sizes,
			decimal price)
		{
			MetaClass metaClass = MetaClass.Load(_metaDataContext, VariantMetaClassName);

			CatalogEntryDto entryDto = new CatalogEntryDto();

			//we create a variant entry for each combination of color and size
			foreach (string color in colors)
			{
				foreach (string size in sizes)
				{
					//create entry first
					CatalogEntryDto.CatalogEntryRow variantRow = entryDto.CatalogEntry.NewCatalogEntryRow();
					string variantCode = String.Format("{0}_{1}_{2}", code, color, size).Replace(' ', '_');
					PopulateEntryRow(name, variantCode, metaClass.Id, EntryType.Variation,
						ref variantRow);

					//add row to dataset
					entryDto.CatalogEntry.AddCatalogEntryRow(variantRow);
					//save variant
					_catalogSystem.SaveCatalogEntry(entryDto);

					#region set meta data properties

					_metaDataContext.UseCurrentThreadCulture = false;
					_metaDataContext.Language = "en";
					MetaObject metaObject = new MetaObject(metaClass, variantRow.CatalogEntryId);
					//DisplayName meta field is associated automatically by EPiServer to every catalog meta class
					MetaHelper.SetMetaFieldValue(_metaDataContext, metaObject, "DisplayName", new object[] {name});
					MetaHelper.SetMetaFieldValue(_metaDataContext, metaObject, "Color", new object[] {color});
					MetaHelper.SetMetaFieldValue(_metaDataContext, metaObject, "Size", new object[] {size});
					metaObject.AcceptChanges(_metaDataContext);

					_metaDataContext.Language = "sv";
					MetaHelper.SetMetaFieldValue(_metaDataContext, metaObject, "DisplayName", new object[] { name });
					MetaHelper.SetMetaFieldValue(_metaDataContext, metaObject, "Color", new object[] { color });
					MetaHelper.SetMetaFieldValue(_metaDataContext, metaObject, "Size", new object[] { size });
					metaObject.AcceptChanges(_metaDataContext);
					_metaDataContext.UseCurrentThreadCulture = true;
					#endregion

					#region add the variant entry to the category

					AddEntryToCategory(variantRow);

					#endregion

					#region add inventory to the variant

					CatalogKey variantCatalogKey = new CatalogKey(variantRow);
					IWarehouseInventory warehouseInventory = new WarehouseInventory
					{
						CatalogKey = variantCatalogKey,
						AllowBackorder = true,
						AllowPreorder = true,
						BackorderQuantity = 10,
						InStockQuantity = 1000,
						InventoryStatus = InventoryTrackingStatus.Enabled,
						WarehouseCode = WarehouseCode,
						BackorderAvailabilityDate = DateTime.UtcNow,
						PreorderAvailabilityDate = DateTime.UtcNow,
						PreorderQuantity = 10,
						ReservedQuantity = 0,
						ReorderMinQuantity = 1
					};
					_warehouseInventoryService.Save(warehouseInventory);

					#endregion

					#region add pricing to the variant

					IPriceValue priceValue = new PriceValue
					{
						CatalogKey = variantCatalogKey,
						CustomerPricing = new CustomerPricing(CustomerPricing.PriceType.AllCustomers, String.Empty),
						MarketId = MarketId.Default,
						MinQuantity = 1,
						UnitPrice = new Money(price, Currency.USD),
						ValidFrom = DateTime.UtcNow,
						ValidUntil = DateTime.UtcNow.AddYears(20)
					};
					_priceService.SetCatalogEntryPrices(variantCatalogKey, new[] {priceValue});

					#endregion

					#region add variant entry as variant of the parent product

					CatalogRelationDto relationDto = new CatalogRelationDto();
					CatalogRelationDto.CatalogEntryRelationRow entryRelationRow =
						relationDto.CatalogEntryRelation.NewCatalogEntryRelationRow();
					entryRelationRow.ParentEntryId = productId;
					entryRelationRow.ChildEntryId = variantRow.CatalogEntryId;
					entryRelationRow.RelationTypeId = EntryRelationType.ProductVariation;
					entryRelationRow.Quantity = 1;
					entryRelationRow.SortOrder = 0;
					entryRelationRow.GroupName = "default";
					relationDto.CatalogEntryRelation.AddCatalogEntryRelationRow(entryRelationRow);
					_catalogSystem.SaveCatalogRelationDto(relationDto);

					#endregion
				}
			}
		}

		private void AddEntryToCategory(CatalogEntryDto.CatalogEntryRow entryRow)
		{
			CatalogRelationDto catalogRelationDto = new CatalogRelationDto();
			CatalogRelationDto.NodeEntryRelationRow nodeEntryRelationRow =
				catalogRelationDto.NodeEntryRelation.NewNodeEntryRelationRow();
			nodeEntryRelationRow.CatalogEntryId = entryRow.CatalogEntryId;
			nodeEntryRelationRow.CatalogId = _catalogId;
			nodeEntryRelationRow.CatalogNodeId = _parentCatalogNode.CatalogNodeId;
			nodeEntryRelationRow.SortOrder = 0;
			catalogRelationDto.NodeEntryRelation.AddNodeEntryRelationRow(nodeEntryRelationRow);
			//save relation
			_catalogSystem.SaveCatalogRelationDto(catalogRelationDto);
		}

		private void PopulateEntryRow(string name, string code, int metaClassId, string entryType,
			ref CatalogEntryDto.CatalogEntryRow entryRow)
		{
			entryRow.ApplicationId = _applicationId;
			entryRow.CatalogId = _catalogId;
			entryRow.ClassTypeId = entryType;
			entryRow.Code = code;
			entryRow.EndDate = DateTime.UtcNow.AddYears(20);
			entryRow.IsActive = true;
			entryRow.Name = name;
			entryRow.StartDate = DateTime.UtcNow;
			entryRow.MetaClassId = metaClassId;
		}
	}
}