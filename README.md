Skybrud.Umbraco.GridData
========================

**Skybrud.Umbraco.GridData** is a small package with a strongly typed model for the new grid in Umbraco 7.2 and above.

The package makes it easy to use the model in your MVC views, master pages or even in your custom logic - eg. to index the grid data in Examine for better searches.

## Installation

1. [**NuGet Package**][NuGetPackage]  
Install this NuGet package in your Visual Studio project. Makes updating easy.

1. [**Umbraco package**][UmbracoPackage]  
Install the package through the Umbraco backoffice.

1. [**ZIP file**][GitHubRelease]  
Grab a ZIP file of the latest release; unzip and move the contents to the root directory of your web application.

[NuGetPackage]: https://www.nuget.org/packages/Skybrud.Umbraco.GridData
[UmbracoPackage]: https://our.umbraco.org/projects/developer-tools/skybrudumbracogriddata/
[GitHubRelease]: https://github.com/skybrud/Skybrud.Umbraco.GridData

## Examples

The package has its own property value converter, so you can simply get the grid model as:

```C#
GridDataModel grid = Model.Content.GetPropertyValue<GridDataModel>("body");
```

If you have the raw JSON string, you can parse it like:

```C#
GridDataModel grid = GridDataModel.Deserialize(json);
```

### Indexing with Examine

The Gist below gives a quick example on how the Grid can be indexed in Examine:

* [Gist: Indexing the Umbraco Grid.md](https://gist.github.com/abjerner/bdd89e0788d274ec5a33)

#### Property Value Converter

By default in Umbraco, calling `Model.Content.GetPropertyValue("body")` (assuming that `body` is our grid property), an instance of `JObject` will be returned.

After installing the package, the property value converter will make sure that an instance of `GridDataModel` is returned instead.

Unfortunately this will also break the existing logic in Umbraco's `GetGridHtml` extension methods. If you still wish to use `GetGridHtml` in your project, you can disable the property value converter by calling the following line during startup:

```C#
GridPropertyValueConverter.IsEnabled = false;
```

###Rendering in a View
There are two ways to render the grid html in a view. 
*In the examples below, "PageContent" is our strongly-typed GridDataModel property.*

####1 - Using the Standard Umbraco GetGridHtml helper
This will use the grid editors in the standard ~/Views/Partials/Grid/Editors folder. The "PropertyAlias" property on the GridDataModel will provide the Umbraco doctype alias for the property, so we can avoid relying upon "magic strings".

```C#
@if (Model.Content.PageContent.IsValid)
{
    @Model.Content.GetGridHtml(Model.Content.PageContent.PropertyAlias, "bootstrap3")
}
```


####2 - Using the Provided GetHtml method on the GridDataModel
This will look for grid editors in a folder named ~/Views/Partials/**TypedGrid**/Editors

```C#
@if (Model.Content.PageContent.IsValid)
{
    @Model.Content.PageContent.GetHtml(Html, "bootstrap3")
}
```