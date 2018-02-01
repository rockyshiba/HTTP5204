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