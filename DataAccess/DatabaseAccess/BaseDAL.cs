
using Microsoft.Data.SqlClient;
using System.IO;

using Microsoft.Extensions.Configuration;

namespace DataAccess.DatabaseAccess
{
    public class BaseDAL
    {
        public StockDataProvider dataProvider { get; set; } = null;
        public SqlConnection connection = null;
        public BaseDAL()
        {
            var connectionString = GetConnectionString();
            dataProvider = new StockDataProvider(connectionString);
        }

        public string GetConnectionString()
        {
            string connectionString;
            IConfiguration config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).
                                                            AddJsonFile("appsettings.json", true, true).Build();
            return config["ConnectionString:MyStockDB"];
        }

        public void CloseConnection() => dataProvider.CloseConnection(connection);

    }

}
