using System;
using Mediachase.Commerce.Orders.Dto;
using Mediachase.Web.Console.BaseClasses;
using Mediachase.Web.Console.Interfaces;

namespace EPiServer.Commerce.Sample.ShippingConfiguration.TestShipping
{
	public partial class ConfigureShipping : OrderBaseUserControl, IGatewayControl
	{
		private ShippingMethodDto _shippingMethodDto;
		private const string KeyParam = "Key";
		private const string PasswordParam = "Password";

		protected void Page_Load(object sender, EventArgs e)
		{
		}

		public void SaveChanges(object dto)
		{
			if (!Page.IsValid)
				return;
			_shippingMethodDto = dto as ShippingMethodDto;
			if (_shippingMethodDto == null)
				return;

			AddOrUpdateParam(KeyParam, txtKey.Text);
			AddOrUpdateParam(PasswordParam, txtPassword.Text);
		}

		public void LoadObject(object dto)
		{
			_shippingMethodDto = dto as ShippingMethodDto;
			if (_shippingMethodDto == null)
			{
				Visible = false;
				return;
			}
			if (_shippingMethodDto.ShippingOptionParameter.Rows.Count <= 0)
				return;
			foreach (ShippingMethodDto.ShippingOptionParameterRow row in _shippingMethodDto.ShippingOptionParameter.Rows)
			{
				if (row.Parameter.Equals(KeyParam, StringComparison.OrdinalIgnoreCase))
				{
					txtKey.Text = row.Value;
				}
				else if (row.Parameter.Equals(PasswordParam, StringComparison.OrdinalIgnoreCase))
				{
					txtPassword.Text = row.Value;
				}
			}
		}

		public string ValidationGroup { get; set; }

		private void AddNewRow(string parameter, string value)
		{
			ShippingMethodDto.ShippingOptionParameterRow newRow =
				_shippingMethodDto.ShippingOptionParameter.NewShippingOptionParameterRow();
			newRow.Parameter = parameter;
			newRow.Value = value;
			newRow.ShippingOptionId = _shippingMethodDto.ShippingOption[0].ShippingOptionId;
			_shippingMethodDto.ShippingOptionParameter.AddShippingOptionParameterRow(newRow);
		}

		private void AddOrUpdateParam(string parameterName, string value)
		{
			ShippingMethodDto.ShippingOptionParameterRow row = null;
			foreach (ShippingMethodDto.ShippingOptionParameterRow item in _shippingMethodDto.ShippingOptionParameter)
			{
				if (item.Parameter.Equals(parameterName, StringComparison.OrdinalIgnoreCase))
				{
					row = item;
					break;
				}
			}
			if (row == null)
			{
				AddNewRow(parameterName, value);
			}
			else
			{
				row.Value = value;
			}
		}
	}
}