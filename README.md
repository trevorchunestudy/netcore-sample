# netcore-sample
A sample .net core project

Uses CQRS pattern.

## Setup
1. Create localdb of 'MyDb' and 'MyDb-Test'
2. Add appsettings.Development.json to 'Sample.Web' include connection string to 'MyDb' AND 'MyDb-Test'.  Comment out test db.
3. Add appsettings.Development.json to 'Sample.Tests.Integration' include connection string to 'MyDb-Test'.
4. Run `dotnet ef database update` against 'Sample.Web'.
5. Comment/Uncomment 'MyDb/MyDb-Test' and run same command of `dotnet ef database update` for the testing db against 'Sample.Web'.

