Write-Output 'Configuring DB connection'
if ($env:DB_CONNECTION_STRING_PATH -And (Test-Path $env:DB_CONNECTION_STRING_PATH)) {
    
    Remove-Item -Force -Path "$env:APP_ROOT\connectionStrings.config"
        
    New-Item -Path "$env:APP_ROOT\connectionStrings.config" `
             -ItemType SymbolicLink `
             -Value $env:DB_CONNECTION_STRING_PATH

    Write-Verbose "INFO: Using connection string from secret"
}
else {
    Write-Verbose "WARN: Using default connection strings, secret file not found at: $env:DB_CONNECTION_STRING_PATH"
}

Write-Output 'Starting handler'
.\SignUp.MessageHandlers.SaveProspect.exe