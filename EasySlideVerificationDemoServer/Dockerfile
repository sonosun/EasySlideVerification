FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["EasySlideVerificationDemoServer/EasySlideVerificationDemoServer.csproj", "EasySlideVerificationDemoServer/"]
RUN dotnet restore "EasySlideVerificationDemoServer/EasySlideVerificationDemoServer.csproj"
COPY . .
WORKDIR "/src/EasySlideVerificationDemoServer"
RUN dotnet build "EasySlideVerificationDemoServer.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "EasySlideVerificationDemoServer.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "EasySlideVerificationDemoServer.dll"]