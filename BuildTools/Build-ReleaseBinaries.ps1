#
# Compile projects and build Nuget packages
# -------------------------------------------------------------------------------------------------------------------

# Init
$InvocationPath = Resolve-Path $MyInvocation.InvocationName
$ScriptPath = [System.IO.Path]::GetDirectoryName( $InvocationPath )
$SolutionPath = [System.IO.Path]::GetDirectoryName( $ScriptPath )
$Nuget = "$ScriptPath\NuGet.exe"
$DestPath = [System.IO.Path]::Combine($SolutionPath, "release-bin")
$BuildOptions = "/p:Configuration=Release "

# Search MsBuild
$MsBuild = "${env:ProgramFiles(x86)}\MSBuild\12.0\Bin\MSBuild.exe"
if( ! (Test-Path $MsBuild) ){
    $MsBuild = "$env:ProgramFiles\MSBuild\12.0\Bin\MSBuild.exe"
}
if( ! (Test-Path $MsBuild) ){
    $MsBuild = "$env:windir\Microsoft.NET\Framework64\v4.0.30319\MSBuild.exe"
}

# Prepare release-bin folder
if (! [System.IO.Directory]::Exists($DestPath))
{
    $res = [System.IO.Directory]::CreateDirectory($DestPath)
}

# Prepare nuget folder
$NugetOuput = [System.IO.Path]::Combine($DestPath, "nuget")
if ([System.IO.Directory]::Exists($NugetOuput))
{
    [System.IO.Directory]::Delete($NugetOuput, $true)
}
$res = [System.IO.Directory]::CreateDirectory($NugetOuput)

# Build SwissEphNet package
$Project = "$SolutionPath\SwissEphNet\SwissEphNet.csproj"
& $Nuget pack $Project -IncludeReferencedProjects -Prop Configuration=Release -Build -OutputDirectory $NugetOuput

# Clean and compile release binaries
$CommandClean = "&""$MsBuild"" $SolutionPath\SwissEphNet.sln $BuildOptions /t:clean "
Invoke-Expression $CommandClean
$CommandBuild = "&""$MsBuild"" $SolutionPath\SwissEphNet.sln $BuildOptions /t:build "
Invoke-Expression $CommandBuild

# Prepare binaries folder
$Binaries = [System.IO.Path]::Combine($DestPath, "bin")
if ([System.IO.Directory]::Exists($Binaries))
{
    [System.IO.Directory]::Delete($Binaries, $true)
}
$res = [System.IO.Directory]::CreateDirectory($Binaries)

# Copy all binaries
Get-ChildItem -Recurse $SolutionPath -Force -ErrorAction SilentlyContinue -File `
    | Where-Object { 
        ($_.PSIsContainer -eq $false) `
        -and ($_.DirectoryName.EndsWith("bin\Release")) `
        -and !($_.Name.Contains(".vshost.")) `
        -and !($_.Name.Contains("Tests")) `
        -and ( ( $_.Name -like "*.dll") -or ( $_.Name -like "*.exe") ) `
    } `
    | ForEach-Object { Copy-Item $_.FullName $Binaries } 

