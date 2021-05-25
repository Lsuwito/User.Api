PROJECT_NAME = User.Api
TEST_PROJECT_NAME = $(PROJECT_NAME).Tests

clean:
	dotnet clean; rm -rf ./tests/$(TEST_PROJECT_NAME)/TestResults

run-dev:
	dotnet run -p src/$(PROJECT_NAME)/$(PROJECT_NAME).csproj -c Debug

run:
	dotnet run -p src/$(PROJECT_NAME)/$(PROJECT_NAME).csproj -c Release

image:
	docker build --no-cache -t userapi -f src/${PROJECT_NAME}/Dockerfile src/$(PROJECT_NAME)/

run-docker:
	docker run -d -p 5000:5000 -p 5001:5001 --rm userapi

build:
	dotnet build -c Release

test:
	dotnet test --collect:"XPlat Code Coverage" tests/$(TEST_PROJECT_NAME)/$(TEST_PROJECT_NAME).csproj; \
	$$HOME/.dotnet/tools/reportgenerator "-reports:tests/$(TEST_PROJECT_NAME)/TestResults/*/coverage.cobertura.xml" "-targetdir:tests/User.Api.Tests/TestResults/CoverageReport" -reporttypes:Html;

install:
	dotnet tool install -g dotnet-reportgenerator-globaltool


