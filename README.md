# Jobus

### Tech stack:
* ASP.NET Core
* Autofac
* AutoMapper
* EF Code First
* Npgsql
* IMemoryCache
* xUnit
* Moq
* AutoFixture
* FluentAssertions
* NLog
* Swashbuckle (Swagger)

### Code First:
1. **Microsoft.EntityFrameworkCore.Tools** package required.
1. PowerShell 3.0 or higher -> https://docs.microsoft.com/en-us/powershell/scripting/setup/installing-windows-powershell?view=powershell-6
1. In **Package Manager Console** pick **Default project** with **DbContext**.
2. `Add-Migration InitialCreate` - creates migration file.
2. `Update-Database` - creates table on db.