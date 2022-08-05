FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ZombiBus/ZombiBus.csproj", "ZombiBus/"]
RUN dotnet restore "ZombiBus/ZombiBus.csproj"
COPY . .
WORKDIR "/src/ZombiBus"
RUN dotnet build "ZombiBus.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ZombiBus.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ZombiBus.dll"]
