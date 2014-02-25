using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.Configuration;
using LocationsModule.Model;

namespace LocationsModule.Web.UI.Public.Designers
{
	public class LocationsViewDesigner : ContentViewDesignerBase
	{
		/// <summary>
		/// Gets the name of the javascript type that the designer will use.
		/// The designers can reuse for example the base class implementation and just customize some labels
		/// </summary>
		/// <value>The name of the script descriptor type.</value>
		protected override string ScriptDescriptorTypeName
		{
			get
			{
				return typeof(ContentViewDesignerBase).FullName;
			}
		}

		/// <summary>
		/// Gets a type from the resource assembly.
		/// Resource assembly is an assembly that contains embedded resources such as templates, images, CSS files and etc.
		/// By default this is Telerik.Sitefinity.Resources.dll.
		/// </summary>
		/// <value>The resources assembly info.</value>
		protected override System.Type ResourcesAssemblyInfo
		{
			get
			{
				return Config.Get<ControlsConfig>().ResourcesAssemblyInfo;
			}
		}

		/// <summary>
		/// Adds the designer views.
		/// </summary>
		/// <param name="views">The views.</param>
		protected override void AddViews(Dictionary<string, ControlDesignerView> views)
		{
			var listSettings = new ListSettingsDesignerView();
			listSettings.DesignedMasterViewType = typeof(MasterListView).FullName;

			var singleItemSettings = new SingleItemSettingsDesignerView();
			singleItemSettings.DesignedDetailViewType = typeof(DetailsView).FullName;

			views.Add(listSettings.ViewName, listSettings);
			views.Add(singleItemSettings.ViewName, singleItemSettings);
		}
	}
}
