using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AhpilyServer.Tool
{
    public class ConnHelp
    {

        public const string CONNNECTIONSTRING = "DataSource=127.0.0.1;port=3306;Database=mycard;UserId=root;password=123456;";
        public static MySqlConnection Connect()
        {
            MySqlConnection conn = new MySqlConnection(CONNNECTIONSTRING);
            try
            {
                conn.Open();
                return conn;
            }
            catch (Exception e)
            {
                Console.WriteLine("连接数据库出现异常" + e);
                return null;
            }
        }
        public static void CloseConnection(MySqlConnection conn)
        {
            if (conn != null)
                conn.Close();
            else
            {
                Console.WriteLine("不能为空");
            }
        }

    }
}
