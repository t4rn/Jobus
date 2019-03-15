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

### Hosting on IIS [https://docs.microsoft.com/en-us/aspnet/core/publishing/iis?tabs=aspnetcore2x]:
* Install .NET SDK https://download.microsoft.com/download/D/8/1/D8131218-F121-4E13-8C5F-39B09A36E406/dotnet-sdk-2.1.104-win-gs-x64.exe (from https://www.microsoft.com/net/learn/get-started/windows)
* Install .NET Core Windows Server Hosting bundle https://dotnet.microsoft.com/download/thank-you/dotnet-runtime-2.2.1-windows-hosting-bundle-installer [2.0.8: https://aka.ms/dotnetcore-2-windowshosting]
* CMD -> `net stop was /y`  ->  `net start w3svc`

### Web Deploy:
* Install WebDeploy_amd64_en-US.msi https://www.microsoft.com/en-us/download/details.aspx?id=43717 (Complete installation)
* IIS: Management Service -> Enable Remote Connections
* Check, if Server is listening (in CMD):  `netstat -aon | findstr :8172`
* Check, if Remote Agent Service and Web Management Service are running: `net start wmsvc` & `net start msdepsvc`

## Version check:
`dotnet --version`

`dotnet --info`
