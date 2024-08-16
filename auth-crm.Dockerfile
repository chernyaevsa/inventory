FROM instrumentisto/flutter:3.24
RUN apt-get update && \
    apt-get -y install nginx
COPY auth-crm /src
WORKDIR /src
RUN flutter build web
RUN rm -Rf /var/www/html
RUN ln -s /src/build/web /var/www/html
ENTRYPOINT [ "/bin/sh" , "-c", "exec nginx -g 'daemon off;'" ]

