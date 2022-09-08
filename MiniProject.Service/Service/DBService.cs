using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using MiniProject.Service.Interface.Service;
using MySql.Data.MySqlClient;

namespace MiniProject.Service.Service
{
    public class DBService : IDBService
    {
        private readonly IDbConnection _db;

        public DBService(IConfiguration configuration)
        {
            _db = new MySqlConnection(configuration.GetConnectionString("ConnectGameDatabase")); ;
        }        

        public async Task<List<T>> GetData<T>(string command, object param)
        {
            List<T> result = (await _db.QueryAsync<T>(command, param)).ToList();
            return result;
        }

        public async Task<int> ModifyData(string command, object param)
        {
            var result = await _db.ExecuteAsync(command, param);
            return result;
        }
        /*public async Task<int> GetAData(string command, object param)
        {
            var result = await _db.(command, param);
            return result;
        }*/
    }
}
