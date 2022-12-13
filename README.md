[![Coverage Status](https://coveralls.io/repos/github/domanteli0/BlazeCart/badge.svg?branch=main)](https://coveralls.io/github/domanteli0/BlazeCart?branch=main)

# BlazeCart .NET MAUI App

## App design in Figma
Click [here](https://www.figma.com/file/I7gXX51ld8kFgJUxB7puwP/App-Design?node-id=23%3A475) to see the design.

## Overview

This project is organized as such:

* `Scraper` (Class library) - has actual scraper implementations
* `ScraperFunction` - an [Azure Function](https://learn.microsoft.com/en-us/azure/azure-functions/functions-overview) which runs regularly and puts scraped contents into a database; it's hosted on Azure, duh...
* `DB` (Entity Framework) - contains code-first DBContexts
* `Api` (ASP.NET Core Web API) - REST Api for accessing scraped contents from DB
* `BlazeCart`(MAUI) - is the UI mobile client

## Troubleshooting

If you have run into problems make consult with this checklist:

* DB connection strings are added in configs (appsettings.json, secrets.json).
* Dotnet and dependency versions match (Recomened version 6).

