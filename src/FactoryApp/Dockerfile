# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /source

# Install NodeJs
RUN curl -fsSL https://deb.nodesource.com/setup_16.x | bash -
RUN apt-get install -y nodejs

# copy csproj file and restore
COPY ./*.csproj .
RUN dotnet restore

# copy everything else and build app
COPY . .
RUN dotnet publish -c release -o /app --no-restore --nologo

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build /app ./

# Copy wait-for-it.sh to wait until database is ready
COPY wait-for-it.sh wait-for-it.sh
RUN chmod +x wait-for-it.sh

# Entrypoint
# ENTRYPOINT ["dotnet", "FactoryApp.dll"]