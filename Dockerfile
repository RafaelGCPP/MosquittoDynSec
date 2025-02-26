FROM mcr.microsoft.com/dotnet/sdk:9.0.2-noble AS aspnetbuild

RUN apt update
RUN apt upgrade -y
RUN apt clean

WORKDIR /build
COPY DynSec.API/ DynSec.API/
COPY DynSec.GraphQL/ DynSec.GraphQL/
COPY DynSec.Model/ DynSec.Model/
COPY DynSec.MQTT/ DynSec.MQTT/
COPY DynSec.Protocol/ DynSec.Protocol/
COPY DynSec.Web/ DynSec.Web/

COPY DynSec.sln .

RUN dotnet publish -c Release -o /app


FROM node:lts-alpine AS nodebuild

WORKDIR /build
COPY DynSec.Web/ .

RUN npm ci
RUN npm run build # --prod


FROM mcr.microsoft.com/dotnet/aspnet:9.0-noble-chiseled AS runtime

COPY --from=aspnetbuild /app /app
COPY --from=nodebuild /build/dist/dyn-sec.web/browser /app/wwwroot
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "DynSec.API.dll"]


