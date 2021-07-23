using System;
using System.Collections.Generic;
using System.Text;
using PMUnifiedAPI.Models;

namespace PseudoMarkets.Unified.AuthLib.DataAccess.Shared.Interfaces
{
    public interface IAccountsRepository
    {
        Accounts GetAccountUsing(int userId);

        Accounts GetAccountFromTokenUsing(string token);
    }
}
