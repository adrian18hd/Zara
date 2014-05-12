using System;
using System.Collections.Generic;
using System.Linq;
using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Commerce.Catalog.Linking;
using Mediachase.Commerce;
using Mediachase.Commerce.Catalog.Managers;
using Mediachase.Commerce.Catalog.Objects;
using Mediachase.Commerce.Inventory;
using Mediachase.Commerce.Orders;
using Mediachase.Commerce.Website.Helpers;
using Zara.Web.CatalogModels.Product;
using Zara.Web.Helpers;
using Zara.Web.Views.BaseClasses;

namespace Zara.Web.Views.CommerceTemplates.ProductDetail.Units
{
	public partial class VariantsSelector : RendererControlBase<ProductContent>
	{
		private const string ColorRequestParam = "color";
		private const string SizeRequestParam = "size";
		private const string AddToCartRequestParam = "addToCart";
		private const string QuantityRequestParam = "quantity";
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

		protected string Price
		{
			get { return EntryHelper.LoadEntryPrice(Variants.First().Code).ToString("C2"); }
		}

		protected override void OnLoad(EventArgs e)
		{
			pnlError.Visible = pnlSuccess.Visible = false;
			if (Variants == null || Variants.Count == 0)
			{
				Visible = false;
				return;
			}

			BindColorsAndSizes();

			HandlePostEvent();

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

		private void HandlePostEvent()
		{
			if (Request[AddToCartRequestParam] != null)
			{
				string color = Request[ColorRequestParam] ?? string.Empty;
				string size = Request[SizeRequestParam] ?? string.Empty;
				int quantity;
				int.TryParse(Request[QuantityRequestParam], out quantity);
				ZaraVariantContent variant =
					Variants.FirstOrDefault(
						v =>
							v.Color.Equals(color, StringComparison.OrdinalIgnoreCase) &&
							v.Size.Equals(size, StringComparison.OrdinalIgnoreCase));
				if (variant != null)
				{
					string errorMessage = AddToCart(variant, quantity);
					if (!String.IsNullOrWhiteSpace(errorMessage))
					{
						ShowError(errorMessage);
					}
					else
					{
						ShowSuccess("Product was added to cart");
					}
				}
				else
				{
					ShowError(String.Format("Unable to find variation with Size = '{0}' and Color = '{1}'", size, color));
				}
			}
		}

		private string AddToCart(VariationContent variant, int quantity)
		{
			CartHelper cartHelper = new CartHelper(Cart.DefaultName);
			Entry entry =
				variant.LoadEntry(CatalogEntryResponseGroup.ResponseGroup.CatalogEntryFull);
			IWarehouseInventory warehouseInventory = entry.WarehouseInventories.WarehouseInventory.FirstOrDefault();
			if (warehouseInventory == null)
				return "No warehouse inventory set for the variant.";
			string errorMessage;
			if (AllowAddToCart(entry, warehouseInventory, quantity, out errorMessage))
			{
				cartHelper.AddEntry(entry, quantity, false);
			}
			return errorMessage;
		}

		private bool AllowAddToCart(Entry entry, IWarehouseInventory inventory, int quantity, out string errorMessage)
		{
			errorMessage = String.Empty;
			if (inventory.InventoryStatus != InventoryTrackingStatus.Enabled) return true;
			if (entry.StartDate > FrameworkContext.Current.CurrentDateTime)
			{
				if (inventory.AllowPreorder)
				{
					if (inventory.PreorderAvailabilityDate > FrameworkContext.Current.CurrentDateTime)
					{
						errorMessage = "Preorder is not available";
						return false;
					}
					if (quantity > inventory.PreorderQuantity)
					{
						errorMessage = "Not enough for Pre-order quantity";
						return false;
					}
				}
				else
				{
					errorMessage = "Product is not yet available. Pre-order is not allowed";
					return false;
				}
			}
			if (inventory.InStockQuantity > 0 &&
			    inventory.InStockQuantity >= inventory.ReservedQuantity + quantity)
			{
				return true;
			}

			//Not enough quantity in stock, check for backorder
			if (!inventory.AllowBackorder)
			{
				if (inventory.InStockQuantity < quantity ||
				    (inventory.InStockQuantity -
				     inventory.ReservedQuantity) < quantity)
				{
					errorMessage = "Out of stock.";
					return false;
				}
				errorMessage = "Not enough quantity.";
				return false;
			}

			if (inventory.BackorderAvailabilityDate > FrameworkContext.Current.CurrentDateTime)
			{
				errorMessage = "Not enough quantity. Backorder is unavailable.";
				return false;
			}
			if (quantity > inventory.InStockQuantity -
			    inventory.ReservedQuantity + inventory.BackorderQuantity)
			{
				errorMessage = "Not enough for Backorder quantity.";
				return false;
			}

			return true;
		}

		private void ShowError(string errorMessage)
		{
			lblError.Text = errorMessage;
			pnlError.Visible = true;
		}

		private void ShowSuccess(string message)
		{
			lblSuccess.Text = message;
			pnlSuccess.Visible = true;
		}
	}
}