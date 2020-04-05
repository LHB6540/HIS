using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
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
            string constr = "server=;Database=;Uid=;password=";
            return new MySqlConnection(constr);
        }
    }

    //新版本数据库连接
    //仅用于提供连接的类
    public class mySql
    {
        private MySqlConnection conn;
        //打开连接
        private bool openConnetction()
        {
            try
            {
                string mysqlInfo = "server=;User Id=;password=;Database=;";
                conn = new MySqlConnection(mysqlInfo);
                conn.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("连接不上服务器，服务器在新加坡，一般是网络原因，请联系管理员Lhb"); break;
                    case 1045:
                        MessageBox.Show("数据库账户或密码错误，还是请联系管理员Lhb"); break;

                }
                return false;
            }
        }

        //关闭连接
        private bool closeConnection()
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
                    MessageBox.Show("已有同名账户");
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

        //统计语句
        public int count(string query) 
        {
            if (this.openConnetction() == true)
            {
                try
                {
                    MySqlCommand count = new MySqlCommand(query, conn);
                    //Console.WriteLine(Convert.ToInt32(count.ExecuteScalar()));
                    return Convert.ToInt32(count.ExecuteScalar()); 
                }
                catch
                {
                    
                    MessageBox.Show("数据库连接正常，数据库查询依据异常");
                    return -2;
                }
                finally 
                {
                    this.closeConnection();
                }
            }
            else
                return -1;
        }

        public void update(string query) 
        {
            if (this.openConnetction() == true)
            {
                try
                {
                    MySqlCommand update = new MySqlCommand(query,conn);
                    update.ExecuteNonQuery();
                }
                catch 
                {
                    MessageBox.Show("数据库连接是正常的，更新语句错误，开发的锅");
                }
                finally
                {
                    this.closeConnection();
                }
            }
            else
            { 
                //do nothing 
                //openconnection函数中已经有异常机制
            }
        }
    }
}
