FROM mcr.microsoft.com/dotnet/sdk:8.0
WORKDIR /usr/src/auth-service
COPY auth-service /usr/src/auth-service
EXPOSE 5090
ENTRYPOINT ["dotnet", "run"]