Updates to DB schema are done with `dotnet ef` cli tool.

Steps:
1. `dotnet ef migrations add <Migration name>`
2. `dotnet ef database update`
