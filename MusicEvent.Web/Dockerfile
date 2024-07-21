#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["MusicEvent.Web/MusicEvent.Web.csproj", "MusicEvent.Web/"]
COPY ["MusicEvent.Application/MusicEvent.Application.csproj", "MusicEvent.Application/"]
COPY ["MusicEvent.Core/MusicEvent.Core.csproj", "MusicEvent.Core/"]
COPY ["MusicEvent.Domain/MusicEvent.Domain.csproj", "MusicEvent.Domain/"]
COPY ["MusicEvent.Infra.IoC/MusicEvent.Infra.IoC.csproj", "MusicEvent.Infra.IoC/"]
COPY ["MusicEvent.Infra.Bus/MusicEvent.Infra.Bus.csproj", "MusicEvent.Infra.Bus/"]
COPY ["MusicEvent.Infra.Data/MusicEvent.Infra.Data.csproj", "MusicEvent.Infra.Data/"]
RUN dotnet restore "MusicEvent.Web/MusicEvent.Web.csproj"
COPY . .
WORKDIR "/src/MusicEvent.Web"
RUN dotnet build "MusicEvent.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MusicEvent.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MusicEvent.Web.dll"]