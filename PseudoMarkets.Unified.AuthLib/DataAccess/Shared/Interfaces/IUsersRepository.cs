using System;
using System.Collections.Generic;
using System.Text;
using PMUnifiedAPI.Models;

namespace PseudoMarkets.Unified.AuthLib.DataAccess.Shared.Interfaces
{
    interface IUsersRepository
    {
        Users GetUserUsing(int userId);

        Users GetUserUsing(string username);

        Users GetUserFromTokenUsing(string token);

        bool CreateUserUsing(string username, string password, int envId = 1);
    }
}
