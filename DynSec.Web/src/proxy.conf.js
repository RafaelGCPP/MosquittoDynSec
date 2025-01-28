const { env } = require('process');

env.ASPNETCORE_HTTPS_PORT = "7044";

const target = env.ASPNETCORE_HTTPS_PORT ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}` :
    env.ASPNETCORE_URLS ? env.ASPNETCORE_URLS.split(';')[0] : 'https://localhost:7012';

console.log(`target: ${target}`);

const PROXY_CONFIG = [
  {
    context: [
      "/graphql",
      "/scalar"
    ],
    target,
    secure: false
  }
]

module.exports = PROXY_CONFIG;
