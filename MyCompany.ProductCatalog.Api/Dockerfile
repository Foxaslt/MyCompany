#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM microsoft/dotnet:2.1-aspnetcore-runtime-nanoserver-1803 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk-nanoserver-1803 AS build
WORKDIR /src
COPY ["MyCompany.ProductCatalog.Api/MyCompany.ProductCatalog.Api.csproj", "MyCompany.ProductCatalog.Api/"]
RUN dotnet restore "MyCompany.ProductCatalog.Api/MyCompany.ProductCatalog.Api.csproj"
COPY . .
WORKDIR "/src/MyCompany.ProductCatalog.Api"
RUN dotnet build "MyCompany.ProductCatalog.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "MyCompany.ProductCatalog.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MyCompany.ProductCatalog.Api.dll"]