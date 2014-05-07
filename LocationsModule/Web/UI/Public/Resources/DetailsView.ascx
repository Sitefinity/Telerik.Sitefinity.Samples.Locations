<%@ Control Language="C#" %>



<%@ Register Assembly="Telerik.Sitefinity" Namespace="Telerik.Sitefinity.Web.UI.ContentUI" TagPrefix="sf" %>
<%@ Register Assembly="Telerik.Sitefinity" Namespace="Telerik.Sitefinity.Web.UI" TagPrefix="sf" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<div class="locations-single content">

    <telerik:RadListView ID="DetailsView" ItemPlaceholderID="ItemsContainer" runat="server" EnableEmbeddedSkins="false" EnableEmbeddedBaseStylesheet="false">
        <LayoutTemplate>
            <asp:PlaceHolder ID="ItemsContainer" runat="server" />
        </LayoutTemplate>
        <ItemTemplate>
            <div class="column-small">
                <div class="block">
                    <p id="photo" runat="server"></p>
				    <h1><sf:FieldListView ID="Title" runat="server" Text="{0}" Properties="Title" /></h1>
				    <address>
					    <sf:FieldListView ID="Address" runat="server" Text="{0}" Properties="Address" /><br />
						<sf:FieldListView ID="City" runat="server" Text="{0}" Properties="City" /> 
						<sf:FieldListView ID="Region" runat="server" Text="{0}" Properties="Region" /><br />
					    <sf:FieldListView ID="PostalCode" runat="server" Text="{0}" Properties="PostalCode" /><br />
					    
					    
				    </address>

					<p><a href="<%= ResolveUrl(Telerik.Sitefinity.Web.SiteMapBase.GetActualCurrentNode().Url) %>">&laquo; All Locations</a></p>
			    </div>
            </div>
        </ItemTemplate>
    </telerik:RadListView>
</div>