# Stage 1: Build (Using .NET 9 SDK)
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy all .csproj files first for faster caching
# Adjust these paths if your folders are named differently
COPY ["JobBoardAPI/API.csproj", "JobBoardAPI/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]

RUN dotnet restore "JobBoardAPI/API.csproj"

# Copy everything else and build
COPY . .
WORKDIR "/src/JobBoardAPI"
RUN dotnet build "API.csproj" -c Release -o /app/build

# Stage 2: Publish
FROM build AS publish
RUN dotnet publish "API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 3: Final Runtime (Using .NET 9 Runtime)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Railway/Render specific port configuration
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

# Make sure this matches your actual project output name
ENTRYPOINT ["dotnet", "API.dll"]