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
using Mediachase.Commerce.Orders.Managers;
using Mediachase.Commerce.Orders;
namespace Mediachase.Commerce.Workflow.Activities.ReturnForm
{
	public partial class CalculateExchangeOrderStatusActivity : ReturnFormBaseActivity
	{
		public CalculateExchangeOrderStatusActivity()
		{
			InitializeComponent();
		}

		protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
		{
			try
			{
				if (base.ReturnFormStatus == ReturnFormStatus.Complete)
				{
					//Need change ExchangeOrder from AvaitingCompletition to InProgress
					var exchangeOrder = ReturnExchangeManager.GetExchangeOrderForReturnForm(base.ReturnOrderForm);
					if (exchangeOrder != null && OrderStatusManager.GetOrderGroupStatus(exchangeOrder) == OrderStatus.AwaitingExchange)
					{
						OrderStatusManager.ProcessOrder(exchangeOrder);
						exchangeOrder.AcceptChanges();
					}
				}
				// Retun the closed status indicating that this activity is complete.
				return ActivityExecutionStatus.Closed;
			}
			catch
			{
				// An unhandled exception occured.  Throw it back to the WorkflowRuntime.
				throw;
			}
		}
	}
}
