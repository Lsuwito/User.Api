FROM amazonlinux:2 AS build-env
RUN yum update && yum install -y dotnet-sdk-5.0
WORKDIR /app

COPY ./User.Api.csproj ./User.Api.csproj
  
RUN dotnet restore

COPY ./app .
RUN dotnet publish -c Release -o dist

FROM amazonlinux:2
RUN yum update && yum install -y aspnetcore-runtime-5.0
WORKDIR /app
COPY --from=build-env /app/dist .

EXPOSE 5001

ENTRYPOINT ["dotnet", "User.Api.dll"]