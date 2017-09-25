﻿using Models.Core;
using Models.WholeFarm.Activities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models.WholeFarm.Groupings
{
	///<summary>
	/// Contains a group of filters to identify individul ruminants
	///</summary> 
	[Serializable]
	[ViewName("UserInterface.Views.GridView")]
	[PresenterName("UserInterface.Presenters.PropertyPresenter")]
    [ValidParent(ParentType = typeof(IATGrowCropLabour))]
    [ValidParent(ParentType = typeof(RuminantActivityBuySell))]
	[ValidParent(ParentType = typeof(RuminantActivityMuster))]
	[ValidParent(ParentType = typeof(RuminantActivityFeed))]
	[ValidParent(ParentType = typeof(RuminantActivityHerdCost))]
	[ValidParent(ParentType = typeof(RuminantActivityMilking))]
	[ValidParent(ParentType = typeof(OtherAnimalsActivityBreed))]
	[ValidParent(ParentType = typeof(OtherAnimalsActivityFeed))]
    [ValidParent(ParentType = typeof(RuminantActivityCollectManureAll))]
    [ValidParent(ParentType = typeof(RuminantActivityCollectManurePaddock))]
    public class LabourFilterGroupSpecified: LabourFilterGroup
	{

        /// <summary>
        /// Days labour required per unit or fixed (days)
        /// </summary>
        [Description("Days labour required [per unit or fixed] (days)")]
        public double LabourPerUnit { get; set; }

        /// <summary>
        /// Size of unit
        /// </summary>
        [Description("Size of unit")]
        public double UnitSize { get; set; }

        /// <summary>
        /// Labour unit type
        /// </summary>
        [Description("Units for 'Size of unit'")]
		public LabourUnitType UnitType { get; set; }

	}
}
