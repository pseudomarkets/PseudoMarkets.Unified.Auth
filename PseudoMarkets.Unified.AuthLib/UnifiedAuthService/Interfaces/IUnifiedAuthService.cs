using System;
using System.Collections.Generic;
using System.Text;
using PMUnifiedAPI.Models;
using PseudoMarkets.Unified.AuthLib.Helpers;

namespace PseudoMarkets.Unified.AuthLib.UnifiedAuthService.Interfaces
{
    public interface IUnifiedAuthService
    {
        public (Users User, Accounts Account, TokenHelper.TokenStatus) AuthenticateUser(string authToken);
    }
}
