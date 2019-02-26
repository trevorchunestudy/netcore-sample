# 'asp equals 'AppServicePlan'#
[CmdletBinding()]
param(
    [string]$resourceGroupName,
    [string]$aspName,
    [string]$aspLocation,
    [string]$aspTier,
    [string]$appName,
    [string]$dbConnectionString
)

# Get or Create AppServicePlan #
Write-Verbose "Determining if app service plan already exists..."
$currentAsp = Get-AzureRmAppServicePlan -ResourceGroupName $resourceGroupName -Name $aspName `
    -ErrorAction SilentlyContinue
if(!$currentAsp)
{
   Write-Verbose "Creating app service plan '$aspName'."
    New-AzureRmAppServicePlan -ResourceGroupName $resourceGroupName -Name $aspName `
     -Location $aspLocation -Tier $aspTier

    Write-Verbose "App service plan '$aspName' created." 
}
else 
{
    Write-Verbose "App service plan already exists.  Continuing..."
}

# Get or Create WebApp #
Write-Verbose "Determining if app already exists..."
$currentApp = Get-AzureRmWebApp -ResourceGroupName $resourceGroupName -Name $appName `
    -ErrorAction SilentlyContinue
if(!$currentApp)
{
    Write-Verbose "Creating web app '$appName'."
    New-AzureRmWebApp -ResourceGroupName $resourceGroupName -AppServicePlan $aspName `
     -Name $appName -Location $aspLocation

    Write-Verbose "Web app '$appName' created."
}
else 
{
    Write-Verbose "App already exists. Continuing..."
}

# Set WebApp connection string #
Write-Verbose "Setting connection string."
$connectionStrings = @{Default = @{Type = "SqlServer"; Value = $dbConnectionString}};
Set-AzureRmWebApp -ResourceGroupName $resourceGroupName -Name $appName `
 -ConnectionStrings $connectionStrings
Write-Verbose "WebApp configuration completed."