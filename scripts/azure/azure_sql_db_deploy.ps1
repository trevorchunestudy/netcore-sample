[CmdletBinding()]
param([string]$resourceGroupName, [string]$sqlServerName,  [string]$databaseName, [string]$databaseEdition)

Write-Verbose "Determining if database already exists.."
$currentDatabase = Get-AzureRmSqlDatabase -ResourceGroupName $resourceGroupName `
 -ServerName $sqlServerName -DatabaseName $databaseName -erroraction 'silentlycontinue'
if(!$currentDatabase)
{
	Write-Verbose "Database $databaseName does not exist"
	Write-Verbose "Creating a '$databaseEdition' edition database, '$databaseName' on SQL Server '$sqlServerName' in Resource Group '$resourceGroupName'."
	$currentDatabase = New-AzureRmSqlDatabase -ResourceGroupName $resourceGroupName `
	-ServerName $sqlServerName -DatabaseName $databaseName `
	-Edition $databaseEdition 
	Write-Verbose "'$databaseName' database created."
}
else 
{
	Write-Verbose "Database $databaseName already exists. Exiting..."
}