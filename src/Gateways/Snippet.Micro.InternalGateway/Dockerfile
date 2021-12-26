#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Gateways/Snippet.Micro.InternalGateway/Snippet.Micro.InternalGateway.csproj", "Gateways/Snippet.Micro.InternalGateway/"]
COPY ["Infrastructures/Snippet.Micro.Consul/Snippet.Micro.Consul.csproj", "Infrastructures/Snippet.Micro.Consul/"]
COPY ["Infrastructures/Snippet.Micro.Yarp/Snippet.Micro.Yarp.csproj", "Infrastructures/Snippet.Micro.Yarp/"]
COPY ["Services/Snippet.Micro.TestService/Snippet.Micro.TestService.csproj", "Services/Snippet.Micro.TestService/"]
COPY ["Infrastructures/Snippet.Micro.Serilog/Snippet.Micro.Serilog.csproj", "Infrastructures/Snippet.Micro.Serilog/"]
COPY ["Infrastructures/Snippet.Micro.MassTransit/Snippet.Micro.MassTransit.csproj", "Infrastructures/Snippet.Micro.MassTransit/"]
COPY ["Services/Snippet.Micro.Identity/Snippet.Micro.Identity.csproj", "Services/Snippet.Micro.Identity/"]
RUN dotnet restore "Gateways/Snippet.Micro.InternalGateway/Snippet.Micro.InternalGateway.csproj"
COPY . .
WORKDIR "/src/Gateways/Snippet.Micro.InternalGateway"
RUN dotnet build "Snippet.Micro.InternalGateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Snippet.Micro.InternalGateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Snippet.Micro.InternalGateway.dll"]