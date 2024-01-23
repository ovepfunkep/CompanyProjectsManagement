FROM node:20-alpine as build-stage

WORKDIR /app
COPY WebApp/webapp.client/package*.json .
RUN npm install

COPY WebApp/webapp.client/ .
RUN npm run build

FROM nginx:stable-alpine as production-stage

COPY --from=build-stage /app/dist /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]