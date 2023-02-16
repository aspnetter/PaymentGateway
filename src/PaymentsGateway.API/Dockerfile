FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["PaymentsGateway.API/PaymentsGateway.API.csproj", "PaymentsGateway.API/"]
RUN dotnet restore "PaymentsGateway.API/PaymentsGateway.API.csproj"
COPY . .
WORKDIR "/src/PaymentsGateway.API"
RUN dotnet build "PaymentsGateway.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PaymentsGateway.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PaymentsGateway.API.dll"]
