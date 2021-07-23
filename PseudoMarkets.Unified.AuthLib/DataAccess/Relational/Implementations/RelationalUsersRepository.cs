using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using PMCommonEntities.Models;
using PMUnifiedAPI.Models;
using PseudoMarkets.Unified.AuthLib.DataAccess.Shared.Interfaces;
using PseudoMarkets.Unified.AuthLib.Helpers;

namespace PseudoMarkets.Unified.AuthLib.DataAccess.Relational.Implementations
{
    public class RelationalUsersRepository : IUsersRepository
    {
        private readonly PseudoMarketsDbContext _dbContext;
        public RelationalUsersRepository(PseudoMarketsDbContext dbDbContext)
        {
            _dbContext = dbDbContext;
        }

        public Users GetUserUsing(int userId)
        {
            return _dbContext.Users.FirstOrDefault(x => x.Id == userId);
        }

        public Users GetUserUsing(string username)
        {
            return _dbContext.Users.FirstOrDefault(x => x.Username == username);
        }

        public Users GetUserFromTokenUsing(string token)
        {
            var user = _dbContext.Tokens.Where(x => x.Token == token)
                .Join(_dbContext.Users, tokens => tokens.UserID, users => users.Id, (tokens, users) => users)
                .FirstOrDefault();

            return user;
        }

        public bool CreateUserUsing(string username, string password, int envId = 1)
        {
            if (!_dbContext.Users.Any(x => string.Equals(x.Username, username, StringComparison.InvariantCultureIgnoreCase)))
            {
                byte[] salt = new byte[128 / 8];
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(salt);
                }

                Users newUser = new Users()
                {
                    Username = username,
                    Password = PasswordHashHelper.GetHash(password, salt),
                    Salt = salt,
                    EnvironmentId = (RDSEnums.EnvironmentId) envId
                };

                _dbContext.Users.Add(newUser);
                _dbContext.SaveChanges();

                Users createdUser = _dbContext.Users.FirstOrDefault(x => x.Username == username);

                if (createdUser != null)
                {
                    Tokens newToken = new Tokens()
                    {
                        UserID = createdUser.Id,
                        Token = TokenHelper.GenerateToken(username, TokenHelper.TokenType.Standard),
                        EnvironmentId = RDSEnums.EnvironmentId.ProductionPrimary
                    };

                    Accounts newAccount = new Accounts()
                    {
                        UserID = createdUser.Id,
                        Balance = 1000000.99,
                        EnvironmentId = RDSEnums.EnvironmentId.ProductionPrimary
                    };

                    _dbContext.Tokens.Add(newToken);
                    _dbContext.Accounts.Add(newAccount);

                    _dbContext.SaveChanges();
                }

                return true;
            }

            return false;
        }
    }
}
