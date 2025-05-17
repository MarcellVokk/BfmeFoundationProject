# BfmeKit by Online Battle Arena Team
#### This project is part of the Bfme Foundation Project!
<a href="https://github.com/MarcellVokk/BfmeFoundationProject">
    <img src="https://img.shields.io/badge/GitHub-Foundation Project-lime"/>
</a>

## Welcome
This library allows you to easily modify the registry entries used by the BFME games. Additionaly, it provides a way to list and generate previews of BFME maps, as well as list available color options and factions. It can also create installation registries from scratch.

## Get on NuGet
<a href="https://www.nuget.org/packages/BfmeFoundationProject.BfmeKit">
   <img src="https://img.shields.io/nuget/v/BfmeFoundationProject.BfmeKit"/>
</a>

## BfmeRegistryManager
- `BfmeRegistryManager.CreateNewInstallRegistry()` - Create new installation registry (install the game)
- `BfmeRegistryManager.GetKeyValue()` - Get a value from the game registry (get game language, installation directory, user data folder, etc...)
- `BfmeRegistryManager.SetKeyValue()` - Set a value in the games registry
- `BfmeRegistryManager.EnsureDefaults()` - Ensures that all necesary default values exist for the game to start without errors
- `BfmeRegistryManager.IsInstalled()` - Returns a bool indicating wether the game is installed or not.
- `BfmeRegistryManager.EnsureFixedRegistry()` - Manualy fixes the registry if it has been corrupted. Called every time when calling `BfmeRegistryManager.GetKeyValue()`.

## BfmeColorImporter
- `BfmeColorImporter.ImportColors()` - Returns a list of color options available in the curently installed version of the game specified.

## BfmeMapImporter
- `BfmeMapImporter.ImportMaps()` - Returns a list of maps available in the curently installed version of the game specified.
- `BfmeMapImporter.GenerateMapPreview()` - Generates and returns a bitmap image preview of the map specified.

## BfmeFactionImporter
- `BfmeFactionImporter.ImportFactions()` - Returns a list of factions available in the curently installed version of the game specified.

###### Developed by The Online Battle Arena Team (Beterwel, MarcellVokk)
