param(
    [string]$DB_SERVER = $env:DB_SERVER,
    [string]$DB_NAME = $env:DB_NAME,
    [string]$DB_USER = $env:DB_USER,
    [string]$DB_PASS = $env:DB_PASS
)

$webConfigPath = "C:\inetpub\wwwroot\Web.config"

if (Test-Path $webConfigPath) {
    Write-Host "Updating Web.config connection string..."
    
    $xml = [xml](Get-Content $webConfigPath)
    
    # Construct new connection string
    # Note: Assuming Entity Framework connection string format based on existing Web.config
    $newConnString = "metadata=res://*/Models.Gemini.csdl|res://*/Models.Gemini.ssdl|res://*/Models.Gemini.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=$DB_SERVER;initial catalog=$DB_NAME;user id=$DB_USER;password=$DB_PASS;MultipleActiveResultSets=true;App=EntityFramework&quot;"
    
    $connStringNode = $xml.configuration.connectionStrings.add | Where-Object { $_.name -eq "GeminiEntities" }
    if ($connStringNode) {
        $connStringNode.connectionString = $newConnString
        $xml.Save($webConfigPath)
        Write-Host "Web.config updated successfully."
    } else {
        Write-Warning "Connection string 'GeminiEntities' not found in Web.config."
    }
} else {
    Write-Warning "Web.config not found at $webConfigPath"
}

# Start IIS ServiceMonitor
Write-Host "Starting IIS..."
C:\ServiceMonitor.exe w3svc
