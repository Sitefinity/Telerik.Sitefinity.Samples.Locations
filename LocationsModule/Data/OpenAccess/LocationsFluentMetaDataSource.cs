using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Data.OA;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.GenericContent.Data;

namespace LocationsModule.Data.OpenAccess
{
	public class LocationsFluentMetadataSource : ContentBaseMetadataSource
	{
		public LocationsFluentMetadataSource() : base(null) 
        { 
        }

		/// <summary>
		/// Initializes a new instance of the <see cref="LocationsFluentMetadataSource"/> class.
		/// </summary>
		/// <param name="context">The context.</param>
		public LocationsFluentMetadataSource(IDatabaseMappingContext context) : base(context) 
        { 
        }

		/// <summary>
		/// Builds the custom mappings for the data provider.
		/// </summary>
		/// <returns></returns>
		protected override IList<IOpenAccessFluentMapping> BuildCustomMappings()
		{
			var sitefinityMappings = base.BuildCustomMappings();
			sitefinityMappings.Add(new LocationsFluentMapping(this.Context));
			return sitefinityMappings;
		}
	}
}
