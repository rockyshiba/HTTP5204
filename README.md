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

# Commit a820a712a3cd35df5d2d5f9b2df477832a3bb572

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

# Commit 7813fa15a54b4a4d17d17ab42f23f91ea5838d67

## Error Handling

I've added instances of error handling. .NET MVC has a wide array of methods to handle errors so the ones here are just two examples. For this, an Errors controller was added, an Errors directory inside Views with views, and Web.config was modified.

### Try/Catch

The controllers have now been modified so that error prone actions, like database interactions, are using try/catch. Take a look at the actions using a database, such as retrieval of rows, and note the use of try/catch. If all goes well, then within try there is a return to a view that displays the results of a database interaction. If something goes wrong, then there are a series of catch scenarios that pass exception details into ViewBag variables that are then passed to a view that will display the results of those exceptions. These views have ViewBag variables ready to display the values of those variables assigned from a controller. After the catch statements, there is then a return to a view that will render the errors instead of the view that shows successful database interaction.

### 404

This code is one you've most likely encountered. It is one of many HTTP error codes and 404 is the code for cotent that is requested but not there on the server.

A 404 error cannot be easily implemented in your controllers. How would you program for something that doesn't exist? There's an easy way to prepare for 404 errors in your Web.config file. Within the system.web element, there is a "customErrors" element. When mode="On" you can redirect the user to more user-friendly error pages. You can add elements that represent the errors encountered using the "error" element. Here, if the user encounters a 404 error, the application will redirect the user to the URL provided in the "redirect" attrbute which in this case will go to the errors controller, code action.

<system.web>

    <customErrors mode="On" defaultRedirect="~/errors/code">
        <error statusCode="404" redirect="~/errors/code"/>
    </customErrors>
    
</system.web>

## Grabing a single row from the database

There are two examples of this:

### CountriesController.cs

In the Details action, a querystring variable is used to get a single row where in this case is a single country. Logic has been set up if the querystring does not exist or retrieve a row from the database. Go to the view and see how the links are generated that provide the necessary querystring variable to go to a single country.

### CityController.cs

In the Details action, a default id parameter is used to get a single city from the database. This parameter is an integer after the succeeding "/" of the action and not a querystring variable. Go to the view and see how the links are generated that provide the neccessary parameter in the URL to go to a single city.

## Views, Advanced

The HTML helpers can be hard to get used to at first, but the purpose here is to not hard code HTML in case the controllers and actions change which they often do. Hard coding the HTML would result in hours of having to retype the HTML and checking back and forth if your HTML is correct.

Take a look at the view files and look for @HTML.ActionLink. These helpers generate the hyperlinks that will guide the user to the appropriate controller and action. Remember, the URL is looking for controllers and actions, not individual files. Views/Home/Index.cshtml is a good start to look at @Html.ActionLink.

## Missing Dates
### Models/Country.cs
### Models/City.cs
We may not know a certain date for an entity. Perhaps the founding of a country or a city is not known. While they are nullable in SQL, C# doesn't explicitly allow nullable date properties of an object so you must declare those properties as nullable. 

# Commit cca99a082337d5dc2ac325cd9f73ad45a5486f44

## Forms

This commit focuses on making forms and will use City as an example. 

The best approach to constructing forms in .NET MVC is to know your data first. Ultimately, all your data is stored to some standard: numbers stored as some form of number, dates stored as dates, and text stored in various forms of varchar. What goes out, must go in and so your data has to be manipulated to the same standards: providing a number must be a number, a date as some form of date string, and text which can allow almost anything. 

### Models/City.cs
With your models set up, make use of the DataAnnotations provided by the framework. Here you can manipulate how your data will be displayed by using DisplayName. Although the properties of the models might not be user firendly, you can make them user friendly using DisplayName. Columns that are NOT NULL can be reflected in model properties as [Required]. You can also see examples of how these properties are to be displayed in <<input>> tags, such as date. Although not available here, there is the [Email] annotation to validate properties that are to expect email formattting. 

### Controllers/CityController.cs

