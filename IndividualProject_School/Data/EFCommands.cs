using IndividualProject_School.Models;
using Microsoft.EntityFrameworkCore;

namespace IndividualProject_School.Data
{
    internal class EFCommands
    {
        public static void GetEmployees()
        {
            try
            {
                Console.WriteLine("How'd you like to group the teachers?");
                Console.WriteLine("1. By classes");
                Console.WriteLine("2. By subjects");

                string choice = Console.ReadLine();

                using (var context = new IndividuelltProjektEgzonContext())
                {
                    IEnumerable<Employee> employees = context.Employees
                        .Include(e => e.Profession)
                        .Include(s => s.Subjects)
                        .Include(c => c.Classes)
                        .ToList();

                    IEnumerable<Employee> teachers = employees.Where(e => e.Profession.ProfessionName == "Teacher");

                    switch (choice)
                    {
                        case "1":
                            // Group teachers by classes
                            var groupedByClasses = teachers
                                .SelectMany(t => t.Classes, (teacher, classObj) => new { Teacher = teacher, Class = classObj }) // Flatten teachers and classes
                                .GroupBy(tc => tc.Class.ClassName); // Group by class name

                            // Display the groups
                            foreach (var group in groupedByClasses)
                            {
                                Console.WriteLine($"Class: {group.Key}"); // Output the subject name
                                foreach (var tc in group) // Loop through the teachers for that subject
                                {
                                    Console.WriteLine($"- {tc.Teacher.FirstName} {tc.Teacher.LastName}"); // Output teacher name
                                }
                                Console.WriteLine(); // Add a blank line after each subject
                            }
                            break;

                        case "2":
                            var groupedBySubjects = teachers
                                .SelectMany(t => t.Subjects, (teacher, subject) => new { teacher, subject })
                                .GroupBy(ts => ts.subject.SubjectName)
                                .OrderBy(group => group.Key);

                            foreach (var group in groupedBySubjects)
                            {
                                Console.WriteLine($"Subject: {group.Key}");
                                foreach (var ts in group)
                                {
                                    Console.WriteLine($"- {ts.teacher.FirstName} {ts.teacher.LastName}");
                                }
                                Console.WriteLine();
                            }
                            break;


                        default:
                            Console.WriteLine("Invalid choice.");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
        public static void GetAllStudents()
        {
            try
            {
                using (var context = new IndividuelltProjektEgzonContext())
                {
                    IEnumerable<Student> students = context.Students
                        .Include(c => c.Class)
                        .ToList();

                    IEnumerable<Class> classes = context.Classes;

                    var groupedByClasses = students
                        .GroupBy(s => s.Class.ClassName);

                    foreach (var group in groupedByClasses)
                    {
                        Console.WriteLine($"Class: {group.Key}");
                        foreach (var student in group)
                        {
                            Console.WriteLine($"- {student.StudentId}: {student.FirstName} {student.LastName}, {student.Ssn}");
                        }
                        Console.WriteLine();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occured: {ex.Message}");
            }
        }
        public static void GetActiveSubjects()
        {
            try
            {
                using (var context = new IndividuelltProjektEgzonContext())
                {
                    IEnumerable<Subject> activeSubjects = context.Subjects.Where(s => s.IsActive).OrderBy(s => s.SubjectName).ToList();

                    Console.WriteLine("List of all active subjects:");
                    foreach (var subject in activeSubjects)
                    {
                        Console.WriteLine($"- {subject.SubjectName}");
                    }
                    Console.WriteLine();

                    IEnumerable<Subject> inactiveSubjects = context.Subjects.Where(s => s.IsActive == false).OrderBy(s => s.SubjectName).ToList();

                    Console.WriteLine("List of all inactive subjects:");
                    foreach (var subject in inactiveSubjects)
                    {
                        Console.WriteLine($"- {subject.SubjectName}");
                    }
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occured: {ex.Message}");
            }
        }

    }
}
