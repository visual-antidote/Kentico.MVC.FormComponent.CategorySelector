
  
  

# *** ALPHA ***

This project is currently under development.

  

# Kentico 12 MVC Category Selector Form Component

  ## Requirements
* Kentico **12.0.29** or later version is required to use this component. Installing on an older version of Kentico may cause problems.  
  

### Kentico.MVC.FormComponent.CategorySelector

A custom Kentico MVC form component that allows editors to select categories from a list of check-boxes in a modal window.

  

  

The component returns the selected categories as a list of category code names.

  

![VisualAntidoteCategorySelectorFormComponent1.png](https://github.com/visual-antidote/Kentico.MVC.FormComponent.CategorySelector/blob/master/SampleImages/VisualAntidoteCategorySelectorFormComponent1.png?raw=true)

  

![VisualAntidoteCategorySelectorFormComponent2.png](https://github.com/visual-antidote/Kentico.MVC.FormComponent.CategorySelector/blob/master/SampleImages/VisualAntidoteCategorySelectorFormComponent2.png?raw=true)

  

## Adding Repository Source

  

  

Since this package is hosted in a user-created Azure Artifact Feed, and not the official nuget repository, you must first make Visual Studio aware of this feed.

  

  

[VA Test Feed](https://pkgs.dev.azure.com/vasandbox/0675b2f1-7fa9-4bd4-9472-5e8ff3b5f45e/_packaging/VATestFeed/nuget/v3/index.json)

  

  

1. In Visual Studio. Navigate to `Tools > NuGet Package Manager > Manage Nuget Packages for Solution...`

2. Click on the gear icon in the top right corner

  

3. Click the green + icon to add a new package source

  

4. Enter the Name and Source from the Azure Feed: [VA Test Feed](https://pkgs.dev.azure.com/vasandbox/0675b2f1-7fa9-4bd4-9472-5e8ff3b5f45e/_packaging/VATestFeed/nuget/v3/index.json) (right-click and select 'Copy link address')

  

6. Click Update

  

## Install the Nuget Package

  

  

Once the repository source is added and selected, you can install the Nuget package.

  

Using Visual Studio's Nuget package manager, search for the following

  

**VisualAntidote.Kentico.MVC.FormComponent.CategorySelector**

  

Install that package to enable use of the Category Selector Form Component.

  

Or install via the command line:

  

	Install-Package VisualAntidote.Kentico.MVC.FormComponent.CategorySelector

  

  

## Set Up

  

  

1. Make sure the page has a reference to jQuery.

2. Add a routing rule using the helper method `ApplyCategorySelectorRoute`

  

Example:

  

    VisualAntidote.Kentico.MVC.FormComponent.CategorySelector.Helpers.RouteHelper.ApplyCategorySelectorRoute(routes);

  

where `routes` is the `RouteCollection` object.

  

3. Modify the widget properties code file. Create a new property of type `List<String>` and annotate it with the `CategorySelectComponent` attribute.

  

Example:

  

	[EditingComponent(VisualAntidote.Kentico.MVC.FormComponent.CategorySelector.Models.FormComponents.CategorySelectComponent.IDENTIFIER, Label = "Category list")]
	public List<string> CategoryCodeNameList { get; set; }

  

In the example above, once the editor saves the widget, any selected categories will be available in `CategoryCodeNameList`

  

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

  
  

### Restrict number of selected categories

  

You can set a minimum and maximum number of required categories.

  

  

By default, there is no minimum and maximum restriction

  

  

    [EditingComponentProperty(nameof(VisualAntidote.Kentico.MVC.FormComponent.CategorySelector.Models.FormComponents.CategorySelectProperties.MinimumSelectedCategoryNumber), 2)]

    [EditingComponentProperty(nameof(VisualAntidote.Kentico.MVC.FormComponent.CategorySelector.Models.FormComponents.CategorySelectProperties.MaximumSelectedCategoryNumber), 4)]

  

# License

  

This project is build under the MIT license. Check the [LICENSE](https://github.com/visual-antidote/Kentico.MVC.FormComponent.CategorySelector/blob/master/LICENSE) file for more information.

  

# Development

This project used the [Shareable Component Boilerplate](https://github.com/KenticoDevTrev/ShareableComponentBoilerplate) as a starting point.

  

If you would like to clone/fork this project, follow these steps*:

  

1. Clone this project onto your computer.

2. Open the Solution file, right click on your solution and Clean Solution

3. Rebuild your solution

  

- note, if you get a `Roslyn.exe` error, clean and build the Views Web App separately, this sometimes happens

  

### (optional) Add to your Kentico Instance

[See instructions here](https://github.com/KenticoDevTrev/ShareableComponentBoilerplate#optional-add-to-your-kentico-instance)

  

### Developing your own fork

If you would like to fork this project and create your own nuget packages from the compiled project, follow these steps:

1. Update the `Kentico.MVC.FormComponent.CategorySelector.nuspec` file in the `CategorySelector` folder with the details required.

2. Update the project's version number (make sure this is done before compilation)
		
	a. Right-click on the `CategorySelector` project and select Properties
		
	b. Click the `Application` tab
		
	c. Click the `Assembly information` button
		
	d. Update the Assembly and File versions as required

3. Compile the project in Release mode

4. Run the `BuildPackage.bat` batch file (found in the `CategorySelector` folder)

5. A new `.nupkg` file is generated in the current folder. Upload this package to your desired repository (i.e. nuget.org, Azure DevOps Artifact Feeds, etc...)

  

*It is recommended to first familiarize yourself with the [Shareable Component Boilerplate](https://github.com/KenticoDevTrev/ShareableComponentBoilerplate).

  
## 3rd Party Libraries

This project was based on the [Shareable Component Boilerplate](https://github.com/KenticoDevTrev/ShareableComponentBoilerplate) by Trevor Fayas.

The Checkbox Treeview UI uses the [hummingbird-treeview](https://github.com/hummingbird-dev/hummingbird-treeview) library by Sebastian Mieruch.

