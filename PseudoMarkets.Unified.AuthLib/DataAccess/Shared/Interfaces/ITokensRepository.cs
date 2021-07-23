using System;
using System.Collections.Generic;
using System.Text;
using PMUnifiedAPI.Models;

namespace PseudoMarkets.Unified.AuthLib.DataAccess.Shared.Interfaces
{
    public interface ITokensRepository
    {
        Tokens GetTokenUsing(int userId);
    }
}
