using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PMUnifiedAPI.Models;
using PseudoMarkets.Unified.AuthLib.DataAccess.Shared.Interfaces;

namespace PseudoMarkets.Unified.AuthLib.DataAccess.Relational.Implementations
{
    public class RelationalTokensRepository : ITokensRepository
    {
        private readonly PseudoMarketsDbContext _dbContext;
        public RelationalTokensRepository(PseudoMarketsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Tokens GetTokenUsing(int userId)
        {
            return _dbContext.Tokens.FirstOrDefault(x => x.UserID == userId);
        }
    }
}
