SwissEphNet
===========

This project is an Astrodienst Swiss Ephemeris (http://www.astro.com/swisseph/) .Net portage from C to C# in a PCL project for cross platform usage.

# Usage

Now SwissEphNet is available as a [Nuget package](https://www.nuget.org/packages/SwissEphNet): `Install-Package SwissEphNet`

Or you can download the binaries in [the last release](https://github.com/ygrenier/SwissEphNet/releases/latest).

SwissEphNet is a Portable Class Library with support for .Net 4+, Silverlight 5, Windows Phone 8, Windows Store apps, Xamarin.Android and Xamarin.iOS.

## Create an instance

SwissEphNet.SwissEph is ```IDisposable``` so you can use it with an ```using``` statement.

```C#
using (var sweph = new SwissEphNet.SwissEph()) {
    // Use it
}
```

## Loading files

SwissEphNet is a Portable Classe Library and we don't have file access.

As Swiss Ephemeris use some data files, an event exists for loading the files required.

```C#
using (var sweph = new SwissEphNet.SwissEph()) {
    sweph.OnLoadFile += (s, e) => {
        // Loading file
    };
    // Use it
}
```

For more information [read this page](https://github.com/ygrenier/SwissEphNet/wiki/Loading-files).

# Firsts steps

Our first step is to convert the C source code to C#, and provide some conversions from C like string format.

## (x)printf 

We implements (x)printf base methods with the Richard Prinz project (http://www.codeproject.com/Articles/19274/A-printf-implementation-in-C) with somes updates.

## (x)scanf

We implements (x)scanf base methods with the Jonathan Wood project (http://www.blackbeltcoder.com/Articles/strings/a-sscanf-replacement-for-net) with some updates and unit tests.

## C conversion

All C files are included in the partial class 'SwissEph' each in a specific file.

All exported constants are defined as public in the class.

All exported methods are defined as public in the class.

The other elements are declared as private.

The compilation configuration use pre-processor constants. We remove lot of them in our case. The other are converted as constants, not pre-processor.

# Seconds steps

When we got a correct C conversion, we rewrite public methods to use a more .Net/C# friendly code. Using classes and structs
instead of array of double, returns values instead of reference parameters, throw exception instead of reference 'serr', etc.

For this we create more classes to manage each domain (date, math, etc.) and the Sweph class federate all this classes.

The 'Swisseph' inherits from 'Sweph', and we replace each 'swe_*' methods by equivalent in 'Sweph'.

# References

The Swiss Ephemeris Programming Interface documentation : http://www.astro.com/swisseph/swephprg.htm.

Last code source of Swiss Ephemeris from ftp://ftp.astro.ch/pub/swisseph/.

The NASA JPL resouces : http://www.jpl.nasa.gov/, http://ssd.jpl.nasa.gov/.
