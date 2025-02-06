﻿using Microsoft.Data.SqlClient;
using System.Data;

namespace IndividualProject_School.Data
{
    internal class ADOCommands
    {
        private const string _connectionString = @"Data Source=localhost;
                                                   Initial Catalog=IndividuelltProjekt_Egzon;
                                                   Integrated Security=True;
                                                   Encrypt=True;
                                                   Trust Server Certificate=True;
                                                   Application Intent=ReadWrite;
                                                   Multi Subnet Failover=False";

        public static void GetEmployees()
        {
            string query = @"SELECT 
                             e.EmployeeId AS Id, 
                             e.FirstName + ' ' + e.LastName AS Name, 
                             p.ProfessionName AS Profession,
                             DATEDIFF(YEAR, e.EmploymentDate, GETDATE()) AS YearsEmployed
                             FROM Employees e
                             INNER JOIN Professions p ON e.ProfessionId = p.ProfessionId";

            ExecuteQuery(query, 16);
        }
        public static void GetSalaryPerMonthPerProfession()
        {
            string query = @"SELECT p.ProfessionName,
                             SUM(e.Salary) AS TotalSalary
                             FROM Employees e
                             JOIN Professions p ON e.ProfessionId = p.ProfessionId
                             GROUP BY p.ProfessionName
                             ORDER BY p.ProfessionName";

            ExecuteQuery(query, 16);
        }
        public static void GetAvgSalary()
        {
            string query = @"SELECT 
                             p.ProfessionName AS Profession,
                             ROUND(AVG(e.Salary), 2) AS AverageSalary
                             FROM Employees e
                             INNER JOIN Professions p ON e.ProfessionId = p.ProfessionId
                             GROUP BY p.ProfessionName
                             ORDER BY AverageSalary DESC"; // From highest to lowest

            ExecuteQuery(query, 16);
        }
        public static void AddEmployee()
        {
            Console.WriteLine("Profession?");
            List<(int Id, string ProfessionName)> professions = ListProfessions();  // Fetch all professions

            if (!int.TryParse(Console.ReadLine(), out int professionId))
            {
                Console.WriteLine("Please enter a valid profession ID.");
                return;
            }

            // Check if professionId exists in the list
            if (!professions.Any(p => p.Id == professionId))
            {
                Console.WriteLine("Please enter a valid profession ID.");
                return;
            }

            Console.WriteLine("Enter the employee's first name:");
            string firstName = Console.ReadLine();

            Console.WriteLine("Enter the employee's last name:");
            string lastName = Console.ReadLine();

            Console.WriteLine("Enter the employee's salary (between 20.000 - 50 000):");
            int salary;
            if (!int.TryParse(Console.ReadLine(), out salary) || salary < 20000 || salary > 50000)
            {
                Console.WriteLine("Please enter a valid number.");
                return;
            }

            Console.WriteLine("Enter the employee's employment date (YYYY-MM-DD):");
            DateOnly employmentDate;
            if (!DateOnly.TryParse(Console.ReadLine(), out employmentDate))
            {
                Console.WriteLine("Please enter a valid date.");
                return;
            }

            // Build the query for adding the employee
            string query = @"INSERT INTO Employees (FirstName, LastName, ProfessionId, Salary, EmploymentDate)
             VALUES (@FirstName, @LastName, @ProfessionId, @Salary, @EmploymentDate)";

            // Create the parameters for the query
            SqlParameter[] parameters = new SqlParameter[] {
        new SqlParameter("@FirstName", SqlDbType.NVarChar) { Value = firstName },
        new SqlParameter("@LastName", SqlDbType.NVarChar) { Value = lastName },
        new SqlParameter("@ProfessionId", SqlDbType.Int) { Value = professionId },
        new SqlParameter("@Salary", SqlDbType.Int) { Value = salary },
        new SqlParameter("@EmploymentDate", SqlDbType.Date) { Value = employmentDate }
    };

            // Call ExecuteNonQuery to execute the INSERT query
            ExecuteNonQuery(query, parameters);
        }
        public static void GetGradesFromSpecificStudent()
        {
            Console.WriteLine("Which student?");
            List<(int StudentId, string FirstName, string LastName, string ClassName)> students = ListStudents();  // Fetch all students

            foreach (var student in students)
            {
                Console.WriteLine($"{student.StudentId}: {student.ClassName} - {student.FirstName} {student.LastName}");
            }

            if (!int.TryParse(Console.ReadLine(), out int studentId))
            {
                Console.WriteLine("Please enter a valid student ID.");
                return;
            }

            // Check if the studentId exists in the list
            if (!students.Any(s => s.StudentId == studentId))
            {
                Console.WriteLine("Please enter a valid student ID.");
                return;
            }

            // Query to fetch grades for a specific student
            string query = @"
                             SELECT 
                             s.FirstName + ' ' + s.LastName AS Student,
                             sub.SubjectName AS Subject,
                             g.Grade AS Grade,
                             e.FirstName + ' ' + e.LastName AS SetByMentor,
                             FORMAT(g.DateAssigned, 'yyyy-MM-dd') AS DateAssigned
                             FROM Grades g
                             INNER JOIN Students s ON g.StudentId = s.StudentId
                             INNER JOIN Subjects sub ON g.SubjectId = sub.SubjectId
                             INNER JOIN Employees e ON g.EmployeeId = e.EmployeeId
                             WHERE s.StudentId = @StudentId";

            // Create the parameter for the query
            SqlParameter studentIdParam = new SqlParameter("@StudentId", SqlDbType.Int)
            {
                Value = studentId
            };

            // Execute the query with the parameter
            ExecuteQuery(query, 16, studentIdParam);
        }
        public static void GetStudentById()
        {
            Console.WriteLine("Which student?");
            List<(int StudentId, string FirstName, string LastName, string ClassName)> students = ListStudents();  // Fetch all students

            // Print the student list for selection
            foreach (var student in students)
            {
                Console.WriteLine($"{student.StudentId}: {student.ClassName} - {student.FirstName} {student.LastName}");
            }

            if (!int.TryParse(Console.ReadLine(), out int studentId))
            {
                Console.WriteLine("Invalid input. Please enter a valid student ID.");
                return;
            }

            // Check if the studentId exists in the list
            if (!students.Any(s => s.StudentId == studentId))
            {
                Console.WriteLine("Invalid Student ID. Please choose a valid Student ID from the list.");
                return;
            }

            // Query for the stored procedure
            string query = "GetStudentById";

            // Define the parameters for the stored procedure
            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@StudentId", SqlDbType.Int) { Value = studentId }
            };

