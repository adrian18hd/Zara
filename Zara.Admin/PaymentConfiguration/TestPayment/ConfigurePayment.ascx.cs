using System;
using System.Linq;
using Mediachase.Commerce.Orders.Dto;
using Mediachase.Commerce.Orders.Managers;
using Mediachase.Web.Console.BaseClasses;
using Mediachase.Web.Console.Interfaces;

namespace EPiServer.Commerce.Sample.PaymentConfiguration.TestPayment
{
	public partial class ConfigurePayment : OrderBaseUserControl, IGatewayControl
	{
		private const string PaymentParam = "TestConfiguration";
		private PaymentMethodDto _paymentMethodDto;

		protected void Page_Load(object sender, EventArgs e)
		{
		}

		public void SaveChanges(object dto)
		{
			_paymentMethodDto = dto as PaymentMethodDto;
			if (_paymentMethodDto == null)
				return;
			PaymentMethodDto.PaymentMethodParameterRow row = GetRowByParamName(PaymentParam);
			row.Value = txtParam.Text;
		}

		public void LoadObject(object dto)
		{
			_paymentMethodDto = dto as PaymentMethodDto;
			if (_paymentMethodDto == null)
			{
				Visible = false;
				return;
			}
			PaymentMethodDto.PaymentMethodRow paymentMethodRow = _paymentMethodDto.PaymentMethod[0];
			if (_paymentMethodDto.PaymentMethodParameter.Rows.Count == 0)
			{
				_paymentMethodDto.PaymentMethodParameter.AddPaymentMethodParameterRow(paymentMethodRow, PaymentParam, String.Empty);
				PaymentManager.SavePayment(_paymentMethodDto);
			}
			string value = GetRowByParamName(PaymentParam).Value;
			txtParam.Text = value;
		}

		private PaymentMethodDto.PaymentMethodParameterRow GetRowByParamName(string paramName)
		{
			return _paymentMethodDto.PaymentMethodParameter.FirstOrDefault(pm => pm.Parameter == PaymentParam);
		}

		public string ValidationGroup { get; set; }
	}
}