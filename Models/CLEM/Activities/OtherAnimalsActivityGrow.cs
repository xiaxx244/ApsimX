﻿using Models.Core;
using Models.CLEM.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.CLEM.Activities
{
	/// <summary>Other animals grow activity</summary>
	/// <summary>This activity grows other animals and includes aging</summary>
	[Serializable]
	[ViewName("UserInterface.Views.GridView")]
	[PresenterName("UserInterface.Presenters.PropertyPresenter")]
	[ValidParent(ParentType = typeof(CLEMActivityBase))]
	[ValidParent(ParentType = typeof(ActivitiesHolder))]
	[ValidParent(ParentType = typeof(ActivityFolder))]
    [Description("This activity performs the growth and aging of a specified type of other animal.")]
    public class OtherAnimalsActivityGrow : CLEMActivityBase
	{
		[Link]
		private ResourcesHolder Resources = null;

		/// <summary>
		/// Name of Other Animal Type
		/// </summary>
		[Description("Name of Other Animal Type")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Name of Other Animal Type to use required")]
        public string OtherAnimalType { get; set; }

		private OtherAnimalsType animalType { get; set; }

        /// <summary>An event handler to allow us to initialise ourselves.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        [EventSubscribe("CLEMInitialiseActivity")]
        private void OnCLEMInitialiseActivity(object sender, EventArgs e)
        {
            // locate OtherAnimalsType resource
            animalType = Resources.GetResourceItem(this, typeof(OtherAnimals), OtherAnimalType, OnMissingResourceActionTypes.ReportErrorAndStop, OnMissingResourceActionTypes.ReportErrorAndStop) as OtherAnimalsType;
		}

		/// <summary>
		/// Function to age other animals
		/// This needs to be undertaken prior to herd management
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		[EventSubscribe("CLEMAgeResources")]
		private void OnCLEMAgeResources(object sender, EventArgs e)
		{
			// grow all individuals
			foreach (OtherAnimalsTypeCohort cohort in animalType.Cohorts.Where(a => a.GetType() == typeof(OtherAnimalsTypeCohort)))
			{
				cohort.Age++;
			}

			// death from old age
			while(animalType.Cohorts.Where(a => a.Age > animalType.MaxAge).Count() > 0)
			{
				animalType.Remove(animalType.Cohorts.Where(a => a.Age > animalType.MaxAge).FirstOrDefault(), this.Name, "Died");
			}
		}

		/// <summary>
		/// Method to determine resources required for this activity in the current month
		/// </summary>
		/// <returns>A list of resource requests</returns>
		public override List<ResourceRequest> GetResourcesNeededForActivity()
		{
			return null;
		}

		/// <summary>
		/// Method used to perform activity if it can occur as soon as resources are available.
		/// </summary>
		public override void DoActivity()
		{
			return;
		}

		/// <summary>
		/// Method to determine resources required for initialisation of this activity
		/// </summary>
		/// <returns></returns>
		public override List<ResourceRequest> GetResourcesNeededForinitialisation()
		{
			return null;
		}

		/// <summary>
		/// Resource shortfall event handler
		/// </summary>
		public override event EventHandler ResourceShortfallOccurred;

		/// <summary>
		/// Shortfall occurred 
		/// </summary>
		/// <param name="e"></param>
		protected override void OnShortfallOccurred(EventArgs e)
		{
			if (ResourceShortfallOccurred != null)
				ResourceShortfallOccurred(this, e);
		}

		/// <summary>
		/// Resource shortfall occured event handler
		/// </summary>
		public override event EventHandler ActivityPerformed;

		/// <summary>
		/// Shortfall occurred 
		/// </summary>
		/// <param name="e"></param>
		protected override void OnActivityPerformed(EventArgs e)
		{
			if (ActivityPerformed != null)
				ActivityPerformed(this, e);
		}

	}
}
