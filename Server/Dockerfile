FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["API/API.csproj", "API/"]
COPY ["ApplicationData/ApplicationData.csproj", "ApplicationData/"]
COPY ["Business/Business.csproj", "Business/"]
COPY ["Services/Services.csproj", "Services/"]

RUN dotnet restore "API/API.csproj"

COPY . .

WORKDIR "/src/API"
RUN dotnet publish "API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "API.dll"]