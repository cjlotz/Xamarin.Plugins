#addin "Cake.FileHelpers"

// =====================
//  Arguments
// =====================

var target = Argument("target", "Default");

// Build
var configuration = Argument("configuration", "Debug");
var platform = Argument("platform", "Any%20CPU");

// NuGet Packaging
var versionNumber = Argument<string>("versionNumber", null);
var buildNumber = Argument("buildNumber", 0);
var buildTag = Argument<string>("buildTag", null);

// =====================
//  Globals
// =====================

var publishPath = "";
var packageVersion = "";
var projectName = "Plugin.Messaging";
var nuspecFile = File($"./{projectName}.nuspec");

// =====================
//  Build Tasks 
// =====================

Task("Clean")
    .ContinueOnError()
    .Does(() =>
{
	DeleteDirectory("./packages", true);    
    CleanDirectories("./**/bin");
	CleanDirectories("./**/obj");
});

Task("RestoreNuGetPackages")
    .Does(() =>
{
    NuGetRestore($"./{projectName}.sln");
});

Task("Build")
    .IsDependentOn("RestoreNuGetPackages")
    .Does(() =>
{
    Information($"Configuration: {configuration}");
    Information($"Platform: {platform}");
    
    DotNetBuild($"./{projectName}.sln", settings =>
        settings.SetConfiguration(configuration)
			.SetVerbosity(Verbosity.Minimal)
            .WithTarget("Build")
            .WithProperty("TreatWarningsAsErrors", "false")
            .WithProperty("Platform", platform));
});

Task("BuildSamples")
    .IsDependentOn("RestoreNuGetPackages")
    .Does(() =>
{
    Information($"Configuration: {configuration}");
    Information($"Platform: {platform}");
    
    DotNetBuild($"./{projectName}.Samples.sln", settings =>
        settings.SetConfiguration(configuration)
			.SetVerbosity(Verbosity.Minimal)
            .WithTarget("Build")
            .WithProperty("TreatWarningsAsErrors", "false")
            .WithProperty("Platform", platform));
});

Task("Rebuild")
	.IsDependentOn("Clean")
	.IsDependentOn("Build")
    .Does(() =>
{
});

Task("RebuildSamples")
	.IsDependentOn("Clean")
	.IsDependentOn("BuildSamples")
    .Does(() =>
{
});

// ========================
//  Packaging Tasks 
// ========================

Task("VersionSetup")
    .Does(() =>
{
    if (buildTag == null)
        throw new Exception("buildTag argument not specified. Use -buildTag=<ALPHA|BETA|RTM>");
    
    if (buildNumber == 0)
        throw new Exception("buildNumber argument not specified. Use -buildNumber=<number>");
    
    if (versionNumber == null)
        throw new Exception("versionNumber argument not specified. Use -versionNumber=<major.minor.patch>");
    
    packageVersion = versionNumber;        
    if (!string.IsNullOrEmpty(buildTag) && buildTag != "RTM")
        packageVersion += $"-{buildTag.ToLower()}";
});

Task("Version")
	.IsDependentOn("VersionSetup")
    .Does(() =>
{
    var asmInfo = File($"./{projectName}.Shared/GlobalAssemblyInfo.cs");
	
	var asmVersion = $"{versionNumber}.{buildNumber}";

    CreateAssemblyInfo(asmInfo, new AssemblyInfoSettings {   
        Configuration = configuration.ToUpper(),
		Company = "",
        Product = projectName,
        Copyright = $"Copyright (c) {DateTime.Now.Year}",
        Trademark = "",
        Version = "1.0.0.0",
        FileVersion = asmVersion,
        InformationalVersion = packageVersion
    });

	ReplaceRegexInFiles("./Component/component.yaml", @"version: \d+\.\d+\.\d+\.\d+", $"version: {asmVersion}");
	ReplaceRegexInFiles("./Component/component.yaml", $@"Xam.Plugins.Messaging, Version=\d+\.\d+\.\d+\.\d+", $"Xam.Plugins.Messaging, Version={asmVersion}");
});

Task("Package")
	.IsDependentOn("Version")	
	.IsDependentOn("Rebuild")	
    .Does(() =>
{
	CleanDirectory("./output");
	
    // Build Nuget packages
	NuGetPack (nuspecFile, new NuGetPackSettings { 
		Version = packageVersion,
		Verbosity = NuGetVerbosity.Detailed,
		OutputDirectory = "./output/",
		BasePath = "./",
	});

    // Build Xamarin component package
	var yamlDir = "./Component/";	
	StartProcess("./xamarin-component/xamarin-component.exe", $"package {yamlDir}");

	var file = $"Xam.Plugins.Messaging-{versionNumber}.{buildNumber}.xam";
    var oldFile = File($"./Component/{file}");
	var newFile = File($"./output/{file}");
	CopyFile(oldFile, newFile);
    DeleteFile(oldFile);
});

RunTarget(target)