#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Backend/PlaceEvalution/PlaceEvalution.API.csproj", "Backend/PlaceEvalution/"]
COPY ["Common/BaseRepository/BaseRepository.csproj", "Common/BaseRepository/"]
RUN dotnet restore "Backend/PlaceEvalution/PlaceEvalution.API.csproj"
COPY . .
WORKDIR "/src/Backend/PlaceEvalution"
RUN dotnet build "PlaceEvalution.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PlaceEvalution.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PlaceEvalution.API.dll"]