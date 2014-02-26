using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Abstractions.VirtualPath.Configuration;
using LocationsModule.Web.UI.Public;
using Telerik.Sitefinity.Abstractions;
using LocationsModule.Data;
using LocationsModule.Model;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Web.UI.ContentUI.Enums;
using Telerik.Sitefinity.Samples.Common;
using Telerik.Sitefinity.Data.OA;
using Telerik.Sitefinity.Services;

namespace SitefinityWebApp
{
	public class Global : System.Web.HttpApplication
	{
		private const string SamplesThemeName = "SamplesTheme";
		private const string SamplesThemePath = "~/App_Data/Sitefinity/WebsiteTemplates/Samples/App_Themes/Samples";

		private const string SamplesTemplateId = "015b4db0-1d4f-4938-afec-5da59749e0e8";
		private const string SamplesTemplateName = "SamplesMasterPage";
		private const string SamplesTemplatePath = "~/App_Data/Sitefinity/WebsiteTemplates/Samples/App_Master/Samples.master";

		private const string LocationsPageID = "495F4490-149F-403F-9E30-5549CBCE9533";

		private const string TaxonomyID = "A5D6B2ED-9A8D-45CE-A6D2-B03C60F0DDC3";

		protected void Application_Start(object sender, EventArgs e)
		{
			Telerik.Sitefinity.Abstractions.Bootstrapper.Initializing += new EventHandler<Telerik.Sitefinity.Data.ExecutingEventArgs>(Bootstrapper_Initializing);
			Telerik.Sitefinity.Abstractions.Bootstrapper.Initialized += new EventHandler<Telerik.Sitefinity.Data.ExecutedEventArgs>(Bootstrapper_Initialized);
		}

        protected void Bootstrapper_Initialized(object sender, Telerik.Sitefinity.Data.ExecutedEventArgs args)
        {
            if (args.CommandName == "Bootstrapped")
            {
                SystemManager.RunWithElevatedPrivilegeDelegate worker = new SystemManager.RunWithElevatedPrivilegeDelegate(CreateSampleWorker);
                SystemManager.RunWithElevatedPrivilege(worker);
            }
        }

        private void CreateSampleWorker(object[] args)
        {                        
            SampleUtilities.RegisterToolboxWidget("Locations Widget", typeof(LocationsView), "Samples");
            SampleUtilities.RegisterTheme(SamplesThemeName, SamplesThemePath);
            SampleUtilities.RegisterTemplate(new Guid(SamplesTemplateId), SamplesTemplateName, SamplesTemplateName, SamplesTemplatePath, SamplesThemeName);

            // Create Locations public landing page
            var result = SampleUtilities.CreatePage(new Guid(LocationsPageID), "Locations", true);
            if (result)
            {
                SampleUtilities.SetTemplateToPage(new Guid(LocationsPageID), new Guid(SamplesTemplateId));
                var locationsView = new LocationsView();
                locationsView.ContentViewDisplayMode = ContentViewDisplayMode.Automatic;
                SampleUtilities.AddControlToPage(new Guid(LocationsPageID), locationsView, "Content", "Locations Widget");
            }

            #region Create Taxonomy

            // ensure Categories taxonomy exists
            var taxonomyMgr = TaxonomyManager.GetManager();
            var taxonomy = taxonomyMgr.GetTaxonomies<HierarchicalTaxonomy>().FirstOrDefault(t => t.Name == "Categories");
            if (taxonomy == null)
            {
                taxonomy = taxonomyMgr.CreateTaxonomy<HierarchicalTaxonomy>(new Guid(TaxonomyID));
                taxonomy.Name = "Categories";
                taxonomy.Title = "Categories";
                taxonomy.TaxonName = "Categories";
                taxonomyMgr.SaveChanges();
            }

            // Add continents as categories
            if (taxonomy.Taxa.Count == 0)
            {
                var taxon = taxonomyMgr.CreateTaxon<HierarchicalTaxon>();
                taxon.Title = "Europe";
                taxon.Name = "Europe";
                taxon.UrlName = "europe";
                taxonomy.Taxa.Add(taxon);

                taxon = taxonomyMgr.CreateTaxon<HierarchicalTaxon>();
                taxon.Title = "North America";
                taxon.Name = "North America";
                taxon.UrlName = "north-america";
                taxonomy.Taxa.Add(taxon);

                taxon = taxonomyMgr.CreateTaxon<HierarchicalTaxon>();
                taxon.Title = "Australia";
                taxon.Name = "Australia";
                taxon.UrlName = "australia";
                taxonomy.Taxa.Add(taxon);

                taxonomyMgr.SaveChanges();
            }

            #endregion

            #region Create Sample Data

            // init content manager
            var mgr = new LocationsManager();

            // create sample items if not exist
            if (mgr.GetLocations().Count() == 0)
            {
                // North American Offices
                CreateLocation(mgr, "Boston Office", "460 Totten Pond Rd, Suite 640", "Waltham", "MA", "02451", "USA", "North America");
                CreateLocation(mgr, "Houston Office", "10200 Grogans Mill Rd, Suite 130", "The Woodlands", "TX", "77380", "USA", "North America");
                CreateLocation(mgr, "Austin Office", "701 Brazos Street, Suite 320", "Austin", "TX", "78701", "USA", "North America");
                CreateLocation(mgr, "Canada Office", "460 Totten Pond Rd, Suite 640", "Waltham", "MA", "02451", "USA", "North America");

                // Europe Offices
                CreateLocation(mgr, "London Office", "15 Bedford Square", "London", "London", "WC1B 3JA", "UK", "Europe");
                CreateLocation(mgr, "Bulgaria Office", "33 Alexander Malinov Blvd.", "Sofia", "Sofia", "1729", "Bulgaria", "Europe");
                CreateLocation(mgr, "Germany Office", "Balanstrasse 73", "Munich", "Munich", "81541", "Germany", "Europe");

                // Australian Office
                CreateLocation(mgr, "Sydney Office", "81-91 Military Road", "Neutral Bay", "NSW", "2089", "Australia", "Australia");
            }

            // save locations
            mgr.SaveChanges();
            SampleUtilities.CreateUsersAndRoles();

            #endregion
        }

