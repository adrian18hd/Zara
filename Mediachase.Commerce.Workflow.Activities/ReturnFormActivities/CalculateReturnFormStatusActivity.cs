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
	public partial class CalculateReturnFormStatusActivity : ReturnFormBaseActivity
	{
		public CalculateReturnFormStatusActivity()
		{
			InitializeComponent();
		}

		protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
		{
			try
			{
				var newStatus = CalculateReturnFormStatus();
				if (newStatus != base.ReturnFormStatus)
				{
					ChangeReturnFormStatus(newStatus);
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

		private ReturnFormStatus CalculateReturnFormStatus()
		{
			ReturnFormStatus retVal = base.ReturnFormStatus;

			return retVal;
		}
	}
}
