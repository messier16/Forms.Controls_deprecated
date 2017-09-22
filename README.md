## This repository is no longer being mantained, you should check the new one. Which is pretty much the same, except that the other one is now .NET standarized

# [Here is the link](https://github.com/messier16/Forms.Controls)

# Forms.Controls
Home of all Messier16 Xamarin.Forms controls :grin:

A collection of simple Xamarin.Forms controls for your shiny apps: 

- Checkbox
- RatingBar

Currently they work only on iOS and Android, though, they are coming to a platform near you.

## I want them now!

#### 1 - Install  
From NuGet: [![NuGet version](https://badge.fury.io/nu/Messier16.Forms.Controls.svg)](https://www.nuget.org/packages/Messier16.Forms.Controls/)

Remember to install the package in ALL your platform specific projects

#### 2 - SetUp 
Make sure to call `Messier16Controls.InitAll();` right after `Xamarin.Forms.Init();` in your projects. Look a tht `AppDelegate.cs` code snippet from the sample app:

```
    global::Xamarin.Forms.Forms.Init();
    Messier16Controls.InitAll();
```

Each renderer provide it's own `Init` method if you are just using an specific control.


#### 3 - Usage

Please check the [Sample/ Test app](https://github.com/messier16/Forms.Controls/tree/master/TestApp) for details on the usage of any of the controls.

Also, check the list of [known bugs](https://github.com/messier16/Forms.Controls/labels/bug) and feel free to report any other bug that you run into.

## Acknlowledgements & licenses

I'm not the smartest guy regarding licensing terms and stuff, but:

- This project uses a port of [Marxon13's M13Checkbox](https://github.com/Marxon13/M13Checkbox) which is under the MIT License
- This project uses a port of [erndev's EDStarRating](https://github.com/erndev/EDStarRating) which is under the BSD License
- This project uses (and takes a lor of inspiration from) [XLabs' controls](https://github.com/XLabs/Xamarin-Forms-Labs) which are under the Apache 2.0 license
- This project uses [xvare's UWPRatingControl](https://github.com/xvare/UWPRatingControl) which is under... who knows?

If you could explain to me (@fferegrino) how does those licenses work it would be greatly appreciated. And if you asked under which license the Messier16 controls are I'd say they're under MIT License.
