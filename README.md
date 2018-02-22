# HTTP5204
Repository for HTTP5204 Mobile Development

# Manual Branch
This is the Manual branch. Here you will learn how to create a web application in .NET MVC. 

To understand these files, follow the mini developer logs that I will type here in the README.md. 

## Start with a connection to the database
Go to Web.config in your project. That file is ignored here so you won't see it on github but it should be present in your projects.
Do not confuse Web.config with web.config, the latter which is in the Views directory

There is a Web.Debug.config file with a commented out connectionstring xml element. This is meant as an example of what to type in Web.config for storing a connectionstring there. 

Your connectionstring is provided by your database server. 

Add a named connectionstring within connectionstrings. We will use this later.

## Entity Framework
For the following to work, Entity Framework needs to be installed and that is suppled by your Nuget packages. 

# Commit ea659c940e12ee3e70e8c8e49fb69d3184ed0c2b
## Making Models
### Country.cs
Take a look in the models directory here. There are two classes defined, each for a different purpose. Country.cs is a class for the country entities to be represented in C# from the database. There are only properties here. Each property represents a column from the Country table. It's best to name the properties exactly how you named them as columns in your table and match the datatypes. This is using SQL Server, so gap between C# datatypes and SQL Server SQL is small. And integer in C# is an integer in SQL, a DateTime in C# can correspond to Date in SQL Server etc. 

You may notice something new: the properties are getting and setting themselves. In .NET MVC, the convention is that classes typically have properties with auto gets and sets without the use of fields. 

For this table to work in this application, two additional using statements are needed:
-System.ComponentModel.DataAnnotations.Schema
    This one is responsible for the [Table] annotation above the Country class name. It points to a table in the database.
-System.ComponentModel.DataAnnotations
    This one is reponsible for the [Key] annotation for C# to know that this property is a key value, often referring to you parent key.  

### GeoContext.cs
This is the class object that will interact with our database. It requires System.Data.Entity from Entity Framework that we downloaded earlier through nuget. The name of this class should match the name you gave the connectionstring in Web.config. 

These classes inherit a parent class already defined in .NET MVC called DbContext. In it contains the properties and methods necessary to interact with your database. What you add to the child class, in this case GeoContext.cs, you add properties of the DbSet class and take in the classes you defined earlier that represent your database tables. By convention, the names of these properties are the plural form of the class object they represent. Here, you have the Countries property of DbSet that takes in the Country class. 

As far as models go, this is the minimum that you can do to display contents of your database before you start defining controllers and views.

# Commit 9ae6eda85732b5e924ffeb86545390ee39047ccb
## Making Controllers
Controllers are the classes that mitigate events in your web application. Inside these clases are methods that are called actions. They determine what web pages are brought up and how data is passed through them. Websites made with .NET MVC use the address bar to point to a controller and action, not a specific file on the server, to access web pages.

Controllers are housed in the Controllers directory. To make one, simply right click that directory, hover over Add, and click on "Controller...". For our purposes and to understand controllers better, select "MVC 5 Controller - Empty". Visual Studio will set up an empty controller with a single Index action. When you name these controllers, it is convention to provide a name with the first letter in uppercase. When the controller is made, Visual Studio adds "Controller" to that name as the .NET MVC framwork uses these names to identify and map these classes as controllers.

### HomeController.cs
This will typically be a .NET MVC project's first controller. The Home controller is the controller for your website root. They are the first controllers because the Home controller can be accessed without explicitly typing "home" in the address bar:

[mysite.domain]/home

[mysite.domain]/

Typing either of those examples in the address bar will still access the Home controller. 

When you make controllers, Visual Studio automatically provides an Index action. Controllers require actions because the code inside these methods determine what is to be done for the website. When you see a web page brought forth or an error message, this is from code inside an action. Think of controllers and their actions as traffic conductors. 

