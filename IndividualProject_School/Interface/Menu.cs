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
                                  "1. List teachers by department, EF\n" +
                                  "2. List students and their info, EF\n" +
                                  "3. List all active subjects, EF\n" +
                                  "4. List employees, ADO.Net\n" +
                                  "5. Add employee, ADO.Net\n" +
                                  "6. List grades for student, ADO.Net\n" +
                                  "7. List salary totals by profession, ADO.Net\n" +
                                  "8. List average salaries by profession, ADO.Net\n" +
                                  "9. Get student info (Stored Procedure), ADO.Net\n" +
                                  "10. Add grade through transaction, ADO.Net\n" +
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

                    case "10":
                        ADOCommands.AssignGradeToStudent();
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
