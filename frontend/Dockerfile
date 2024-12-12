FROM node:18-alpine AS build

WORKDIR /app

COPY package.json package-lock.json ./

RUN npm install

COPY . .

EXPOSE 3000

# Inicia a aplicação
# Apenas para desenvolvimento
CMD ["npm", "run", "dev"]
