# SQLite database integration to Xamarin.Forms App

This tutorial will guide you through SQLite local database integration to your Xamarin.Forms application.

## SQLite

Source: https://www.sqlite.org/index.html

### What Is SQLite?

SQLite is a C-language library that implements a small, fast, self-contained, high-reliability, full-featured, SQL database engine. SQLite is the most used database engine in the world. SQLite is built into all mobile phones and most computers and comes bundled inside countless other applications that people use every day. More Information...

The SQLite file format is stable, cross-platform, and backwards compatible and the developers pledge to keep it that way through at least the year 2050. SQLite database files are commonly used as containers to transfer rich content between systems and as a long-term archival format for data. There are over 1 trillion (1e12) SQLite databases in active use.

SQLite source code is in the public-domain and is free to everyone to use for any purpose.

### Tutorial

Source: https://docs.microsoft.com/en-us/xamarin/get-started/tutorials/local-database/?tutorial-step=1&tabs=vswin

#### Add SQLite.NET

1. Launch Visual Studio, and create a new blank Xamarin.Forms app named LocalDatabaseTutorial. Ensure that the app uses .NET Standard as the shared code mechanism.

2. In Solution Explorer, select the LocalDatabaseTutorial project, right-click and select Manage NuGet Packages for Solution...

3. In the NuGet Package Manager, select the Browse tab, search for the sqlite-net-pcl NuGet package, select it, and click the Install button to add it to the project:

![sqlite-net-pcl NuGet package](https://docs.microsoft.com/en-us/xamarin/get-started/tutorials/local-database/images/vs/add-package.png)


    There are a number of NuGet packages with similar names. The correct package has these attributes:

    * Author(s): Frank A. Krueger
    * Id: sqlite-net-pcl
    * NuGet link: sqlite-net-pcl

    Despite the package name, this NuGet package can be used in .NET Standard projects.


4. Build the solution to ensure there are no errors.

#### Create data access classes

1. In Solution Explorer, in the LocalDatabaseTutorial project, add a new class named Person to the project. Then, in Person.cs, remove all of the template code and replace it with the following code:

```csharp
using SQLite;

namespace LocalDatabaseTutorial
{
    public class Person
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
```

This code defines a Person class that will store data about each person in the application. The ID property is marked with PrimaryKey and AutoIncrement attributes to ensure that each Person instance in the database will have a unique id provided by SQLite.NET.

2. In Solution Explorer, in the LocalDatabaseTutorial project, add a new class named Database to the project. Then, in Database.cs, remove all of the template code and replace it with the following code:

```csharp
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

namespace LocalDatabaseTutorial
{
    public class Database
    {
        readonly SQLiteAsyncConnection _database;

        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Person>().Wait();
        }

        public Task<List<Person>> GetPeopleAsync()
        {
            return _database.Table<Person>().ToListAsync();
        }

        public Task<int> SavePersonAsync(Person person)
        {
            return _database.InsertAsync(person);
        }
    }
}
```

This class contains code to create the database, read data from it, and write data to it. The code uses asynchronous SQLite.NET APIs that move database operations to background threads. In addition, the Database constructor takes the path of the database file as an argument. This path will be provided by the App class in the next exercise.

3. In Solution Explorer, in the LocalDatabaseTutorial project, expand App.xaml and double-click App.xaml.cs to open it. Then, in App.xaml.cs, remove all of the template code and replace it with the following code:

```csharp
using System;
using System.IO;
using Xamarin.Forms;

namespace LocalDatabaseTutorial
{
    public partial class App : Application
    {
        static Database database;

        public static Database Database
        {
            get
            {
                if (database == null)
                {
                    database = new Database(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "people.db3"));
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
```

This code defines a Database property that creates a new Database instance as a singleton. A local file path and filename, which represents where to store the database, are passed as the argument to the Database class constructor.

The advantage of exposing the database as a singleton is that a single database connection is created that's kept open while the application runs, therefore avoiding the expense of opening and closing the database file each time a database operation is performed.

4. Build the solution to ensure there are no errors.

#### Consume data access classes

In this exercise you will create a user interface to consume the previously created data access classes.

1. In Solution Explorer, in the LocalDatabaseTutorial project, double-click MainPage.xaml to open it. Then, in MainPage.xaml, remove all of the template code and replace it with the following code:

```xaml
<?xml version="1.0" encoding="utf-8"?>
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="LocalDatabaseTutorial.MainPage">
    <StackLayout Margin="20,35,20,20">
        <Entry x:Name="nameEntry"
               Placeholder="Enter name" />
        <Entry x:Name="ageEntry"
               Placeholder="Enter age" />
        <Button Text="Add to Database"
                Clicked="OnButtonClicked" />
        <ListView x:Name="listView">
            <ListView.ItemTemplate>
                <DataTemplate>
                    <TextCell Text="{Binding Name}"
                              Detail="{Binding Age}" />
                </DataTemplate>
            </ListView.ItemTemplate>
        </ListView>
    </StackLayout>
</ContentPage>
```

This code declaratively defines the user interface for the page, which consists of two Entry instances, a Button, and a ListView in a StackLayout. Each Entry has its Placeholder property set, which specifies the placeholder text that's shown prior to user input. The Button sets its Clicked event to an event handler named OnButtonClicked that will be created in the next step. The ListView sets its ItemTemplate property to a DataTemplate, which uses a TextCell to define the appearance of each row in the ListView. The TextCell data binds its Text and Detail properties to the Name and Age properties of each Person object, respectively.

In addition, the Entry instances and ListView have names specified with the x:Name attribute. This enables the code-behind file to access these objects using the assigned names.

2. 

```csharp

```
