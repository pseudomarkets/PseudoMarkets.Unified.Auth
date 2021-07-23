using System;
using System.Collections.Generic;
using System.Text;
using Aerospike.Client;
using Microsoft.Extensions.Options;
using PseudoMarkets.Unified.AuthLib.DataAccess.RealTime.Interfaces;
using PseudoMarkets.Unified.AuthLib.DataAccess.Shared;
using PseudoMarkets.Unified.AuthLib.Models;

namespace PseudoMarkets.Unified.AuthLib.DataAccess.RealTime.Implementations
{
    public class RealTimeDbContext : IRealTimeDbContext
    {
        private readonly IOptions<Config> _dataAccessConfig;
        private readonly IAerospikeClient _aerospikeClient;

        private Policy _readPolicy = new Policy()
        {
            sendKey = true
        };

        private QueryPolicy _queryPolicy = new QueryPolicy()
        {
            sendKey = true
        };

        private WritePolicy _writePolicy = new WritePolicy();

        public RealTimeDbContext(IOptions<Config> config)
        {
            _dataAccessConfig = config;

            _aerospikeClient =
                new AerospikeClient(_dataAccessConfig.Value.AuthDataAccessConfig.RealTimeDataSource.Hostname,
                    _dataAccessConfig.Value.AuthDataAccessConfig.RealTimeDataSource.Port);
        }

        public Record GetRecord(Key key)
        {
            return _aerospikeClient.Get(_readPolicy, key);
        }

        public void InsertRecord(Key key, params Bin[] bins)
        {
            _aerospikeClient.Put(_writePolicy, key, bins);
        }

        public List<(Key key, Record Record)> QueryRecords(Statement statement)
        {
            var recordsList = new List<(Key key, Record Record)>();

            var records = _aerospikeClient.Query(_queryPolicy, statement);

            while (records != null && records.Next())
            {
                var record = records?.Record;

                var key = records?.Key;

                recordsList.Add((key, record));
            }

            return recordsList;
        }
    }
}
