using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ver1._0
{
    public partial class sighIn : Form
    {
        public sighIn()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*
             
              string name = textBox1.Text;
            string passwd = textBox2.Text;


            string md5Passwd = md5_passwd(passwd);

            string query = "INSERT INTO `his`.`user` (`name`, `passwd`) VALUES ('" + name + "', '" + md5Passwd + "');";
            bool sighResult = mysql.addDate(query);
            if (sighResult == true)
            {
                MessageBox.Show("注册成功");
            }

             */
            if (textBox2.Text == "")
            {
                MessageBox.Show("请填写姓名");
            }
            else if (textBox3.Text == "")
            {
                MessageBox.Show("请填写性别");
            }
            else if(textBox5.Text=="")
            {
                MessageBox.Show("请填写联系方式");
            }
            else if (textBox6.Text != textBox4.Text)
            {
                MessageBox.Show("两次输入的密码不一致");
            }
            else if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("请选择职位类别");
            }
            else
            {
                string type = (comboBox1.SelectedIndex + 1).ToString();
                md5 jiami = new md5();
                string hadjiami= jiami.md5_passwd(textBox4.Text);

                string query = " SELECT COUNT(*) FROM user WHERE type='"+type+"'";

                mySql sql = new mySql();
                int num=sql.count(query)+1;

                string gonghao = "0" + type + num.ToString();

                query = "INSERT INTO user (`name`, `passwd`,`realname`,`sex`,`type`,`date`,`phone`) VALUES ('" + gonghao + "', '" + hadjiami + "','"+textBox2.Text+"','"+textBox3.Text+"','"+type+"','"+dateTimePicker1.Value.Date.ToString()+"','"+textBox5.Text+"');";

                bool result=sql.addDate(query);

                if (result)
                {
                    MessageBox.Show("注册成功，您的工号是" + gonghao);
                    textBox2.Text = textBox3.Text = textBox4.Text = textBox5.Text = textBox6.Text = "";
                }
                else
                {
                    MessageBox.Show("注册失败，请重试");
                }
             
            }






        }

        private void button2_Click(object sender, EventArgs e)
        {
            new sigh().Show();
            this.Close();
        }
    }
}
