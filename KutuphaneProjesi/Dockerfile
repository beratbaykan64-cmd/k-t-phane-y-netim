# 1. Aşama: Build (İnşa Etme)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Proje dosyasını kopyala
COPY *.csproj ./
# Kütüphaneleri indir
RUN dotnet restore

# Tüm dosyaları kopyala ve yayınla
COPY . ./
RUN dotnet publish -c Release -o out

# 2. Aşama: Run (Çalıştırma)
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

# Uygulamayı başlat
ENTRYPOINT ["dotnet", "KutuphaneYonetim.dll"]