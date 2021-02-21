using Business_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess_Layer;
namespace Minesweeper_ArinPatrick.Models
{
    public class UserData
    {
        CommWDataAccess userDAL = new CommWDataAccess();

        //Get users from business layer
        public IEnumerable<UserModel> GetAllUsers()
        {
            IEnumerable<UserModel> allusers = userDAL.GetAllUsers();
            return allusers;
        }

        public void AddUser(UserModel user)
        {
            userDAL.AddUser(user);
        }


    }

}
