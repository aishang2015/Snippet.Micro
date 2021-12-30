#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Snippet.Micro.TestService/Snippet.Micro.TestService.csproj", "Services/Snippet.Micro.TestService/"]
COPY ["Infrastructures/Snippet.Micro.Consul/Snippet.Micro.Consul.csproj", "Infrastructures/Snippet.Micro.Consul/"]
COPY ["Infrastructures/Snippet.Micro.Serilog/Snippet.Micro.Serilog.csproj", "Infrastructures/Snippet.Micro.Serilog/"]
COPY ["Infrastructures/Snippet.Micro.MassTransit/Snippet.Micro.MassTransit.csproj", "Infrastructures/Snippet.Micro.MassTransit/"]
RUN dotnet restore "Services/Snippet.Micro.TestService/Snippet.Micro.TestService.csproj"
COPY . .
WORKDIR "/src/Services/Snippet.Micro.TestService"
RUN dotnet build "Snippet.Micro.TestService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Snippet.Micro.TestService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Snippet.Micro.TestService.dll"]