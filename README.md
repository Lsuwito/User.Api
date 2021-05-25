# User Management API

Requirements:
1. .NET 5.0 SDK
2. Docker
3. dotnet-reportgenerator-globaltool for test coverage report

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
dotnet test --collect:"XPlat Code Coverage" tests/User.Api.Tests/User.Api.Tests.csproj;
```

Install report generator tool
```
dotnet tool install -g dotnet-reportgenerator-globaltool
```

Generate the code coverage report in Html format
```
reportgenerator "-reports:tests/User.Api.Tests/TestResults/*/coverage.cobertura.xml" "-targetdir:tests/User.Api.Tests/TestResults/CoverageReport" -reporttypes:Html;
```

## Build and Run using Make

```
make build
make run-dev
make image run-docker
make test
```


