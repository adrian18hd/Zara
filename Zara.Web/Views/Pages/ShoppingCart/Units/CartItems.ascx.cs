using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Core;
using EPiServer.ServiceLocation;
using Mediachase.Commerce;
using Mediachase.Commerce.Catalog;
using Mediachase.Commerce.Engine;
using Mediachase.Commerce.Orders;
using Mediachase.Commerce.Website.Helpers;
using Zara.Web.CatalogModels.Product;
using Zara.Web.Views.BaseClasses;

namespace Zara.Web.Views.Pages.ShoppingCart.Units
{
	public partial class CartItems : RendererControlBase<CatalogContentBase>
	{
		private readonly CartHelper _cartHelper = new CartHelper(Cart.DefaultName);
		private readonly IMarket _currentMarket = ServiceLocator.Current.GetInstance<ICurrentMarket>().GetCurrentMarket();

		protected override void OnLoad(EventArgs e)
		{
			if (_cartHelper.IsEmpty)
			{
				Visible = false;
				return;
			}
			pnlWorkflowWarnings.Visible = false;
			HandleFormPost();
			BindData();
		}

		protected string GetLineItemUrl(LineItem lineItem)
		{
			ContentReference contentLink = ReferenceConverter.GetContentLink(lineItem.ParentCatalogEntryId,
				CatalogContentType.CatalogEntry);
			string language = _currentMarket.DefaultLanguage.Name;
			return UrlResolver.GetUrl(contentLink, language);
		}

		protected string LineItemSize(LineItem lineItem)
		{
			ZaraVariantContent variantContent = GetZaraVariantContentBasedOnLineItem(lineItem);
			return variantContent == null ? string.Empty : variantContent.Size;
		}

		protected string LineItemColor(LineItem lineItem)
		{
			ZaraVariantContent variantContent = GetZaraVariantContentBasedOnLineItem(lineItem);
			return variantContent == null ? string.Empty : variantContent.Color;
		}

		private ZaraVariantContent GetZaraVariantContentBasedOnLineItem(LineItem lineItem)
		{
			if (lineItem == null) return null;
			ContentReference variantReference = ReferenceConverter.GetContentLink(lineItem.CatalogEntryId,
				CatalogContentType.CatalogEntry);
			ZaraVariantContent zaraVariantContent = ContentLoader.Get<ZaraVariantContent>(variantReference);
			return zaraVariantContent;
		}

		private void BindData()
		{
			WorkflowResults workflowResults = _cartHelper.Cart.RunWorkflow("CartValidate");
			StringBuilder warnings = new StringBuilder();
			StringDictionary warningsDictionary = workflowResults.OutputParameters["Warnings"] as StringDictionary;
			if (warningsDictionary != null)
			{
				foreach (string key in warningsDictionary.Keys)
				{
					warnings.AppendFormat("<li>{0}</li>", warningsDictionary[key]);
				}
			}
			if (warnings.Length > 0)
			{
				ltWarningList.Text = warnings.ToString();
				pnlWorkflowWarnings.Visible = true;
			}
			if (_cartHelper.IsEmpty)
				_cartHelper.Delete();
			_cartHelper.Cart.AcceptChanges();
			BindLineItems();
		}

		private void HandleFormPost()
		{
			if (Request["UpdateItems"] == null) return;
			Dictionary<int, int> lineItemsWithQuantity = new Dictionary<int, int>();
			int[] lineItemsIds = Request["lineItemId"].Split(',').Select(item => Convert.ToInt32(item)).ToArray();
			int[] quantities = Request["quantity"].Split(',').Select(item => Convert.ToInt32(item)).ToArray();
			for (int i = 0; i < lineItemsIds.Length; i++)
			{
				lineItemsWithQuantity.Add(lineItemsIds[i], quantities[i]);
			}
			foreach (KeyValuePair<int, int> item in lineItemsWithQuantity)
			{
				LineItem lineItem = _cartHelper.LineItems.First(li => li.LineItemId == item.Key);
				if (lineItem.Quantity != item.Value)
				{
					if (item.Value == 0)
						lineItem.Delete();
					else
						lineItem.Quantity = item.Value;
				}
			}
		}

		private void BindLineItems()
		{
			if (_cartHelper.IsEmpty)
			{
				rptLineItems.Visible = false;
			}
			else
			{
				rptLineItems.DataSource = _cartHelper.LineItems;
				rptLineItems.DataBind();
			}
		}
	}
}