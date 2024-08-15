#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:8.0-nanoserver-1809 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0-nanoserver-1809 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["AhCha.Fortunate.Api/AhCha.Fortunate.Api.csproj", "AhCha.Fortunate.Api/"]
COPY ["AhCha.Fortunate.ModelsDto/AhCha.Fortunate.ModelsDto.csproj", "AhCha.Fortunate.ModelsDto/"]
COPY ["AhCha.Fortunate.Repositories/AhCha.Fortunate.Repositories.csproj", "AhCha.Fortunate.Repositories/"]
COPY ["AhCha.Fortunate.Common/AhCha.Fortunate.Common.csproj", "AhCha.Fortunate.Common/"]
COPY ["AhCha.Fortunate.Entity/AhCha.Fortunate.Entity.csproj", "AhCha.Fortunate.Entity/"]
COPY ["AhCha.Fortunate.Service/AhCha.Fortunate.Service.csproj", "AhCha.Fortunate.Service/"]
COPY ["AhCha.Fortunate.IService/AhCha.Fortunate.IService.csproj", "AhCha.Fortunate.IService/"]
RUN dotnet restore "./AhCha.Fortunate.Api/AhCha.Fortunate.Api.csproj"
COPY . .
WORKDIR "/src/AhCha.Fortunate.Api"
RUN dotnet build "./AhCha.Fortunate.Api.csproj" -c %BUILD_CONFIGURATION% -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./AhCha.Fortunate.Api.csproj" -c %BUILD_CONFIGURATION% -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AhCha.Fortunate.Api.dll"]