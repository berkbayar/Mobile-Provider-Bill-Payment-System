# Mobile Provider Bill Payment System

## Overview
The Mobile Provider Bill Payment System is designed to enable clients to query phone bills, view detailed billing information, pay bills, and administratively add billing records through a web interface. It serves a fictional mobile service provider and supports interactions from a mobile app, banking app, and website.

## Features
- RESTful endpoints for bill querying, detailed bill information retrieval, bill payments, and adding new bills.
- Support for JWT-based authentication.
- Versionable API endpoints to facilitate future upgrades.
- Pagination for detailed bill queries.
- Developed using .NET 8 and Entity Framework Core.

## Getting Started
To get a local copy up and running follow these steps:

1. Clone the repository.
2. Navigate to the project directory.
3. Install the required NuGet packages.
    Entity Framework Core
    dotnet add package Microsoft.EntityFrameworkCore

    Entity Framework Core Design
    dotnet add package Microsoft.EntityFrameworkCore.Design

    Entity Framework Core Tools
    dotnet add package Microsoft.EntityFrameworkCore.Tools

    SQLite Database Provider for Entity Framework Core
    dotnet add package Microsoft.EntityFrameworkCore.Sqlite

    Microsoft Identity for JWT Authentication
    dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore

    JWT Bearer Authentication
    dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer

    Swashbuckle (Swagger)
    dotnet add package Swashbuckle.AspNetCore
4. Use the provided `appsettings.json` to set up your database connection string and JWT secret key.
5. Run the application using `dotnet run`.

## Usage
Refer to the Swagger UI documentation at `https://<your-host>/swagger` to test the endpoints and view their specifications.

## Authentication
This API uses JWT for secure authentication. Obtain a token via the `/api/auth/login` endpoint and include it as a bearer token in the header of your requests.

## API Endpoints
Details of the available endpoints are described in the Swagger UI documentation. The primary endpoints are:

- `GET /api/mobileapp/querybill`
- `GET /api/mobileapp/querybilldetailed`
- `GET /api/bankingapp/queryunpaidbills`
- `POST /api/website/paybill`
- `POST /api/website/addbill`
- `GET /api/website/getall`

## Data Model
The data model consists of `Subscriber` and `Bill` entities. `Subscribers` are uniquely identified by `SubscriberId`, and `Bills` are linked to subscribers while containing billing information such as `TotalAmount` and `PaidAmount`.


## Contributing
Contributions are what make the open-source community such an amazing place to learn, inspire, and create. Any contributions you make are **greatly appreciated**.

## License
Distributed under the MIT License. See `LICENSE` for more information.

## Contact
Berk Durmuş Bayar - https://www.linkedin.com/in/berk-durmu%C5%9F-bayar-ba0362248/ - berkbayr@gmail.com


