using System;
using System.Collections.Generic;
using System.Linq;
using Mediachase.Commerce;
using Mediachase.Commerce.Orders;
using Mediachase.Commerce.Orders.Dto;
using Mediachase.Commerce.Orders.Managers;

namespace TestShippingMethod
{
	public class TestShippingGateway : IShippingGateway
	{
		public TestShippingGateway(MarketId marketId)
		{
			
		}

		public TestShippingGateway(IMarket market)
		{
			
		}

		public ShippingRate GetRate(Guid methodId, Shipment shipment, ref string message)
		{
			decimal rate = 0;
			ShippingMethodDto shippingMethodDto = ShippingManager.GetShippingMethod(methodId);
			if (shippingMethodDto != null && shippingMethodDto.ShippingMethod.Count > 0)
			{
				ShippingMethodDto.ShippingMethodRow shippingMethodRow = shippingMethodDto.ShippingMethod[0];
				ShippingMethodDto.ShippingMethodParameterRow shippingMethodParameter =
					shippingMethodDto.ShippingMethodParameter.FirstOrDefault(row => row.Parameter.Equals("QuantityCost"));
				if (shippingMethodParameter == null)
					throw new ApplicationException("Need to set the QuantityCost parameter for the shipping method");
				decimal cost;
				if (!Decimal.TryParse(shippingMethodParameter.Value, out cost))
					throw new ApplicationException("Need to set the QuantityCost parameter for the shipping method");
				List<LineItem> shipmentLineItems = Shipment.GetShipmentLineItems(shipment);
				decimal totalQuantity = shipmentLineItems != null ? shipmentLineItems.Sum(item => item.Quantity) : 0;

				rate = shippingMethodRow.BasePrice + cost * totalQuantity;
			}
			return new ShippingRate(methodId, shipment.ShippingMethodName,
				new Money(rate, Currency.USD));
		}
	}
}