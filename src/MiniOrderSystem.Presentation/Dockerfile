# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
# For more information, please see https://aka.ms/containercompat

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/MiniOrderSystem.Presentation/MiniOrderSystem.Presentation.csproj", "src/MiniOrderSystem.Presentation/"]
COPY ["src/core/MiniOrderSystem.Application/MiniOrderSystem.Application.csproj", "src/core/MiniOrderSystem.Application/"]
COPY ["src/core/MiniOrderSystem.Domain/MiniOrderSystem.Domain.csproj", "src/core/MiniOrderSystem.Domain/"]
COPY ["src/MiniOrderSystem.Infrastructure/MiniOrderSystem.Infrastructure.csproj", "src/MiniOrderSystem.Infrastructure/"]
COPY ["src/MiniOrderSystem.Shared/MiniOrderSystem.Shared.csproj", "src/MiniOrderSystem.Shared/"]
RUN dotnet restore "./src/MiniOrderSystem.Presentation/MiniOrderSystem.Presentation.csproj"
COPY . .
WORKDIR "/src/src/MiniOrderSystem.Presentation"
RUN dotnet build "./MiniOrderSystem.Presentation.csproj" -c %BUILD_CONFIGURATION% -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MiniOrderSystem.Presentation.csproj" -c %BUILD_CONFIGURATION% -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MiniOrderSystem.Presentation.dll"]