# escape=`

FROM sixeyed/msbuild:netfx-4.5.2-webdeploy-10.0.14393.1198 AS builder

WORKDIR C:\src\SignUp.Web
COPY src\SignUp\SignUp.Web\packages.config .
RUN nuget restore packages.config -PackagesDirectory ..\packages

COPY src\SignUp C:\src
RUN msbuild SignUp.Web.csproj /p:OutputPath=c:\out\web\SignUpWeb `
        /p:DeployOnBuild=true /p:VSToolsPath=C:\MSBuild.Microsoft.VisualStudio.Web.targets.14.0.0.3\tools\VSToolsPath

# app image
FROM microsoft/aspnet:windowsservercore-10.0.14393.1198
SHELL ["powershell", "-Command", "$ErrorActionPreference = 'Stop'; $ProgressPreference = 'SilentlyContinue';"]

RUN Set-ItemProperty -path 'HKLM:\SYSTEM\CurrentControlSet\Services\Dnscache\Parameters' -Name ServerPriorityTimeLimit -Value 0 -Type DWord

WORKDIR C:\web-app
RUN Import-Module WebAdministration; `
    Remove-Website -Name 'Default Web Site'; `
    New-WebAppPool -Name 'ap-signup'; `
    Set-ItemProperty IIS:\AppPools\ap-signup -Name managedRuntimeVersion -Value v4.0; `
    Set-ItemProperty IIS:\AppPools\ap-signup -Name processModel.identityType -Value LocalSystem; `
    New-Website -Name 'web-app' `
                -Port 80 -PhysicalPath 'C:\web-app' `
                -ApplicationPool 'ap-signup'

HEALTHCHECK --interval=5s `
 CMD powershell -command `
    try { `
     $response = iwr http://localhost/SignUp -UseBasicParsing; `
     if ($response.StatusCode -eq 200) { return 0} `
     else {return 1}; `
    } catch { return 1 }

ENV MESSAGE_QUEUE_URL="nats://message-queue:4222" `
    DB_CONNECTION_STRING_PATH="C:\ProgramData\Docker\secrets\signup-db.connectionstring" `
    DB_MAX_RETRY_COUNT="5" `
    DB_MAX_DELAY_SECONDS="10"

ENTRYPOINT ["powershell", "C:\\bootstrap.ps1"]

COPY .\docker\web\bootstrap.ps1 C:\
COPY --from=builder C:\out\web\SignUpWeb\_PublishedWebsites\SignUp.Web C:\web-app