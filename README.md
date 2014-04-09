Telerik.Sitefinity.Samples.Locations
====================================

The Locations sample project is a content-based module for maintaining a list of locations, such as office addresses, branch locations, an so on. The sample demonstrates the basic functionality of a content-based module. 

### Requirements

* Sitefinity 6.3 license

* .NET Framework 4

* Visual Studio 2012

* Microsoft SQL Server 2008R2 or later versions


### Installation instructions: SDK Samples from GitHub



1. In Solution Explorer, navigate to _SitefinityWebApp_ -> *App_Data* -> _Sitefinity_ -> _Configuration_ and select the **DataConfig.config** file. 
2. Modify the **connectionString** value to match your server address.

The project refers to the following NuGet packages:

**LocationsModule** library

* Telerik.Sitefinity.Core.nupkg

* OpenAccess.Core.nupkg

* Telerik.Sitefinity.Content.nupkg

*  Telerik.Web.UI.nupkg

* SitefinityWebApp.nupkg

* Telerik.Sitefinity.All.nupkg

* Telerik.Sitefinity.Samples.Common.nupkg

**Telerik.Sitefinity.Core** library

* OpenAccess.Core.nupkg

* Telerik.Sitefinity.Content.nupkg

You can find the packages in the official [Sitefinity Nuget Server](http://nuget.sitefinity.com).


### Integrate the OpenAccess enhancer


Sitefinity ships with OpenAccess ORM. To use OpenAccess in the data provider of a module, you must integrate the OpenAccess enhancer. To do this:


1. From the context menu of the project **LocationsModule**, click _Unload Project_.
2. From the context menu of the unloaded project, click _Edit LocationsModule.csproj_.
3. Find the **<ProjectExtensions>** tag and replace it with the following lines of code:
```xml
<ProjectExtensions>
  <VisualStudio>
    <UserProperties OpenAccess_EnhancementOutputLevel="1" OpenAccess_UpdateDatabase="False" OpenAccess_Enhancing="False" OpenAccess_ConnectionId="DatabaseConnection1" OpenAccess_ConfigFile="App.config" />
  </VisualStudio>
</ProjectExtensions>
<PropertyGroup>
  <OpenAccessPath>C:\GitHub\Telerik.Sitefinity.Samples.Dependencies</OpenAccessPath>
</PropertyGroup>
<Import Condition="Exists('$(OpenAccessPath)\OpenAccess.targets')" Project="$(OpenAccessPath)\OpenAccess.targets" />

```

4. In the **OpenAccessPath** element, place the path to the folder containing the **OpenAccess.targets** file. 

    This is the location of the **Telerik.Sitefinity.Samples.Dependencies** repository that you cloned locally. In the example above, the repository is cloned in **C:\GitHub**.
    
5. Save the changes.
6. From the context menu of the project, click _Reload Project_.
7. Repeat the above procedure for the **Telerik.StarterKit.Modules.Agents project**.
8. Build the solution.



### Login

To login to Sitefinity backend, use the following credentials: 

**Username:** admin

**Password:** password


