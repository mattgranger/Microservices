FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY Services/MongoService/MongoService.csproj Services/MongoService/
RUN dotnet restore Services/MongoService/MongoService.csproj
COPY . .
WORKDIR /src/Services/MongoService
RUN dotnet build MongoService.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish MongoService.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MongoService.dll"]
