FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5141

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
COPY . /src
WORKDIR /src
RUN ls
RUN dotnet build "server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "server.dll"]