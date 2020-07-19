# HCSS
Hitachi Coding Software Suite

Software(s) / SDK:
Node: V10.16.0
.NET: 3.0.100
Angular-cli: 9.1.1

Clone and Install dependencies:

1. Clone the Repository
2. cd to directory
3. npm install
4. NuGet Restore


HCSS.UnitTests

- This project can be used for UnitTests. Uses XUnit framework

HCSS.Core

- This project contains all the domain-model code. This project has no dependency on any other project.



HCSS.Data

- This project contains code related to data access concerns. It also reference HCSS.Core project as dependency.
- EF core is used along with PostgreSQL.



HCSS.Web:

- This project contains the front-end and backend code for web part.

- The "src" folder inside the project is the root for Angular. Angular-cli shall be used.

	1. src>>ng serve (to start development server for angular part)

