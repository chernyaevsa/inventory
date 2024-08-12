FROM mysql:8.0.38

RUN chown -R mysql:root /var/lib/mysql/

ARG MYSQL_DATABASE
ARG MYSQL_USER
ARG MYSQL_PASSWORD
ARG MYSQL_ROOT_PASSWORD

ENV MYSQL_DATABASE=$MYSQL_DATABASE
ENV MYSQL_USER=$MYSQL_USER
ENV MYSQL_PASSWORD=$MYSQL_PASSWORD
ENV MYSQL_ROOT_PASSWORD=$MYSQL_ROOT_PASSWORD

ADD service-db/service_db.sql /etc/mysql/service_db.sql

RUN sed -i 's/MYSQL_DATABASE/'$AUTH_DB_DATABASE'/g' /etc/mysql/service_db.sql
RUN cp /etc/mysql/service_db.sql /docker-entrypoint-initdb.d

EXPOSE 3307