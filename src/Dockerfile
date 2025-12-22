FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
LABEL maintainer="vina"
WORKDIR /src
EXPOSE 8080

COPY ./Directory.Packages.props .


COPY ./src/ ./src/
RUN dotnet restore src/Kr.__PROJECT_NAME__.Domain/*.csproj &&\
    dotnet restore src/Kr.__PROJECT_NAME__.Infrastructure/*.csproj &&\
    dotnet restore src/Kr.__PROJECT_NAME__.Persistence/*.csproj &&\
    dotnet restore src/Kr.__PROJECT_NAME__.Application/*.csproj &&\ 
    dotnet restore src/Kr.__PROJECT_NAME__.Adapter/*.csproj &&\ 
    dotnet restore src/Kr.__PROJECT_NAME__.Api/*.csproj


WORKDIR /src/Kr.__PROJECT_NAME__.Api
RUN dotnet publish Kr.__PROJECT_NAME__.Api.csproj -c Release --no-restore -o /core

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /core

COPY --from=build /core .
ENTRYPOINT ["dotnet", "Kr.__PROJECT_NAME__.Api.dll"]