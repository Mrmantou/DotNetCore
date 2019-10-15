FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["Albert.Demo.Web/Albert.Demo.Web.csproj", "Albert.Demo.Web/"]
RUN dotnet restore "Albert.Demo.Web/Albert.Demo.Web.csproj"
COPY . .
WORKDIR "/src/Albert.Demo.Web"
RUN dotnet build "Albert.Demo.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Albert.Demo.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Albert.Demo.Web.dll"]