FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["Nova.ApiGateways.Ocelot/Nova.ApiGateways.Ocelot.csproj", "Nova.ApiGateways.Ocelot/"]
RUN dotnet restore "Nova.ApiGateways.Ocelot/Nova.ApiGateways.Ocelot.csproj"
COPY . .
WORKDIR "/src/Nova.ApiGateways.Ocelot"
RUN dotnet build "Nova.ApiGateways.Ocelot.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Nova.ApiGateways.Ocelot.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Nova.ApiGateways.Ocelot.dll"]