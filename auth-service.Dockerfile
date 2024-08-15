FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
#EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS publish
WORKDIR /src

COPY AuthService/*.sln .
COPY AuthService/*.csproj .
RUN dotnet restore -v n /ignoreprojectextensions:.dcproj

COPY AuthService/. .
RUN dotnet publish AuthService.csproj -c debug -o /app

ARG API_KEY
ARG ADMIN_PASS
ARG ASPNETCORE_ENVIRONMENT

ENV API_KEY=$API_KEY
ENV ADMIN_PASS=$ADMIN_PASS
ENV ASPNETCORE_ENVIRONMENT=$ASPNETCORE_ENVIRONMENT

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "AuthService.dll"]