Think of controllers as gatekeepers of your data-driven website. There are a number of actions that will manipulate the database(s) here: Add, Edit, and Delete. Notice and each of these actions are using method overloading. Each action is set up for GET and POST. The Actions set for GET are annotated with [HttpGet] and the actions for POST are annotated with [HttpPost]. The GET actions are responsible for page requests meaning they will bring database contents to view. The POST actions are for post requests meaning they will take form data from the page and process those contents to then be passed off into data storage. These actions are extensively prepared for when things go wrong using try/catch.

There are actions with parameters and these are used to determine some interaction with your views and models. By default, the id parameter is reserved in this framework. When you pass id into an action, the URL request is expecting {controller}/{action}/{id} where id is a number to be used inside the action. There are parameters that are strings and by default, the framework is expecting querystring variables in the URL. For example, if there is a parameter that looks like this (string code), then the action will look for a url like this {controller}/{action}/?code={value} where value will be used in the action referenced by "code". Actions that use Model parameters are typically ones that expect a post action. Take a look at the HttpPost actions for Add and Edit. They are defined with the City model with an instance, city like so: (City city). These actions are waiting for a post object to be passed in during a post request meaning that the form values to be passed into the action conforms to the definition of that model. The "Name" attribute of the input tags in the views share the same name as model properties and .NET MVC interprets these as property values when the user submits a form. The property values together form an object to be then used inside of the post action.

### Views/City

This directory contains the views necessary for the user to interact with the website. In order for them to interact, they are set up with forms and Razor uses Html.BeginForm() to mark the beginning and end of a form. 

HTML helpers are used extensively here to create the appropriate HTML tags necessary to create a form and populate input fields where necessary. These helpers cut down on the amount of typing needed for HTML tags and help to avoid the common mistake of mistyping HTML. The helpers use Lamba expressions which are used to represent the model. Views in .NET MVC are strongly typed so the lamba expressions refer to the model that the view is typed to. Helpers like TextBoxFor and EditorFor create input tags while DisplayNameFor and LabelFor create static text to appropriately label those input tags.

Validation messages for each property of a model can be displayed here using ValidationMessageFor. These are hidden when there are no form errors but display only when the form has failed. Required fields and incorrect input format on forms will trigger these helpers.

### Views/City/Details.cshtml
Remember that you can use programming logic in these views so some of that is used to prevent C# errors that might arise. Dates cannot display as null in these views so if logic has been employed to detect if they are null to display something else. You might also have related models that are optional but Razor does not allow null models so if logic is used there as well.

#Commit 

## Forms - AJAX

AJAX enhances user experience by making it appear as though something has happened instantly on a webpage. This commit will explore the different uses of AJAX through a search bar, a drop down list, and validation.

### Requirements

In order to use the features of AJAX in .NET MVC, the following nuget packages are required:
jQuery.Validation
Microsoft.jQuery.Unobstrusive.Ajax
Microsoft.jQuery.Unobstrusive.Validation

These will include the JavaScript files necessary for AJAX in .NET MVC. Those Javascript files will then appear in your Scripts directory and must be referenced in your views, assuming that JQuery is already referenced:

jquery.unobtrusive-ajax.min.js OR jquery.unobtrusive-ajax.js
jquery.validate.min.js OR jquery.validate.js
jquery.validate.unobtrusive.min.js OR jquery.validate.unobtrusive.js

The example on this repository is using the minified versions. 

## Search using AJAX

### HomeController.cs

AJAX takes in requests and retrieves results asynchronously as in the page does not load to handle transactions with the server. With this in mind, we cannot return a full view in .NET MVC in order for the effect of AJAX to occur so a Partial View must do. The Countries_Cities_Search action serves this purpose by returning a Partial View containing the countries and/or cities queried by the user from the view. Using FormCollection, this action will store the result inside a variable by using an input with name="term". This variable is then used to retrieve rows from the database that are LIKE the variable using the .Contains method. This is akin to coding WHERE ... LIKE ... in SQL. The rows are then stored inside a view model, countries_Cities, that then gets passed into a partial view for that partial view to use. 

### /Views/Home/_Search_Results.cshtml

The Partial View that be asynchronously rendered on a page. It is using a view model to render both countries and cities because views, full or partial, are strongly typed to a model. Certain C# datatypes, like DateTime and Int, cannot render null in a view so If statements are used here to detect the prescence of null results to prevent any error resulting from trying to display null.

