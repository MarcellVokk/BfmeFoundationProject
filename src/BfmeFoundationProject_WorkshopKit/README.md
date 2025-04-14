# WorkshopKit by the Bfme Foundation Project
#### This project is part of the Bfme Foundation Project!
<a href="https://github.com/MarcellVokk/bfme-foundation-project">
    <img src="https://img.shields.io/badge/GitHub-Foundation Project-lime"/>
</a>

## Welcome
Welcome to the official github repository of WorkshopKit!
This package allows you to access the BFME Workshop, and also provides realy powerful tools for patch switching, modding and more!

## Get on NuGet
<a href="https://www.nuget.org/packages/BfmeFoundationProject.WorkshopKit">
   <img src="https://img.shields.io/nuget/v/BfmeFoundationProject.WorkshopKit"/>
</a>

## Usage
- `BfmeWorkshopManager` exposes the curent state of the users workshop library, and for example provides functions to get the curently enabled mod for a specified game, or get a list of enabled enhancements for a specified game.
- `BfmeWorkshopQueryManager` exposes several methods that can be used to query or search the workshop.
- `BfmeWorkshopDownloadManager` exposes several methods that allow you to download packages from the workshop. Downloading a package is required when syncing, as a package preview (obtained by a query) is not enough.
- `BfmeWorkshopSyncManager` is the workshops built in "patch switcher". It is highly recommended that you use this instead of trying to build your own!
- `BfmeWorkshopLibraryManager` is used to access the users library, and query, add or remove workshop entries from it.
- `BfmeWorkshopAdminManager` is for uploading or deleting your own entries to the workshop.
- `BfmeWorkshopAuthManager` is used for authentication. This is the way you can get a `BfmeWorkshopAuthInfo` to use in the methods exposed by `BfmeWorkshopAdminManager`.

## Examples
A very simple workshop browser example made in WinForms is available in this repository.

###### Developed by The Online Battle Arena Team, in collaboration with the Patch 2.22 Team
