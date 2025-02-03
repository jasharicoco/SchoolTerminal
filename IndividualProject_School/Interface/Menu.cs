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
        //public static void ShowMenu()
        //{
        //    while (true)
        //    {
        //        Console.Clear();
        //        Console.WriteLine("Hello and welcome to the School.");
        //        Console.WriteLine("What would you like to do?\n" +
        //                          "1. List students\n" +
        //                          "2. List students in a specific class\n" +
        //                          "3. Add staff\n" +
        //                          "4. Exit");

        //        string choice = Console.ReadLine();

        //        switch (choice)
        //        {
        //            case "1":
        //                Commands.GetAllStudents();
        //                Console.ReadKey();
        //                break;

        //            case "2":
        //                Commands.GetStudentsByClass();
        //                Console.ReadKey();
        //                break;

        //            case "3":
        //                Commands.AddEmployee();
        //                Console.ReadKey();
        //                break;

        //            case "4":
        //                Console.WriteLine("Exiting the program...");
        //                Console.ReadKey();
        //                return;

        //            default:
        //                Console.WriteLine("Invalid choice. Please try again.");
        //                Console.ReadKey();
        //                break;
        //        }

        //        Console.WriteLine(); // Mellanslag mellan valen
        //    }
        //}
    }
}
