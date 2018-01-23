# escape=`
FROM microsoft/dotnet-framework-build:4.7.1-windowsservercore-ltsc2016 AS builder

WORKDIR C:\src\SignUp.MessageHandlers.IndexProspect
COPY src\SignUp\SignUp.MessageHandlers.IndexProspect\packages.config .
RUN nuget restore packages.config -PackagesDirectory ..\packages

COPY src\SignUp C:\src
RUN msbuild SignUp.MessageHandlers.IndexProspect.csproj /p:OutputPath=c:\out\

# app image
FROM microsoft/dotnet-framework:4.7.1-windowsservercore-1709
SHELL ["powershell", "-Command", "$ErrorActionPreference = 'Stop'; $ProgressPreference = 'SilentlyContinue';"]

WORKDIR /index-prospect-handler
CMD .\SignUp.MessageHandlers.IndexProspect.exe

ENV MESSAGE_QUEUE_URL="nats://message-queue:4222" `
    ELASTICSEARCH_URL="http://elasticsearch:9200"

COPY --from=builder C:\out .