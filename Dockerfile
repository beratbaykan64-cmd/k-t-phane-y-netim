FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["KutuphaneProjesi/KutuphaneYonetim/KutuphaneYonetim.csproj", "KutuphaneProjesi/KutuphaneYonetim/"]
RUN dotnet restore "KutuphaneProjesi/KutuphaneYonetim/KutuphaneYonetim.csproj"

COPY . .
WORKDIR "/src/KutuphaneProjesi/KutuphaneYonetim"
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "KutuphaneYonetim.dll"]