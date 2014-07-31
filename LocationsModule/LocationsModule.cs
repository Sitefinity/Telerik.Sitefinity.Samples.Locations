using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LocationsModule.Configuration;
using LocationsModule.Data;
using LocationsModule.Model;
using LocationsModule.Web.Services;
using LocationsModule.Web.UI;
using LocationsModule.Web.UI.Public;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Abstractions.VirtualPath;
using Telerik.Sitefinity.Abstractions.VirtualPath.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Modules.ControlTemplates;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI.ContentUI;

namespace LocationsModule
{
    public class LocationsModule : ContentModuleBase
    {
        /// <summary>
        /// Initializes the service with specified settings.
        /// </summary>
        /// <param name="settings">The settings.</param>
        public override void Initialize(ModuleSettings settings)
        {
            base.Initialize(settings);

            // initialize configuration file
            App.WorkWith()
                .Module(settings.Name)
                .Initialize()
                    .Configuration<LocationsModuleConfig>()
                    .WebService<LocationsBackendService>("Sitefinity/Services/Content/Locations.svc");
        }

        /// <summary>
        /// Installs this module in Sitefinity system for the first time.
        /// </summary>
        /// <param name="initializer">The Site Initializer. A helper class for installing Sitefinity modules.</param>
        public override void Install(SiteInitializer initializer)
        {
            base.Install(initializer);

            // register module ?
            IModule locationsModule;
            SystemManager.ApplicationModules.TryGetValue(LocationsModule.ModuleName, out locationsModule);

            initializer.Context.SaveMetaData(true);
            this.InstallCustomVirtualPaths(initializer);
        }

        private void InstallCustomVirtualPaths(SiteInitializer initializer)
        {
            var virtualPathConfig = initializer.Context.GetConfig<VirtualPathSettingsConfig>();
            ConfigManager.Executed += new EventHandler<ExecutedEventArgs>(ConfigManager_Executed);
            var locationsModuleVirtualPathConfig = new VirtualPathElement(virtualPathConfig.VirtualPaths)
            {
                VirtualPath = "~/LocationTemplates/*",
                ResolverName = "EmbeddedResourceResolver",
                ResourceLocation = "LocationsModule"
            };
            if (!virtualPathConfig.VirtualPaths.ContainsKey("~/LocationTemplates/*"))
                virtualPathConfig.VirtualPaths.Add(locationsModuleVirtualPathConfig);
        }

        private void ConfigManager_Executed(object sender, Telerik.Sitefinity.Data.ExecutedEventArgs args)
        {
            if (args.CommandName == "SaveSection")
            {
                var section = args.CommandArguments as VirtualPathSettingsConfig;
                if (section != null)
                {
                    // Reset the VirtualPathManager whenever we save the VirtualPathConfig section.
                    // This is needed so that our prefixes for the widget templates in the module assembly are taken into account.
                    VirtualPathManager.Reset();
                }
            }
        }

        /// <summary>
        /// Installs the pages.
        /// </summary>
        /// <param name="initializer">The initializer.</param>
        protected override void InstallPages(SiteInitializer initializer)
        {
            initializer.Installer
                .CreateModuleGroupPage(LocationsPageGroupID, "Locations")
                    .PlaceUnder(SiteInitializer.ModulesNodeId)
                    .SetOrdinal(1)
                    .SetTitle("Locations")
                    .SetUrlName("Locations")
                    .SetDescription("Module for managing a list of Locations")
                    .AddChildPage(LandingPageId, "Locations")
                        .SetTitle("Locations")
                        .SetHtmlTitle("Locations")
                        .SetUrlName("Locations")
                        .SetDescription("Module for managing a list of Locations")
                        .AddContentView(b =>
                        {
                            b.ControlDefinitionName = LocationsDefinitions.BackendDefinitionName;
                        })
                        .Done();
        }

        public override void Upgrade(SiteInitializer initializer, Version upgradeFrom)
        {
            // not needed
        }

        /// <summary>
        /// Registers the module data item type into the taxonomy system
        /// </summary>
        /// <param name="initializer">The initializer.</param>
        protected override void InstallTaxonomies(SiteInitializer initializer)
        {
            this.InstallTaxonomy(initializer, typeof(LocationItem));
        }

        /// <summary>
        /// Gets the module config.
        /// </summary>
        /// <returns></returns>
        protected override ConfigSection GetModuleConfig()
        {
            // code to return Module configuration
            return Config.Get<LocationsModuleConfig>();
        }

        /// <summary>
        /// Installs module's toolbox configuration.
        /// </summary>
        /// <param name="initializer">The initializer.</param>
        protected override void InstallConfiguration(SiteInitializer initializer)
        {
            // Module widget is installed on Bootstrapper_Initialized
            initializer.Installer
                .PageToolbox()
                    .LoadOrAddSection("Locations")
                        .LoadOrAddWidget<LocationsView>("LocationsView")
                            .SetTitle("LocationsViewTitle")
                            .SetDescription("LocationsViewDescription")
                            .Done()
                        .Done()
                    .Done();
        }

        #region Public Properties

        /// <summary>
        /// Gets the landing page id for each module inherit from <see cref="T:Telerik.Sitefinity.Services.SecuredModuleBase"/> class.
        /// </summary>
        /// <value>
        /// The landing page id.
        /// </value>
        public override Guid LandingPageId
        {
            get { return LocationsModuleLandingPage; }
        }

        public override Type[] Managers
        {
            get { return new[] { typeof(LocationsManager) }; }
        }

        #endregion

        #region Constants

        /// <summary>
        /// The name of the Locations Module
        /// </summary>
        public const string ModuleName = "Locations";

        // Page IDs
        public static readonly Guid LocationsPageGroupID = new Guid("000262BF-E8EA-4BE3-8C67-E1C2486A57BE");
        public static readonly Guid LocationsModuleLandingPage = new Guid("7A0F43CE-064A-4E09-A3B9-59CA2E1640A6");

        #endregion
    }
}