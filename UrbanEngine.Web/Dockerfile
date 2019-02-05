#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 61951
EXPOSE 44397

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["UrbanEngine.Web/UrbanEngine.Web.csproj", "UrbanEngine.Web/"]
COPY ["UrbanEngine.Core/UrbanEngine.Core.csproj", "UrbanEngine.Core/"]
COPY ["UrbanEngine.Infrastructure/UrbanEngine.Infrastructure.csproj", "UrbanEngine.Infrastructure/"]
RUN dotnet restore "UrbanEngine.Web/UrbanEngine.Web.csproj"
COPY . .
WORKDIR "/src/UrbanEngine.Web"
RUN dotnet build "UrbanEngine.Web.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "UrbanEngine.Web.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "UrbanEngine.Web.dll"]