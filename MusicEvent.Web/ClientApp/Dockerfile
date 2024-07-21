### STAGE 1: Build ###
FROM node:lts AS build
WORKDIR /usr/src/app
COPY package.json package-lock.json ./
RUN npm install
COPY . .
ARG env
RUN npm run buildprod
### STAGE 2: Run ###
FROM nginx:1.24.0-alpine
ARG env
COPY /nginx-custom-prod.conf /etc/nginx/conf.d/default.conf
COPY --from=build /usr/src/app/dist /usr/share/nginx/html

# Copy the EntryPoint
COPY ./appEntryPoint.sh /
RUN chmod +x appEntryPoint.sh
ENTRYPOINT ["sh","/appEntryPoint.sh"]
CMD ["nginx", "-g", "daemon off;"]
