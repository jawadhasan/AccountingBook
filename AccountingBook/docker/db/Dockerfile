# escape=`
FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /scr

#copy sourceCode to /app folder on container
COPY AccountingBook/ .

#restore nuget packages -- #RUN: Used to execute a command during the build process of the docker image
RUN dotnet restore "AccountingBook.Web/AccountingBook.Web.csproj"

#build the application
RUN dotnet build "AccountingBook.Web/AccountingBook.Web.csproj" -c Release -r linux-x64 -o /src/build

#db script generation
RUN dotnet tool install --global dotnet-ef --version 3.0.*
ENV PATH="${PATH}:/root/.dotnet/tools"
RUN dotnet ef
RUN dotnet ef migrations script --project AccountingBook.Data `
            --startup-project AccountingBook.Web --idempotent --output /src/database/AccountingBookDB.sql


FROM postgres:11 AS db

ENV POSTGRES_PASSWORD sasa
ENV POSTGRES_DB AccountingBook

#copy the dbscript output of .NET EF
COPY --from=build /src/database/AccountingBookDB.sql /docker-entrypoint-initdb.d/



#docker image build -t accountingdb .