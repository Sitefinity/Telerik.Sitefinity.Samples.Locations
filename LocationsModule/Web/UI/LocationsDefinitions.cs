using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;
using Telerik.Sitefinity.Configuration;
using LocationsModule.Model;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Config;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Config;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Enums;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail;
using Telerik.Sitefinity.Web.UI.Fields.Enums;
using System.Web.UI;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.UI.Fields.Config;
using Telerik.Sitefinity.Web.UI.Validation.Config;
using Telerik.Web.UI;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master;
using LocationsModule.Web.UI.Public;

namespace LocationsModule.Web.UI
{
	public class LocationsDefinitions
	{
		#region Constructors

		/// <summary>
		/// Static constructor that makes it impossible to use the definitions
		/// without the module 
		/// </summary>
		static LocationsDefinitions()
		{
			SystemManager.GetApplicationModule(LocationsModule.ModuleName);
		}

		#endregion

		#region Backend ContentView

		/// <summary>
		/// Defines the locations backend content view (control panel and views).
		/// </summary>
		/// <param name="parent">The parent element hosting the backend content view.</param>
		/// <returns></returns>
		public static ContentViewControlElement DefineLocationsBackendContentView(ConfigElement parent)
		{
			// initialize the content view; this is the element that will be returned to the page and holds all views of the admin panel
			var backendContentView = new ContentViewControlElement(parent)
			{
				ControlDefinitionName = BackendDefinitionName,
				ContentType = typeof(LocationItem),
				UseWorkflow = false
			};

			// GridView element serves as the "List View" for the item list. Grid columns are defined later
			var locationsGridView = new MasterGridViewElement(backendContentView.ViewsConfig)
			{
				ViewName = LocationsDefinitions.BackendListViewName,
				ViewType = typeof(MasterGridView),
				AllowPaging = true,
				DisplayMode = FieldDisplayMode.Read,
				ItemsPerPage = 50,
				SearchFields = "Title",
				SortExpression = "Title ASC",
				Title = "Locations",
				WebServiceBaseUrl = "~/Sitefinity/Services/Content/Locations.svc/"
			};
			backendContentView.ViewsConfig.Add(locationsGridView);

			#region Module Main Toolbar definition

			// Toolbar is the top menu with action buttons such as Create, Delete, Search, etc.
			var masterViewToolbarSection = new WidgetBarSectionElement(locationsGridView.ToolbarConfig.Sections)
			{
				Name = "Toolbar"
			};

			// "Create" Button for Toolbar
			var createLocationsWidget = new CommandWidgetElement(masterViewToolbarSection.Items)
			{
				Name = "CreateLocationsCommandWidget",
				ButtonType = CommandButtonType.Create,
				CommandName = DefinitionsHelper.CreateCommandName,
				Text = "Create Location",
				CssClass = "sfMainAction",
				WidgetType = typeof(CommandWidget)
			};
			masterViewToolbarSection.Items.Add(createLocationsWidget);

			// "Delete" button for Toolbar
			var deleteLocationsWidget = new CommandWidgetElement(masterViewToolbarSection.Items)
			{
				Name = "DeleteLocationsCommandWidget",
				ButtonType = CommandButtonType.Standard,
				CommandName = DefinitionsHelper.GroupDeleteCommandName,
				Text = "Delete",
				WidgetType = typeof(CommandWidget),
				CssClass = "sfGroupBtn"
			};
			masterViewToolbarSection.Items.Add(deleteLocationsWidget);

			// "Search" button for toolbar
			masterViewToolbarSection.Items.Add(DefinitionsHelper.CreateSearchButtonWidget(masterViewToolbarSection.Items, typeof(LocationItem)));
			locationsGridView.ToolbarConfig.Sections.Add(masterViewToolbarSection);

			#endregion

			#region Locations Grid (List View)

			// Define GridView mode
			var gridMode = new GridViewModeElement(locationsGridView.ViewModesConfig)
			{
				Name = "Grid"
			};
			locationsGridView.ViewModesConfig.Add(gridMode);

			#region Locations Grid Columns

			// Title column
			DataColumnElement titleColumn = new DataColumnElement(gridMode.ColumnsConfig)
			{
				Name = "Title",
				HeaderText = "Title",
				HeaderCssClass = "sfTitleCol",
				ItemCssClass = "sfTitleCol",
				ClientTemplate = @"<a sys:href='javascript:void(0);' sys:class=""{{ 'sf_binderCommand_edit sfItemTitle sfpublished"">
					<strong>{{Title}}</strong></a>"
			};
			gridMode.ColumnsConfig.Add(titleColumn);

			ActionMenuColumnElement actionsColumn = new ActionMenuColumnElement(gridMode.ColumnsConfig)
			{
				Name = "Actions",
				HeaderText = "Actions",
				HeaderCssClass = "sfMoreActions",
				ItemCssClass = "sfMoreActions"
			};
			actionsColumn.MenuItems.Add(DefinitionsHelper.CreateActionMenuCommand(actionsColumn.MenuItems, "View", HtmlTextWriterTag.Li, "preview", "View", string.Empty));
			actionsColumn.MenuItems.Add(DefinitionsHelper.CreateActionMenuCommand(actionsColumn.MenuItems, "Delete", HtmlTextWriterTag.Li, "delete", "Delete", string.Empty));

			gridMode.ColumnsConfig.Add(actionsColumn);

			#endregion

			#endregion

			#region Dialog Window definitions

			#region Insert Item Dialog

			// Insert Item Parameters
			var parameters = string.Concat(
				"?ControlDefinitionName=",
				LocationsDefinitions.BackendDefinitionName,
				"&ViewName=",
				LocationsDefinitions.BackendInsertViewName);

			// Insert Item Dialog
			DialogElement createDialogElement = DefinitionsHelper.CreateDialogElement(
				locationsGridView.DialogsConfig,
				DefinitionsHelper.CreateCommandName,
				"ContentViewInsertDialog",
				parameters);

			// add dialog to Backend
			locationsGridView.DialogsConfig.Add(createDialogElement);

			#endregion

			#region Edit Item Dialog

			// "Edit Item" Parameters
			parameters = string.Concat(
				"?ControlDefinitionName=",
				LocationsDefinitions.BackendDefinitionName,
				"&ViewName=",
				LocationsDefinitions.BackendEditViewName);

			// "Edit Item" Dialog
			DialogElement editDialogElement = DefinitionsHelper.CreateDialogElement(
				locationsGridView.DialogsConfig,
				DefinitionsHelper.EditCommandName,
				"ContentViewEditDialog",
				parameters);

			// Add Dialog to Backend
			locationsGridView.DialogsConfig.Add(editDialogElement);

			#endregion

			#region Preview Item Dialog

			// "Preview Item" parameters
			parameters = string.Concat(
				"?ControlDefinitionName=",
				LocationsDefinitions.BackendDefinitionName,
				"&ViewName=",
				LocationsDefinitions.BackendPreviewName,
				"&backLabelText=", "BacktoItems", "&SuppressBackToButtonLabelModify=true");

			// Preview Item Dialog
			DialogElement previewDialogElement = DefinitionsHelper.CreateDialogElement(
				locationsGridView.DialogsConfig,
				DefinitionsHelper.PreviewCommandName,
				"ContentViewEditDialog",
				parameters);

			// Add Dialog to Backend
			locationsGridView.DialogsConfig.Add(previewDialogElement);

			#endregion

			#endregion

			#region Admin Forms Views

			#region Create Item Form View

			// bind create item view to web service
			var locationsInsertDetailView = new DetailFormViewElement(backendContentView.ViewsConfig)
			{
				Title = "Create Location",
				ViewName = LocationsDefinitions.BackendInsertViewName,
				ViewType = typeof(DetailFormView),
				ShowSections = true,
				DisplayMode = FieldDisplayMode.Write,
				ShowTopToolbar = true,
				WebServiceBaseUrl = "~/Sitefinity/Services/Content/locations.svc/",
				IsToRenderTranslationView = false,
				UseWorkflow = false
			};

			backendContentView.ViewsConfig.Add(locationsInsertDetailView);

			#endregion

			#region Edit Item Form View

			// bind Edit item form to web service
			var locationsEditDetailView = new DetailFormViewElement(backendContentView.ViewsConfig)
			{
				Title = "Edit Location",
				ViewName = LocationsDefinitions.BackendEditViewName,
				ViewType = typeof(DetailFormView),
				ShowSections = true,
				DisplayMode = FieldDisplayMode.Write,
				ShowTopToolbar = true,
				WebServiceBaseUrl = "~/Sitefinity/Services/Content/Locations.svc/",
				IsToRenderTranslationView = false,
				UseWorkflow = false
			};

			backendContentView.ViewsConfig.Add(locationsEditDetailView);

			#endregion

			#region Preview Item Form View

			// bind Preview Form to web service
			var locationsPreviewDetailView = new DetailFormViewElement(backendContentView.ViewsConfig)
			{
				Title = "Location Preview",
				ViewName = LocationsDefinitions.BackendPreviewName,
				ViewType = typeof(DetailFormView),
				ShowSections = true,
				DisplayMode = FieldDisplayMode.Read,
				ShowTopToolbar = true,
				ShowNavigation = true,
				WebServiceBaseUrl = "~/Sitefinity/Services/Content/Locations.svc/",
				UseWorkflow = false
			};

			backendContentView.ViewsConfig.Add(locationsPreviewDetailView);

			#endregion

			#endregion

			#region Locations Backend Forms Definition

			#region Insert Form

			LocationsDefinitions.CreateBackendSections(locationsInsertDetailView, FieldDisplayMode.Write);
			LocationsDefinitions.CreateBackendFormToolbar(locationsInsertDetailView, true, true);

			#endregion

			#region Edit Form

			LocationsDefinitions.CreateBackendSections(locationsEditDetailView, FieldDisplayMode.Write);
			LocationsDefinitions.CreateBackendFormToolbar(locationsEditDetailView, false, true);

			#endregion

			#region Preview Form

			CreateBackendSections(locationsPreviewDetailView, FieldDisplayMode.Read);

			#endregion

			#endregion

			return backendContentView;
		}

