FROM node:22-alpine AS build

COPY . .

RUN npm install
RUN npm run build


FROM nginx:1.27.4-alpine

RUN apk add --no-cache python3

WORKDIR /usr/share/nginx/html

# Remove default nginx static assets
RUN rm -rf ./*

# Copy static assets from builder stage
COPY --from=build /build .

COPY start.py /start.py

CMD python3 /start.py /usr/share/nginx/html/env.js && nginx -g "daemon off;"