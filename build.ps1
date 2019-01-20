#Requires -Version 5.0

param
(
    [Parameter()]
    [String[]] $TaskList = @("RestorePackages", "Build", "CopyArtifacts"),
	[Parameter()]
    [string] $OutputPath = "PhpTravels.UITests\bin\Debug",
    # Also add following parameters: 
    #   Configuration
    #   Platform
    #   OutputPath
	[Parameter()]
	[String] $Configuration = "Debug",
	[Parameter()]
	[string] $Platform = "Any CPU",
	
	

    # And use these parameters inside BuildSolution function while calling for MSBuild.
    # Use /p swith to pass the parameter. For example:
    #   MSBuild.exe src/Solution.sln /p:Configuration=$Configuration
    # More info here: https://docs.microsoft.com/en-us/visualstudio/msbuild/common-msbuild-project-properties?view=vs-2017

    [Parameter()]
    [String] $BuildArtifactsFolder = "C:\BuildArtifacts"
)
$NugetUrl = "https://dist.nuget.org/win-x86-commandline/latest/nuget.exe"
$NugetExe = Join-Path $PSScriptRoot "nuget.exe"
$MSBuildExe = "C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\MSBuild\15.0\Bin\MSBuild.exe" 
$Solution = Join-Path $PSScriptRoot "PhpTravels.UITests.sln"

# Define additional variables here (MSBuild path, etc.)

Function DownloadNuGet()
{
    if (-Not (Test-Path $NugetExe)) 
    {
        Write-Output "Installing NuGet from $NugetUrl..."
        Invoke-WebRequest $NugetUrl -OutFile $NugetExe -ErrorAction Stop
    }
}

Function RestoreNuGetPackages()
{
    DownloadNuGet
    Write-Output 'Restoring NuGet packages...'
   & $NugetExe restore $Solution
} 

Function BuildSolution()
{
    #Write-Output "Building '$Solution' solution..."
	 & $MSBuildExe $Solution /p:Configuration=$Configuration /p:Platform=$Platform
    # MSBuild.exe call here
}

Function CopyBuildArtifacts()
{
    param
    (
        [Parameter(Mandatory)]
        [String] $SourceFolder,
        [Parameter(Mandatory)]
        [String] $DestinationFolder
		
    )
	   Copy-item -Force -Recurse -Verbose $SourceFolder -Destination $DestinationFolder

	#Copy-Item  $SourceFolder -Destination  $DestinationFolder -Recurse
    # Copy all files from $SourceFolder to $DestinationFolder
    #
    # Useful commands:
    #   Test-Path - check if path exists
    #   Remove-Item - remove folders/files
    #   New-Item - create folder/file
    #   Get-ChildItem - gets items from specified path
    #   Copy-Item - copies item into destination folder
    #
    #     NOTE: you can chain methods using pipe (|) symbol. For example:
    #           Get-ChildItem ... | Copy-Item ...
    #
    #           which will get items (Get-ChildItem) and will copy them (Copy-Item) to the target folder
}

foreach ($Task in $TaskList) {
   if ($Task.ToLower() -eq 'restorepackages')
    {
		$error.clear()
        RestoreNuGetPackages
		if($error -Or $LastExitCode -ne 0)
		{
		 Throw "RestoreNuGetPackages Error"
		}
    }
    if ($Task.ToLower() -eq 'build')
    {
		$error.clear()
        BuildSolution
		if($error -Or $LastExitCode -ne 0)
		{
		 Throw "Build Error"
		}
    }
    if ($Task.ToLower() -eq 'copyartifacts')
    {
        $error.clear()
             CopyBuildArtifacts "$OutputPath" "$BuildArtifactsFolder"
            if($error)
            {
                Throw "An error occured while copying build artifacts."
            }
    }
}