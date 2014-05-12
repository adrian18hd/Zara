using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Linq;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Serialization;
using System.Workflow.Runtime;
using System.Workflow.Activities;
using System.Workflow.Activities.Rules;
using Mediachase.Commerce.Inventory;
using System.Collections.Generic;

namespace Mediachase.Commerce.Workflow.Activities.PurchaseOrderActivities
{
    public partial class HandoffActivityBase : OrderGroupActivityBase
    {
        public static DependencyProperty PickupWarehouseInShipmentProperty = DependencyProperty.Register("PickupWarehouseInShipment", typeof(Dictionary<int, IWarehouse>), typeof(HandoffActivityBase));

        [ValidationOption(ValidationOption.Required)]
        [BrowsableAttribute(true)]
        public Dictionary<int, IWarehouse> PickupWarehouseInShipment
        {
            get
            {
                return (Dictionary<int, IWarehouse>)(base.GetValue(HandoffActivityBase.PickupWarehouseInShipmentProperty));
            }
            set
            {
                base.SetValue(HandoffActivityBase.PickupWarehouseInShipmentProperty, value);
            }
        }

        public HandoffActivityBase()
        {
            InitializeComponent();
        }
    }
}
