# См. статью по ссылке https://aka.ms/customizecontainer, чтобы узнать как настроить контейнер отладки и как Visual Studio использует этот Dockerfile для создания образов для ускорения отладки.

# Этот этап используется при запуске из VS в быстром режиме (по умолчанию для конфигурации отладки)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
ENV SMS_API_KEY = "yourKey"
ENV AUTHENTIFICATION_KEY = "keyForAuthToAPI"
ENV SMS_BASE_ADDRESS "https://sms.ru/"
ENV CERTIFICATE_NAME = "Test Certificate"
EXPOSE 8080
EXPOSE 8081


# Этот этап используется для сборки проекта службы
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MessageSenderService/MessageSenderService.csproj", "MessageSenderService/"]
RUN dotnet restore "./MessageSenderService/MessageSenderService.csproj"
COPY . .
WORKDIR "/src/MessageSenderService"
ENV SMS_API_KEY = "yourKey"
ENV AUTHENTIFICATION_KEY = "keyForAuthToAPI"
ENV SMS_BASE_ADDRESS "https://sms.ru/"
ENV CERTIFICATE_NAME = "Test Certificate"
RUN dotnet build "./MessageSenderService.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Этот этап используется для публикации проекта службы, который будет скопирован на последний этап
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
ENV SMS_API_KEY = "yourKey"
ENV AUTHENTIFICATION_KEY = "keyForAuthToAPI"
ENV SMS_BASE_ADDRESS "https://sms.ru/"
ENV CERTIFICATE_NAME = "Test Certificate"
RUN dotnet publish "./MessageSenderService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Этот этап используется в рабочей среде или при запуске из VS в обычном режиме (по умолчанию, когда конфигурация отладки не используется)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV SMS_API_KEY = "yourKey"
ENV AUTHENTIFICATION_KEY = "keyForAuthToAPI"
ENV SMS_BASE_ADDRESS "https://sms.ru/"
ENV CERTIFICATE_NAME = "Test Certificate"
ENTRYPOINT ["dotnet", "MessageSenderService.dll"]