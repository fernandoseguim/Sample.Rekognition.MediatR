FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["src/Sample.Rekognition.MediatR/Sample.Rekognition.MediatR.csproj", "src/Sample.Rekognition.MediatR/"]
COPY ["src/Sample.Rekognition.MediatR.Rules/Sample.Rekognition.MediatR.Rules.csproj", "src/Sample.Rekognition.MediatR.Rules/"]
RUN dotnet restore "src/Sample.Rekognition.MediatR/Sample.Rekognition.MediatR.csproj"
COPY . .
WORKDIR "/src/src/Sample.Rekognition.MediatR"
RUN dotnet build "Sample.Rekognition.MediatR.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Sample.Rekognition.MediatR.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Sample.Rekognition.MediatR.dll"]
