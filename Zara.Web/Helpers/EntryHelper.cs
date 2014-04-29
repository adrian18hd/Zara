using Mediachase.Commerce;
using Mediachase.Commerce.Catalog;
using Mediachase.Commerce.Catalog.Managers;
using Mediachase.Commerce.Catalog.Objects;
using Mediachase.Commerce.Website.Helpers;

namespace Zara.Web.Helpers
{
	public static class EntryHelper
	{
		public static decimal LoadEntryPrice(string entryCode)
		{
			Entry entry = CatalogContext.Current.GetCatalogEntry(entryCode,
				new CatalogEntryResponseGroup(CatalogEntryResponseGroup.ResponseGroup.CatalogEntryFull));
			Price price = new Price(new Money(0, Currency.USD));
			if (entry.EntryType == EntryType.Variation)
			{
				price = StoreHelper.GetSalePrice(entry, 1);
			}
			else if (entry.Entries.TotalResults > 0)
			{
				Entry firstVariant = entry.Entries[0];
				price = StoreHelper.GetSalePrice(firstVariant, 1);
			}
			return price.Money.Amount;
		}
	}
}