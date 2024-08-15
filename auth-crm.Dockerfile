FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
#EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS publish
WORKDIR /src

COPY AuthCRM/*.sln .
COPY AuthCRM/*.csproj .
RUN dotnet restore -v n /ignoreprojectextensions:.dcproj

COPY AuthCRM/. .
RUN dotnet publish AuthCRM.csproj -c debug -o /app

ARG ASPNETCORE_ENVIRONMENT

ENV ASPNETCORE_ENVIRONMENT=$ASPNETCORE_ENVIRONMENT

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "AuthCRM.dll"]