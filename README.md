# Cereal API
RESTFull web API for cereal products. The API is implemented with ASP.NET and Entity Framework. When running in development a Swagger UI is available at `http://localhost:5148/swagger/index.htm` or whatever ip the webapplication is running on.  



## Build
#### Dependencies
Web Application runs on *dot NET8.0* framework but the 9.* is installed. It requires a running SQL server running.
```
dotnet tool install --global dotnet-ef --version 9.*
```
**Nuget Packaged**
Install the following packedges
- Microsoft.AspNetCore.Authentication.JwtBearer 8.0.0
- Microsoft.AspNetCore.Authentication.Negotiate 8.0.20
- Microsoft.EntityFrameworkCore 9.0.9
- Pomelo.EntityFrameworkCore.MySql 9.0.0
- Swashbuckle.AspNetCore 9.0.5
- System.IdentityModel.Tokens.Jwt 8.14.0

#### Update appsettings.json
Edit the database **connection string** and the authentication **token** in *appsettings.json* found in the root directory of the repository.
```
...
  "ConnectionStrings": {
    "DefaultConnection": "<INSERT_CONNECTION_STRING_HERE>"
  },
...

"Token": "<INSERT_64_BYTE_TOKEN>",
...
```

#### Add models & update server
Go to root directory of solution
```
dotnet ef migrations add Initial
dotnet ef database update
```
#### Seed database
```
dotnet run seed
```

### Run
```
dotnet run 
```
### Test
```
http://localhost:5148/swagger/index.html
```