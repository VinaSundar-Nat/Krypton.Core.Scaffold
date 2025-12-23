FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
LABEL maintainer="vina"
WORKDIR /src

COPY Directory.Packages.props Kr.__PROJECT_NAME__.sln ./

COPY Kr.__PROJECT_NAME__.Domain/*.csproj ./Kr.__PROJECT_NAME__.Domain/
COPY Kr.__PROJECT_NAME__.Infrastructure/*.csproj ./Kr.__PROJECT_NAME__.Infrastructure/
COPY Kr.__PROJECT_NAME__.Persistence/*.csproj ./Kr.__PROJECT_NAME__.Persistence/
COPY Kr.__PROJECT_NAME__.Application/*.csproj ./Kr.__PROJECT_NAME__.Application/
COPY Kr.__PROJECT_NAME__.Api/*.csproj ./Kr.__PROJECT_NAME__.Api/
COPY Tests/Kr.__PROJECT_NAME__.UnitTests/*.csproj ./Tests/Kr.__PROJECT_NAME__.UnitTests/

RUN dotnet restore Kr.__PROJECT_NAME__.sln

COPY . .

WORKDIR /src/Kr.__PROJECT_NAME__.Api
RUN dotnet publish -c Release --no-restore -o /core

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /core
EXPOSE 8080

COPY --from=build /core .
ENTRYPOINT ["dotnet", "Kr.__PROJECT_NAME__.Api.dll"]