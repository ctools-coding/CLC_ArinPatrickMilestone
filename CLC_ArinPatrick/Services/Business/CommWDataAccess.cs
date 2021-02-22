﻿using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using DataAccess_Layer;
using Minesweeper_ArinPatrick.Models;

namespace Minesweeper_ArinPatrick.Services.Business
{
    public class CommWDataAccess
    {
        
        public IEnumerable<UserModel> GetAllUsers()
        {
            DBManager database = new DBManager();
            return database.GetAllUsers();
        }

        /// <summary>
        /// pass the user post data to our Data Access layer
        /// </summary>
        /// <param name="user"></param>
        public bool AddUser(UserModel user)
        {
            DBManager dbmanager = new DBManager();
            return dbmanager.AddUser(user);
        }

        public bool GetUserByUserPass(UserModel user)
        {
            DBManager dbmanager = new DBManager();
            return dbmanager.GetUserLogin(user);
        }

    }
}



