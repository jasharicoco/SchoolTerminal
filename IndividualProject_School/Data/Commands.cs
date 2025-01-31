using IndividualProject_School.Models;
using Microsoft.EntityFrameworkCore;

//  KONSOLLMENY
//* HUR MÅNGA LÄRARE JOBBAR? (EF)
//* VISA INFORMATION OM ELEVER (EF)
//* VISA ALLA AKTIVA KURSER (EF)
//* HÄMTA PERSONAL: NAMN, BEFATTNINGAR, HUR MÅNGA ÅR HAR DE ARBETAT PÅ SKOLAN (SQL via ADO.Net)
//* LÄGG TILL PERSONAL (ADO.Net)
//* HÄMTA ELEVER OCH SE VILKEN KLASS DE GÅR I, HÄMTA BETYG FÖR VARJE KURS DE LÄST OCH SE VILKEN LÄRARE SOM SATT BETYGET (MED DATUM) (ADO.Net)
//* LÖNER TILL PERSONAL (SQL)
//* MEDELLÖN (SQL)
//* STORED PROCEDURE SOM TAR EMOT ID PÅ ELEV OCH GER INFO OM ELEVEN (SQL)
//* SÄTTA BETYG GENOM TRANSACTIONS (SQL)

namespace IndividualProject_School.Data
{
    internal class Commands
    {
        public static void GetEmployees()
        {
            try
            {
                Console.WriteLine("How'd you like to group the teachers?");
                Console.WriteLine("1. By classes");
                Console.WriteLine("2. By subjects");
                Console.WriteLine("3. ");
                Console.WriteLine("4. ");

                string choice = Console.ReadLine();

                using (var context = new IndividuelltProjektEgzonContext())
                {
                    IEnumerable<Employee> employees = context.Employees
                        .Include(e => e.Profession)
                        .Include(s => s.Subjects)
                        .ToList();

                    IEnumerable<Employee> teachers = employees.Where(e => e.Profession.ProfessionName == "Teacher");

                    foreach (var teacher in teachers)
                    {
                        Console.WriteLine($"{teacher.FirstName} {teacher.LastName}");

                        if (teacher.Subjects != null && teacher.Subjects.Any())
                        {
                            Console.WriteLine("Subjects taught:");
                            foreach (var subject in teacher.Subjects)
                            {
                                Console.WriteLine($"- {subject.SubjectName}");  // Adjust property based on your Subject class
                            }
                        }
                        else
                        {
                            Console.WriteLine("No subjects assigned.");
                        }

                        Console.WriteLine();  // Add a blank line between teachers for clarity
                    }

                    Console.WriteLine($"Number of teachers: {teachers.Count()}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occured: {ex.Message}");
            }
        }
        public static void GetAllStudents()
        {
            try
            {
                Console.WriteLine("Choose how you want to sort the students:");
                Console.WriteLine("1. Sort by first name ascending");
                Console.WriteLine("2. Sort by first name descending");
                Console.WriteLine("3. Sort by last name ascending");
                Console.WriteLine("4. Sort by last name descending");

                string choice = Console.ReadLine();

                using (var context = new IndividuelltProjektEgzonContext())
                {
                    IEnumerable<Student> sortedStudents;

                    switch (choice)
                    {
                        case "1":
                            sortedStudents = context.Students.OrderBy(s => s.FirstName).ToList();
                            break;

                        case "2":
                            sortedStudents = context.Students.OrderByDescending(s => s.FirstName).ToList();
                            break;

                        case "3":
                            sortedStudents = context.Students.OrderBy(s => s.LastName).ToList();
                            foreach (var student in sortedStudents)
                            {
                                Console.WriteLine($"{student.LastName} {student.FirstName}");
                            }
                            return; // Returnera efter utskrift för att undvika dublett-utskrift

                        case "4":
                            sortedStudents = context.Students.OrderByDescending(s => s.LastName).ToList();
                            foreach (var student in sortedStudents)
                            {
                                Console.WriteLine($"{student.LastName} {student.FirstName}");
                            }
                            return; // Returnera efter utskrift för att undvika dublett-utskrift

                        default:
                            Console.WriteLine("Invalid choice. The default option is sorting by first name ascending.");
                            sortedStudents = context.Students.OrderBy(s => s.FirstName).ToList();
                            break;
                    }

                    foreach (var student in sortedStudents)
                    {
                        Console.WriteLine($"{student.FirstName} {student.LastName}");
                    }
                }
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Invalid input. Please enter a number between 1 and 4.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occured: {ex.Message}");
            }
        }
        public static void GetStudentsByClass()
        {
            try
            {
                using (var context = new IndividuelltProjektEgzonContext())
                {
                    // Hämta alla klasser
                    var classes = context.Classes.ToList();

                    Console.WriteLine("Choose a class:");
                    for (int i = 0; i < classes.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {classes[i].ClassName}");
                    }

                    int classChoice = int.Parse(Console.ReadLine());

                    if (classChoice < 1 || classChoice > classes.Count)
                    {
                        Console.WriteLine("Invalid choice. Please try again.");
                        return;
                    }

                    var selectedClass = classes[classChoice - 1];

                    // Hämta elever i den valda klassen
                    var studentsInClass = context.Students
                        .Where(s => s.ClassId == selectedClass.ClassId)
                        .ToList();

                    Console.WriteLine($"Students in {selectedClass.ClassName}:");
                    foreach (var student in studentsInClass)
                    {
                        Console.WriteLine($"{student.FirstName} {student.LastName} - {student.Class.ClassName}");
                    }
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
        public static void AddEmployee()
        {
            try
            {
                using (var context = new IndividuelltProjektEgzonContext())
                {
                    // Hämta och visa alla professioner
                    var professions = context.Professions.ToList();
                    Console.WriteLine("Available professions:");
                    foreach (var p in professions)
                    {
                        Console.WriteLine($"{p.ProfessionId}: {p.ProfessionName}");
                    }

                    Console.WriteLine("Enter the profession ID for the new employee:");
                    if (!int.TryParse(Console.ReadLine(), out int professionId) || !professions.Any(p => p.ProfessionId == professionId))
                    {
                        Console.WriteLine("Invalid profession ID. Please try again.");
                        return;
                    }

                    Console.WriteLine("Enter the first name for the new employee:");
                    string firstName = Console.ReadLine();

                    Console.WriteLine("Enter the last name for the new employee:");
                    string lastName = Console.ReadLine();

                    // Kontrollera om den valda professionen existerar
                    var profession = professions.FirstOrDefault(p => p.ProfessionId == professionId);
                    if (profession == null)
                    {
                        Console.WriteLine("The profession ID could not be found. Please try again.");
                        return;
                    }

                    // Skapa en ny Employee och spara till databasen
                    var newEmployee = new Employee
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        ProfessionId = professionId
                    };

                    context.Employees.Add(newEmployee);
                    context.SaveChanges();

                    Console.WriteLine("The new employee has been added to the database.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while saving the new employee: {ex.Message}");
            }
        }

        //public static void GetAllGrades() // Sparar till eventuellt nästa labb
        //{
        //    try
        //    {
        //        using (var context = new IndividuelltProjektEgzonContext())
        //        {
        //            // Hämta alla betyg med relaterad student och ämne
        //            var allGrades = context.Grades
        //                .Include(g => g.Student)   // Hämtar den relaterade studenten
        //                .Include(g => g.Subject)   // Hämtar det relaterade ämnet
        //                .ToList();                 // Hämta till en lista

        //            // Skriv ut betygen
        //            foreach (var grade in allGrades)
        //            {
        //                string studentName = grade.Student.FirstName + " " + grade.Student.LastName;
        //                string subjectName = grade.Subject.SubjectName;
        //                string gradeValue = grade.Grade1;
        //                DateOnly dateAssigned = grade.DateAssigned;

        //                // Skriv ut informationen för varje betyg
        //                Console.WriteLine($"{studentName} - {subjectName}: {gradeValue} (Assigned on {dateAssigned.ToShortDateString()})");
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"An error occured: {ex.Message}");
        //    }
        //}
    }
}
