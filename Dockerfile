FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["trantr/trantr.csproj", "trantr/"]
RUN dotnet restore "trantr/trantr.csproj"
COPY . .
WORKDIR "/src/trantr"
RUN dotnet build "trantr.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "trantr.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "trantr.dll"]
