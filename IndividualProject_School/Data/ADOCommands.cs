using Microsoft.Data.SqlClient;

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

        public static void ShowClasses()
        {
            string query = @"SELECT * FROM Classes";
            ExecuteQuery(query, 25);
        }

        // Vår metod för att köra SQL queries mot databasen
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
