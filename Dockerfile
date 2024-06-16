FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

COPY src/Base/Base.API/*.csproj ./src/Base/Base.API/
RUN dotnet restore ./src/Base/Base.API/Base.API.csproj

COPY src/Base/Base.Domain/*.csproj ./src/Base/Base.Domain/
RUN dotnet restore ./src/Base/Base.Domain/Base.Domain.csproj

COPY src/Base/Base.Infrastructure/*.csproj ./src/Base/Base.Infrastructure/
RUN dotnet restore ./src/Base/Base.Infrastructure/Base.Infrastructure.csproj

COPY src/Shared/Shared.Lib/*.csproj ./src/Shared/Shared.Lib/
RUN dotnet restore ./src/Shared/Shared.Lib/Shared.Lib.csproj

COPY . .

RUN dotnet build -c Release -o /app/build src/Base/Base.API/Base.API.csproj
# RUN dotnet publish -c Release -o /app/build src/Base/Base.API/Base.API.csproj

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

WORKDIR /app
RUN apt-get update && apt-get install -y iputils-ping
RUN curl -sSL https://aka.ms/getvsdbgsh | bash /dev/stdin -v latest -l /remote_debugger

EXPOSE 3510

COPY --from=build /app/build .
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://+:3510

ENTRYPOINT ["dotnet", "Base.API.dll"]
