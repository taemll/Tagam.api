#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Tagam.FavoriteApi/Tagam.FavoriteApi.csproj", "Tagam.FavoriteApi/"]
COPY ["JwtAuthenticationManager/JwtAuthenticationManager.csproj", "JwtAuthenticationManager/"]
RUN dotnet restore "Tagam.FavoriteApi/Tagam.FavoriteApi.csproj"
COPY . .
WORKDIR "/src/Tagam.FavoriteApi"
RUN dotnet build "Tagam.FavoriteApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Tagam.FavoriteApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tagam.FavoriteApi.dll"]