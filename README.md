# User Management API

Requirements:
1. .NET 5.0 SDK
2. Docker
3. dotnet-reportgenerator-globaltool for test coverage report
4. PostgreSQL

## Building the API

```
dotnet build -c Debug
```

## Running the API

Run the api as a Docker container
```
docker build -t userapi -f src/User.Api/Dockerfile src/User.Api/
docker run -d -p 5000:5000 -p 5001:5001 --rm userapi
```

Or run the api directly
```
dotnet run -p src/User.Api/User.Api.csproj -c Debug
```

## Running Unit Tests

Run the unit tests and collect the code coverage data
```
dotnet test --collect:"XPlat Code Coverage" tests/User.Api.Tests/User.Api.Unit.Tests.csproj;
```

Install report generator tool
```
dotnet tool install -g dotnet-reportgenerator-globaltool
```

Generate the code coverage report in Html format
```
reportgenerator "-reports:tests/User.Api.Tests/TestResults/*/coverage.cobertura.xml" "-targetdir:tests/User.Api.Tests/TestResults/CoverageReport" -reporttypes:Html;
```

## Integration Tests

Run the API and database as Docker containers
```
docker-compose up -d
```

Run the integration tests
```
dotnet test tests\User.Api.Tests\User.Api.Integration.Tests.csproj -l html
```

Or run the integration tests in docker container
```
docker build -t user_api_integration_tests -f tests\User.Api.Integration.Tests\Dockerfile .
docker run --rm -v /your/testresult/path/:/app/TestResults --networks userapi_elk user_api_integration_tests
```

## Build and Run using Make

```
make build
make run-dev
make image run-docker
make test
```

## Operation Flow

UsersController -> UserService -> UserRepository -> DataAccess

## DataAccess Implementation

This API uses PostgreSQL database. The data access implementation uses Dapper as the ORM and Npgsql for database access to PostgreSQL. 

## Database Setup

You can use docker-compose to provision and initialize a PostgreSQL database for testing. The database scripts can be found in the db folder. It is mounted as the docker-entrypoint-initdb.d volume of the PostgreSQL container. 

