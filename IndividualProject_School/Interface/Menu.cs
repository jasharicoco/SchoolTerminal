using IndividualProject_School.Data;

//KONSOLLMENY
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

namespace IndividualProject_School.Interface
{
    internal class Menu
    {
        public static void ShowMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Hello and welcome to the School.");
                Console.WriteLine("What would you like to do?\n" +
                                  "1. EF: List teachers in different departments" +
                                  "2. EF: List students and their information\n" +
                                  "3. EF: List all active subjects\n" +
                                  "4. ADO: List all active subjects\n" +
                                  "5. ADO: List all active subjects\n" +
                                  "6. ADO: List all active subjects\n" +
                                  "7. ADO: List all active subjects\n" +
                                  "8. ADO: List all active subjects\n" +
                                  "9. ADO: List all active subjects\n" +
                                  "10. Exit");

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
                        Console.ReadKey();
                        break;

                    case "5":
                        Console.ReadKey();
                        break;

                    case "6":
                        Console.ReadKey();
                        break;

                    case "7":
                        Console.ReadKey();
                        break;

                    case "8":
                        Console.ReadKey();
                        break;

                    case "9":
                        Console.ReadKey();
                        break;

                    case "10":
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
