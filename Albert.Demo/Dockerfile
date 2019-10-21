FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln .
COPY . /app
# copy everything else and build app

WORKDIR /app/Albert.Demo.Web
RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.0 AS runtime
WORKDIR /app
COPY --from=build /app/Albert.Demo.Web/out ./
ENTRYPOINT ["dotnet", "Albert.Demo.Web.dll"]