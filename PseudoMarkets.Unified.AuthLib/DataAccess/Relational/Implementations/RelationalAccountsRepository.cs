using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using PMUnifiedAPI.Models;
using PseudoMarkets.Unified.AuthLib.DataAccess.Shared.Interfaces;

namespace PseudoMarkets.Unified.AuthLib.DataAccess.Relational.Implementations
{
    public class RelationalAccountsRepository : IAccountsRepository
    {
        private readonly PseudoMarketsDbContext _dbContext;

        public RelationalAccountsRepository(PseudoMarketsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Accounts GetAccountUsing(int userId)
        {
            return _dbContext.Accounts.FirstOrDefault(x => x.UserID == userId);
        }

        public Accounts GetAccountFromTokenUsing(string token)
        {
            var account = _dbContext.Tokens.Where(x => x.Token == token).Join(_dbContext.Accounts,
                    tokens => tokens.UserID, accounts => accounts.UserID, (tokens, accounts) => accounts)
                .FirstOrDefault();

            return account;
        }
    }
}
