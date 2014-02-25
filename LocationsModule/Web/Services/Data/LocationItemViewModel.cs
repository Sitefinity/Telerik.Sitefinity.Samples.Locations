using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.GenericContent.Model;
using LocationsModule.Model;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Modules;

namespace LocationsModule.Web.Services.Data
{
	public class LocationItemViewModel : ContentViewModelBase
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="LocationItemViewModel"/> class.
		/// </summary>
		public LocationItemViewModel() : base() { }

		/// <summary>
		/// Initializes a new instance of the <see cref="LocationItemViewModel"/> class.
		/// </summary>
		/// <param name="location">The location item.</param>
		/// <param name="provider">The provider.</param>
		public LocationItemViewModel(LocationItem location, ContentDataProviderBase provider)
			: base(location, provider)
		{
			this.Address = location.Address;
			this.City = location.City;
			this.Region = location.Region;
			this.PostalCode = location.PostalCode;
		}

		#endregion

		#region Public Methods and Overrides

		/// <summary>
		/// Get live version of this.ContentItem using this.provider
		/// </summary>
		/// <returns>
		/// Live version of this.ContentItem
		/// </returns>
		protected override Content GetLive()
		{
			return this.provider.GetLiveBase<LocationItem>((LocationItem)this.ContentItem);
		}

		/// <summary>
		/// Get temp version of this.ContentItem using this.provider
		/// </summary>
		/// <returns>
		/// Temp version of this.ContentItem
		/// </returns>
		protected override Content GetTemp()
		{
			return this.provider.GetTempBase<LocationItem>((LocationItem)this.ContentItem);
		}

		#endregion

		#region Public Properties

		public string Address { get; set; }
		public string City { get; set; }
		public string Region { get; set; }
		public string PostalCode { get; set; }

		#endregion
	}
}
