using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Versioning.Serialization.Attributes;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.GenericContent.Model;

namespace LocationsModule.Model
{
	public class LocationItemUrlData : UrlData
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="LocationItemUrlData" /> class.
		/// </summary>
		public LocationItemUrlData() : base() { this.Id = Guid.NewGuid(); }

		/// <summary>
		/// Gets or sets the parent product item
		/// </summary>
		/// <value>The product item</value>
		[NonSerializableProperty]
		public override IDataItem Parent
		{
			get
			{
				if (this.parent != null)
					((IDataItem)this.parent).Provider = ((IDataItem)this).Provider;
				return this.parent;
			}
			set { this.parent = (LocationItem)value; }
		}

		private LocationItem parent;
	}
}
