# Parking Management Project

## To Compile:

1. Clone the repository and cd to /ParkingSolution/ParkingAPI/
2. Create Local DB Schema by running these commands in your terminal, in order: 
    -  ``` dotnet ef database update ```
3. After creating the DB, start the API with the command: ``` dotnet run  ```
 (Swagger documentation for the API can be accessed at https://localhost:5001/swagger/index.html)
4. In a separate terminal, navigate to /ParkingSolution/ParkingFrontend
5. run command ```npm install``` to install dependencies
6. run command ```http-server --cors```
7. Open Browser on http://127.0.0.1:8080/

## Testing:

1. cd to /ParkingSolution/ParkingAPI.Tests
2. run command:  ```dotnet test  ``` to run unit test suite

## Notable Libraries/Frameworks Used:
- Nunit: https://nunit.org/
- FastBootStrap: https://fastbootstrap.com/
- Microsoft Entity Framework Core: https://learn.microsoft.com/en-us/ef/core/
- Sqlite: https://learn.microsoft.com/en-us/dotnet/standard/data/sqlite/?tabs=net-cli
- Swagger: https://swagger.io/
