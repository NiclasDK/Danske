# Danske

The database will be an in-memory database as doing a MSSQL-database would require some setup.
  I know how to do it, by creating the database-table, then connecting to ut using SQL Server Object Explorer in Visual Studio.
  Then get the connectionstring, create the connectionstring in appsettings and reference it in program.cs through builder.configuration.GetConnectionString("...")