Look at the Index action. Currently, this action doesn't define any parameters to be used within the action so that's why there is nothing in the parentheses. ActionResult is the return type for this action and this is seen quite often in .NET MVC. ActionResult is a predefined class in .NET MVC that will allow action to return a view. Here, the Index action is returning the View() method which points to a view. View() has a number of overloads that allow different arguments inside the parentheses. If left blank, then .NET MVC looks for a .cshtml file found in a directory with a default pattern like so:

[name of controller without "Controller"]/[name of action].cshtml

to be returned to the browser as a webpage. 

## Making Views
A view in .NET MVC is what the user sees as a result of an action inside a controller. Views are made using razor pages, pages that incorporate C# within HTML. These pages are suffixed with .cshtml. 

There are a couple of ways to make a view. The first way is to right-click on the Views directory and add a view in similar fashion to a controller, only this time click on "View" instead of "Controller". The other way to make a view is to right click the View() method inside of an action and select "Add View...".

Razor pages to be brought forth as views are mapped to actions. To find these pages, they need to be in the Views directory of your .NET MVC project. Each subdirectory of the Views directory is mapped to a controller, without "Controller" in the subdirectory name, and each .cshtml file is named after an action.

### Views/Home/Index.cshtml
An example of a view. At the top is where razor syntax is used to specify that Layout = null, Layout being a predestined variable specifies whether or not this page will use a razor HTML template. If this were not null, then it would be a string that points to the directory containing a layout. More on info layouts will be covered later. 

Parts of C# code can be used here, much like in PHP. Prefixed with @, anything immediately after @ will be intepreted as C# and the output will vary based on the C# provided. In this example, the current date and the numbers 1 through 10 are displayed using C#. 

### Views/Home/Countries.cshtml
Here's an example of a strongly typed view. These views have a @model declaration at the top of the page that will represent a model to be used throughout the page. @model is a powerful tool that allows consistent and accurate view construction. Typed views such as these require that data, stored in a model or a list of models, be passed from the controller to the view via the actions in those controllers. Take a look at the Countries action inside the Home controller to see an example of passing a model into a view.

This particular view is typed to a list of Country models which allows the use of loops to process the many Country objects passed into this view.

# Commit

## More on Views

### Controllers/CityController.cs
A new controller with an index action. 

There is also a partial action named Cities that will pass a partial view to a view.

### Controllers/CountriesController.cs
A new controller. The Index action is passing a list of Country models to the view. 

The Details action is passing a view model to a view. Here, the properties representing the different models in the view model are given values. 

### Models/Country.cs 
Each property can be annotated with a DataAnnotation. The Code property has been annotated with a display name so that views will display the display name over the property name.

### Models/Countries_Cities.cs
### Models/Country_Cities.cs
These are view models. View models can carry multiple models into views which only allow a single model. Each property of these view models represents another model to be carried into views.

### Views/City/_Cities.cshtml
A partial view typed to a model meant to be rendered into another view.

### Views/City/Index.cshtml
A view that is including a partial view by using the @Html.Action() method. This method will call to an action in a controller by naming the action and/or controller as a string. The action in the controller will then execute as programmed.

### Views/Countries/Index.cshtml
A view that is using a layout. Notice that Layout has a value on line 3. The value provided is a string path to the location that the layout is in.

### Views/Home/Index.cshtml
This has been updated to be typed to a model, in this case a view model. The view is then looping through the list of objects in each property of the view model.

### Shared/
This directory contains views meant to be shared or repeated like layouts and partial views.

Layout views are views that will be used in mulitple instances. Many pages have elements that stay the same so layouts are made once for this reason. By convention, they are named with a leading _ to let other developers know that these views are used many times.

_Footer.cshtml is an example of a partial view. These views aren't meant to be on their own. Views incorporating partial views require razor methods to render them, like @Html.Partal([path to partial view as a string]). Partial views that incorporate data require the use of a controller action that returns a partial view result. Views that use these require the use of @Html.Action().