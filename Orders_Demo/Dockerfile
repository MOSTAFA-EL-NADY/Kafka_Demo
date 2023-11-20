
# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY ./Orders_Demo/*.csproj ./Orders_Demo/
RUN dotnet restore ./Orders_Demo/*.csproj

# copy everything else and build app
COPY ./Orders_Demo/. ./Orders_Demo/
WORKDIR /source/Orders_Demo
RUN dotnet publish -c release -o /app --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "Orders_Demo.dll"]