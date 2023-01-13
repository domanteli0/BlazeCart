[![Coverage Status](https://coveralls.io/repos/github/domanteli0/BlazeCart/badge.svg?branch=main)](https://coveralls.io/github/domanteli0/BlazeCart?branch=main)

# BlazeCart

## Overview

To get the general idea, you can [take a look at our presentation for this project](https://www.canva.com/design/DAFVBYjb4c8/JMEdZXMOcKCG3mo3Hxnpgw/view#1).

NOTE: the most current progress is on `Ats2` branch.

This project is organized as such:

* `Scraper` (Class library) - has actual scraper implementations
* `ScraperFunction` - an [Azure Function](https://learn.microsoft.com/en-us/azure/azure-functions/functions-overview) which runs regularly and puts scraped contents into a database; it's hosted on Azure.
* `CategoryMap` - contains common ~~category tree~~ categories and mapper classes which catagories from other stores are mapped onto.
* `Models` - contains classes used through out the project. __NOTE:__ `BlazeCart` uses different classes to represent the same data, such as `Item` and `Category`.
* `DB` (Entity Framework) - contains code-first DBContexts
* `Api` (ASP.NET Core Web API) - REST API for accessing scraped contents from DB
* `BlazeCart`(MAUI) - the UI, mobile (Android) app
* `Common` - contains various helper extention methods
* `Tests` - it's... tests

The client relies on a working instance of `Api`, which relies on `DB`, which is populated by `ScraperFunction`; see `Untitled Diagram.drawio` for clarity. 

### DB
Updates to DB schema are done with `dotnet ef` cli tool.

Steps:
1. `dotnet ef migrations add <Migration name>` - generates migration(s) from changes
2. `dotnet ef database update` - actually updates the database

Migrations are needed then properties of classes `Entity`, `Category`, `Item` have been changed, added or removed.

### Tests

To run all tests: `dotnet test` to test.

To run a specific test use `--filter`, example: `dotnet test --filter "FullyQualifiedName=Tests1.Scraper.BarboraScarperTest.DuplicateTest"`

NOTE: `CategoryMap/Implementations` tests may be incorrect thus may not pass even if the code is correct.

### UI

In theory iOS is supported, but hasn't been tested since it was primarily developed on Android. The app is based on [mvvm](https://learn.microsoft.com/en-us/dotnet/architecture/maui/mvvm) .

#### App design in Figma

Click [here](https://www.figma.com/file/I7gXX51ld8kFgJUxB7puwP/App-Design?node-id=23%3A475) to see the design.

#### Dependencies

This project uses [DevExpress](https://nuget.devexpress.com), you'll need to obtain feed url from them and add it to your Nuget sources.

#### Running

Use Visual Studio or [cli](https://mauiman.dev/maui_cli_commandlineinterface.html) and then either: connect a physical device via `adb` or use an emulator.

## Useful links

* [MAUI check](https://github.com/Redth/dotnet-maui-check)
* [MVVM and MAUI](https://learn.microsoft.com/en-us/dotnet/maui/xaml/fundamentals/mvvm?view=net-maui-7.0)
* [Data Caching](https://www.youtube.com/watch?v=a37qBMt0V9w)
* [JSON to C# classes](https://quicktype.io)
* [Curl to C# code converter](https://curl.olsh.me)
* [Cron expression generator](https://crontab.cronhub.io)

## Troubleshooting

If you have run into problems make consult with this checklist:

* DB connection strings are added in configs (appsettings.json, secrets.json).
* Dotnet and dependency versions match (this project targets `net6.0`).

### Api

Api may take a few minutes to start up, it may send 503 response method while it does.

### ScraperFunction

* You may need to fetch app-settings using [func](https://learn.microsoft.com/en-us/azure/azure-functions/functions-run-local?tabs=v4%2Cmacos%2Ccsharp%2Cportal%2Cbash#install-the-azure-functions-core-tools). 

    Run `func azure functionapp fetch-app-settings <function-name>`

* If you get this:
    ```
    [TIMESTAMP] Executed 'ScraperFunction' (Failed, Id=[...], Duration=80ms)
    [TIMESTAMP] System.Private.Uri: Value cannot be null. (Parameter 'uriString').
    ```
    This _may_ mean that (Consult Google first for most common causes) `Uri` field is `null` in some `Category` or `Item`, because some part of code will try to deep clone it. [NOTE: MAY BE OUTDATED INFO]

## A short post-mortem

As many student projects and assignments, this one was done in quick spirts before a deadline and then almost abandomed for weeks. As a result many features were left half-baked, code quality may have suffered.
