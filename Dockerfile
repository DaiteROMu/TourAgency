FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["TourAgency/TourAgency.Web/TourAgency.Web.csproj", "TourAgency/TourAgency.Web/"]
RUN dotnet restore "TourAgency/TourAgency.Web/TourAgency.Web.csproj"
COPY . .
WORKDIR "/src/TourAgency/TourAgency.Web"
RUN dotnet build "TourAgency.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TourAgency.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TourAgency.Web.dll"]
