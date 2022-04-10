
# SettingsPaths dotnet tool

Reads Appsettings.json or supplied json configuration file and creates a settingsPaths.csv file with the configuration paths and values for entering in tools like Octopus Deploy


# Install

 ***Requires .NET 6 Runtime***

`dotnet tool install SettingsPaths --global`

available on [Nuget](https://www.nuget.org/packages/SettingsPaths) 

![Nuget Badge](https://buildstats.info/nuget/SettingsPaths)

# Uninstall
`dotnet tool uninstall SettingsPaths --global`

# How to Run

`SettingsPaths pathToSettingsFile with or without the including the settings file name.
If no settings file supplied it will assume appsettings.json`
```
Examples: At command line, SettingsPaths C:\MyProjectFolder or SettingsPaths C:\MyProjectFolder\appsettings.json
```

`SettingsPaths with out any arguments. If no settings file supplied it will assume appsettings.json`
```
At command line, SettingsPaths no arguments will prompt, "Enter path to appsettings.json:" for path with or without settings file name.
```
# OUTPUT
```
File named settingsPaths.csv, comma delimited, at the same path.
```
```
VARIABLE NAME,VALUE
AllowedHosts,*
AppSettings:TargetPath,C:\watcher
Serilog:Enrich:0,FromLogContext
Serilog:Enrich:1,WithMachineName
Serilog:Enrich:2,WithProcessId
Serilog:Enrich:3,WithThreadId
Serilog:MinimumLevel:Default,Information
Serilog:MinimumLevel:Override:Microsoft,Warning
Serilog:MinimumLevel:Override:System,Warning
Serilog:Properties:ApplicationName,MyApplication
Serilog:WriteTo:0:Args:path,C:\temp\log.txt
Serilog:WriteTo:0:Args:rollingInterval,Day
Serilog:WriteTo:0:Name,File
```