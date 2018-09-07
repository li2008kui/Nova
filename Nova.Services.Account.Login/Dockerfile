FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["Nova.Services.Account.Login/Nova.Services.Account.Login.csproj", "Nova.Services.Account.Login/"]
RUN dotnet restore "Nova.Services.Account.Login/Nova.Services.Account.Login.csproj"
COPY . .
WORKDIR "/src/Nova.Services.Account.Login"
RUN dotnet build "Nova.Services.Account.Login.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Nova.Services.Account.Login.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Nova.Services.Account.Login.dll"]