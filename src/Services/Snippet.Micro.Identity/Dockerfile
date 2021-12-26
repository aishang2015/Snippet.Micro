#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Snippet.Micro.Identity/Snippet.Micro.Identity.csproj", "Services/Snippet.Micro.Identity/"]
COPY ["Infrastructures/Snippet.Micro.Consul/Snippet.Micro.Consul.csproj", "Infrastructures/Snippet.Micro.Consul/"]
RUN dotnet restore "Services/Snippet.Micro.Identity/Snippet.Micro.Identity.csproj"
COPY . .
WORKDIR "/src/Services/Snippet.Micro.Identity"
RUN dotnet build "Snippet.Micro.Identity.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Snippet.Micro.Identity.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Snippet.Micro.Identity.dll"]