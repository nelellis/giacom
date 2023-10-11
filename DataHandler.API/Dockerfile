#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DataHandler.API/DataHandler.API.csproj", "DataHandler.API/"]
RUN dotnet restore "DataHandler.API/DataHandler.API.csproj"
COPY . .
WORKDIR "/src/DataHandler.API"
RUN dotnet build "DataHandler.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DataHandler.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DataHandler.API.dll"]