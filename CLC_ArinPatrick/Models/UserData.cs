using Minesweeper_ArinPatrick.Services.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
