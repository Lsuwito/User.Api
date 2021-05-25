FROM amazonlinux:2 AS build-env

RUN rpm -Uvh https://packages.microsoft.com/config/centos/7/packages-microsoft-prod.rpm && \
    yum update && yum install -y dotnet-sdk-5.0

WORKDIR /app

COPY ./User.Api.csproj ./User.Api.csproj
  
RUN dotnet restore

COPY . .
RUN dotnet publish -c Release -o dist

FROM amazonlinux:2

ENV ASPNETCORE_URLS="http://*:5000"

RUN rpm -Uvh https://packages.microsoft.com/config/centos/7/packages-microsoft-prod.rpm && \
    yum update && yum install -y aspnetcore-runtime-5.0

WORKDIR /app

COPY --from=build-env /app/dist .

EXPOSE 5000
EXPOSE 5001

HEALTHCHECK --interval=5m --timeout=3s CMD curl -f http://localhost:5000/health || exit 1

ENTRYPOINT ["dotnet", "User.Api.dll"]
