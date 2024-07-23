FROM mcr.microsoft.com/dotnet/sdk:8.0
WORKDIR /usr/src/AuthService

ARG API_KEY
ARG ADMIN_PASS
ARG URLS

ENV API_KEY=$API_KEY
ENV ADMIN_PASS=$ADMIN_PASS
ENV URLS=$URLS


COPY AuthService /usr/src/AuthService
EXPOSE 5062
ENTRYPOINT ["dotnet", "run"]