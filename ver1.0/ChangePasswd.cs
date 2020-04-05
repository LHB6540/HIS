using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ver1._0
{
    public partial class ChangePasswd : Form
    {
       
        public ChangePasswd()
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
            string firstNew = textBox3.Text;
            string secondNew = textBox4.Text;
            string md5Old = md5_passwd(passwd);
            string md5New = md5_passwd(firstNew);

            if (firstNew != secondNew)
            {
                MessageBox.Show("两次输入的密码不一致");
            }
            else
            {
                mySql mysql = new mySql();
                string query = "SELECT COUNT(*) FROM `User`  WHERE `name`='" + name + "' AND `passwd`='" + md5Old + "'";
                int returnNumber = mysql.count(query);
                if (returnNumber == -2)
                {
                    //do nothing
                }
                else if (returnNumber == -1)
                {
                    //do nothing
                }
                else if (returnNumber == 0)
                {
                    MessageBox.Show("原密码错误");
                }
                else
                {
                    if (returnNumber == 1)
                    {
                        //change number
                        string updatePasswd = "UPDATE `User` set passwd='" + md5New + "' WHERE name='" + name + "'";
                        mysql.update(updatePasswd);
                        MessageBox.Show("改密成功");
                        textBox2.Clear();
                        textBox3.Clear();
                        textBox4.Clear();

                    }
                    else if (returnNumber > 1)
                    {
                        MessageBox.Show("系统缺陷，数据库没有做主键限制，数据库中存在两个或以上的同名用户，无法改密");
                    }
                }

            }
        }
    }
}
