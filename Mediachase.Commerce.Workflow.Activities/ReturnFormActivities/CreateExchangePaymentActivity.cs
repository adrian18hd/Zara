using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Drawing;
using System.Linq;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Serialization;
using System.Workflow.Runtime;
using System.Workflow.Activities;
using System.Workflow.Activities.Rules;
using Mediachase.Commerce.Workflow.Activities.ReturnForm;
using Mediachase.Commerce.Orders;
using Mediachase.Commerce.Orders.Managers;
using Mediachase.Commerce.Orders.Dto;

namespace Mediachase.Commerce.Workflow.Activities.ReturnFormActivities
{
	public partial class CreateExchangePaymentActivity : ReturnFormBaseActivity
	{
		public CreateExchangePaymentActivity()
		{
			InitializeComponent();
		}

		protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
		{
			try
			{
				CreateExchangePayments();
				// Retun the closed status indicating that this activity is complete.
				return ActivityExecutionStatus.Closed;
			}
			catch
			{
				// An unhandled exception occured.  Throw it back to the WorkflowRuntime.
				throw;
			}
		}

		public  void CreateExchangePayments()
		{
			var origPurchaseOrder = ReturnOrderForm.Parent as PurchaseOrder;
			var origOrderForm = origPurchaseOrder.OrderForms[0];
			var exchangeOrder = ReturnExchangeManager.GetExchangeOrderForReturnForm(ReturnOrderForm);
			if (exchangeOrder != null)
			{
				var exchangeOrderForm = exchangeOrder.OrderForms[0];
				//Exchange payments
				//Credit exchange payment to original order
				decimal paymentTotal = Math.Min(ReturnOrderForm.Total, exchangeOrder.Total);
				ExchangePayment creditExchangePayment = CreateExchangePayment(TransactionType.Credit, paymentTotal);
				origOrderForm.Payments.Add(creditExchangePayment);

				//Debit exchange payment to exchange order
				ExchangePayment debitExchangePayment = CreateExchangePayment(TransactionType.Capture, paymentTotal);
				exchangeOrderForm.Payments.Add(debitExchangePayment);

				OrderStatusManager.RecalculatePurchaseOrder(exchangeOrder);
				exchangeOrder.AcceptChanges();
			}
			
		}

		private static ExchangePayment CreateExchangePayment(TransactionType tranType, decimal amount)
		{
			ExchangePayment retVal = new ExchangePayment();
			PaymentMethodDto paymentMethods = PaymentManager.GetPaymentMethods("en", true);

			foreach (PaymentMethodDto.PaymentMethodRow row in paymentMethods.PaymentMethod.Rows)
			{
				if (row.SystemKeyword == ExchangePayment.PaymentMethodSystemKeyword)
				{
					retVal.PaymentMethodId = row.PaymentMethodId;
					retVal.PaymentMethodName = row.Name;
					break;
				}
			}

			retVal.Amount = amount;
			retVal.TransactionType = tranType.ToString();
			retVal.Status = PaymentStatus.Processed.ToString();

			return retVal;
		}

	}
}
