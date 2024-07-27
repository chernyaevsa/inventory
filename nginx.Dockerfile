FROM nginx:stable-alpine
ARG DOMAIN
ENV DOMAIN=${DOMAIN}
COPY nginx/nginx.conf /etc/nginx/nginx.conf.template
CMD ["/bin/sh" , "-c" , "envsubst '$DOMAIN' < /etc/nginx/nginx.conf.template > /etc/nginx/nginx.conf && exec nginx -g 'daemon off;'"]
EXPOSE 80