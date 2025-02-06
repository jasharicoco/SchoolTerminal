using IndividualProject_School.Data;

//* 10: Sätt betyg på en elev genom att använda Transactions ifall något går fel.

namespace IndividualProject_School.Interface
{
    internal class Menu
    {
        public static void ShowMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Hello and welcome to the School.\n");
                Console.WriteLine("What would you like to do?\n" +
                                  "1. EF: List teachers in different departments\n" +
                                  "2. EF: List students and their information\n" +
                                  "3. EF: List all active subjects\n" +
                                  "4. ADO: List employees\n" +
                                  "5. ADO: Add employee\n" +
                                  "6. ADO: List grades for a specific student\n" +
                                  "7. ADO: List total sum of salaric payments in different professions\n" +
                                  "8. ADO: List average salaries for different professions\n" +
                                  "9. ADO: Get information about student through Stored Procedure\n" +
                                  "10. ADO: Add grade to student through Transactions\n" +
                                  "11. Exit");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        EFCommands.GetEmployees();
                        Console.ReadKey();
                        break;

                    case "2":
                        EFCommands.GetAllStudents();
                        Console.ReadKey();
                        break;

                    case "3":
                        EFCommands.GetActiveSubjects();
                        Console.ReadKey();
                        break;

                    case "4":
                        ADOCommands.GetEmployees();
                        Console.ReadKey();
                        break;

                    case "5":
                        ADOCommands.AddEmployee();
                        Console.ReadKey();
                        break;

                    case "6":
                        ADOCommands.GetGradesFromSpecificStudent();
                        Console.ReadKey();
                        break;

                    case "7":
                        ADOCommands.GetSalaryPerMonthPerProfession();
                        Console.ReadKey();
                        break;

                    case "8":
                        ADOCommands.GetAvgSalary();
                        Console.ReadKey();
                        break;

                    case "9":
                        ADOCommands.GetStudentById();
                        Console.ReadKey();
                        break;

                    case "10":;
                        Console.ReadKey();
                        break;

                    case "11":
                        Console.WriteLine("Exiting the program...");
                        return;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        Console.ReadKey();
                        break;
                }

                Console.WriteLine(); // Mellanslag mellan valen
            }
        }
    }
}
