using System;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using LocationsModule.Data;
using LocationsModule.Model;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization.Configuration;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend;
using Telerik.Sitefinity.Web.UI.Templates;
using Telerik.Sitefinity.Web.UrlEvaluation;
using Telerik.Web.UI;

namespace LocationsModule.Web.UI.Public
{
	public class MasterListView : ViewBase
	{
		#region Override Template Properties

		/// <summary>
		/// Gets the name of the embedded layout template.
		/// </summary>
		/// <value></value>
		/// <remarks>
		/// No longer used; replaced with new Virtual Path Provider. Returns null.
		/// </remarks>
		protected override string LayoutTemplateName
		{
			get { return null; }
		}

		/// <summary>
		/// Gets or sets the layout template path.
		/// </summary>
		/// <value>
		/// The layout template path.
		/// </value>
		public override string LayoutTemplatePath
		{
			get
			{
				var path = "~/LocationTemplates/" + layoutTemplateName;
				return path;
			}

			set
			{
				base.LayoutTemplatePath = value;
			}
		}

		#endregion

		#region Control References

		/// <summary>
		/// Gets the repeater for Location Items list.
		/// </summary>
		/// <value>The repeater.</value>
		protected internal virtual RadListView LocationsListControl
		{
			get
			{
				return this.Container.GetControl<RadListView>("LocationsList", true);
			}
		}

		/// <summary>
		/// Gets the pager.
		/// </summary>
		/// <value>The pager.</value>
		protected internal virtual Pager Pager
		{
			get
			{
				return this.Container.GetControl<Pager>("pager", true);
			}
		}

		#endregion

		#region Methods

		/// <summary>
		/// Initializes the controls.
		/// </summary>
		/// <param name="container">The controls container.</param>
		/// <param name="definition">The content view definition.</param>
		protected override void InitializeControls(GenericContainer container, IContentViewDefinition definition)
		{
			// ensure a valid definition is passed
			var masterDefinition = definition as IContentViewMasterDefinition;
			if (masterDefinition == null) return;

			// retrieve locations from the manager
			var manager = LocationsManager.GetManager(this.Host.ControlDefinition.ProviderName);
			var query = manager.GetLocations();

			// check for filters on the locations query
            if (masterDefinition.AllowUrlQueries.HasValue && masterDefinition.AllowUrlQueries.Value)
            {
                query = this.EvaluateUrl(query, "Date", "PublicationDate", this.Host.UrlEvaluationMode, this.Host.UrlKeyPrefix);
                query = this.EvaluateUrl(query, "Author", "Owner", this.Host.UrlEvaluationMode, this.Host.UrlKeyPrefix);
                query = this.EvaluateUrl(query, "Taxonomy", string.Empty, typeof(LocationItem), this.Host.UrlEvaluationMode, this.Host.UrlKeyPrefix);
            }

			// modify pager based on query results
			int? totalCount = 0;
			int? itemsToSkip = 0;
			if (masterDefinition.AllowPaging.HasValue && masterDefinition.AllowPaging.Value)
				itemsToSkip = this.GetItemsToSkipCount(masterDefinition.ItemsPerPage, this.Host.UrlEvaluationMode, this.Host.UrlKeyPrefix);

			// culture for Urls in pager
            CultureInfo uiCulture = null;
            if (Config.Get<ResourcesConfig>().Multilingual)
				uiCulture = System.Globalization.CultureInfo.CurrentUICulture;

			// check for additional filters set by the definition
			var filterExpression = String.Empty;

			// modify the query with everything from above
			query = Telerik.Sitefinity.Data.DataProviderBase.SetExpressions(
				query,
				filterExpression,
				masterDefinition.SortExpression,
				uiCulture,
				itemsToSkip,
				masterDefinition.ItemsPerPage,
				ref totalCount);
			this.IsEmptyView = (totalCount == 0);

			// display results
			if (totalCount == 0)
				this.LocationsListControl.Visible = false;
			else
			{
				this.ConfigurePager(totalCount.Value, masterDefinition);
				this.LocationsListControl.DataSource = query.ToList();
			}
		}

        /// <summary>
        /// Configures the pager.
        /// </summary>
        /// <param name="virtualItemCount">The virtual item count.</param>
        /// <param name="masterDefinition">The master definition.</param>
		protected virtual void ConfigurePager(int virtualItemCount, IContentViewMasterDefinition masterDefinition)
		{
			if (masterDefinition.AllowPaging.HasValue &&
				masterDefinition.AllowPaging.Value &&
				masterDefinition.ItemsPerPage.GetValueOrDefault() > 0)
			{
				this.Pager.VirtualItemCount = virtualItemCount;
				this.Pager.PageSize = masterDefinition.ItemsPerPage.Value;
				this.Pager.QueryParamKey = this.Host.UrlKeyPrefix;
			}
			else
			{
				this.Pager.Visible = false;
			}
		}

		#endregion

		internal const string layoutTemplateName = "LocationsModule.Web.UI.Public.Resources.MasterListView.ascx";
	}
}
