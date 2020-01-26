#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM microsoft/dotnet:2.1-aspnetcore-runtime-nanoserver-1709 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk-nanoserver-1709 AS build
WORKDIR /src
COPY ["CreditConfirmation.Web.API/CreditConfirmation.Web.API.csproj", "CreditConfirmation.Web.API/"]
RUN dotnet restore "CreditConfirmation.Web.API/CreditConfirmation.Web.API.csproj"
COPY . .
WORKDIR "/src/CreditConfirmation.Web.API"
RUN dotnet build "CreditConfirmation.Web.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "CreditConfirmation.Web.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "CreditConfirmation.Web.API.dll"]