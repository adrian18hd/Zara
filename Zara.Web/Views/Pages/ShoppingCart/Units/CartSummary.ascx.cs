using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EPiServer.Commerce.Catalog.ContentTypes;
using Mediachase.Commerce.Orders;
using Mediachase.Commerce.Website.Helpers;
using Zara.Web.Views.BaseClasses;

namespace Zara.Web.Views.Pages.ShoppingCart.Units
{
	public partial class CartSummary : RendererControlBase<CatalogContentBase>
	{
		protected override void Render(HtmlTextWriter writer)
		{
			BindCartSummary();
			base.Render(writer);
		}

		private decimal GetDiscountTotal(Cart cart)
		{
			decimal discountTotal = 0;
			foreach (OrderForm orderForm in cart.OrderForms.ToArray())
			{
				foreach (LineItem lineItem in orderForm.LineItems.ToArray())
				{
					discountTotal += lineItem.LineItemDiscountAmount;
				}
				if (orderForm.Shipments != null && orderForm.Shipments.Count > 0)
				{
					foreach (Shipment shipment in orderForm.Shipments.ToArray())
					{
						discountTotal += shipment.ShippingDiscountAmount;
					}
				}
				discountTotal += orderForm.DiscountAmount;
			}
			return discountTotal;
		}

		private void BindCartSummary()
		{
			CartHelper ch = new CartHelper(Cart.DefaultName);
			if (ch.IsEmpty)
			{
				Visible = false;
				return;
			}
			lblSubTotal.Text = ch.Cart.SubTotal.ToString("C2");
			lblShippingTotal.Text = ch.Cart.ShippingTotal.ToString("C2");
			lblDiscountTotal.Text = GetDiscountTotal(ch.Cart).ToString("C2");
			lblTaxTotal.Text = ch.Cart.TaxTotal.ToString("C2");
			lblTotal.Text = ch.Cart.Total.ToString("C2");

		}
	}
}