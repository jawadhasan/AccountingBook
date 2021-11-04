# escape=`

### 1: Build Angular Application ###

FROM node:12.7-alpine AS node-build

#setup directory inside container
WORKDIR /usr/src/app

#copy source-code to container
COPY AccountingBook/AccountingBook.Web/ .

#install npm packages and build the angular application
RUN npm install
RUN npm run build


### 2: Build/Publish .NET Core Web Application ###

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build

#set up a directory in container
WORKDIR /scr

#copy sourceCode to /src folder on container
COPY AccountingBook/ .

#restore nuget packages -- #RUN: Used to execute a command during the build process of the docker image
RUN dotnet restore "AccountingBook.Web/AccountingBook.Web.csproj"

#build the application
RUN dotnet build "AccountingBook.Web/AccountingBook.Web.csproj" -c Release -r linux-x64 -o /src/build

#publish the application as a publish-stage
FROM build AS publish
RUN dotnet publish "AccountingBook.Web/AccountingBook.Web.csproj" -c Release -r linux-x64 --self-contained true -o /src/publish


### 3: Run on Ubuntu ###

FROM ubuntu
RUN apt-get update
RUN apt-get install -y
RUN apt-get install libssl1.1


#set up a directory in container
WORKDIR /app

#copy the published output of .NET Core code
COPY --from=publish /src/publish . 

#copy the angular output
COPY --from=node-build /usr/src/app/wwwroot /app/wwwroot 

WORKDIR /app
CMD ["./AccountingBook.Web"]


#Run these commands from the AccountingBookExample directory
###########   Commands/Links##########################
#docker image build -t accountingbook -f ./docker/web/Dockerfile .
#docker run --name accountingbook-docker -p 5000:5000 accountingbook
