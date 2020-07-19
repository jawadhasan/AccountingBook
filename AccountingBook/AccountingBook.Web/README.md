# AccountingApp

Install following software/SDKs on your development machine:

Software(s) / SDK:
Node: V10.16.0
.NET: 3.0.100
Angular-cli: 9.1.1


Clone and Install dependencies:

1. Clone the Repository
2. cd to directory of AccountingBook.Web project
3. npm install
4. NuGet Restore

Debug The Applicatoin:

Open two powershell windows in the AccountingBook.Web project and run following commands:

-> Backend: dotnet watch run
-> Frontend: ng serve --proxy-config proxy.config.json

Solution Strcuture:

AccountingBook.Core

- This project contains all the domain-model code. This project has no dependency on any other project.

AccountingBoiok.Data

- This project contains code related to data access concerns. It also reference AccountingBook.Core project as dependency.

- EF core is used along with PostgreSQL.

AccountingBook.Web:

- This project contains the front-end and backend code for web part.

- The "src" folder inside the project is where source-code is for Angular. Angular-cli shall be used.

	1. src-> ng serve (to start development server for angular part)

	
Publish and Deploy

AngularApp:
-> npm run build -prod -aot

.NET Core:
-> dotnet publish AccountingBook.Web.csproj -c debug -r win-x64 --self-contained true
or
-> dotnet publish -c Debug -r win10-x64 /p:PublishSingleFile=true /p:PublishTrimmed=true

