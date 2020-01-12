# Urban Engine API

[![Build Status](https://dev.azure.com/urbanengine/Urban%20Engine/_apis/build/status/urban-engine-api?branchName=master)](https://dev.azure.com/urbanengine/Urban%20Engine/_build/latest?definitionId=2&branchName=master)

# DEV Notes

This section will contain any notes to be aware of for developing Urban Engine API

## Results

To make things consistent objects in the [Results](Core/Common/Results) folder are used. Use this to return responses from API endpoints to provide consistent return types

* FailureResult: used to indicate a failure occurred, for example an exception was thrown
* CommandResult: used to indicate result of a command such as an Insert, Update, Delete
* QueryResult: used to indicate result of a query that was performed, returns data and paging information if applicable

## CRUD Operations End to End for simple Entity

If creating an entity and all that is needed are basic CRUD operations follow this checklist to add various interfaces and classes at the appropriate layers

- 

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

Run the following command to create a migration, replace `InitialCreate` with name of a migration you want to use. 
NOTE: if you are using SQL Lite it only allows one migration and you will have to remove migrations

```console
dotnet ef migrations add InitialCreate --startup-project Services\UrbanEngineApi --project Infrastructure\Persistence --output-dir Data\Migrations
```

To remove the last migration

```console
dotnet ef migrations remove --startup-project Services\UrbanEngineApi --project Infrastructure\Persistence
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
2. Open a command prompt
3. cd to the `src\UrbanEngine.Infrastructure` directory
4. In the command window type `dotnet ef --help` to ensure you have the dotnet ef tool installed
5. If EF tools are not installed you'll need to install them using

   ```powershell
   dotnet tool install --global dotnet-ef
   ```

6. Run the following command, change the `<yourmigrationname>` to be a name to indicate what is going into this migration

   ```powerhsell
    dotnet ef migrations add <yourmigrationname> --startup-project ../UrbanEngine.Web/UrbanEngine.Web.csproj --project UrbanEngine.Infrastructure.csproj --output-dir Data/Migrations
   ```

7. Go to `UrbanEngine.Web` project and right click, go to properties. Click Debug, If it does not already exist add an environment variable `APPLY_MIGRATIONS` and set the value to be `true`
8. Make sure your database is [running locally](#running-the-dev-database-locally)
9. Run the UrbanEngine.Web project
10. Go to `pgAdmin` and ensure your changes are refelected

Explore other options with `dotnet ef` tools for additional options

### (Step 5) - Creating Specification and Filter

1. Go to `UrbanEngine.Core`
2. Under Specificatiosn create a new Specification
3. Inherit from `BaseSpecification<T>`
4. Create an interface for filtering, this is passed to the constructor of your Specification class
5. Add any properties to the filter class that you would use to search on
6. Look at other Specification classes for examples

### Next Steps

Working with Tyler, got to this point

* Add the Manager class
* Add the Models
* Add the Messages
* Add the Handler
* Add the Controller
* Add the Unit Tests