		#region Backend Form Toolbar

		/// <summary>
		/// Creates the backend form toolbar.
		/// </summary>
		/// <param name="detailView">The detail view.</param>
		/// <param name="resourceClassId">The resource class id.</param>
		/// <param name="isCreateMode">if set to <c>true</c> [is create mode].</param>
		/// <param name="itemName">Name of the item.</param>
		/// <param name="addRevisionHistory">if set to <c>true</c> [add revision history].</param>
		/// <param name="showPreview">if set to <c>true</c> [show preview].</param>
		/// <param name="backToItems">The back to items.</param>
		private static void CreateBackendFormToolbar(DetailFormViewElement detailView, bool isCreateMode, bool showPreview)
		{
			// create toolbar
			var toolbarSectionElement = new WidgetBarSectionElement(detailView.Toolbar.Sections)
			{
				Name = "BackendForm",
				WrapperTagKey = HtmlTextWriterTag.Div,
				CssClass = "sfWorkflowMenuWrp"
			};

			// Create / Save Command
			toolbarSectionElement.Items.Add(new CommandWidgetElement(toolbarSectionElement.Items)
			{
				Name = "SaveChangesWidgetElement",
				ButtonType = CommandButtonType.Save,
				CommandName = DefinitionsHelper.SaveCommandName,
				Text = (isCreateMode) ? String.Concat("Create Location") : "Save Changes",
				WrapperTagKey = HtmlTextWriterTag.Span,
				WidgetType = typeof(CommandWidget)
			});

			// Preview
			if (showPreview == true)
			{
				toolbarSectionElement.Items.Add(new CommandWidgetElement(toolbarSectionElement.Items)
				{
					Name = "PreviewWidgetElement",
					ButtonType = CommandButtonType.Standard,
					CommandName = DefinitionsHelper.PreviewCommandName,
					Text = "Preview",
					ResourceClassId = typeof(Labels).Name,
					WrapperTagKey = HtmlTextWriterTag.Span,
					WidgetType = typeof(CommandWidget)
				});
			}

			// show Actions menu
			if (!isCreateMode)
			{
				var actionsMenuWidget = new ActionMenuWidgetElement(toolbarSectionElement.Items)
				{
					Name = "moreActions",
					Text = Res.Get<Labels>().MoreActionsLink,
					WrapperTagKey = HtmlTextWriterTag.Div,
					WidgetType = typeof(ActionMenuWidget),
					CssClass = "sfInlineBlock sfAlignMiddle"
				};
				actionsMenuWidget.MenuItems.Add(new CommandWidgetElement(actionsMenuWidget.MenuItems)
				{
					Name = "DeleteCommand",
					Text = "DeleteThisItem",
					CommandName = DefinitionsHelper.DeleteCommandName,
					WidgetType = typeof(CommandWidget),
					CssClass = "sfDeleteItm"
				});

				toolbarSectionElement.Items.Add(actionsMenuWidget);
			}

			// Cancel button
			toolbarSectionElement.Items.Add(new CommandWidgetElement(toolbarSectionElement.Items)
			{
				Name = "CancelWidgetElement",
				ButtonType = CommandButtonType.Cancel,
				CommandName = DefinitionsHelper.CancelCommandName,
				Text = "Back to Locations List",
				WrapperTagKey = HtmlTextWriterTag.Span,
				WidgetType = typeof(CommandWidget)
			});


			detailView.Toolbar.Sections.Add(toolbarSectionElement);
		}

