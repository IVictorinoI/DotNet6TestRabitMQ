#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "DotNet6TestRabitMQ.Api/DotNet6TestRabitMQ.Api.csproj"
COPY . .
WORKDIR "/src/DotNet6TestRabitMQ.Api"
RUN dotnet build "DotNet6TestRabitMQ.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DotNet6TestRabitMQ.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DotNet6TestRabitMQ.Api.dll"]