# Urban Engine API

[![Build Status](https://dev.azure.com/urbanengine/Urban%20Engine/_apis/build/status/UrbanEngineApi?branchName=master)](https://dev.azure.com/urbanengine/Urban%20Engine/_build/latest?definitionId=6&branchName=master)
[![License](https://img.shields.io/badge/License-Apache%202.0-blue.svg)](https://opensource.org/licenses/Apache-2.0)

# Code Status
Due to the pandemic Urban Engine is no longer holding CoWorking Night on a weekly basis. CoWorking Night is now held once a month and all scheduling is done via Google Calendar; therefore, this project is no longer needed. However, this code will be publicly archived for reference in the event any devs want to see how we planning to to handle this use case.

## Configuration and Deployment

This section outlines information relating to configuring or deploying the UrbanEngineApi application.

### Provisioning Resources in Azure

For provisioning resources in Azure the following script is available if running locally:

From the root directory run the following command in PowerShell:

```console
& ./scripts/UrbanEngineApi-deploy-azure.ps1
```

**NOTE:** You will need Azure PowerShell installed if you want to run this script. Documentation on this is available at [Install Azure PowerShell](https://docs.microsoft.com/en-us/powershell/azure/install-az-ps?view=azps-3.6.1).

## Accessing Configuration Secrets

### Storing secrets in Development environment

In your local development environment most configuration settings will be managed via `UserSecrets` in a local `secrets.json` file. The `Microsoft.Extensions.Configuration.UserSecrets` NuGet package enables this for us. There is a script located in [scripts/set-secrets-local.ps1](scripts/set-secrets-local.ps1) that will set this up in your local development environment.

From the root folder of this repository run the following command in PowerShell:

```console
& ./scripts/set-secrets-local.ps1
```

You may be prompted to specify certain values such as the database password.

### Storing secrets in Production

In production we will be using `Azure KeyVault` to store secrets. The `set-secrets-prod.ps1` script can be used to set the configuration values in the KeyVault. This script will affect production; therefore, please be sure to only run this when ready.

From the root folder of this repository run the following command in PowerShell:

```console
& ./scripts/set-secrets-prod.ps1
```

Also NOTE: In the [appsettings.json](src/UrbanEngine.Web/appsettings.json) the setting `KeyVaultName` indicates the name of the KeyVault to use. Again, please make sure this settings is correct before deploying to production.

### Updating Configuration Values

Be sure to update the `set-secrets-local.ps1` and `set-secrets-prod.ps1` scripts with any additional settings you add to the project. You can store any configuration values as secrets, they do not necessarily have to be just secrets.

## DEV Notes

This section will contain any notes to be aware of for developing Urban Engine API

## Results

To make things consistent objects in the [Results](Core/Common/Results) folder are used. Use this to return responses from API endpoints to provide consistent return types

* FailureResult: used to indicate a failure occurred, for example an exception was thrown
* CommandResult: used to indicate result of a command such as an Insert, Update, Delete
* QueryResult: used to indicate result of a query that was performed, returns data and paging information if applicable

## Exception Handling for API Controllers

To provide a common response when exceptions occur and to ensure exceptions are logged and sensitive error messages are not returned to the client
an exception handler is added in the [Startup.cs](Services/UrbanEngineApi/Startup.cs) file. This allows the capture of any exceptions
thrown by controller endpoints without having to add `try/catch` and `logging` statements in your controllers themselves.

Example:

```csharp
// see Configure method in Startup.cs
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        // ...see implementation details in Startup.cs file...
    });
});
```

## Migrate

When the UrbanEngineApi starts up it is currently set to call `Migrate` to update the database. Whether this is run or not is controll by an Environment Variable
called `APPLY_MIGRATIONS` and if this is set to the value of `True` then Migrations will be applied on application start up. This is good to have turned on for any
development or testing environments but should be disabled in Production and applied only when expected. To see the code where migrations are run go to
the [Program.cs](Services/UrbanEngineApi/Program.cs) file in the UrbanEngineApi project and look for the `CreateOrMigrateDatabase` method.

## EF Migrations

1. Open a command prompt
2. cd to the `src\UrbanEngine.Infrastructure` directory
3. In the command window type `dotnet ef --help` to ensure you have the dotnet ef tool installed
4. If EF tools are not installed you'll need to install them using

   ```powershell
   dotnet tool install --global dotnet-ef
   ```

5. Run the following command, change the `<yourmigrationname>` to be a name to indicate what is going into this migration

   ```powerhsell
    dotnet ef migrations add <yourmigrationname> --startup-project ../UrbanEngine.Web/UrbanEngine.Web.csproj --project UrbanEngine.Infrastructure.csproj --output-dir Data/Migrations
   ```

If working with the local SQL Lite database you'll have to delete the *.db file first as well

## Seed Data

The [SeedDataGenerator](Infrastructre/Persistence/Data/SeedDataGenerator.cs) is used to seed the database
with any initial data. Use this to add any data that should always be present in the database

## Running the dev database locally

A docker compose file exists to help spin up a docker container that runs PostgresSQL. Follow the steps to run the database locally in your dev environment.

1. Open a command prompt
2. cd to the `UrbanEngineApi` directory
3. Ensure that Docker is running
4. Run the following command

    ```powershell
    & scripts\start-postgres-local.ps1
    ```

5. Open `pgAdmin` and ensure you can connect to the database locally
6. To stop the database when you are finished run the following

    ```powershell
    & scripts\stop-postgres-local.ps1
    ```

## How to setup a new workflow

### (Step 1) - Create your entity

1. Go to the `UrbanEngine.Core` project
2. Go to Entities folder
3. Create a new Entity to model what this will look like in the database, use the naming convention of suffix name of entity with `Entity`
4. Make sure your class inherits from `UrbanEngine.SharedKernel.Data.EntityBase`
5. Add any additional properties to your entity

### (Step 2) - Create the mapping for EF

1. Go to `UrbanEngine.Infrastructure` project
2. Go to `Data\Configuration`
3. Create a new Configuration for EF Core
4. Inherit from `EntityBaseConfiguration`
5. Override the `Configure` method and add in any mappings
6. NOTE: inheriting from EntityBaseConfiguration will map the table, primary key, and any common properties from `EntityBase`
7. Be sure to make the class internal

### (Step 3) - Create the repository

1. Go to `UrbanEngine.Infrastructure` project
2. Go to `Data\Repository
3. Create a new repository for your entity
4. Inherit from `EfRepository<T>`
5. Add a constructor that inherits from base

### (Step 4) - Generate Migrations

1. This step can come when you are completely done modeling your entity but should happen before you create a pull request.
2. Follow the steps at [EF Migrations](#ef-migrations)
3. Go to `UrbanEngine.Web` project and right click, go to properties. Click Debug, If it does not already exist add an environment variable `APPLY_MIGRATIONS` and set the value to be `true`
4. Make sure your database is [running locally](#running-the-dev-database-locally)
5. Run the UrbanEngine.Web project
6. Go to `pgAdmin` and ensure your changes are refelected

Explore other options with `dotnet ef` tools for additional options

### (Step 5) - Creating Specification and Filter

1. Go to `UrbanEngine.Core`
2. Under Specifications create a new Specification
3. Inherit from `BaseSpecification<T>`
4. Create an interface for filtering, this is passed to the constructor of your Specification class
5. Add any properties to the filter class that you would use to search on
6. Look at other Specification classes for examples

### (Step 6) - Create the Manager

1. Go to `UrbanEngine.Core`
2. Go to Managers folder and create a new folder for your manager
3. Name the new class with the convetion suffix the name with `Manager`
4. Inherit from the `ManagerBase<TEntity>` base class
5. Also add an associated interface for this manager and implement that
6. Your interace should inherit from `IManager<TEntity>`
7. In the manager class generate the constructor that inherits from base

### (Step 7) - Add the Models (aka DTOs)

1. Go to `UrganEngine.Core`
2. Under the Models folder create a new folder to store you models
3. Create a both a list DTO and a detail DTO
4. Use the convention to suffix with `DetailDTO` and `ListItemDTO`
5. Enter the necessary properties for each DTO. The ListItemDTO is only a few properties and what you need to see a list of that entity. The detail DTO gives you all the details about the DTO
6. You can use your associated Entity to help define the properties you need. This is also a good place to do any transformations before sending to UI
7. Remove any properties from the DTO that you don't want to expose to outside callers

### (Step 8) - Add the Message

1. Go to `UrbanEngine.Core`
2. Under the Messages folder create a new folder associated with the messages you are creating
3. Typically you will have 4 messages if implementing CRUD operations for your entity
4. Examples (rename entity to be your entity)
   1. DeleteEntityMessage
   2. GetEntityByIdMessage
   3. GetEntityMessage
   4. SaveEntityMessage
5. Each message will implement the Mediatr `IRequest` interface passing in the expected output to receive when the message is processed
6. Look at other messages for examples how each of these should be laid out. The idea behind a message is you are defining the inputs to be processed for some associated action

### (Step 9) - Add the Handler

1. Go to `UrbanEngine.Core`
2. Under the Handlers folder create a new folder associated with the handlers you are creating
3. Typically you will have a handler per message you need to process. So, if implementing CRUD operations you may have 4 handlers
4. Examples (rename entity to be your entity)
   1. DeleteEntityHandler
   2. GetEntityByIdHandler
   3. GetEntityHandler
   4. SaveEntityHandler
5. Each Handler will implement the Mediatr `IRequestHandler` and specify the type of message your handler is to process and the expected output to return. The output must match the same output specified in your message for `IRequest`
6. Look at other handlers for examples how each of these should be laid out. The idea behind the handler is you are defining the logic to perform when a certain message is received
7. Your handler should have the associated manager passed in via dependency injection to this class

### (Step 10) - Update Configuration

1. Go to `UrbanEngine.Web`
2. Go to `Configuration\AutoMapperProfile`
3. You will need to add a mapping for each entity to the associated DTO
4. Go `Startup.cs`, we need to add in Dependency Injection in the `ConfigureServices` method to register your repository and manager

### (Step 11) - Add the Controller

1. Go to `UrbanEngine.Web`
2. Under the Controllers folder add a new API Controller
3. Look at other Controllers for examples how this should be implemented
4. An important note is that you should pass in `IMediator` as dependency and let your controller endpoints receive messages and publish to Mediatr to process
5. Make sure your controller inherits from `ControllerBase`
6. Run `UrbanEngine.Web` and hit `swagger` and test your your controller. If everything is wired up correctly you should be able to test out everything end to end

### (Step 12) - Add the Unit Tests

1. Go to `UrbanEngine.Tests`
2. Under the appropriate folder create a new Tests class.
3. Look at existing examples for how this should be implemented.
4. Create in your test class a private class called DefaultScope that inherits from `TestScope<T>` where T is the class you want to write the test for
5. In the constructor of the DefaultScope mock any dependencies that your instance needs. For unit tests, all external dependencies should be mocked.
6. Create test methods and decorate them with this attribute `[TestMethod, TestCategory(TestCategory.Unit)]`
7. Make sure a test has only one assertion and is testing one thing. Create multiple tests for different assertions.
8. Follow the Arrange/Act/Assert pattern for writing your tests.
9. Inside your test, create an instance of DefaultScope and use the InstanceUnderTest for testing your unit.
10. Use TestExplorer to execute your tests.

### Upcoming TODO

* Write additional Unit Tests
* Recurring Events
* Authentication
* Authorization
