# Xamarin.Forms

## Xamarin.Essentials

Source: https://docs.microsoft.com/en-us/xamarin/essentials/

Xamarin.Essentials provides a single cross-platform API that works with any Xamarin.Forms, Android, iOS, or UWP application that can be accessed from shared code no matter how the user interface is created.<br/>

Xamarin.Essentials provides API for using mobile sensors, access to battery info, device info, connectivity, using SMS, e-mail sending...

#### How to add Xamarin.Essentials to my app?

Source: https://docs.microsoft.com/en-us/xamarin/essentials/get-started?tabs=windows%2Candroid

Xamarin.Essentials is available as a NuGet package and is included in every new project in Visual Studio. It can alo be added to any existing using Visual Studio with the follow steps:
1.	In the Solution Explorer panel, right click on the solution name and select Manage NuGet Packages.
2.	Search for Xamarin.Essentials and install the package into ALL projects including Android, iOS, UWP, and .NET Standard libraries.
3.	Add a reference to Xamarin.Essentials in any C# class to reference the APIs.

````csharp
using Xamarin.Essentials;
````

Xamarin.Essentials supports a minimum Android version of 4.4, corresponding to API level 19, but the target Android version for compiling must be 9.0, corresponding to API level 28. (In Visual Studio, these two versions are set in the Project Properties dialog for the Android project, in the Android Manifest tab. In Visual Studio for Mac, they're set in the Project Options dialog for the Android project, in the Android Application tab.)

Xamarin.Essentials installs version 28.0.0.1 of the Xamarin.Android.Support libraries that it requires. Any other Xamarin.Android.Support libraries that your application requires should also be updated to version 28.0.0.1 using the NuGet package manager. All Xamarin.Android.Support libraries used by your application should be the same, and should be at least version 28.0.0.1. Refer to the [troubleshooting page](https://docs.microsoft.com/en-us/xamarin/essentials/troubleshooting) if you have issues adding the Xamarin.Essentials NuGet or updating NuGets in your solution.

In the Android project's MainLauncher or any Activity that is launched Xamarin.Essentials must be initialized in the OnCreate method:

````csharp
protected override void OnCreate(Bundle savedInstanceState)
{
    base.OnCreate(savedInstanceState);
    Xamarin.Essentials.Platform.Init(this, savedInstanceState);
}
````

To handle [runtime povolenia](https://developer.android.com/training/permissions/requesting) on Android, Xamarin.Essentials must receive any [OnRequestPermissionsResult](https://developer.android.com/reference/androidx/core/app/ActivityCompat.OnRequestPermissionsResultCallback.html#onRequestPermissionsResult(int,%20java.lang.String[],%20int[])). Add the following code to all Activity classes:

````csharp
public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
{
    Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

    base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
}

````

##### Visit the [Xamarin.Essentials GitHub](https://github.com/xamarin/Essentials) Repository to see the current source code, what is coming next, run samples, and clone the repository.

##### Browse through the [API documentation](https://docs.microsoft.com/en-us/dotnet/api/xamarin.essentials?view=xamarin-essentials) for every feature of Xamarin.Essentials.

##### Follow the [guides](https://docs.microsoft.com/en-us/xamarin/essentials/#feature-guides) to integrate these Xamarin.Essentials features into your application.

