# Reduce Xamarin.Forms Shell Template to Bare Minimum

If you want to start with Xamarin.Forms Shell application, there are two options.
1. Start with Blank template, create Shell manually then adapt the rest of code.
2. Start with Shell template and then remove unnecessary generated code which we might not need to use at all.

Let's take a look on the 2nd option:

![XamarinFormsShellTemplate](SelectTemplate-Shell.png)

The template already creates some additional files like:

In **Models** directory:
* `Item.cs`

In **Services** directory:
* `IDataStore.cs`
* `MockDataStore.cs`

In **ViewModels** directory:
* `AboutViewModel.cs`
* `BaseViewModel.cs`
* `ItemDetailViewModel.cs`
* `ItemsViewModel.cs`

In **Views** directory:
* `AboutPage.xaml`
* `AboutPage.xaml.cs`
* `ItemDetailPage.xaml`
* `ItemDetailPage.xaml.cs`
* `ItemsPage.xaml`
* `ItemsPage.xaml.cs`
* `NewItemPage.xaml`
* `NewItemPage.xaml.cs`

### Delete code

If you want to have a clean and empty Shell application start by deleting all these files except `BaseViewModel.cs` which contains some useful implementation of `INotifyPropertyChanged` interface so that you don't have to implement it by yourself again. Keep the directories, they help to keep code organized in well named namespaces.

If you try to build the solution after deleting those files you would get multiple compilation errors. That's expected, we are not done yet.

Open `BaseViewModel.cs` file and delete:
```csharp
using XamarinNavigation.Models;
using XamarinNavigation.Services;
```
and property
```csharp
public IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>();
```

Open `App.xaml.cs` file and delete:
```csharp
using XamarinNavigation.Services;
using XamarinNavigation.Views;
```
and registration on dependency service:
```csharp
DependencyService.Register<MockDataStore>();
```

If you try to build solution again, you still get compilation error that says something like:
```
Type ItemsPage not found in xmlns clr-namespace: ... AppShell.xaml
```

This is also expected, since we deleted `ItemsPage.xaml`, so let's create new `ContentPage` called (e.g.) `MainView.xaml` and place it in `Views` directory, like this:

### New MainView

![NewMainView](NewMainView.png)

#### MainView.xaml
```xaml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:d="http://xamarin.com/schemas/2014/forms/design"
             xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
             mc:Ignorable="d"
             x:Class="XamarinNavigation.Views.MainView">
    <ContentPage.Content>
        <StackLayout>
            <Label Text="Welcome to Xamarin.Forms!"
                VerticalOptions="CenterAndExpand" 
                HorizontalOptions="CenterAndExpand" />
        </StackLayout>
    </ContentPage.Content>
</ContentPage>
```

#### MainView.xaml.cs
```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinNavigation.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainView : ContentPage
    {
        public MainView()
        {
            InitializeComponent();
        }
    }
}
```

### Adapt AppShell.xaml

Now, change the `AppShell.xaml` to have only single page (without any additional flyouts or tabs):

```xaml
<?xml version="1.0" encoding="UTF-8" ?>
<Shell
    x:Class="XamarinNavigation.AppShell"
    xmlns="http://xamarin.com/schemas/2014/forms"
    xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
    xmlns:d="http://xamarin.com/schemas/2014/forms/design"
    xmlns:local="clr-namespace:XamarinNavigation.Views"
    xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
    Title="XamarinNavigation"
    mc:Ignorable="d">

    <Shell.Resources>
        <ResourceDictionary>
            <Color x:Key="NavigationPrimary">#2196F3</Color>
            <Style x:Key="BaseStyle" TargetType="Element">
                <Setter Property="Shell.BackgroundColor" Value="{StaticResource NavigationPrimary}" />
                <Setter Property="Shell.ForegroundColor" Value="White" />
                <Setter Property="Shell.TitleColor" Value="White" />
                <Setter Property="Shell.DisabledColor" Value="#B4FFFFFF" />
                <Setter Property="Shell.UnselectedColor" Value="#95FFFFFF" />
                <Setter Property="Shell.TabBarBackgroundColor" Value="{StaticResource NavigationPrimary}" />
                <Setter Property="Shell.TabBarForegroundColor" Value="White" />
                <Setter Property="Shell.TabBarUnselectedColor" Value="#95FFFFFF" />
                <Setter Property="Shell.TabBarTitleColor" Value="White" />
            </Style>
            <Style BasedOn="{StaticResource BaseStyle}" TargetType="TabBar" />
        </ResourceDictionary>
    </Shell.Resources>

    <TabBar>
        <Tab Title="Main" Icon="xamarin_logo.png">
            <ShellContent ContentTemplate="{DataTemplate local:MainView}" />
        </Tab>
    </TabBar>

</Shell>
```

Now, build and start your newly created Shell the application:

![EmptyShellApp](EmptyShellApp.png)

Note: if you have an empty flyout (hamburger menu) still visible, it probably means that you are on Xamarin.Forms 4.2, which has this bug inside. Solution is to update your NuGet Xamarin.Forms package to latest stable version. If you plan to use flyout anyway, then you can stay with 4.2 if you want to.
