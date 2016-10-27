function CopyFolder($project, $dest){
    $temp = mkdir $dest -Force
    Get-ChildItem -Recurse $project -Force -ErrorAction SilentlyContinue -File `
        | Where-Object { 
            ($_.PSIsContainer -eq $false) `
            -and !($_.Name.Contains(".vshost.")) `
            -and !($_.Name.Contains("Tests")) `
            -and !($_.Name.EndsWith((".pdb"))) `
            `
        } `
        | ForEach-Object { Copy-Item $_.FullName -Destination $dest }
}

# Build directories
$temp = mkdir ./binaries -Force  
$temp = mkdir ./binaries/net40 -Force  
$temp = mkdir ./binaries/netcore -Force

$config = "Release"
$net = "bin/$config/"
$net40 = "bin/$config/net40"
$netcore = "bin/$config/netcoreapp1.0"
$netstandard13 = "bin/$config/netstandard1.3"

# Copy SweMini
CopyFolder "./Programs/SweMini/$net40" "./binaries/net40/"
CopyFolder "./Programs/SweMini/$netcore" "./binaries/netcore/SweMini"
CopyFolder "./SwissEphNet/$netstandard13" "./binaries/netcore/SweMini"

# Copy SweTest
CopyFolder "./Programs/SweTest/$net40" "./binaries/net40/"
CopyFolder "./Programs/SweTest/$netcore" "./binaries/netcore/SweTest"
CopyFolder "./SwissEphNet/$netstandard13" "./binaries/netcore/SweTest"

# Copy SweWin
CopyFolder "./Programs/SweWin/$net" "./binaries/net40/"
