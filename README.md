# Cereal API
dotnet tool install --global dotnet-ef --version 9.*
### Add models & update server
Go to root directory of solution
```
dotnet ef migrations add Initial
dotnet ef database update
```
### Seed database
```
dotnet run seed
```

### Run
```
dotnet run 
```
### Test API
```
http://localhost:5148/swagger/index.html
```