		#endregion

		#region Backend Section Forms

		/// <summary>
		/// Creates the backend sections. Adds edit/preview controls to the detailView
		/// </summary>
		/// <param name="detailView">The detail view.</param>
		/// <param name="displayMode">The display mode.</param>
		private static void CreateBackendSections(DetailFormViewElement detailView, FieldDisplayMode displayMode)
		{
			// define main content section
			var mainSection = new ContentViewSectionElement(detailView.Sections)
			{
				Name = "MainSection",
				CssClass = "sfFirstForm"
			};

			#region Title Field

			// define title field element
			var titleField = new TextFieldDefinitionElement(mainSection.Fields)
			{
				ID = "titleFieldControl",
				DataFieldName = displayMode == FieldDisplayMode.Write ? "Title.PersistedValue" : "Title",
				DisplayMode = displayMode,
				Title = "Title",
				CssClass = "sfTitleField",
				WrapperTag = HtmlTextWriterTag.Li,
			};

			// add validation
			titleField.ValidatorConfig = new ValidatorDefinitionElement(titleField)
			{
				Required = true,
				MessageCssClass = "sfError",
				RequiredViolationMessage = "Title field is required"
			};

			// add field to section
			mainSection.Fields.Add(titleField);

			#endregion

			#region Content

			var contentField = new HtmlFieldElement(mainSection.Fields)
			{
				ID = "contentFieldControl",
				DataFieldName = displayMode == FieldDisplayMode.Write ? "Content.PersistedValue" : "Content",
				DisplayMode = displayMode,
				CssClass = "sfFormSeparator sfContentField",
				WrapperTag = HtmlTextWriterTag.Li,
				EditorContentFilters = Telerik.Web.UI.EditorFilters.DefaultFilters,
				EditorStripFormattingOptions = (EditorStripFormattingOptions?)(EditorStripFormattingOptions.MSWord | EditorStripFormattingOptions.Css | EditorStripFormattingOptions.Font | EditorStripFormattingOptions.Span | EditorStripFormattingOptions.ConvertWordLists)
			};
			mainSection.Fields.Add(contentField);

			#endregion

			#region Address fields

			// Address field
			var addressField = new TextFieldDefinitionElement(mainSection.Fields)
			{
				ID = "addressFieldControl",
				DataFieldName = "Address",
				DisplayMode = displayMode,
				Title = "Address",
				CssClass = "sfTitleField",
				WrapperTag = HtmlTextWriterTag.Li
			};
			addressField.ValidatorConfig = new ValidatorDefinitionElement(addressField)
			{
				Required = true,
				MessageCssClass = "sfError",
				RequiredViolationMessage = "Location address is required"
			};
			mainSection.Fields.Add(addressField);

			// City field
			var cityField = new TextFieldDefinitionElement(mainSection.Fields)
			{
				ID = "cityFieldControl",
				DataFieldName = "City",
				DisplayMode = displayMode,
				Title = "City",
				CssClass = "sfTitleField",
				WrapperTag = HtmlTextWriterTag.Li
			};
			mainSection.Fields.Add(cityField);

			// State / Region field
			var regionField = new TextFieldDefinitionElement(mainSection.Fields)
			{
				ID = "regionFieldControl",
				DataFieldName = "Region",
				DisplayMode = displayMode,
				Title = "State / Region",
				CssClass = "sfTitleField",
				WrapperTag = HtmlTextWriterTag.Li
			};
			regionField.ValidatorConfig = new ValidatorDefinitionElement(regionField)
			{
				Required = true,
				MessageCssClass = "sfError",
				RequiredViolationMessage = "State / Region is a required field"
			};
			mainSection.Fields.Add(regionField);

			// Postal Code field
			var postalCodeField = new TextFieldDefinitionElement(mainSection.Fields)
			{
				ID = "postalCodeFieldControl",
				DataFieldName = "PostalCode",
				DisplayMode = displayMode,
				Title = "Postal Code",
				CssClass = "sfTitleField",
				WrapperTag = HtmlTextWriterTag.Li
			};
			postalCodeField.ValidatorConfig = new ValidatorDefinitionElement(postalCodeField)
			{
				Required = true,
				MessageCssClass = "sfError",
				RequiredViolationMessage = "Postal code is a required field"
			};
			mainSection.Fields.Add(postalCodeField);

			// Country field
			var countryField = new TextFieldDefinitionElement(mainSection.Fields)
			{
				ID = "countryFieldControl",
				DataFieldName = "Country",
				DisplayMode = displayMode,
				Title = "Country",
				CssClass = "sfTitleField",
				WrapperTag = HtmlTextWriterTag.Li
			};

			countryField.ValidatorConfig = new ValidatorDefinitionElement(countryField)
			{
				Required = true,
				MessageCssClass = "sfError",
				RequiredViolationMessage = "Country is a required field"
			};

			mainSection.Fields.Add(countryField);

			// add section to view
			detailView.Sections.Add(mainSection);

			#endregion

			#region Categories and Tags Section

			// define new section
			var taxonSection = new ContentViewSectionElement(detailView.Sections)
			{
				Name = "TaxonSection",
				Title = "Categories and Tags",
				CssClass = "sfExpandableForm",
				ExpandableDefinitionConfig =
				{
					Expanded = false
				}
			};

			// add categories field to section
			var categories = DefinitionTemplates.CategoriesFieldWriteMode(taxonSection.Fields);
			categories.DisplayMode = displayMode;

			// add categories section to view
			taxonSection.Fields.Add(categories);

			// add tags field to section
			var tags = DefinitionTemplates.TagsFieldWriteMode(taxonSection.Fields);
			tags.DisplayMode = displayMode;
			tags.CssClass = "sfFormSeparator";
			tags.ExpandableDefinition.Expanded = true;
			tags.Description = "TagsFieldInstructions";
			taxonSection.Fields.Add(tags);

			// add tags section to view
			detailView.Sections.Add(taxonSection);

			#endregion

			#region More options Section

			// define new section
			var moreOptionsSection = new ContentViewSectionElement(detailView.Sections)
			{
				Name = "MoreOptionsSection",
				Title = "More Options",
				CssClass = "sfExpandableForm",
				ExpandableDefinitionConfig =
				{
					Expanded = false
				}
			};

			// Url field for insert view
			if (displayMode == FieldDisplayMode.Write)
			{
                var urlName = new MirrorTextFieldElement(moreOptionsSection.Fields)
                {
                    Title = "URL",
                    ID = "urlName",
                    MirroredControlId = titleField.ID,
                    DataFieldName = "UrlName.PersistedValue",
                    DisplayMode = displayMode,
                    RegularExpressionFilter = DefinitionsHelper.UrlRegularExpressionFilter,
                    WrapperTag = HtmlTextWriterTag.Li,
                    ReplaceWith = "-"
                };
                var validationDef = new ValidatorDefinitionElement(urlName)
                {
                    Required = true,
                    MessageCssClass = "sfError",
                    RequiredViolationMessage = "Url cannot be empty",
                    RegularExpression = DefinitionsHelper.UrlRegularExpressionFilterForValidator,
                    RegularExpressionViolationMessage = "Invalid Url"
                };
                urlName.ValidatorConfig = validationDef;

                moreOptionsSection.Fields.Add(urlName);
			}

			// add url section to view
			detailView.Sections.Add(moreOptionsSection);

			#endregion
		}

