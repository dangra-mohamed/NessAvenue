# Avenue Code Test

## Database Creation Instructions

To create the database for this project, follow these steps:

1. Install [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) or any other supported database system.
2. Open the project in your preferred IDE.
3. Update the connection string in the `appsettings.json` file to point to your database server.
4. Change the working directory to DAL project using following command
 ```sh
   cd .\AvenueApp.DAL
   ```
5. Run the following Entity Framework Core command in the Package Manager Console to create the database:
```sh
   Update-Database
   ```
This command will apply any pending migrations and create the database based on the DbContext configuration.

5. Once the command completes successfully, the database should be created and ready for use.


