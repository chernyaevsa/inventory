FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
#EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS publish
WORKDIR /src

COPY Service/*.sln .
COPY Service/*.csproj .
RUN dotnet restore -v n /ignoreprojectextensions:.dcproj

COPY Service/. .
RUN dotnet publish Service.csproj -c debug -o /app

ARG API_KEY
ARG ASPNETCORE_ENVIRONMENT

ENV API_KEY=$API_KEY
ENV ASPNETCORE_ENVIRONMENT=$ASPNETCORE_ENVIRONMENT

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Service.dll"]