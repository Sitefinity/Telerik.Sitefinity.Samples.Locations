<%@ Control Language="C#" %>



<%@ Register Assembly="Telerik.Sitefinity, Version=6.3.5000.0, Culture=neutral, PublicKeyToken=b28c218413bdf563" Namespace="Telerik.Sitefinity.Web.UI.ContentUI" TagPrefix="sf" %>
<%@ Register Assembly="Telerik.Sitefinity, Version=6.3.5000.0, Culture=neutral, PublicKeyToken=b28c218413bdf563" Namespace="Telerik.Sitefinity.Web.UI" TagPrefix="sf" %>
<%@ Register Assembly="Telerik.Web.UI, Version=2013.3.1114.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

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