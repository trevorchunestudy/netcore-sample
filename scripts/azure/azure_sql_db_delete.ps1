[CmdletBinding()]
param([string]$resourceGroupName, [string]$sqlServerName,  [string]$databaseName)

Write-Verbose "Deleting database '$databaseName' on SQL Server '$sqlServerName' in Resource Group '$resourceGroupName'."
Remove-AzureRmSqlDatabase -ResourceGroupName $resourceGroupName `
 -ServerName $sqlServerName -DatabaseName $databaseName -erroraction 'silentlycontinue'

Write-Verbose "'$databaseName' database deleted."