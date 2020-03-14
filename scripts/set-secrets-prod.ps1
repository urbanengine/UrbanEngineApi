# stores values in key vault store for production environment
# run this script from the root folder in this repository
# & ./scripts/set-secrets-prod.ps1

Param(
	[Parameter(Mandatory = $true)][string] $KeyVaultName,
	[Parameter(Mandatory = $true)][string] $DbHost,
	[Parameter(Mandatory = $true)][string] $DbName,
	[Parameter(Mandatory = $true)][string] $DbUser,
	[Parameter(Mandatory = $true)][securestring] $DbPassword
)

Function Set-ProdUserSecrets {
	# populate secrets
	Set-AzKeyVaultSecret `
		-VaultName $KeyVaultName `
		-Name 'UrbanEngine--Database' `
		-SecretValue "host=${DbHost};database=${DbName};user id=${DbUser};password=${DbPassword};";
}

# switch to where csproj exists
Set-Location -Path "src/UrbanEngine.Web/";

# load the secrets
Set-ProdUserSecrets;

# switch back to script directory
Set-Location -Path "../../";