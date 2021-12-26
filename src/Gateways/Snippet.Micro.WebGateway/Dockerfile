#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Gateways/Snippet.Micro.WebGateway/Snippet.Micro.WebGateway.csproj", "Gateways/Snippet.Micro.WebGateway/"]
COPY ["Infrastructures/Snippet.Micro.Consul/Snippet.Micro.Consul.csproj", "Infrastructures/Snippet.Micro.Consul/"]
COPY ["Infrastructures/Snippet.Micro.Yarp/Snippet.Micro.Yarp.csproj", "Infrastructures/Snippet.Micro.Yarp/"]
RUN dotnet restore "Gateways/Snippet.Micro.WebGateway/Snippet.Micro.WebGateway.csproj"
COPY . .
WORKDIR "/src/Gateways/Snippet.Micro.WebGateway"
RUN dotnet build "Snippet.Micro.WebGateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Snippet.Micro.WebGateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Snippet.Micro.WebGateway.dll"]