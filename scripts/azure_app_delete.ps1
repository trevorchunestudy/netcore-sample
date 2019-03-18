[CmdletBinding()]
param(
    [string]$resourceGroupName,
    [string]$aspName,
    [string]$appName
)
Write-Verbose "Deleted web app '$appName'."
Remove-AzureRmWebApp -ResourceGroupName $resourceGroupName -Name $appName -Force

Write-Verbose "Web app '$appName' deleted."

Write-Verbose "Deleting app service plan '$aspName'."
Remove-AzureRmAppServicePlan -ResourceGroupName $resourceGroupName -Name $aspName `
 -Force

Write-Verbose "App service plan '$aspName' was deleted."