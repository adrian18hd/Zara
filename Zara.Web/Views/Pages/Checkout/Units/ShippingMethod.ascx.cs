using System;
using System.Web.UI;
using Mediachase.Commerce;
using Mediachase.Commerce.Core;
using Mediachase.Commerce.Orders;
using Mediachase.Commerce.Orders.Dto;
using Mediachase.Commerce.Orders.Managers;
using Mediachase.Commerce.Shared;
using Mediachase.Commerce.Website.Helpers;

namespace Zara.Web.Views.Pages.Checkout.Units
{
	public partial class ShippingMethod : UserControl
	{
		CartHelper cartHelper = new CartHelper(Cart.DefaultName);

		public Guid ShippingMethodId { get; set; }
		public string ShippingMethodName { get; set; }

		protected override void OnLoad(EventArgs e)
		{
			ShippingMethodDto shippingMethods = ShippingManager.GetShippingMethods(SiteContext.Current.LanguageName, false);
			rptShippingMethods.DataSource = shippingMethods.ShippingMethod.Rows;
			rptShippingMethods.DataBind();
		}

		protected decimal GetShippingCost(ShippingMethodDto.ShippingMethodRow row)
		{
			
			Shipment shipment = new Shipment();
			int index = 0;
			foreach (LineItem lineItem in cartHelper.LineItems)
			{
				shipment.AddLineItemIndex(index, lineItem.Quantity);
				index++;
			}
			cartHelper.OrderForm.Shipments.Add(shipment);
			return GetShippingCost(row, shipment, Currency.USD).Amount;
		}

		protected bool IsSelected(Guid shippingMethodId)
		{
			if (cartHelper.OrderForm.Shipments != null && cartHelper.OrderForm.Shipments.Count > 0)
				return cartHelper.OrderForm.Shipments[0].ShippingMethodId == shippingMethodId;
			return false;
		}

		private Money GetShippingCost(ShippingMethodDto.ShippingMethodRow shippingMethod,
			Shipment shipment, Currency currency)
		{
			Money result = new Money(0, currency);
			Type type = Type.GetType(shippingMethod.ShippingOptionRow.ClassName);
			if (type == null)
			{
				throw new TypeInitializationException(shippingMethod.ShippingOptionRow.ClassName, null);
			}

			string message = string.Empty;
			IShippingGateway provider = (IShippingGateway)Activator.CreateInstance(type, MarketId.Default);
			ShippingRate shipmentRate = provider.GetRate(shippingMethod.ShippingMethodId, shipment, ref message);
			if (shipmentRate != null)
			{
				Money shipmentRateMoney = shipmentRate.Money;
				result = shipmentRateMoney;
			}
			return result;
		}
	}
}