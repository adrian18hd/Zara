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
using Mediachase.Commerce.Orders;
using Mediachase.Commerce.Orders.Managers;

namespace Mediachase.Commerce.Workflow.Activities.ReturnForm
{
	public partial class ReturnFormBaseActivity: Activity
	{
		public static DependencyProperty ReturnOrderFormProperty = DependencyProperty.Register("ReturnOrderForm", typeof(OrderForm), typeof(ReturnFormBaseActivity));

	
		[ValidationOption(ValidationOption.Required)]
		[BrowsableAttribute(true)]
		public OrderForm ReturnOrderForm
		{
			get
			{
				return (OrderForm)(base.GetValue(ReturnFormBaseActivity.ReturnOrderFormProperty));
			}
			set
			{
				base.SetValue(ReturnFormBaseActivity.ReturnOrderFormProperty, value);
			}
		}

		protected ReturnFormStatus ReturnFormStatus
		{
			get
			{
				return ReturnFormStatusManager.GetReturnFormStatus(ReturnOrderForm);
			}
		}

		protected void ChangeReturnFormStatus(ReturnFormStatus newStatus)
		{
			this.ReturnOrderForm.Status = newStatus.ToString();
		}

		public ReturnFormBaseActivity()
		{
			InitializeComponent();
		}
	}
}
