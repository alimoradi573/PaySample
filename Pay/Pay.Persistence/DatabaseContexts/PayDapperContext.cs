
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.Common;

namespace Pay.OvetimePolicies.Persistence.DatabaseContexts
{
    public class PayDapperContext:IPayDapperContext
    {

        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public PayDapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("PayConnection");
        }
         
        public DbConnection GetDbconnection()=> new SqlConnection(_connectionString);
        
    }
}
