using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LocationsModule.Data;
using LocationsModule.Model;
using LocationsModule.Web.Services.Data;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Modules.GenericContent;

namespace LocationsModule.Web.Services
{
	public class LocationsBackendService : ContentServiceBase<LocationItem, LocationItemViewModel, LocationsManager>
	{
		/// <summary>
		/// Gets the content items.
		/// </summary>
		/// <param name="providerName">Name of the provider.</param>
		/// <returns></returns>
		public override IQueryable<LocationItem> GetContentItems(string providerName)
		{
			return this.GetManager(providerName).GetLocations();
		}

		/// <summary>
		/// Gets the child content items.
		/// </summary>
		/// <param name="parentId">The parent id.</param>
		/// <param name="providerName">Name of the provider.</param>
		/// <returns></returns>
		public override IQueryable<LocationItem> GetChildContentItems(Guid parentId, string providerName)
		{
			// TODO: Implement this method
			throw new NotImplementedException();
		}

		/// <summary>
		/// Gets the content item.
		/// </summary>
		/// <param name="id">The id.</param>
		/// <param name="providerName">Name of the provider.</param>
		/// <returns></returns>
		public override LocationItem GetContentItem(Guid id, string providerName)
		{
			return this.GetManager(providerName).GetLocation(id);
		}

		/// <summary>
		/// Gets the parent content item.
		/// </summary>
		/// <param name="id">The id.</param>
		/// <param name="providerName">Name of the provider.</param>
		/// <returns></returns>
		public override LocationItem GetParentContentItem(Guid id, string providerName)
		{
			// TODO: Implement this method
			throw new NotImplementedException();
		}

		/// <summary>
		/// Gets the manager.
		/// </summary>
		/// <param name="providerName">Name of the provider.</param>
		/// <returns></returns>
		public override LocationsManager GetManager(string providerName)
		{
			return LocationsManager.GetManager(providerName);
		}

		/// <summary>
		/// Gets the view model list.
		/// </summary>
		/// <param name="contentList">The content list.</param>
		/// <param name="dataProvider">The data provider.</param>
		/// <returns></returns>
		public override IEnumerable<LocationItemViewModel> GetViewModelList(IEnumerable<LocationItem> contentList, ContentDataProviderBase dataProvider)
		{
			var list = new List<LocationItemViewModel>();

			foreach (var location in contentList)
				list.Add(new LocationItemViewModel(location, dataProvider));

			return list;
		}
	}
}
