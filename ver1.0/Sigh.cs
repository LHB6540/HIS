using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace ver1._0
{
    public partial class sigh : Form
    {
        mySql mysql = new mySql();
        public sigh()
        {
            InitializeComponent();
        }

        private string md5_passwd(string passwd)
        {
            byte[] result = Encoding.Default.GetBytes(passwd);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] newBuffer = md5.ComputeHash(result);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < newBuffer.Length; i++)
            {
                sb.Append(newBuffer[i].ToString("x2"));
            }
            return sb.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string name = textBox1.Text;
            string passwd = textBox2.Text;
            string md5Passwd = md5_passwd(passwd);

            string query = "SELECT COUNT(*) FROM `user`  WHERE `name`='" + name + "' AND `passwd`='" + md5Passwd + "'";
            int userRight = mysql.count(query);
            if (userRight >= 1)
            {

                string useID = name;
                string type = "";

                query = "SELECT type FROM `user`  WHERE `name`='" + name + "' AND `passwd`='" + md5Passwd + "'";
                MySqlDataReader searchResult =mysql.searchData(query);
                while (searchResult.Read())
                {
                    type = searchResult["type"].ToString();
                }
                if (type == "")
                {
                    MessageBox.Show("当前账号无权限，请联系管理员或更换账号");
                }
                else
                {
                    new choose(type, useID).Show();
                    this.Hide();
                }
            }
            else if (userRight == 0)
            {
                MessageBox.Show("密码错误");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new sighIn().Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new changePasswd().Show();
        }
    }

}
