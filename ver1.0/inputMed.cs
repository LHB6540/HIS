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

namespace ver1._0
{
    public partial class inputMed : Form
    {
        public inputMed()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("请输入药品编号");
            }

            else if (textBox2.Text == "")
            {
                MessageBox.Show("请输入新增库存量");
            }

            else 
            {
                if (textBox1.Text.Substring(0, 1) == "c")
                {
                    string query = "SELECT COUNT(*) FROM chinesemed WHERE  chinesemedid='" + textBox1.Text + "' ";
                    mySql sql = new mySql();
                    int result = sql.count(query);
                    if (result == 0)
                    {
                        MessageBox.Show("没有查询到该中药");
                    }
                    else if (result == 1)
                    {
                        query = "UPDATE  `chinesemed` set ckucun=ckucun+"+textBox2.Text+"   WHERE chinesemedid='" + textBox1.Text + "'";
                        result = sql.update(query);
                        if (result == 1)
                        {
                            MessageBox.Show("更新成功");
                        }
                        else
                        {
                            MessageBox.Show("更新失败");
                        }
                        query = "SELECT chinesemedid,cname,ckucun FROM chinesemed WHERE chinesemedid='" + textBox1.Text + "'";
                        MySqlDataReader mdr = sql.searchData(query);
                        dataGridView2.Rows.Clear();
                        int i = 0;
                        while (mdr.Read())
                        {
                            dataGridView2.Rows.Add();
                            dataGridView2.Rows[i].Cells["Column4"].Value = mdr["chinesemedid"].ToString();
                            dataGridView2.Rows[i].Cells["Column5"].Value = mdr["cname"].ToString();
                            dataGridView2.Rows[i].Cells["Column6"].Value = mdr["ckucun"].ToString();
                        }
                        tabControl1.SelectedTab = tabPage2;



                    }
                    else
                    {
                        MessageBox.Show("数据库错误");
                    }
                }
                else if (textBox1.Text.Substring(0, 1) == "w")
                {
                    string query = "SELECT COUNT(*) FROM westmed WHERE  westmedid='" + textBox1.Text + "' ";
                    mySql sql = new mySql();
                    int result = sql.count(query);
                    if (result == 0)
                    {
                        MessageBox.Show("没有查询到该西药");
                    }
                    else if (result == 1)
                    {
                        query = "UPDATE  `westmed` set wkucun=wkucun+"+textBox2.Text+"  WHERE westmedid='" + textBox1.Text + "'";
                        result = sql.update(query);
                        if (result == 1)
                        {
                            MessageBox.Show("更新成功");
                        }
                        else
                        {
                            MessageBox.Show("更新失败");
                        }
                        query = "SELECT westmedid,wname,wkucun FROM westmed WHERE westmedid='" + textBox1.Text + "'";
                        MySqlDataReader mdr = sql.searchData(query);
                        dataGridView1.Rows.Clear();
                        int i = 0;
                        while (mdr.Read())
                        {
                            dataGridView1.Rows.Add();
                            dataGridView1.Rows[i].Cells["Column1"].Value = mdr["westmedid"].ToString();
                            dataGridView1.Rows[i].Cells["Column2"].Value = mdr["wname"].ToString();
                            dataGridView1.Rows[i].Cells["Column3"].Value = mdr["wkucun"].ToString();
                        }
                        tabControl1.SelectedTab = tabPage1;



                    }
                    else
                    {
                        MessageBox.Show("数据库错误");
                    }




                }
                else
                { MessageBox.Show("药品编号不合法"); }
            }



        }
    }
}
