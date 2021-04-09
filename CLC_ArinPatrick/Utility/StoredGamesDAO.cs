using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
namespace Minesweeper_ArinPatrick.Utility
{
    public class StoredGamesDAO
    {
        public bool SaveGame(GameObject gameObject)
        {
            bool success = false;
           
            String connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=dbMinesweeper;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            String query = "INSERT INTO dbo.Games (GameString, Username) VALUES (@GameString, @Username)";
                

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlCommand.Parameters.Add("@GameString", SqlDbType.Text).Value = gameObject.JSONString;
                    sqlCommand.Parameters.Add("@Username", SqlDbType.NVarChar).Value = gameObject.Username;

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
        public GameObject LoadGame(String username)
        {
            GameObject gameObject = new GameObject(1, "", "");
            String connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=dbMinesweeper;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            String query = "SELECT TOP 1 * FROM dbo.Games" +
                "WHERE Username = (@Username)" +
                "ORDER BY Id DESC";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlCommand.Parameters.Add("@Username", SqlDbType.NVarChar).Value = username;
                    try
                    {
                        sqlConnection.Open();
                        SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                        while(sqlDataReader.Read())
                        {
                            gameObject.Id = sqlDataReader.GetInt32(0);
                            gameObject.JSONString = sqlDataReader.GetString(1);
                            gameObject.Username = sqlDataReader.GetString(2);
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
    }
}
