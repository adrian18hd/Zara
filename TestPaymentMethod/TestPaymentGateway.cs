using System;
using Mediachase.Commerce.Orders;
using Mediachase.Commerce.Plugins.Payment;

namespace TestPaymentMethod
{
	public class TestPaymentGateway : AbstractPaymentGateway
	{
		public override bool ProcessPayment(Payment payment, ref string message)
		{
			if (Settings == null || !Settings.ContainsKey("TestConfiguration"))
			{
				message = "The payment method is not setup correctly";
				return false;
			}
			CreditCardPayment creditCardPayment = payment as CreditCardPayment;
			if (creditCardPayment == null)
			{
				message = "Invalid payment method passed.";
				return false;
			}
			creditCardPayment.TransactionID = new Guid().ToString();
			return true;
		}
	}
}