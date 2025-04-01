# Individual Project: School Management System

This project is a school management system designed to handle various administrative tasks within a school environment. It manages data about employees, students, subjects, grades, and more. The system uses both **Entity Framework (EF)** and **ADO.NET** for database operations, providing flexibility for interacting with the database.

## Features

### EF Commands
- **Get Employees**: Groups employees (teachers) by either their classes or subjects, displaying relevant details such as name and profession.
- **Get All Students**: Displays a list of students, grouped by their classes.
- **Get Active Subjects**: Retrieves active and inactive subjects, ordered by their name.

### ADO.NET Commands
- **Get Employees**: Retrieves employee data (name, profession, years employed) using SQL queries.
- **Get Salary Per Month Per Profession**: Retrieves the total salary and the number of employees per profession.
- **Get Average Salary**: Retrieves the average salary, number of employees per profession, and orders by the highest salary.
- **Add Employee**: Allows the addition of new employees, including selecting a profession, entering a salary, and an employment date.
- **Get Grades From Specific Student**: Allows querying grades for a specific student by their ID.
- **Get Student By ID**: Allows querying a student's information by their ID using a stored procedure.
- **Assign Grade to Student**: Assigns a grade to a student for a specific subject.

## Database Operations

### Entity Framework (EF) Approach
- The EFCommands class interacts with the database using Entity Framework Core.

### ADO.NET Approach
- The ADOCommands class uses raw SQL queries and stored procedures to interact directly with the database.
