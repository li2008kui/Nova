FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["Nova.Services.Account.Register/Nova.Services.Account.Register.csproj", "Nova.Services.Account.Register/"]
RUN dotnet restore "Nova.Services.Account.Register/Nova.Services.Account.Register.csproj"
COPY . .
WORKDIR "/src/Nova.Services.Account.Register"
RUN dotnet build "Nova.Services.Account.Register.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Nova.Services.Account.Register.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Nova.Services.Account.Register.dll"]