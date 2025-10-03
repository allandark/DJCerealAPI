# Cereal API
RESTFull web API for cereal products. The API is implemented with ASP.NET and Entity Framework. When running in development a Swagger UI is available at `http://localhost:5148/swagger/index.htm` or whatever ip the webapplication is running on.  The project utilizes OOP design patterns such as factory, singleton and repository to provide a clean and scaleable code base.


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

## API Overview
**Base URL** https://localhost:5148/api/v1 (change to machine IP when publishing to web)
```Put```, ```Post``` and ```Delete``` requires a Bearer token in the authorization header.
## Endpoints

### `GET /products`

**Description:**  
Retrieves a list of products. Supports optional filtering via query expressions.

#### Query Parameters

| Name   | Type   | Required | Description                |
|--------|--------|----------|----------------------------|
| query  | string | No       | Optional filter expression |

---

#### Query Filter Operations

You can use the following operators in the `query` parameter to filter results:

|Operation|Character| Description|
|-|-|-|
| Less Than             | `<`    | Return products where the property is less than the value    |
| Less Than or Equal    | `<=`   | Return products where the property is less than or equal     |
| Greater Than          | `>`    | Return products where the property is greater than the value |
| Greater Than or Equal | `>=`   | Return products where the property is greater or equal       |

> **Note:** Use URL encoding when testing in a browser or tools like Swagger:
> - `>` → `%3E`
> - `<` → `%3C`
> - `!=` → `%21%3D`

---

#### Acceptable Filter Values

**Types:**
- `cold`
- `hot`

**Manufacturers:**
- `american_home_food_products`
- `general_mills`
- `kelloggs`
- `nabisco`
- `post`
- `quaker_oats`
- `ralston_purina`

**Examble Query**
```api/product?sort=calories&manufacturer=post&calories<130```
**Response**
```
[
  {
    "id": 1,
    "name": "100% Bran",
    "mfr": "N",
    "type": "C",
    "calories": 70,
    "protien": 4,
    "fat": 1,
    "sodium": 130,
    "fiber": 10,
    "carbo": 5,
    "sugars": 6,
    "potass": 280,
    "vitamins": 25,
    "shelf": 3,
    "weight": 1,
    "cups": 0.33,
    "rating": 68403
  },...
]
```
**Status Codes:**
| Status Code | Description|
|---|---|
|200 Ok|Success|
|404 Not Found|Product id is not in database |
|400 Bad Request|Invalid model|


### `GET /products{id}`
| Name      | Type     | Required   |Description|
| --------  | -------  |-------     |------- |
| id   | int     | Yes| Product identifier |

**Status Codes:**
| Status Code | Description|
|---|---|
|200 Ok|Success|
|404 Not Found|Product id is not in database |
|400 Bad Request|Invalid model|

**Response**
```
  {
    "id": 1,
    "name": "100% Bran",
    "mfr": "N",
    "type": "C",
    "calories": 70,
    "protien": 4,
    "fat": 1,
    "sodium": 130,
    "fiber": 10,
    "carbo": 5,
    "sugars": 6,
    "potass": 280,
    "vitamins": 25,
    "shelf": 3,
    "weight": 1,
    "cups": 0.33,
    "rating": 68403
  }
```
**Status Codes:**

| Status Code | Description|
|---|---|
|200 Ok|Success|
|404 Not Found|Product id is not in data base|
|400 Bad Request|Invalid model|


### `POST /products`

**Description:**  
Creates a new product.

**Authorization:** Required (`Bearer Token`)
**Path Parameters:**
| Name      | Type     | Required   |Description|
| --------  | -------  |-------     |------- |
| product   | json     | Yes| Product object |

**Request Body:**
```
{
  "id": 0,
  "name": "string",
  "mfr": "string",
  "type": "string",
  "calories": 0,
  "protien": 0,
  "fat": 0,
  "sodium": 0,
  "fiber": 0,
  "carbo": 0,
  "sugars": 0,
  "potass": 0,
  "vitamins": 0,
  "shelf": 0,
  "weight": 0,
  "cups": 0,
  "rating": 0
}
```

**Status Codes:**
| Status Code | Description|
|---|---|
|201 Created|Successfully created or modified product|
|400 Bad Request|Invalid model|
|401 Unauthorized | Unauthorized request|



### `POST /products/{id}`
Description:
Creates a new product if the ID does not exist, or updates the existing product if it does.
**Authorization:** Required (`Bearer Token`)
**Path Parameters:**
| Name      | Type     | Required   |Description|
| --------  | -------  |-------     |------- |
| product   | json     | Yes| Product object |
| id   | int     | Yes| Product identifier |

**Request Body:** Same as above.
**Responses:**

| Status Code | Description|
|---|---|
|201 Created|Successfully created or modified product|
|404 Not Found|Product id is not in database |
|400 Bad Request|Invalid model|
|401 Unauthorized | Unauthorized request|

### `PUT /products/{id}`
**Description**
Updates an existing product by ID.
**Authorization:** Required (`Bearer Token`)
**Path Parameters**

| Name      | Type     | Required   |Description|
| --------  | -------  |-------     |------- |
| product   | json     | Yes| Product object |
| id   | int     | Yes| Product identifier |

**Request Body:** Same as above.

**Responses:**

| Status Code | Description|
|---|---|
|200 Ok|Success|
|404 Not Found|Product id is not in database |
|400 Bad Request|Invalid model|
|401 Unauthorized | Unauthorized request|


### `DELETE /products/{id}`
**Description**
Deletes a product by ID.
**Authorization:** Required (`Bearer Token`)
**Path Parameters**
| Name      | Type     | Required   |Description|
| --------  | -------  |-------     |------- |
| id   | int     | Yes| Product identifier |
**Responses:**

| Status Code | Description|
|---|---|
|200 Ok|Success|
|404 Not Found|Product id is not in database |
|400 Bad Request|Invalid model|
|401 Unauthorized | Unauthorized request|
|500 Internal | Internal server error|

### `POST /auth/register`
**Description**
Create an user.

**Request Body:**
```
{
  "username": "string",
  "password": "string"
}
```

**Responses:**

```
{
  "id": 0,
  "username": "string",
  "passwordHash": "string"
}
```

| Status Code | Description|
|---|---|
|200 Ok|Success|
|400 Bad Request|Invalid model|




### `POST /auth/login`
**Description**
Login to user to get Bearer Token.
**Request Body:**
Same as above

**Responses:**
String with token

| Status Code | Description|
|---|---|
|200 Ok|Success|
|400 Bad Request|Invalid model|

