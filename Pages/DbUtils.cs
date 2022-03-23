using System.Data;
using MySql.Data.MySqlClient;

namespace nhl_stenden_cafe.Pages
{
    public class DbUtils
    {
        public IDbConnection GetDbConnection()
        {
            return new MySqlConnection(
                    "Server=127.0.0.1;Port=3306;" +
                    "Database=nhlcafe;" +
                    "Uid=root;Pwd=;"
                    );
        }
    }
}