### /Views/Home/Index.cshtml

The view reponsible for the Index action of the Home controller. It has been altered to include AJAX forms starting on line 9. .NET MVC handles most of the Javascript responsible for AJAX by using Ajax.BeginForm instead of the regular Html.BeginForm. In addition to naming the action and controller this form will post to, AJAX options need to be declared. HttpMethod can be declared as POST or GET, UpdateTargetId will write the results of the form action to an HTML element with the id provided, and InsertionMode determines how the results are to be written in the target id. This example replacing the contents of the target id with the results; there are other options like appending and prepending the contents of the target id. Additional attributes have been given to the form such as id and autocomplete, the latter which disables suggested terms provided by the browser. 

Below the form is a div with the target id of "search_results" targetted by the ajax form that will contain the results of the AJAX call. 

### Scripts/Home/homepage_ajax.js

To achieve the effect of a realtime search, additional javascript is required on top of the AJAX javascript already provided by the framework. On line 18, the jQuery keyup function on the form is used to submit the form. When browser detects that a key has been pressed and lifted and the cursor is in the form, the form will submit. However, because the form has been defined as an AJAX form, the form will not full submit (it will return false) but the AJAX code provided by the framework will send the contents of the form to the specified controller/action. 

## Drop down list AJAX

### Controllers/HomeController.cs

Much like AJAX using the search bar, the City_DDL action will handle AJAX requests from the view and will return a Partial View. The parameter being used here is the same, FormCollection, and it is looking for the value of an input tag with name="City". Converting the string id to an int, the action is retrieving a row from the database and passing it into the partial view. 

The drop down list is prepopulated so a view model containing countries and cities from the database is passed into the view. The view will then use this to populate the drop down list.

### /Views/City/_City.cshtml

The Partial View typed to the City model. This Partial View doesn't necessarily have to be used with AJAX but this view will work. Again, to avoid unnecessary errors due to rendering nulls, the view is checking if the model sent to it has any content. 

### /Views/Home/Index.cshtml

The drop down list for the AJAX call starts on line 45. As with the search bar, the drop down list is contained within an AJAX form with similar AJAX configurations; the method will be POST, it will update an id element, and it will replace the contents of that element.

Line 53 is the HTML helper to create a drop down list. The lambda expression populates the drop down list from the database and assigns the value and displayed text for each option tag in the drop down list. In this example, the Cities property of the model contains a list of cities from the database and will use the Id property to fill in the value attribute and the Name property to fill in the option tag for the user to see the name of the city. "-Select a City-" is the default option and contains no value and an id attribute has been assigned with "city_ddl".

### Scripts/Home/homepage_ajax.js

Using the Change function from jQuery, the form containing the drop down list will submit when the drop down list, in this example id="city_ddl" changes its value.

## AJAX validation 

It's incredibly useful to prevent a form submission if a known error is to occur. The Country table is using a non-autogenerated primary key using 3 characters to denote a country code that is provided by the user upon the creation of a country row. Primary keys are unique so it's preferrable to let the user know that a country code already exists in the database. 

### Models/Country.cs

The Code property has a Remote annotation. This annotation acts as another type of validation. While the other validation annotations add a layer of validation at the C# level, Remote is asynchronously checking the database for existing values. Remote requires the name of the action and the controller containing that action for it to work and can provide a custom error message if the developer wishes. In this example, the IsCodeAvailable action inside the Countries controller will handle the database operations.

### Controllers/CountriesController.cs

IsCodeAvailable will check the database for existing country codes using a string parameter called "code". Unlike other actions, this action returns a Json result. Look carefully at the return statement: if there are rows in the database, then false is returned in Json and triggers a validation violation somewhere in the framework. If there are no rows in the database, then true is returned in Json and no validation violation is detected.

When the application runs, the "code" arugment is provided by the user when the user enters in a value for the property of the model used in the view. In this example, when the user enters in a value for a country code in the view, .NET MVC will use IsCodeAvailable as declared by the Remote tag in the Country model and return a Json result from the database. If there are no results, then user can submit the form.