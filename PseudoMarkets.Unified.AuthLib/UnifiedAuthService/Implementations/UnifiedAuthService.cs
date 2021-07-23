using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Options;
using PMUnifiedAPI.Models;
using PseudoMarkets.Unified.AuthLib.DataAccess.Shared;
using PseudoMarkets.Unified.AuthLib.Helpers;
using PseudoMarkets.Unified.AuthLib.Models;
using PseudoMarkets.Unified.AuthLib.UnifiedAuthService.Interfaces;

namespace PseudoMarkets.Unified.AuthLib.UnifiedAuthService.Implementations
{
    public class UnifiedAuthService : IUnifiedAuthService
    {
        public Config _serviceConfig;

        public UnifiedAuthService(IOptions<Config> serviceConfig)
        {

        }

        public (Users User, Accounts Account, TokenHelper.TokenStatus) AuthenticateUser(string authToken)
        {
            throw new NotImplementedException();
        }
    }
}
