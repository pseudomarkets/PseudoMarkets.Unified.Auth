using System;
using System.Collections.Generic;
using System.Text;
using Aerospike.Client;

namespace PseudoMarkets.Unified.AuthLib.DataAccess.RealTime.Interfaces
{
    public interface IRealTimeDbContext
    {
        Record GetRecord(Key key);

        void InsertRecord(Key key, params Bin[] bins);

        List<(Key key, Record Record)> QueryRecords(Statement statement);
    }
}
