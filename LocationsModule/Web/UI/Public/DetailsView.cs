using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend;
using Telerik.Web.UI;
using System.Web.UI;
using Telerik.Sitefinity.Web.UI.Templates;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using LocationsModule.Model;
using System.Web;

namespace LocationsModule.Web.UI.Public
{
	public class DetailsView : ViewBase
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
		/// Gets the repeater for news list.
		/// </summary>
		/// <value>The repeater.</value>
		protected internal virtual RadListView DetailsViewControl
		{
			get
			{
				return this.Container.GetControl<RadListView>("DetailsView", true);
			}
		}

		#endregion

		#region Overridden methods

		/// <summary>
		/// Initializes the controls.
		/// </summary>
		/// <param name="container">The controls container.</param>
		/// <param name="definition">The content view definition.</param>
		protected override void InitializeControls(GenericContainer container, IContentViewDefinition definition)
		{
			// ensure a valid definition is passed
			var detailDefinition = definition as IContentViewDetailDefinition;
			if (detailDefinition == null) return;

			// retrieve item from host control
			var locationsView = (LocationsView)this.Host;
			var item = locationsView.DetailItem as LocationItem;
			if (item == null)
			{
				// no item
				if (this.IsDesignMode())
				{
					this.Controls.Clear();
					this.Controls.Add(new LiteralControl("A location item was not selected or has been deleted. Please select another one."));
				}
				return;
			}
			
			// show item details
			this.DetailsViewControl.DataSource = new LocationItem[] { item };
		}

		#endregion

		internal const string layoutTemplateName = "LocationsModule.Web.UI.Public.Resources.DetailsView.ascx";
	}
}
