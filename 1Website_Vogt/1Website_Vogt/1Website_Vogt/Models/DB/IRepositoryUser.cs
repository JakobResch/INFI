using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _1Website_Vogt.Models.DB
{
    public interface IRepositoryUser
    {
        Task ConnectAsync();
        Task DisconnectAsync();

        //CRUD_Operationen ... Create Read Update Delete
        bool InsertAsync(User user);
        bool Delete(int userId);
        bool Update(int userId, User newUserData);
        User GetUser(int userId);
        Task<List<User>> GetAllUsersAsync();
        bool Login(string Username, string Password);
    }
}
