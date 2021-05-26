FROM amazonlinux:2 AS build-env

RUN rpm -Uvh https://packages.microsoft.com/config/centos/7/packages-microsoft-prod.rpm && \
    yum update && yum install -y dotnet-sdk-5.0

WORKDIR /app

COPY ./tests/User.Api.Integration.Tests/User.Api.Integration.Tests.csproj ./User.Api.Integration.Tests.csproj
  
RUN dotnet restore

COPY ./tests/User.Api.Integration.Tests/ .

RUN dotnet publish -c Release -o dist

ENTRYPOINT ["dotnet", "test", "dist/User.Api.Integration.Tests.dll", "-l", "html"]
