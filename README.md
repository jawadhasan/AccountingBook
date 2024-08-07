# Build Accounting Application Book

This repo contains source code for build Accounting application book.

[Book](https://amzn.eu/d/bAZcdPj/)   

## Updated Demos

[Updated to ASP .NET Core 6](https://hexquote.com/migrating-asp-net-core-3-1-web-application-to-asp-net-core-6/)    
[Deploy to AWS Serverless Lambda](https://hexquote.com/migrate-on-prem-web-app-net-core-angular-and-postgres-to-aws-serverless/)    
[Docker and Kubernetes](https://hexquote.com/deploying-a-web-application-to-kubernetes-basics/)    

## Deployed Application

[Deployed Application (as AWS Serverless Lambda)] (https://accounting.awsclouddemos.com/)    

## Update 01.11.2020

- Web application routing is adjusted for SPA routing.
- Update ConnectionString if needed (AccountingBook.Web).
- Chart.js script reference is removed from angular.json file.
- If you downloaded the code you might need to setup the database. You can run migration using ef command (update-database).

# AccountingApp

Install following software/SDKs on your development machine:

### Software(s) / SDK
Node: V10.16.0    
.NET: 3.0.100    
Angular-cli: 9.1.1    

### Clone and Install dependencies

1. Clone the Repository    
2. cd to directory of AccountingBook.Web project    
3. npm install    
4. NuGet Restore    

### Debug The Applicatoin

Open two powershell windows in the AccountingBook.Web project and run following commands:

- Backend: `dotnet watch run`
- Frontend: `ng serve --proxy-config proxy.config.json`


## Solution Strcuture

### AccountingBook.Core

This project contains all the domain-model code. This project has no dependency on any other project.    

### AccountingBook.Data

This project contains code related to data access concerns. It also reference AccountingBook.Core project as dependency.    

- EF core is used along with PostgreSQL.   
- Run EF migrations (update-database).    

### AccountingBook.Web

- This project contains the front-end and backend code for web part.
- The "src" folder inside the project is where source-code is for Angular. Angular-cli shall be used.
	
###Publish and Deploy

AngularApp:    
`npm run build -prod -aot`

.NET Core:    
`dotnet publish AccountingBook.Web.csproj -c debug -r win-x64 --self-contained true`
or    
`dotnet publish -c Debug -r win10-x64 /p:PublishSingleFile=true /p:PublishTrimmed=true`

