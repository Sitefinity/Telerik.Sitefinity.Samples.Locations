<%@ Control Language="C#" %>



<%@ Register Assembly="Telerik.Sitefinity, Version=6.3.5000.0, Culture=neutral, PublicKeyToken=b28c218413bdf563" Namespace="Telerik.Sitefinity.Web.UI.ContentUI" TagPrefix="sf" %>
<%@ Register Assembly="Telerik.Sitefinity, Version=6.3.5000.0, Culture=neutral, PublicKeyToken=b28c218413bdf563" Namespace="Telerik.Sitefinity.Web.UI" TagPrefix="sf" %>
<%@ Register Assembly="Telerik.Web.UI, Version=2013.3.1114.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<h1>Locations</h1>

<telerik:RadListView ID="LocationsList" ItemPlaceholderID="ItemsContainer" GroupPlaceholderID="GroupContainer" GroupItemCount="3" runat="server" EnableEmbeddedSkins="false" EnableEmbeddedBaseStylesheet="false">
    <LayoutTemplate>
        <div class="Locations">
            <asp:PlaceHolder ID="GroupContainer" runat="server" />
        </div>
    </LayoutTemplate>
    <GroupTemplate>
        <div class="row">
            <asp:PlaceHolder ID="ItemsContainer" runat="server" />
        </div>
    </GroupTemplate>
    <ItemTemplate>
        <div class="column-small">
            <div class="block">
                <p><sf:DetailsViewHyperLink ID="DetailsViewHyperLink1" runat="server"><div id="photoDiv" runat="server"></div></sf:DetailsViewHyperLink></p>
				<h3><sf:DetailsViewHyperLink ID="DetailsViewHyperLink" TextDataField="Title" ToolTipDataField="Description" runat="server" /></h3>
				<address>
					<sf:FieldListView ID="Address" runat="server" Text="{0}" Properties="Address" /><br />
					
					<sf:FieldListView ID="City" runat="server" Text="{0}" Properties="City" />
					<sf:FieldListView ID="Region" runat="server" Text="{0}" Properties="Region" /> 
					<sf:FieldListView ID="PostalCode" runat="server" Text="{0}" Properties="PostalCode" /> 
					<sf:FieldListView ID="Country" runat="server" Text="{0}" Properties="Country" />
				</address>
				
			</div>
        </div>
    </ItemTemplate>
</telerik:RadListView>

<sf:Pager id="pager" runat="server" />
