# Build Angular app
FROM node:18-alpine AS build
WORKDIR /app
COPY package*.json ./
RUN npm install
COPY . .
RUN npm run build -- --configuration=docker


# Serve with Nginx
FROM nginx:alpine
COPY --from=build /app/dist/shoes-store-web.client /usr/share/nginx/html
COPY nginx.conf /etc/nginx/nginx.conf
EXPOSE 80
