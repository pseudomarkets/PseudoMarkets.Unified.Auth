using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aerospike.Client;
using PMCommonEntities.Models;
using PMCommonEntities.Models.PseudoMarkets;
using PMUnifiedAPI.Models;
using PseudoMarkets.Unified.AuthLib.DataAccess.Shared.Interfaces;

namespace PseudoMarkets.Unified.AuthLib.DataAccess.RealTime.Implementations
{
    public class RealTimeUsersRepository : IUsersRepository
    {
        private readonly RealTimeDbContext _dbContext;

        public RealTimeUsersRepository(RealTimeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Users GetUserUsing(int userId)
        {
            throw new NotImplementedException();
        }

        public Users GetUserUsing(string username)
        {
            var key = new Key(PseudoMarketsSharedNamespace.ProdNamespace, PseudoMarketsSharedNamespace.SetUsers.Set,
                username);

            var record = _dbContext.GetRecord(key);

            if (record != null && record.bins.Any())
            {
                var password = record.GetString(PseudoMarketsSharedNamespace.SetUsers.PasswordBin);
                var salt =  record.GetValue(PseudoMarketsSharedNamespace.SetUsers.SaltBin);
                var userId = record.GetInt(PseudoMarketsSharedNamespace.SetUsers.LegacyUserIdBin);
                var envId = record.GetInt(PseudoMarketsSharedNamespace.SetUsers.EnvironmentIdBin);

                return new Users()
                {
                    EnvironmentId = (RDSEnums.EnvironmentId) envId,
                    Id = userId,
                    Password = password,
                    Salt = salt as byte[]
                };
            }
            else
            {
                return default;
            }
        }

        public Users GetUserFromTokenUsing(string token)
        {
            throw new NotImplementedException();
        }

        public bool CreateUserUsing(string username, string password, int envId = 1)
        {
            throw new NotImplementedException();
        }
    }
}