            // Execute the query with the parameters
            ExecuteQuery(query, 16, parameters);
        }
        public static List<(int Id, string ProfessionName)> ListProfessions()
        {
            string query = @"SELECT ProfessionId AS Id, ProfessionName AS Profession FROM Professions";
            var professionList = new List<(int, string)>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string name = reader.GetString(1);
                            professionList.Add((id, name));
                            Console.WriteLine($"{id}: {name}");  // Skriver ut listan av yrken
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return professionList;
        }
        public static List<(int StudentId, string FirstName, string LastName, string ClassName)> ListStudents()
        {
            string query = @"SELECT 
                             s.StudentId, 
                             s.FirstName, 
                             s.LastName, 
                             c.ClassName
                             FROM 
                             Students s
                             INNER JOIN 
                             Classes c ON s.ClassId = c.ClassId";

            List<(int StudentId, string FirstName, string LastName, string ClassName)> studentList = new List<(int, string, string, string)>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Read values from the SQL data reader
                            int studentId = reader.GetInt32(0);  // StudentId
                            string firstName = reader.GetString(1);  // FirstName
                            string lastName = reader.GetString(2);  // LastName
                            string className = reader.GetString(3);  // ClassName

                            studentList.Add((studentId, firstName, lastName, className));
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return studentList;
        }

        // Methods to run our SQL queries
        // ExecuteQuery to fetch data
        public static void ExecuteQuery(string query, int padding, params SqlParameter[] parameters)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);

                // Add parameters if provided
                if (parameters != null && parameters.Length > 0)
                {
                    foreach (var param in parameters)
                    {
                        command.Parameters.Add(param);
                    }
                }

                // Detect if it's our stored procedure
                if (query.StartsWith("Get", StringComparison.OrdinalIgnoreCase))
                {
                    command.CommandType = CommandType.StoredProcedure;
                }
                else
                {
                    command.CommandType = CommandType.Text;
                }

                try
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            Console.Write(reader.GetName(i).PadRight(padding));
                        }
                        Console.WriteLine();

                        while (reader.Read())
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                Console.Write(reader[i].ToString().PadRight(padding));
                            }
                            Console.WriteLine();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error executing query: " + ex.Message);
                }
            }
        }
        // ExecuteNonQuery to write/change data
        public static void ExecuteNonQuery(string query, params SqlParameter[] parameters)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters if provided
                    if (parameters != null && parameters.Length > 0)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    try
                    {
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            Console.WriteLine($"{rowsAffected} rows affected. Action was successful!");
                        }
                        else
                        {
                            Console.WriteLine("No rows affected. Something went wrong.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
            }
        }
    }
}
