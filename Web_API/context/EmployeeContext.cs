using Microsoft.Data.SqlClient;
using System.Data;

namespace Web_API.context
{
    public class EmployeeContext
    {

        private readonly IConfiguration _configuration;
        private readonly string _connectionstring;

        public EmployeeContext(IConfiguration configuration)
        {
            _configuration=configuration;
                _connectionstring = _configuration.GetConnectionString("DefaultConnection");
     
        }

        public IDbConnection CreateConnection() => new SqlConnection(_connectionstring);
      

    }
}
