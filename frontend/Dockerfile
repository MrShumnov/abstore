FROM node:18.4.0-alpine

COPY package.json /app/package.json
COPY package-lock.json /app/package-lock.json

WORKDIR /app

RUN npm install --production

ARG REACT_APP_BACKEND_SERVER
ENV REACT_APP_BACKEND_SERVER=$REACT_APP_BACKEND_SERVER
# ENV WDS_SOCKET_PORT=3000

COPY . ./

EXPOSE 3000

CMD ["npm", "start"]