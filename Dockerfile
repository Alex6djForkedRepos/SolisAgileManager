﻿FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app


FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:9.0 AS publish
WORKDIR /src
COPY . .
WORKDIR "/src"
ARG TARGETARCH
ARG VERSION
RUN dotnet publish SolisManager/SolisManager.csproj -a $TARGETARCH -c Release --self-contained true -p:PublishTrimmed=false --property:PublishDir=/app/publish /p:Version=$VERSION
RUN mkdir /app/core && \
    mv -v /app/publish/Microsoft.*.dll /app/core && \
    mv -v /app/publish/System.*.dll /app/core

FROM base AS final
WORKDIR /app
COPY --from=publish /app/core .
COPY --from=publish /app/publish .

RUN chmod +x /app/SolisManager

ENTRYPOINT ["/app/SolisManager", "/appdata"]
