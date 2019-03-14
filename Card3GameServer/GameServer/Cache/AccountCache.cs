using AhpilyServer;
using AhpilyServer.Concurrent;
using GameServer.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Cache
{
    /// <summary>
    /// 账号缓存
    ///     简单来说：存账号的
    /// </summary>
    public class AccountCache
    {

        #region  z字典储存帐号密码
        /// <summary>
        /// 账号 对应的  账号数据模型
        /// </summary>
        //private Dictionary<string, AccountModel> accModelDict = new Dictionary<string, AccountModel>();

        ///// <summary>
        ///// 是否存在账号
        ///// </summary>
        ///// <param name="account">账号</param>
        ///// <returns></returns>
        //public bool IsExist(string account)
        //{
        //    return accModelDict.ContainsKey(account);
        //}

        ///// <summary>
        ///// 用来存储账号的id
        /////     后期 是 数据库来处理的
        ///// </summary>
        //private ConcurrentInt id = new ConcurrentInt(-1);

        ///// <summary>
        ///// 创建账号数据模型信息
        ///// </summary>
        ///// <param name="accout">账号</param>
        ///// <param name="password">密码</param>
        //public void Create(string account, string password)
        //{
        //    AccountModel model = new AccountModel(id.Add_Get(), account, password);
        //    accModelDict.Add(model.Account, model);
        //}

        ///// <summary>
        ///// 获取账号对应的数据模型
        ///// </summary>
        ///// <param name="account"></param>
        ///// <returns></returns>
        //public AccountModel GetModel(string account)
        //{
        //    return accModelDict[account];
        //}

        ///// <summary>
        ///// 账号密码是否匹配
        ///// </summary>
        ///// <param name="account"></param>
        ///// <param name="password"></param>
        ///// <returns></returns>
        //public bool IsMatch(string account, string password)
        //{
        //    AccountModel model = accModelDict[account];
        //    return model.Password == password;
        //}

        #endregion

        #region  数据库储存帐号密码
        /// <summary>
        /// 是否存在账号
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool IsExist(MySqlConnection conn, string account)//是否存在帐号
        {
            MySqlDataReader reader = null;
            try
            {
                MySqlCommand cmd = new MySqlCommand("select*from ddzuser where username =@username ", conn);
                cmd.Parameters.AddWithValue("username", account);
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("UserName出现异常" + e);
            }
            finally
            {
                if (reader != null) reader.Close();
            }
            return false;
        }
        /// <summary>
        /// 检查帐号密码是否匹配
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool IsMatch(MySqlConnection conn, string password)// 帐号密码是否正确
        {
            MySqlDataReader reader = null;
            try
            {
                MySqlCommand cmd = new MySqlCommand("select*from ddzuser where  password=@password ", conn);
                cmd.Parameters.AddWithValue("password", password);
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("UserName出现异常" + e);
            }
            finally
            {
                if (reader != null) reader.Close();
            }
            return false;
        }
        /// <summary>
        /// 添加帐号
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public void Create(MySqlConnection conn, string account, string password)//添加帐号 
        {

            try
            {
                MySqlCommand cmd = new MySqlCommand("insert into ddzuser set username =@username , password=@password", conn);
                cmd.Parameters.AddWithValue("username", account);
                cmd.Parameters.AddWithValue("password", password);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine("AddUser出现异常" + e);
            }
        }
        #endregion

        /// <summary>
        /// 账号 对应 连接对象
        /// </summary>
        private Dictionary<string, ClientPeer> accClientDict = new Dictionary<string, ClientPeer>();
        private Dictionary<ClientPeer, string> clientAccDict = new Dictionary<ClientPeer, string>();

        /// <summary>
        /// 是否在线
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public bool IsOnline(string account)
        {
            return accClientDict.ContainsKey(account);
        }

        /// <summary>
        /// 是否在线
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public bool IsOnline(ClientPeer client)
        {
            return clientAccDict.ContainsKey(client);
        }

        /// <summary>
        /// 用户上线
        /// </summary>
        /// <param name="client"></param>
        /// <param name="account"></param>
        public void Online(ClientPeer client, string account)
        {
            accClientDict.Add(account, client);
            clientAccDict.Add(client, account);
        }

        /// <summary>
        /// 下线
        /// </summary>
        /// <param name="client"></param>
        public void Offline(ClientPeer client)
        {
            string account = clientAccDict[client];
            clientAccDict.Remove(client);
            accClientDict.Remove(account);
        }

        /// <summary>
        /// 下线
        /// </summary>
        /// <param name="account"></param>
        public void Offline(string account)
        {
            ClientPeer client = accClientDict[account];
            accClientDict.Remove(account);
            clientAccDict.Remove(client);
        }

        /// <summary>
        /// 获取在线玩家的id
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public int GetId(ClientPeer client)
        {
            string account = clientAccDict[client];
            //AccountModel model = accModelDict[account];
            //return model.Id;
            return GetId(client.MySqlConnection, account);

        }

        private int GetId(MySqlConnection conn, string account)
        {
            MySqlDataReader reader = null;
            try
            {
                //  and password=@password
                MySqlCommand cmd = new MySqlCommand("select*from ddzuser where username =@username ", conn);
                cmd.Parameters.AddWithValue("username", account);
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    int id = reader.GetInt32("id");
                    return id;
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("VerifyUser出现异常" + e);
            }
            finally
            {
                if (reader != null) reader.Close();
            }
            return -1;
        }
    }
}





