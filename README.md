[![Coverage Status](https://coveralls.io/repos/github/domanteli0/BlazeCart/badge.svg?branch=main)](https://coveralls.io/github/domanteli0/BlazeCart?branch=main)

# BlazeCart .NET MAUI App

## App design in Figma
Click [here](https://www.figma.com/file/I7gXX51ld8kFgJUxB7puwP/App-Design?node-id=23%3A475) to see the design.

## Overview

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

## Troubleshooting

If you have run into problems make consult with this checklist:

* DB connection strings are added in configs (appsettings.json, secrets.json).
* Dotnet and dependency versions match (Recomened version 6).

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
    This _may_ mean that (Consult Google first for most common causes) `Uri` field is `null` in some `Category`.
