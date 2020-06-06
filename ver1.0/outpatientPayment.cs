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
    public partial class outpatientPayment : Form
    {
        string shoufeiyuanid = "";
        public outpatientPayment(string id)
        {
            InitializeComponent();
            shoufeiyuanid = id;
            textBox7.Text = shoufeiyuanid;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        void yaofei()
        {
            mySql sql = new mySql();
            string query = "SELECT medname,meddanwei,medshuliang,medjiage,medzongjia FROM chufang WHERE regid='"+textBox5.Text+"'";
            MySqlDataReader mdr = sql.searchData(query);
            int i = dataGridView1.Rows.Count;
            while (mdr.Read())
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells["Column1"].Value = mdr["medname"].ToString();
                dataGridView1.Rows[i].Cells["Column2"].Value = mdr["meddanwei"].ToString();
                dataGridView1.Rows[i].Cells["Column3"].Value = mdr["medshuliang"].ToString();
                dataGridView1.Rows[i].Cells["Column4"].Value = mdr["medjiage"].ToString();
                dataGridView1.Rows[i].Cells["Column5"].Value = mdr["medzongjia"].ToString();
                i++;
            }
            sql.closeConnection();
        }

        void allcheck()
        {
            mySql sql = new mySql();
            string query = "SELECT appnum,feiyong FROM `check` WHERE appnum REGEXP '.."+textBox5.Text+"'";
            MySqlDataReader mdr = sql.searchData(query);
            int i = dataGridView1.Rows.Count;
            while (mdr.Read())
            {
                dataGridView1.Rows.Add();
                string type = mdr["appnum"].ToString().Substring(0, 2);
                dataGridView1.Rows[i].Cells["Column1"].Value = type;
                dataGridView1.Rows[i].Cells["Column2"].Value = "次";
                dataGridView1.Rows[i].Cells["Column3"].Value = "1";
                dataGridView1.Rows[i].Cells["Column4"].Value = mdr["feiyong"].ToString()+"/次";
                dataGridView1.Rows[i].Cells["Column5"].Value = mdr["feiyong"].ToString();
                i++;
            }
            sql.closeConnection();
        }

        void CS()
        {
            mySql sql = new mySql();
            string query = "SELECT appnum,feiyong FROM `check` WHERE appnum = 'CS" + textBox5.Text + "'";
            MySqlDataReader mdr = sql.searchData(query);
            int i = dataGridView1.Rows.Count;
            while (mdr.Read())
            {
                dataGridView1.Rows.Add();
                string type = mdr["appnum"].ToString().Substring(0, 2);
                dataGridView1.Rows[i].Cells["Column1"].Value = type;
                dataGridView1.Rows[i].Cells["Column2"].Value = "次";
                dataGridView1.Rows[i].Cells["Column3"].Value = "1";
                dataGridView1.Rows[i].Cells["Column4"].Value = mdr["feiyong"].ToString() + "/次";
                dataGridView1.Rows[i].Cells["Column5"].Value = mdr["feiyong"].ToString();
                i++;
            }
            sql.closeConnection();
        }
        void CT()
        {
            mySql sql = new mySql();
            string query = "SELECT appnum,feiyong FROM `check` WHERE appnum = 'CT" + textBox5.Text + "'";
            MySqlDataReader mdr = sql.searchData(query);
            int i = dataGridView1.Rows.Count;
            while (mdr.Read())
            {
                dataGridView1.Rows.Add();
                string type = mdr["appnum"].ToString().Substring(0, 2);
                dataGridView1.Rows[i].Cells["Column1"].Value = type;
                dataGridView1.Rows[i].Cells["Column2"].Value = "次";
                dataGridView1.Rows[i].Cells["Column3"].Value = "1";
                dataGridView1.Rows[i].Cells["Column4"].Value = mdr["feiyong"].ToString() + "/次";
                dataGridView1.Rows[i].Cells["Column5"].Value = mdr["feiyong"].ToString();
                i++;
            }
            sql.closeConnection();

        }

        void MR()
        {
            mySql sql = new mySql();
            string query = "SELECT appnum,feiyong FROM `check` WHERE appnum = 'MR" + textBox5.Text + "'";
            MySqlDataReader mdr = sql.searchData(query);
            int i = dataGridView1.Rows.Count;
            while (mdr.Read())
            {
                dataGridView1.Rows.Add();
                string type = mdr["appnum"].ToString().Substring(0, 2);
                dataGridView1.Rows[i].Cells["Column1"].Value = type;
                dataGridView1.Rows[i].Cells["Column2"].Value = "次";
                dataGridView1.Rows[i].Cells["Column3"].Value = "1";
                dataGridView1.Rows[i].Cells["Column4"].Value = mdr["feiyong"].ToString() + "/次";
                dataGridView1.Rows[i].Cells["Column5"].Value = mdr["feiyong"].ToString();
                i++;
            }
            sql.closeConnection();
        }

        void HY()
        {
            mySql sql = new mySql();
            string query = "SELECT appnum,feiyong FROM `check` WHERE appnum = 'HY" + textBox5.Text + "'";
            MySqlDataReader mdr = sql.searchData(query);
            int i = dataGridView1.Rows.Count;
            while (mdr.Read())
            {
                dataGridView1.Rows.Add();
                string type = mdr["appnum"].ToString().Substring(0, 2);
                dataGridView1.Rows[i].Cells["Column1"].Value = type;
                dataGridView1.Rows[i].Cells["Column2"].Value = "次";
                dataGridView1.Rows[i].Cells["Column3"].Value = "1";
                dataGridView1.Rows[i].Cells["Column4"].Value = mdr["feiyong"].ToString() + "/次";
                dataGridView1.Rows[i].Cells["Column5"].Value = mdr["feiyong"].ToString();
                i++;
            }
            sql.closeConnection();
        }


        private void button4_Click(object sender, EventArgs e)
        {

            mySql MySQL = new mySql();
            string cmd = "SELECT classanddoctor.`name` FROM regandduty INNER JOIN classanddoctor ON regandduty.doctorID=classanddoctor.ID WHERE regandduty.regID='"+textBox5.Text+"'";
            MySqlDataReader mdr= MySQL.searchData(cmd);
            while (mdr.Read())
            {
                textBox6.Text = mdr["name"].ToString();

            }

            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            if (textBox5.Text == "")
            {
                MessageBox.Show("请输入就诊号");
            }
            else if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("请选择收费项目");
            }
            else
            {
                //全部项目
                if (comboBox1.SelectedIndex == 0)
                {
                    yaofei();
                    allcheck();

                }
                //药费
                if (comboBox1.SelectedIndex == 1)
                {
                    yaofei();
                }

                //全部检查
                if (comboBox1.SelectedIndex == 2)
                {
                    allcheck();
                }
                //超声检查
                if (comboBox1.SelectedIndex == 3)
                {
                    CS();
                }

                //CT检查
                if (comboBox1.SelectedIndex == 4)
                {
                    CT();
                }
                //MR检查

                if (comboBox1.SelectedIndex == 5)
                {
                    MR();
                }
                //化验检查

                if (comboBox1.SelectedIndex == 6)
                {
                    HY();
                }

                decimal sum = 0;
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    string jiage = dataGridView1.Rows[i].Cells["Column5"].Value.ToString();
                    if (jiage.Contains("元") && jiage.IndexOf('元') != 0)
                    {
                        decimal danjia = Convert.ToDecimal(jiage.Substring(0, jiage.IndexOf('元')));
                        sum = sum + danjia;
                    }
                    else if (jiage.Contains("元") && jiage.IndexOf('元') == 0)
                    {
                        sum = sum + 0;
                    }
                    else
                    {
                        decimal danjia = Convert.ToDecimal(jiage);
                        sum = sum + danjia;

                    }
                }
                textBox3.Text = sum.ToString();

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DateTime start = dateTimePicker1.Value.Date;
            DateTime end = dateTimePicker2.Value.Date;
            if (start == end)
            {
                mySql sql = new mySql();
                string query = "SELECT sum( IF (CONTAINS(feiyong,'元'),substr(feiyong,1,LENGTH(feiyong)-3),feiyong) ) allcheck FROM `check` WHERE riqi='"+end.ToString()+"'";
                MySqlDataReader mdr= sql.searchData(query);
                while (mdr.Read())
                {
                    textBox8.Text = mdr["allcheck"].ToString();
                }
                sql.closeConnection();

                query = "SELECT  sum(IF(CONTAINS(medzongjia,'元'),substr(medzongjia,1,LENGTH(medzongjia)-3),medzongjia) ) allyaopin FROM `chufang` WHERE meddate='"+end.ToString()+"'";
                mdr = sql.searchData(query);
                while (mdr.Read())
                {
                    textBox11.Text = mdr["allyaopin"].ToString();
                }
                sql.closeConnection();

                textBox10.Text = (Convert.ToDecimal(textBox8.Text) + Convert.ToDecimal(textBox11.Text)).ToString();


            }
            else if (start > end)
            {
                MessageBox.Show("时间范围错误");
            }
            else
            {
                mySql sql = new mySql();
                string query = "SELECT sum( IF (CONTAINS(feiyong,'元'),substr(feiyong,1,LENGTH(feiyong)-3),feiyong) ) allcheck FROM `check` WHERE riqi BETWEEN '"+start.ToString()+"' AND '"+end.ToString()+"'";
                MySqlDataReader mdr = sql.searchData(query);
                while (mdr.Read())
                {
                    textBox8.Text = mdr["allcheck"].ToString();
                }
                sql.closeConnection();

                query = "SELECT  sum(IF(CONTAINS(medzongjia,'元'),substr(medzongjia,1,LENGTH(medzongjia)-3),medzongjia) ) allyaopin FROM `chufang` WHERE meddate BETWEEN '" + start.ToString() + "' AND '" + end.ToString() + "'";
                mdr = sql.searchData(query);
                while (mdr.Read())
                {
                    textBox11.Text = mdr["allyaopin"].ToString();
                }
                sql.closeConnection();

                textBox10.Text = (Convert.ToDecimal(textBox8.Text) + Convert.ToDecimal(textBox11.Text)).ToString();




            }



        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox5.SelectedIndex == -1)
            {
                MessageBox.Show("请选择缴费方式");
            }
            else 
            {
                if (MessageBox.Show("打印提示", "缴费成功是否打印", MessageBoxButtons.OKCancel) == DialogResult.OK)
                { 
                    //打印
                }
               
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string docname = textBox6.Text;
            string shoufeiyuan = textBox7.Text;
            string zongfeiyong = textBox3.Text;
            string paytype = comboBox5.Text;

            DataTable dataTable = new DataTable();
            //dataTable = (DataTable)dataGridView1.DataSource;
            dataTable.Columns.Add("项目");
            dataTable.Columns.Add("单位");
            dataTable.Columns.Add("数量");
            dataTable.Columns.Add("单价");
            dataTable.Columns.Add("总价");

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                dataTable.Rows.Add();
                dataTable.Rows[i][0] = dataGridView1.Rows[i].Cells["Column1"].Value.ToString();
                dataTable.Rows[i][1] = dataGridView1.Rows[i].Cells["Column2"].Value.ToString();
                dataTable.Rows[i][2] = dataGridView1.Rows[i].Cells["Column3"].Value.ToString();
                dataTable.Rows[i][3] = dataGridView1.Rows[i].Cells["Column4"].Value.ToString();
                dataTable.Rows[i][4] = dataGridView1.Rows[i].Cells["Column5"].Value.ToString();
            }


            new printPay(docname, shoufeiyuan, zongfeiyong, paytype,dataTable ).Show();
        }
    }
}
