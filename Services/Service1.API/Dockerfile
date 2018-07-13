FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY Services/Service1.API/Service1.API.csproj Services/Service1.API/
RUN dotnet restore Services/Service1.API/Service1.API.csproj
COPY . .
WORKDIR /src/Services/Service1.API
RUN dotnet build Service1.API.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Service1.API.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Service1.API.dll"]
