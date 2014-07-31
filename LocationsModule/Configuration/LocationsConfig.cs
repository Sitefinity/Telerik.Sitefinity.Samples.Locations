using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using LocationsModule.Data.OpenAccess;
using LocationsModule.Web.UI;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules.GenericContent.Configuration;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;

namespace LocationsModule.Configuration
{
	public class LocationsModuleConfig : ContentModuleConfigBase
	{
		/// <summary>
		/// Initializes the default providers.
		/// </summary>
		/// <param name="providers">The providers.</param>
		protected override void InitializeDefaultProviders(ConfigElementDictionary<string, DataProviderSettings> providers)
		{
			// add default provider
			providers.Add(new DataProviderSettings(providers)
			{
				Name = "OpenAccessLocationsDataProvider",
				Description = "A provider that stores locations data in database using OpenAccess ORM.",
				ProviderType = typeof(OpenAccessLocationsDataProvider),
				Parameters = new NameValueCollection() { { "applicationName", "/Locations" } }
			});
		}

		/// <summary>
		/// Initializes the default backend and frontend views.
		/// </summary>
		/// <param name="contentViewControls"></param>
		protected override void InitializeDefaultViews(ConfigElementDictionary<string, ContentViewControlElement> contentViewControls)
		{
			// add backend views to configuration
			contentViewControls.Add(LocationsDefinitions.DefineLocationsBackendContentView(contentViewControls));

			// add frontend views to configuration
			contentViewControls.Add(LocationsDefinitions.DefineLocationsFrontendContentView(contentViewControls));
		}

		/// <summary>
		/// Gets or sets the name of the default data provider that is used to manage security.
		/// </summary>
		[ConfigurationProperty("defaultProvider", DefaultValue = "OpenAccessLocationsDataProvider")]
		public override string DefaultProvider
		{
			get { return (string)this["defaultProvider"]; }
			set { this["defaultProvider"] = value; }
		}
	}
}
