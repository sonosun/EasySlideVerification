FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base

##下面三行用于解决linux环境下，依赖包的问题
RUN ln -s /lib/x86_64-linux-gnu/libdl-2.24.so /lib/x86_64-linux-gnu/libdl.so
RUN apt-get update
RUN apt-get install libgdiplus -y && ln -s libgdiplus.so gdiplus.dll

WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["EasySlideVerificationDemoServer.csproj", "EasySlideVerificationDemoServer/"]
RUN dotnet restore "EasySlideVerificationDemoServer/EasySlideVerificationDemoServer.csproj"

WORKDIR "/src/EasySlideVerificationDemoServer"
COPY . .
RUN dotnet build "EasySlideVerificationDemoServer.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "EasySlideVerificationDemoServer.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "EasySlideVerificationDemoServer.dll"]