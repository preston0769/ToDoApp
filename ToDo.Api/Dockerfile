FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["ToDo.Api/ToDo.Api.csproj", "ToDo.Api/"]
RUN dotnet restore "ToDo.Api/ToDo.Api.csproj"
COPY . .
WORKDIR "/src/ToDo.Api"
RUN dotnet build "ToDo.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ToDo.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ToDo.Api.dll"]