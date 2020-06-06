using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//log  4/4/2020
//1、重写Mysql连接公共类，数据库采用AWS数据库服务RDS，MySQL版本5.7
//2、规范化变量命名，本工程统一采用小驼峰命名规范
//3、重写挂号逻辑，目的：项目工程化，考量可维护性

namespace ver1._0
{
    
    //仅用于提供连接的类，旧的数据库连接
    class OC
    {
        public MySqlConnection OnConInf()
        {
            //远程服务器信息
            string constr = "server=106.13.204.223;Database=Reg;Uid=Reg;password=@gkl508";
            return new MySqlConnection(constr);
        }
    }

    //新版本数据库连接
    //仅用于提供连接的类
    public class mySql
    {
        string ip = "";
        string user = "";
        string passwd = "";
        public mySql()
        {
            /// <summary>
            /// 读取JSON文件
            /// </summary>
            /// <param name="key">JSON文件中的key值</param>
            /// <returns>JSON文件中的value值</returns>
            


            string jsonfile = "c://HIS//config.json";
            using (System.IO.StreamReader file = System.IO.File.OpenText(jsonfile))
            {
                using (JsonTextReader reader = new JsonTextReader(file))
                {
                    JObject o = (JObject)JToken.ReadFrom(reader);
                    ip = o["ip"].ToString();
                    user = o["user"].ToString();
                    passwd = o["passwd"].ToString();
                    reader.Close();
                    file.Close();
                    //return value;
                }
            }
                
               

        }
        public MySqlConnection conn;
        //打开连接
        public bool openConnetction()
        {
            try
            {
                string mysqlInfo = "server="+ip+";User Id="+user+";password="+passwd+";Database=HIS;";
                conn = new MySqlConnection(mysqlInfo);
                conn.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("连接不上服务器，服务器在阿里云，一般是网络原因，请联系管理员Lhb"); break;
                    case 1045:
                        MessageBox.Show("数据库账户或密码错误，还是请联系管理员Lhb"); break;

                }
                return false;
            }
        }

        //关闭连接
        public bool closeConnection()
        {
            try
            {
                conn.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        //增加语句
        public bool addDate(string query)
        {
            if (this.openConnetction() == true)
            {
                try
                {
                    MySqlCommand addData = new MySqlCommand(query, conn);
                    addData.ExecuteNonQuery();
                    //this.closeConnection();
                    return true;
                }
                catch
                {
                    MessageBox.Show("已有相同键值数据");
                    return false;
                }
                finally
                {
                    this.closeConnection();
                }
            }
            else 
            {
                return false;
            }
        }

        //查询语句，未完善，看后面具体需求
        /*private bool selectData(string query)
        {
            if (this.openConnetction() == true)
            {
                MySqlCommand selectDate = new MySqlCommand(query,conn);
                MySqlDataReader dataReader = selectDate.ExecuteReader();
                
            }
        }*/

        //数据库端统计语句
        public int count(string query) 
        {
            if (this.openConnetction() == true)
            {
                //try
                {
                    MySqlCommand count = new MySqlCommand(query, conn);
                    //Console.WriteLine(Convert.ToInt32(count.ExecuteScalar()));
                    return Convert.ToInt32(count.ExecuteScalar()); 
                }
                /*catch
                {
                    
                    MessageBox.Show("数据库连接正常，数据库查询语句异常");
                    return -2;
                }
                finally 
                {
                    this.closeConnection();
                }*/
            }
            else
                return -1;
        }

        //更新
        public int update(string query) 
        {
            if (this.openConnetction() == true)
            {
                try
                {
                    MySqlCommand update = new MySqlCommand(query,conn);
                    update.ExecuteNonQuery();
                    return 1;
                }
                catch 
                {
                    MessageBox.Show("数据库连接是正常的，更新语句错误，开发的锅");
                    return -2;
                    
                }
                finally
                {
                    this.closeConnection();
                }
            }
            else
            {
                return -1;
                //do nothing 
                //openconnection函数中已经有异常机制
            }
        }

        //查询,用于本地统计、读取等其他处理,未关闭连接
        public MySqlDataReader searchData(string query) 
        {
            MySqlDataReader dataReader=null;
            if (this.openConnetction() == true)
            {
                try
                {
                    MySqlCommand searchData = new MySqlCommand(query, conn);
                    dataReader = searchData.ExecuteReader();
                    return dataReader;
                }
                catch
                {
                    MessageBox.Show("查询出错，定位：数据库查询");
                    return dataReader;  
                }
                finally
                {
                    //this.closeConnection();
                }
            }
            else
            {
                return dataReader;
            }
        }

        //查询语句，返回datatable,用于datagridview
        public DataTable countForGridView(string query) 
        {
            
            MySqlCommand command = null;
            //MySqlDataReader mdr = null;
            
            string connectInfo = "server=localhost;User Id=HIS;password=123456;Database=HIS;";
            MySqlConnection sqlConnection = new MySqlConnection(connectInfo);
            command = new MySqlCommand(query, sqlConnection);
            sqlConnection.Open();

            MySqlDataAdapter adapter = new MySqlDataAdapter();
            adapter.SelectCommand = command;

            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }

        //更新修改
        public bool del(string query) 
        {
            if (this.openConnetction())
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch
                {
                    return false;
                }
                finally 
                {
                    this.closeConnection();
                }
            }
            else 
            {
                return false;
            }
        }
      
    }
}
