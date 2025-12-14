# escape=`

# Stage 1: Build
FROM mcr.microsoft.com/dotnet/framework/sdk:4.8 AS build
WORKDIR /app

# Copy solution and project files
COPY *.sln .
COPY packages.config .
COPY *.csproj .

# Restore NuGet packages
RUN nuget restore

# Copy the rest of the source code
COPY . .

# Build the application
RUN msbuild /p:Configuration=Release /p:OutputPath=C:\app\publish

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/framework/aspnet:4.8-windowsservercore-ltsc2019 AS runtime
WORKDIR /inetpub/wwwroot

# Copy the published application
COPY --from=build C:\app\publish\_PublishedWebsites\Gemini .

# Copy the entrypoint script
COPY --from=build /app/entrypoint.ps1 .

# Expose port 80
EXPOSE 80

# Set the entrypoint
ENTRYPOINT ["powershell", ".\\entrypoint.ps1"]
