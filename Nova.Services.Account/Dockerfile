FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY Nova.Services.Account/Nova.Services.Account.csproj Nova.Services.Account/
COPY Nova.Utilities.Consul/Nova.Utilities.Consul.csproj Nova.Utilities.Consul/
RUN dotnet restore Nova.Services.Account/Nova.Services.Account.csproj
COPY . .
WORKDIR /src/Nova.Services.Account
RUN dotnet build Nova.Services.Account.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Nova.Services.Account.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Nova.Services.Account.dll"]
