FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY DesafioTecnico.Api/*.csproj ./DesafioTecnico.Api/
RUN dotnet restore ./DesafioTecnico.Api/DesafioTecnico.Api.csproj

COPY DesafioTecnico.Api/. ./DesafioTecnico.Api/
WORKDIR /app/DesafioTecnico.Api
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/DesafioTecnico.Api/out ./
ENV ASPNETCORE_URLS=http://+:5000
EXPOSE 5000
ENTRYPOINT ["dotnet", "DesafioTecnico.Api.dll"]