		protected void Bootstrapper_Initializing(object sender, Telerik.Sitefinity.Data.ExecutingEventArgs args)
		{
			if (args.CommandName == "RegisterRoutes")
			{
                SampleUtilities.RegisterModule<LocationsModule.LocationsModule>("Locations", "A content-based module for maintaining a list of locations, such as office addresses, branch locations, etc.");
			}
		}
		
		private void CreateLocation(LocationsManager mgr, string Title, string Address, string City, string Region, string PostalCode, string Country, string Continent)
		{
			var location = mgr.CreateLocation();
			location.Title = Title;
			location.Address = Address;
			location.City = City;
			location.Region = Region;
			location.PostalCode = PostalCode;
			location.Country = Country;
			location.Content = Content;
			location.UrlName = Title.ToLower().Replace(" ", "-");

			// add taxonomy and save
			var taxonomyMgr = TaxonomyManager.GetManager();
			var taxon = taxonomyMgr.GetTaxa<HierarchicalTaxon>().Where(t => t.Name == Continent).Select(t => t.Id).ToArray();
			location.Organizer.AddTaxa("Category", taxon);
			mgr.RecompileItemUrls<LocationItem>(location);
		}

		protected void Session_Start(object sender, EventArgs e)
		{

		}

		protected void Application_BeginRequest(object sender, EventArgs e)
		{

		}

		protected void Application_AuthenticateRequest(object sender, EventArgs e)
		{

		}

		protected void Application_Error(object sender, EventArgs e)
		{

		}

		protected void Session_End(object sender, EventArgs e)
		{

		}

		protected void Application_End(object sender, EventArgs e)
		{

		}

		private const string Content = @"<p>Telerik’s mission is to make software development easier and more enjoyable. Our tools for agile project management, collaboration, development and testing allow companies of all sizes to create richer, more stable and aesthetic software faster than ever before. Trusted by over 100,000 customers worldwide for our devotion to quality and customer care, Telerik helps technical and business professionals maximize their productivity and ""deliver more than expected"" every day.</p> <p>As true craftsmen, we don't believe in compromises and our goal is to only release tools that we can be proud of.</p>";
	}
}