using Microsoft.Data.SqlClient;
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
        public static void AddEmployee()
        {
            Console.WriteLine("What title would you like to give your employee?");
            List<(int Id, string ProfessionName)> professions = ListProfessions();  // Hämta alla professioner

            if (!int.TryParse(Console.ReadLine(), out int professionId))
            {
                Console.WriteLine("Invalid input. Please enter a valid profession ID.");
                return;
            }

            // Kontrollera om professionId finns i listan
            if (!professions.Any(p => p.Id == professionId))
            {
                Console.WriteLine("Invalid Profession ID. Please choose a valid Profession ID from the list.");
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
                Console.WriteLine("Invalid salary. Please enter a valid number.");
                return;
            }

            Console.WriteLine("Enter the employee's employment date (YYYY-MM-DD):");
            DateOnly employmentDate;
            if (!DateOnly.TryParse(Console.ReadLine(), out employmentDate))
            {
                Console.WriteLine("Invalid date. Please enter a valid date.");
                return;
            }

            // Skapa den nya anställde med de insamlade uppgifterna
            ExecuteAddEmployee(firstName, lastName, professionId, salary, employmentDate);
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
                             ORDER BY AverageSalary DESC"; // Sorterar från högsta till lägsta lön

            ExecuteQuery(query, 16);
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

        // Våra metoder för att köra SQL queries mot databasen
        public static void ExecuteAddEmployee(string firstName, string lastName, int professionId, int salary, DateOnly employmentDate)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @"INSERT INTO Employees (FirstName, LastName, ProfessionId, Salary, EmploymentDate)
                                 VALUES (@FirstName, @LastName, @ProfessionId, @Salary, @EmploymentDate)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FirstName", firstName);
                    command.Parameters.AddWithValue("@LastName", lastName);
                    command.Parameters.AddWithValue("@ProfessionId", professionId);
                    command.Parameters.AddWithValue("@Salary", salary);
                    command.Parameters.AddWithValue("@EmploymentDate", employmentDate);

                    try
                    {
                        connection.Open();
                        var rowsAffected = command.ExecuteNonQuery();
                        Console.WriteLine("Employee added successfully!");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }
        public static void ExecuteQuery(string query, int padding)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Skriv ut kolumnnamnen
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            Console.Write(reader.GetName(i).PadRight(padding));
                        }
                        Console.WriteLine(); // Ny rad efter kolumnnamnen

                        // Skriv ut raderna
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
                    Console.WriteLine(ex.Message);
                }
            }
        } 
    }
}
