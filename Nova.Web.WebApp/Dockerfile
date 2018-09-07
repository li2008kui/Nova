FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["Nova.Web.WebApp/Nova.Web.WebApp.csproj", "Nova.Web.WebApp/"]
RUN dotnet restore "Nova.Web.WebApp/Nova.Web.WebApp.csproj"
COPY . .
WORKDIR "/src/Nova.Web.WebApp"
RUN dotnet build "Nova.Web.WebApp.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Nova.Web.WebApp.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Nova.Web.WebApp.dll"]