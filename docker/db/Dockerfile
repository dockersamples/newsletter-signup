# escape=`
FROM microsoft/mssql-server-windows-express

ENV ACCEPT_EULA="Y" `
    PASSWORD_PATH="C:\ProgramData\Docker\secrets\signup-db-sa.password"

COPY init.ps1 .

CMD ["powershell", "./init.ps1", "-Verbose"]