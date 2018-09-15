FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY Nova.Services.Ticket.API/Nova.Services.Ticket.API.csproj Nova.Services.Ticket.API/
COPY Nova.Utilities.Consul/Nova.Utilities.Consul.csproj Nova.Utilities.Consul/
RUN dotnet restore Nova.Services.Ticket.API/Nova.Services.Ticket.API.csproj
COPY . .
WORKDIR /src/Nova.Services.Ticket.API
RUN dotnet build Nova.Services.Ticket.API.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Nova.Services.Ticket.API.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Nova.Services.Ticket.API.dll"]
