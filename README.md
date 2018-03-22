# HTTP5204

Repository for HTTP5204 Mobile Development

# Commit

## Starting Over

This commit explores the inevitable of restarting a project for whatever reason. And authorizing your web project.

## Scaffold-Proofing your models

### Models/Buddy_Country.cs

### Models/Buddy_City.cs

When your models require to be rebuilt by the framework, the core properties are still there but the annotations, the helpful bits of code that validate your model, will not. That is why it is recommended to annotate your model properties in another "buddy" class that will handle these annotations outside of the core model. If the core model changes, these buddy models will persist as well as annotations that take some time to construct.

Notice the difference between the core model, Country.cs, and its partial counterpart in Buddy_Country.cs. The partial class shares the same name as the core class and it doesn't overide the core class even though the same name is used. A MetadataType annotation is required to point to another class that will contain the properties and annotations that will be shared with the core class.

Reference: [Validating your Models](https://docs.microsoft.com/en-us/aspnet/mvc/overview/older-versions-1/getting-started-with-mvc/getting-started-with-mvc-part7)

## Authorization

### MyRoleProvider.cs

Set up this class to inherit the RoleProvider class, a predefined class provided by the framework. This custom class will overwrite a method.

Customize the GetRolesForUser method to retrieve the role names from the database.

### Web.config (demonstrated with Web.debug.config)

Within system.web, include the authentication element. Within the authentication element, add the forms element. The details of the attributes and their values are in the file. Of note, the type attribute is pointing to the class file containing the RoleProvider class that will return the role from the database.

At this point, this will add functionality to the security annotations you see on controllers and/or actions. The [Authorize] attribute will work from here but for specific roles more code is needed.

To discriminate your controller/actions by role, system.web also requires the roleManager element. Inside roleManager, provide the providers element. Inside providers, provide two siblings clear and add. Add requires a name and type attribute which refers to the class defined to retreive the user's role from the database. Use the name of the "add" element for the value of defaultProvider in roleManager.

### HomeController.cs

In order to use the authorization annonations, an authorization cookie needs to be set. The Login POST action is setting up an authorization cookie based on a database check using the Count() method which returns the number of rows from the database based on a query.

Authorization cookies provided by the framework allow role validation. Recall in MyRoleProvider.cs the GetRolesForUser method that takes in a string parameter named "username". This parameter's value is provided by the authorization cookie set from the user's login and it's this value that is used throughout the code that determines whether or not you are authorized to access certain controllers and/or actions. In the Restricted action, the username of the current user is returned again but from the authorization cookie (that was set earlier from the login action) represented by the User object. This User object can also be used within views, rendering specific HTML content based on the various properties of the User object such as identity or role.

To "sign out", this requires the unset of the Authorization cookie. In the Logout action, FormsAuthentication.SignOut() will do that.