		#endregion

		#endregion

		#region Frontend ContentView

		/// <summary>
		/// Defines the ContentView control for News on the frontend
		/// </summary>
		/// <param name="parent">The parent configuration element.</param>
		/// <returns>A configured instance of <see cref="ContentViewControlElement"/>.</returns>
		internal static ContentViewControlElement DefineLocationsFrontendContentView(ConfigElement parent)
		{
			// define content view control
			var controlDefinition = new ContentViewControlElement(parent)
			{
				ControlDefinitionName = LocationsDefinitions.FrontendDefinitionName,
				ContentType = typeof(LocationItem),
                UseWorkflow = false
			};

			// *** define views ***

			#region Locations List View

			// define element
			var locationsListView = new ContentViewMasterElement(controlDefinition.ViewsConfig)
			{
				ViewName = LocationsDefinitions.FrontendListViewName,
				ViewType = typeof(MasterListView),
				AllowPaging = true,
				DisplayMode = FieldDisplayMode.Read,
				ItemsPerPage = 4,
				FilterExpression = DefinitionsHelper.NotPublishedDraftsFilterExpression,
				SortExpression = "Title ASC",
                UseWorkflow = false
			};

			// add to content view
			controlDefinition.ViewsConfig.Add(locationsListView);

			#endregion

			#region Locations Details View

			// Initialize View
			var locationsDetailsView = new ContentViewDetailElement(controlDefinition.ViewsConfig)
			{
				ViewName = LocationsDefinitions.FrontendDetailViewName,
				ViewType = typeof(DetailsView),
				ShowSections = false,
				DisplayMode = FieldDisplayMode.Read
			};

			// add to ContentView
			controlDefinition.ViewsConfig.Add(locationsDetailsView);

			#endregion

			// return content view control
			return controlDefinition;
		}

		#endregion

		#region Constants

		public const string BackendDefinitionName = "LocationsBackend";
		public const string BackendListViewName = "LocationsBackendListView";
		public const string BackendInsertViewName = "LocationsBackendInsertView";
		public const string BackendEditViewName = "LocationsBackendEditView";
		public const string BackendPreviewName = "LocationsBackendPreview";

		public const string FrontendDefinitionName = "LocationsFrontend";
		public const string FrontendListViewName = "LocationsFrontendListView";
		public const string FrontendDetailViewName = "LocationsDetailView";

		/// <summary>
		/// Name of the view that displays only titles
		/// </summary>
		public const string FrontendDefaultListViewName = "List locations";
		public const string FrontendDefaultDetailViewName = "Full location";

		#endregion
	}
}
