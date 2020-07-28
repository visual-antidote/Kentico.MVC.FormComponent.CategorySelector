# Kentico 12 MVC Category Selector Form Component

## Kentico.MVC.FormComponent.CategorySelector

A custom Kentico MVC form component that allows editors to select categories from a list of check-boxes in a modal window.

The component returns the selected categories as a list of category code names.

## Adding Repository Source

Since this package is hosted in a user-created Azure Artifact Feed, and not the official nuget repository, you must first make Visual Studio aware of this feed.

  [VA Test Feed](https://pkgs.dev.azure.com/vasandbox/0675b2f1-7fa9-4bd4-9472-5e8ff3b5f45e/_packaging/VATestFeed/nuget/v3/index.json)

1. In Visual Studio. Navigate to `Tools > NuGet Package Manager > Manage Nuget Packages for Solution...`

3.  Click on the gear icon in the top right corner
    
4.  Click the green + icon to add a new package source
    
5.  Enter the Name and Source from the Azure Feed: [VA Test Feed](https://pkgs.dev.azure.com/vasandbox/0675b2f1-7fa9-4bd4-9472-5e8ff3b5f45e/_packaging/VATestFeed/nuget/v3/index.json)
    
6.  Click Update
    
## Nuget Package

Once the repository source is added, you can now browse it to find the:
**VisualAntidote.Kentico.MVC.FormComponent.CategorySelector**
Install that package to enable use of the Category Selector Form Component.

## Set Up

1. Make sure the page has a reference to jQuery.
2. Modify the widget properties code file. Create a new property of type List<String> and annotate with this: 
[EditingComponent(VisualAntidote.Kentico.MVC.FormComponent.CategorySelector.Models.FormComponents.CategorySelectComponent.IDENTIFIER, Label = "Category list")]

## Additional Component Settings
You can also restrict/allow what categories are shown to the editors by adding the following annotations:

### Include Global Categories
Default value: **true**

    [EditingComponentProperty(nameof(VisualAntidote.Kentico.MVC.FormComponent.CategorySelector.Models.FormComponents.CategorySelectProperties.IncludeGlobalCategories), false)]

### Include Disabled Categories
Default value: **false**

    [EditingComponentProperty(nameof(VisualAntidote.Kentico.MVC.FormComponent.CategorySelector.Models.FormComponents.CategorySelectProperties.IncludeDisabledCategories), true)]

### Include Sites
To include categories from other sites, supply a comma-separated list of site code names

By default, only the current site's categories are displayed.

    [EditingComponentProperty(nameof(VisualAntidote.Kentico.MVC.FormComponent.CategorySelector.Models.FormComponents.CategorySelectProperties.IncludeSites), "SiteOneCode, SiteTwoCode")]


