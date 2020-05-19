# stores values in local secrets store for development environment
# run this script from the root folder in this repository
# & ./scripts/set-secrets-local.ps1

Param(
	[Parameter(Mandatory = $false)][string] $DbHost = "localhost",
	[Parameter(Mandatory = $false)][string] $DbName = "postgres_local",
	[Parameter(Mandatory = $false)][string] $DbUser = "postgres_admin"
)

Function Convert-ToUnsecureString {
	Param(
		[Parameter(Mandatory = $true)][securestring]$secureString
	)

	$Ptr = [System.Runtime.InteropServices.Marshal]::SecureStringToCoTaskMemUnicode($secureString)
	$result = [System.Runtime.InteropServices.Marshal]::PtrToStringUni($Ptr)
	[System.Runtime.InteropServices.Marshal]::ZeroFreeCoTaskMemUnicode($Ptr)

	return $result;
}

Function Set-LocalUserSecrets {
	# populate secrets
	$dbPassword = Read-Host "Enter Password for ${DbUser}" -AsSecureString
	$dbPasswordValue = Convert-ToUnsecureString $dbPassword;
	dotnet user-secrets set "UrbanEngine:Database" "host=${DbHost};database=${DbName};user id=${DbUser};password=${dbPasswordValue};";
}

# switch to where csproj exists
Set-Location -Path "src/UrbanEngine.Web/";

# only run this command first time
# dotnet user-secrets init

# load the secrets
Set-LocalUserSecrets;

# switch back to script directory
Set-Location -Path "../../";