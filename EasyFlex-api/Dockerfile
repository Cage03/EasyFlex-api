﻿FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["EasyFlex-api/EasyFlex-api.csproj", "EasyFlex-api/"]
RUN dotnet restore "EasyFlex-api/EasyFlex-api.csproj"
COPY . .
WORKDIR "/src/EasyFlex-api"
RUN dotnet build "EasyFlex-api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EasyFlex-api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EasyFlex-api.dll"]

#Note that this runs as a Http and NOT a Https

#Use ConnectionStrings__DefaultConnection=your-databaseconnection Environment variable to give it acces to a database