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
    public partial class Sigh : Form
    {
        mySql mysql = new mySql();
        public Sigh()
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

            string query = "SELECT COUNT(*) FROM `User`  WHERE `name`='"+name+"' AND `passwd`='"+md5Passwd+"'";
            int userRight=mysql.count(query);
            if (userRight >= 1)
            {
                new registrationSystem().Show();
                this.Hide();
            }
            else if (userRight == 0)
            {
                MessageBox.Show("密码错误") ;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;
            string passwd = textBox2.Text;


            string md5Passwd = md5_passwd(passwd);

            string query = "INSERT INTO `HIS`.`User` (`name`, `passwd`) VALUES ('"+name+"', '"+md5Passwd+"');";
            bool sighResult=mysql.addDate(query);
            if (sighResult == true)
            {
                MessageBox.Show("注册成功"); 
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            new ChangePasswd().Show();
        }
    }
    
}
