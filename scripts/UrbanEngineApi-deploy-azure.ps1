Param (
	[Parameter(Mandatory = $true)]
	[ValidateSet('dev', 'qa', 'prod')]
	[string] $Environment,

	[Parameter(Mandatory = $false)]
	[ValidateSet(
		'eastus2', 
		'eastus', 
		'centralus',
		'westus',
		'northcentralus',
		'southcentralus',
		'westcentralus'
	)]
	[string] $Location = "eastus2",

	[Parameter(Mandatory = $false)]
	[string] $TemplatesFolder = "ops/azure/",

	[Parameter(Mandatory = $false)]
	[bool] $TestOnly = $false
)

Function New-ResourceGroupIfNotExists {
	Param(
		[string]$name,
		[string]$location
	)

	$exists = Get-AzResourceGroup -Name $name;
	if ($null -eq $exists) {
		New-AzResourceGroup -Name $name -Location $location;
		Write-Verbose "Resource Group '${name}' at location '${location}' created";
	}
 else {
		Write-Verbose "Resource Group '${name}' already exists";
	}
}

Function New-UrbanEngineApiDeployment {
	Param(
		[string]$environment,
		[string]$location,
		[string]$templatesFolder,
		[bool]$testOnly
	)
	Write-Verbose "Inputs { Environment: ${environment}, Location: ${location}, TemplatesFolder: ${templatesFolder}, TestOnly: ${testOnly}";

	$projectName = "UrbanEngineApi";
	$namePrefix = "${projectName}-${environment}";
	$resourceGroup = "${namePrefix}-rg";
	$templateFile = "${templatesFolder}/${projectName}.deploy.json";
	$parameterFile = "${templatesFolder}/${projectName}.${environment}.parameters.json";

	Write-Verbose "Checking if resource group '${resourceGroup}' exists";
	New-ResourceGroupIfNotExists -name "${resourceGroup}" -location $location;

	if ($testOnly -eq $true) {
		Write-Verbose "Testing resource template '${templateFile}'";
		Test-AzResourceGroupDeployment `
			-Name "${namePrefix}-deploy" `
			-ResourceGroupName $resourceGroup `
			-Mode Incremental `
			-TemplateFile $templateFile `
			-TemplateParameterFile $parameterFile;
	}
	else {
		Write-Verbose "Deploying resource template '${templateFile}'";
		New-AzResourceGroupDeployment `
			-Name "${namePrefix}-deploy" `
			-ResourceGroupName $resourceGroup `
			-Mode Incremental `
			-TemplateFile $templateFile `
			-TemplateParameterFile $parameterFile;
	}
}

# Connect to Azure with a browser sign in token
Connect-AzAccount;

# Perform the deployment for specified environment
New-UrbanEngineApiDeployment `
	-environment $Environment `
	-location $Location `
	-templatesFolder $TemplatesFolder `
	-testOnly $TestOnly;

# Logout
Disconnect-AzAccount;