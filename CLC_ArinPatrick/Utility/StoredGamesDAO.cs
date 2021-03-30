using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Minesweeper_ArinPatrick.Utility
{
    public class StoredGamesDAO
    {
        public bool SaveGame(GameObject gameObject)
        {
            bool success = false;
            String connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=dbMinesweeper;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            String query = "INSERT INTO dbo.Games (GameString) VALUES (@GameString)";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlCommand.Parameters.Add("@GameString", SqlDbType.Text).Value = gameObject.JSONString;

                    try
                    {
                        sqlConnection.Open();
                        sqlCommand.ExecuteNonQuery();
                        sqlConnection.Close();
                        success = true;
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine("Failure");
                        Debug.WriteLine(e.Message);
                    }
                }
            }
            return success;
        }

        public GameObject LoadGame()
        {
            GameObject gameObject = new GameObject(1, "");
            String connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=dbMinesweeper;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            String query = "SELECT TOP 1 * FROM dbo.Games ORDER BY ID DESC";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    try
                    {
                        sqlConnection.Open();
                        SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                        while(sqlDataReader.Read())
                        {
                            gameObject.Id = sqlDataReader.GetInt32(0);
                            gameObject.JSONString = sqlDataReader.GetString(1); ;
                        }
                        sqlConnection.Close();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Failure");
                        Debug.WriteLine(e.Message);
                    }
                }
            }
            return gameObject;
        }

        public IEnumerable<GameObject> GetAllGames()
        {
            String connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=dbMinesweeper;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            List<GameObject> boardList = new List<GameObject>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                //not using stored procedures
                string sqlStatement = "SELECT * FROM dbo." +
                    "Games";

                SqlCommand command = new SqlCommand(sqlStatement, conn);

                try
                {
                    conn.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        boardList.Add(new GameObject((int)reader[0],(string)reader[1]));
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                };
                return boardList;
            }
        }

    }
}
