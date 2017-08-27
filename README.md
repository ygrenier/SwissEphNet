# SwissEphNet

This project is an Astrodienst Swiss Ephemeris (http://www.astro.com/swisseph/) .Net portage from 
C (version 2.06) to C# in a PCL/.Net Core project for cross platform usage.

Since version 2.6.0.21, the nuget package includes 2 versions:
- .Net 4.0
- .Net Standard 1.0

The programs SweMini and SweTest are availables in 2 versions:
- .Net 4.0
- .Net Core App 1.0

These programs are available in the "binary.zip" of [each release](https://github.com/ygrenier/SwissEphNet/releases).

## Samples

A new repos was created https://github.com/ygrenier/SwissEphNet.Samples containing
lot of sample applications for using the library on different application types.

## Works with async

For working with the async context read the [this paragraph](https://github.com/ygrenier/SwissEphNet/wiki/Loading-files#works-in-an-async-context).

# Breaking changes

## V:2.6.0.21

Since .NETStandard are supported, this library is not compiled on PCL version. Only
2 version are available: .NET 4.0 for old legacy application, .NETStandard 1.0
for the new framework.

.NETStandard 1.0 is supported by VS 2015.3 and VS 2017, so PCL are not usefull.

The new repos of https://github.com/ygrenier/SwissEphNet.Samples contains applications
using only this version of the library.

## V:2.5.1.16

Since 2.5.1.16 some libraries don't supports the "Windows-1252" code page. In this case, the default encoding become "UTF-8".

You can change the default encoding by assigning the static property ```SwissEphNet.SwissEph.DefaultEncoding```.

# Thread Local Storage (TLS) support

Since version 2.03.00 the Swiss Ephemeris library supports the 
[Thread-Local Storage (TLS)](https://en.wikipedia.org/wiki/Thread-local_storage), which
allows to run several calculations simultaneously with multiple threads.

As SwissEphNet is build an object ```SwissEphNet.SwissEph```, it always supports multiple
calculations. You just need create one ```SwissEphNet.SwissEph``` per thread. On other hand
it's still not thread-safe, so don't access the same ```SwissEphNet.SwissEph``` instance
from multiple threads.


# Projects splitted (2014-06-06)

From now the SweNet et SwephNet projects are moved to a new repos [SwephNet](https://github.com/ygrenier/SwephNet).

SwephNet is the next version of SwissEph, with a better .Net implementation. The two projects will 
continue to exist in parallel :
- SwissEphNet : is the direct C to C# portage of the Swiss Ephemeris.
- SwephNet : is the full .Net implementation of the Swiss Ephemeris.

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

# Continuous Integration in AppVeyor

The library is built and tested continuously with [AppVeyor CI](https://ci.appveyor.com/project/ygrenier/swissephnet).

Current build status of the branch ```master``` : [![Build status](https://ci.appveyor.com/api/projects/status/srgd3dqui7f4uvq5/branch/master)](https://ci.appveyor.com/project/ygrenier/swissephnet/branch/master)

Beware the build version number in AppVeyor is not the same than the published library.

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

Now the portage is correct, so we create a new project (https://github.com/ygrenier/SwephNet) with
a new interface more adapted to the .Net guidelines.

# References

The Swiss Ephemeris Programming Interface documentation : http://www.astro.com/swisseph/swephprg.htm.

Last code source of Swiss Ephemeris from ftp://ftp.astro.ch/pub/swisseph/.

The NASA JPL resouces : http://www.jpl.nasa.gov/, http://ssd.jpl.nasa.gov/.
