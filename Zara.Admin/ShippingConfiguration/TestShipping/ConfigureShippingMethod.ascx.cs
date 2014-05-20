using System;
using System.Linq;
using Mediachase.Commerce.Orders.Dto;
using Mediachase.Web.Console.BaseClasses;
using Mediachase.Web.Console.Interfaces;

namespace EPiServer.Commerce.Sample.ShippingConfiguration.TestShipping
{
	public partial class ConfigureShippingMethod : OrderBaseUserControl, IGatewayControl
	{
		private string _validationGroup = string.Empty;
		private ShippingMethodDto _shippingMethodDto;

		public string ValidationGroup
		{
			get { return _validationGroup; }
			set { _validationGroup = value; }
		}

		protected void Page_Load(object sender, EventArgs e)
		{
		}

		public override void DataBind()
		{
			BindData();
			base.DataBind();
		}

		public void BindData()
		{
			if (_shippingMethodDto != null && _shippingMethodDto.ShippingMethodParameter != null)
			{
				var row =
					_shippingMethodDto.ShippingMethodParameter.FirstOrDefault(
						item => item.Parameter.Equals("QuantityCost", StringComparison.OrdinalIgnoreCase));
				if (row != null)
					txtCost.Text = row.Value;
			}
			else
			{
				Visible = false;
				return;
			}
		}

		public void SaveChanges(object dto)
		{
			_shippingMethodDto = dto as ShippingMethodDto;
			if (_shippingMethodDto != null && _shippingMethodDto.ShippingMethodParameter != null)
			{
				var row =
					_shippingMethodDto.ShippingMethodParameter.FirstOrDefault(
						item => item.Parameter.Equals("QuantityCost", StringComparison.OrdinalIgnoreCase));
				if (row != null)
				{
					row.Value = txtCost.Text;
				}
				else
				{
					row = _shippingMethodDto.ShippingMethodParameter.NewShippingMethodParameterRow();
					row.ShippingMethodId = _shippingMethodDto.ShippingMethod[0].ShippingMethodId;
					row.Parameter = "QuantityCost";
					row.Value = txtCost.Text;
					_shippingMethodDto.ShippingMethodParameter.AddShippingMethodParameterRow(row);
				}
			}
		}

		public void LoadObject(object dto)
		{
			_shippingMethodDto = dto as ShippingMethodDto;
		}
	}
}