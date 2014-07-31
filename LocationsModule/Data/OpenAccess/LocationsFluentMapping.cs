using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LocationsModule.Model;
using Telerik.OpenAccess;
using Telerik.OpenAccess.Metadata.Fluent;
using Telerik.OpenAccess.Metadata.Fluent.Advanced;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Model;

namespace LocationsModule.Data.OpenAccess
{
	public class LocationsFluentMapping : OpenAccessFluentMappingBase
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="LocationsFluentMapping"/> class.
		/// </summary>
		/// <param name="context">The context.</param>
		public LocationsFluentMapping(IDatabaseMappingContext context) : base(context) 
        { 
        }

        /// <summary>
        /// Creates and returns a collection of OpenAccess mappings
        /// </summary>
        /// <returns></returns>
		public override IList<MappingConfiguration> GetMapping()
		{
			// initialize and return mappings
			var mappings = new List<MappingConfiguration>();
			this.MapItem(mappings);
			this.MapUrlData(mappings);
			return mappings;
		}

		/// <summary>
		/// Maps the LocationItem class.
		/// </summary>
		/// <param name="mappings">The LocationItem class mappings.</param>
		private void MapItem(IList<MappingConfiguration> mappings)
		{
			// initialize mapping
			var itemMapping = new MappingConfiguration<LocationItem>();
			itemMapping.HasProperty(p => p.Id).IsIdentity();
			itemMapping.MapType(p => new { }).ToTable("sf_locations");

			// add properties
			itemMapping.HasProperty(p => p.Address);
			itemMapping.HasProperty(p => p.City);
			itemMapping.HasProperty(p => p.Region).IsNullable();
			itemMapping.HasProperty(p => p.PostalCode);
			itemMapping.HasProperty(p => p.Country);

			// map urls table association
			itemMapping.HasAssociation(p => p.Urls).WithOppositeMember("parent", "Parent").ToColumn("content_id").IsDependent().IsManaged();
			mappings.Add(itemMapping);
		}

		/// <summary>
		/// Maps the LocationItemUrlData class
		/// </summary>
		/// <param name="mappings">The LocatoinItemUrlData class mappings.</param>
		private void MapUrlData(IList<MappingConfiguration> mappings)
		{
			// map the Url data type
			var urlDataMapping = new MappingConfiguration<LocationItemUrlData>();
			urlDataMapping.MapType(p => new { }).Inheritance(InheritanceStrategy.Flat).ToTable("sf_url_data");
			mappings.Add(urlDataMapping);
		}
	}
}
