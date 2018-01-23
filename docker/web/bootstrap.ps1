Write-Output 'Promoting environment variables'
foreach($key in [System.Environment]::GetEnvironmentVariables('Process').Keys) {
    if ([System.Environment]::GetEnvironmentVariable($key, 'Machine') -eq $null) {
        $value = [System.Environment]::GetEnvironmentVariable($key, 'Process')
        [System.Environment]::SetEnvironmentVariable($key, $value, 'Machine')
        Write-Output "Set environment variable: $key"
    }
}

Write-Output 'Configuring DB connection'
if ($env:DB_CONNECTION_STRING_PATH -And (Test-Path $env:DB_CONNECTION_STRING_PATH)) {
    
    Remove-Item -Force -Path "C:\web-app\connectionStrings.config"
        
    New-Item -Path "C:\web-app\connectionStrings.config" `
             -ItemType SymbolicLink `
             -Value $env:DB_CONNECTION_STRING_PATH

    Write-Verbose "INFO: Using connection string from secret"
}
else {
    Write-Verbose "WARN: Using default connection strings, secret file not found at: $env:DB_CONNECTION_STRING_PATH"
}

Write-Output 'Starting IIS'
Start-Service W3SVC

Write-Output 'Running ServiceMonitor'
& C:\ServiceMonitor.exe w3svc