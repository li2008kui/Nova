FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["Nova.Services.Ids4/Nova.Services.Ids4.csproj", "Nova.Services.Ids4/"]
RUN dotnet restore "Nova.Services.Ids4/Nova.Services.Ids4.csproj"
COPY . .
WORKDIR "/src/Nova.Services.Ids4"
RUN dotnet build "Nova.Services.Ids4.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Nova.Services.Ids4.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Nova.Services.Ids4.dll"]