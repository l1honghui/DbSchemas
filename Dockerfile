FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /app
COPY . .
WORKDIR /app/src/Synyi.Emr.DbSchemas.Service
RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM microsoft/dotnet:2.1-aspnetcore-runtime AS runtime
WORKDIR /app
COPY --from=build /app/src/Synyi.Emr.DbSchemas.Service/out ./

# Optional: Set this here if not setting it from docker-compose.yml
# ENV ASPNETCORE_ENVIRONMENT Development

ENTRYPOINT ["dotnet", "Synyi.Emr.DbSchemas.Service.dll"]




