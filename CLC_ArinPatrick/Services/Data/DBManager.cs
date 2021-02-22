using System.Collections.Generic;
using System.Data;
using System;
using System.Text;
using Minesweeper_ArinPatrick.Models;
using System.Data.SqlClient;

namespace Minesweeper_ArinPatrick.Services.Data
{
    public class DBManager
    {
        //defining our class attribute
        public string dbUserconn { get; set; }

        public DBManager()
        {
            this.dbUserconn = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=dbMinesweeper;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        }

        /// <summary>
        /// Gets all users from the database
        /// </summary>
        /// <returns></returns>
        public IEnumerable<UserModel> GetAllUsers()
        {
           List<UserModel> userList = new List<UserModel>();
           using (SqlConnection conn = new SqlConnection(dbUserconn))
           {
                using (SqlCommand cmd = new SqlCommand("usp_GetAllUsers", conn))
                {
                    //By setting the command type to stored procedure, the first parameter
                    //to the SQL command constructor will be interpreted as the name of a stored procedure
                    //instead of interpreting a command string 
                    //which would usually be the sql query 

                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();

                    //this provides a way of reading a forward only steam of rows from 
                    //a SQL server database
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    //Iterate through all the data rows that were read 
                    while (dataReader.Read())
                    {
                        //Instantiate our EmployeeInfo class creating an object called emp
                        UserModel user = new UserModel();
                        try
                        {
                            user.UserID = Convert.ToInt32(dataReader["UserID"].ToString());
                        }
                        catch
                        {
                            user.UserID = 0;
                        }

                        user.First = dataReader["First"].ToString();
                        user.Last = dataReader["Last"].ToString();
                        user.Gender = dataReader["Gender"].ToString();
                        user.Age = Convert.ToInt32(dataReader["Age"]);
                        user.State = dataReader["State"].ToString();
                        user.Email = dataReader["Email"].ToString();
                        user.Username = dataReader["Username"].ToString();
                        user.Password = dataReader["Password"].ToString();

                        //Add (commit) this row of data to our list
                        userList.Add(user);
                    }
                }
                //even though using should clean this up
                //it is goo practice to close the connection
                conn.Close();
           }
             return userList;
        }

        public UserModel GetUserByID(int userID)
        {
            UserModel user = new UserModel();
            using (SqlConnection conn = new SqlConnection(dbUserconn))
            {
                string findCmd = @"SELECT * FROM Users WHERE UserID = @userID";
              
                using (SqlCommand cmd = new SqlCommand(findCmd, conn))
                {
                    conn.Open();
               
                    SqlDataReader dataReader = cmd.ExecuteReader();

                    //Iterate through all the data rows that were read 
                    while (dataReader.Read())
                    {
                        //Instantiate our usermodel class creating an object called user
                        try
                        {
                            user.UserID = Convert.ToInt32(dataReader["UserID"].ToString());
                        }
                        catch
                        {
                            user.UserID = 0;
                        }

                        user.First = dataReader["First"].ToString();
                        user.Last = dataReader["Last"].ToString();
                        user.Gender = dataReader["Gender"].ToString();
                        user.Age = Convert.ToInt32(dataReader["Age"]);
                        user.State = dataReader["State"].ToString();
                        user.Email = dataReader["Email"].ToString();
                        user.Username = dataReader["Username"].ToString();
                        user.Password = dataReader["Password"].ToString();
                    }
                }
                //even though using should clean this up
                //it is goo practice to close the connection
                conn.Close();
            }
            return user;
        }
        /// <summary>
        /// Gets user based off of given username and password
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool GetUserLogin(UserModel user)
        {
            bool success = false;
            using (SqlConnection conn = new SqlConnection(dbUserconn))
            {
                string sqlStmt = @"SELECT * FROM Users WHERE Username = @username AND Password = @password";

                using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                {
                    cmd.Parameters.Add("@Username", System.Data.SqlDbType.NVarChar, 25).Value = user.Username;
                    cmd.Parameters.Add("@Password", System.Data.SqlDbType.NVarChar, 25).Value = user.Password;

                    //Iterate through all the data rows that were read 
                    try
                    {
                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        if(reader.HasRows)
                        {
                            success = true;
                        }

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                //even though using should clean this up
                //it is goo practice to close the connection
                conn.Close();
            }
            return success;
        }

        /// <summary>
        /// Adds a user to the database from the model info given
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool AddUser(UserModel user)
        {
            bool isSuccessful = true;
            using (SqlConnection conn = new SqlConnection(dbUserconn))
            {
                // If you didn't wan to use Stored Procedures this is a second example
                // SqlCommand cmd = new SqlCommand(insertCmd, conn)
                // CommandType would not be included
                using (SqlCommand cmd = new SqlCommand("usp_InsertUser", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Open up the stored procedure and review the parameters
                    // required to be passed into the procdure
                    cmd.Parameters.AddWithValue("@First", user.First);
                    cmd.Parameters.AddWithValue("@Last", user.Last);
                    cmd.Parameters.AddWithValue("@Gender", user.Gender);
                    cmd.Parameters.AddWithValue("@Age", user.Age);
                    cmd.Parameters.AddWithValue("@State", user.State);
                    cmd.Parameters.AddWithValue("@Email", user.Email);
                    cmd.Parameters.AddWithValue("@Username", user.Username);
                    cmd.Parameters.AddWithValue("@Password", user.Password);

                    conn.Open();
                    // ExecuteNonQuery used for executing queries that don't return data
                    // Used for update, insert and delete
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
            return isSuccessful;
        }
    }
}
