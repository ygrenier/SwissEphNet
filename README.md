SwissEphNet
===========

This project is an Astrodienst Swiss Ephemeris (http://www.astro.com/swisseph/) .Net portage from C to C# in a PCL project for cross platform usage.

# Firsts steps

Our first step is to convert the C source code to C#, and provide some conversions from C like string format.

## (x)printf 

We implements (x)printf base methods with the Richard Prinz project (http://www.codeproject.com/Articles/19274/A-printf-implementation-in-C) with somes updates.

## (x)scanf

We implements (x)scanf base methods with the Jonathan Wood project (http://www.blackbeltcoder.com/Articles/strings/a-sscanf-replacement-for-net) with some updates and unit tests.

## C conversion

All C files are included in the partial class 'SwissEph' each in a specific file.

All exported constants are defined as public in the class.

All exported methods are defined as protected in the class. Each of this methods are converts in public method in the class.

The other elements are declared as private.

The compilation configuration use pre-processor constants. We remove lot of them in our case. The other are converted as constants, not pre-processor.

# References

The Swiss Ephemeris Programming Interface documentation : http://www.astro.com/swisseph/swephprg.htm.
Last code source of Swiss Ephemeris from ftp://ftp.astro.ch/pub/swisseph/.
The NASA JPL resouces : http://www.jpl.nasa.gov/, http://ssd.jpl.nasa.gov/
