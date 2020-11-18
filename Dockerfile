#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src

#COPY ["ZappitBugTracker/ZappitBugTracker.csproj", "ZappitBugTracker/"]
COPY ["ZappitBugTracker.csproj", ""]

#RUN dotnet restore "ZappitBugTracker/ZappitBugTracker.csproj"
RUN dotnet restore "./ZappitBugTracker.csproj"

COPY . .
#WORKDIR "/src/ZappitBugTracker"
WORKDIR "/src/"

RUN dotnet build "ZappitBugTracker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ZappitBugTracker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "ZappitBugTracker.dll"]
CMD ASPNETCORE_URLS=http://*:$PORT dotnet ZappitBugTracker.dll
