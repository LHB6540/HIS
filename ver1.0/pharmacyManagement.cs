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
    public partial class pharmacyManagement : Form
    {

        public pharmacyManagement()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
            {
                MessageBox.Show("请输入就诊号");
            }
            else
            {
                MySqlCommand cmd = null;
                MySqlDataAdapter dpt = null;
                mySql sql = new mySql();
                DataSet ds = new DataSet();
                DataTable dtb = null;


                sql.openConnetction();
                cmd = new MySqlCommand("SELECT * FROM chufang WHERE regid='"+textBox3.Text+"'", sql.conn);
                dpt = new MySqlDataAdapter(cmd);
                dpt.Fill(ds);
                dtb = ds.Tables[0];
                dataGridView1.DataSource = dtb;


                this.dataGridView1.AutoGenerateColumns = false;

                this.dataGridView1.Columns["Column19"].DataPropertyName = dtb.Columns["painame"].ToString();

                this.dataGridView1.Columns["Column3"].DataPropertyName = dtb.Columns["medid"].ToString();

                this.dataGridView1.Columns["Column1"].DataPropertyName = dtb.Columns["medname"].ToString();
                this.dataGridView1.Columns["Column7"].DataPropertyName = dtb.Columns["meddanwei"].ToString();
                this.dataGridView1.Columns["Column2"].DataPropertyName = dtb.Columns["medshuliang"].ToString();
                
                this.dataGridView1.Columns["Column5"].DataPropertyName = dtb.Columns["medyongfa"].ToString();
                this.dataGridView1.Columns["Column6"].DataPropertyName = dtb.Columns["medyongliang"].ToString();

                this.dataGridView1.Columns["Column4"].DataPropertyName = dtb.Columns["medkaifang"].ToString();
                this.dataGridView1.Columns["Column16"].DataPropertyName = dtb.Columns["meddate"].ToString();
                this.dataGridView1.Columns["painame"].Visible = false;
                this.dataGridView1.Columns["medid"].Visible = false;
                this.dataGridView1.Columns["medname"].Visible = false;
                this.dataGridView1.Columns["meddanwei"].Visible = false;
                this.dataGridView1.Columns["medshuliang"].Visible = false;
                this.dataGridView1.Columns["medyongfa"].Visible = false;
                this.dataGridView1.Columns["medyongliang"].Visible = false;
                this.dataGridView1.Columns["medkaifang"].Visible = false;
                this.dataGridView1.Columns["meddate"].Visible = false;
                this.dataGridView1.Columns["regid"].Visible = false;
                this.dataGridView1.Columns["paid"].Visible = false;
                this.dataGridView1.Columns["medjiage"].Visible = false;
                this.dataGridView1.Columns["medzongjia"].Visible = false;
                this.dataGridView1.Columns["medclass"].Visible = false;
                this.dataGridView1.Columns["meddoctor"].Visible = false;

                sql.closeConnection();



            }
        }

        

        private void button3_Click(object sender, EventArgs e)
        {

            if (textBox1.Text == "")
            {
                MySqlCommand cmd = null;
                MySqlDataAdapter dpt = null;
                mySql sql = new mySql();
                DataSet ds = new DataSet();
                DataTable dtb = null;

                MySqlCommand cmd1 = null;
                MySqlDataAdapter dpt1 = null;
                mySql sql1 = new mySql();
                DataSet ds1 = new DataSet();
                DataTable dtb1 = null;

                sql.openConnetction();
                cmd = new MySqlCommand("SELECT * FROM westmed WHERE 1", sql.conn);
                dpt = new MySqlDataAdapter(cmd);
                dpt.Fill(ds);
                dtb = ds.Tables[0];
                dataGridView4.DataSource = dtb;


                this.dataGridView4.AutoGenerateColumns = false;

                this.dataGridView4.Columns["wNum1"].DataPropertyName = dtb.Columns["westmedid"].ToString();

                this.dataGridView4.Columns["wName1"].DataPropertyName = dtb.Columns["wname"].ToString();

                this.dataGridView4.Columns["wGuiGe1"].DataPropertyName = dtb.Columns["wguige"].ToString();
                this.dataGridView4.Columns["wYaoJia1"].DataPropertyName = dtb.Columns["wyaojia"].ToString();
                this.dataGridView4.Columns["wDanWei1"].DataPropertyName = dtb.Columns["wdanwei"].ToString();

                this.dataGridView4.Columns["wGongXiao1"].DataPropertyName = dtb.Columns["wyaowugongxiao"].ToString();
                this.dataGridView4.Columns["wZhuZhi1"].DataPropertyName = dtb.Columns["wyaowuzhuzhi"].ToString();

                this.dataGridView4.Columns["wShiYong1"].DataPropertyName = dtb.Columns["wshiyongshuoming"].ToString();
                this.dataGridView4.Columns["wKuCun1"].DataPropertyName = dtb.Columns["wkucun"].ToString();

                this.dataGridView4.Columns["wBeiZhu1"].DataPropertyName = dtb.Columns["wbeizhu"].ToString();

                this.dataGridView4.Columns["westmedid"].Visible = false;
                this.dataGridView4.Columns["wname"].Visible = false;
                this.dataGridView4.Columns["wguige"].Visible = false;
                this.dataGridView4.Columns["wyaojia"].Visible = false;
                this.dataGridView4.Columns["wdanwei"].Visible = false;
                this.dataGridView4.Columns["wyaowugongxiao"].Visible = false;
                this.dataGridView4.Columns["wyaowuzhuzhi"].Visible = false;
                this.dataGridView4.Columns["wshiyongshuoming"].Visible = false;
                this.dataGridView4.Columns["wkucun"].Visible = false;
                this.dataGridView4.Columns["wbeizhu"].Visible = false;

                sql.closeConnection();



                sql1.openConnetction();
                cmd1 = new MySqlCommand("SELECT * FROM chinesemed WHERE 1", sql1.conn);
                dpt1 = new MySqlDataAdapter(cmd1);
                dpt1.Fill(ds1);
                dtb1 = ds1.Tables[0];
                dataGridView12.DataSource = dtb1;

                Console.WriteLine("secong fresh");

                this.dataGridView12.AutoGenerateColumns = false;

                this.dataGridView12.Columns["cNum1"].DataPropertyName = dtb1.Columns["chinesemedid"].ToString();

                this.dataGridView12.Columns["cName1"].DataPropertyName = dtb1.Columns["cname"].ToString();

                this.dataGridView12.Columns["cGuiGe1"].DataPropertyName = dtb1.Columns["cguige"].ToString();

                this.dataGridView12.Columns["cJiaGe1"].DataPropertyName = dtb1.Columns["cjiagedanwei"].ToString();

                this.dataGridView12.Columns["cDanWei1"].DataPropertyName = dtb1.Columns["cdanwei"].ToString();

                this.dataGridView12.Columns["cKuCun1"].DataPropertyName = dtb1.Columns["ckucun"].ToString();

                this.dataGridView12.Columns["cZhuJiao1"].DataPropertyName = dtb1.Columns["czhujiao"].ToString();

                this.dataGridView12.Columns["cBeiZhu1"].DataPropertyName = dtb1.Columns["cbeizhu"].ToString();



                this.dataGridView12.Columns["chinesemedid"].Visible = false;

                this.dataGridView12.Columns["cname"].Visible = false;


                this.dataGridView12.Columns["cguige"].Visible = false;


                this.dataGridView12.Columns["cjiagedanwei"].Visible = false;

                this.dataGridView12.Columns["cdanwei"].Visible = false;

                this.dataGridView12.Columns["ckucun"].Visible = false;

                this.dataGridView12.Columns["czhujiao"].Visible = false;


                this.dataGridView12.Columns["cbeizhu"].Visible = false;

                sql1.closeConnection();






            }

            else
            {
                MySqlCommand cmd = null;
                MySqlDataAdapter dpt = null;
                mySql sql = new mySql();
                DataSet ds = new DataSet();
                DataTable dtb = null;

                MySqlCommand cmd1 = null;
                MySqlDataAdapter dpt1 = null;
                mySql sql1 = new mySql();
                DataSet ds1 = new DataSet();
                DataTable dtb1 = null;

                sql.openConnetction();
                cmd = new MySqlCommand("SELECT * FROM westmed WHERE `wname` LIKE '%" + textBox1.Text + "%'", sql.conn);
                dpt = new MySqlDataAdapter(cmd);
                dpt.Fill(ds);
                dtb = ds.Tables[0];
                dataGridView4.DataSource = dtb;


                this.dataGridView4.AutoGenerateColumns = false;

                this.dataGridView4.Columns["wNum1"].DataPropertyName = dtb.Columns["westmedid"].ToString();

                this.dataGridView4.Columns["wName1"].DataPropertyName = dtb.Columns["wname"].ToString();

                this.dataGridView4.Columns["wGuiGe1"].DataPropertyName = dtb.Columns["wguige"].ToString();
                this.dataGridView4.Columns["wYaoJia1"].DataPropertyName = dtb.Columns["wyaojia"].ToString();
                this.dataGridView4.Columns["wDanWei1"].DataPropertyName = dtb.Columns["wdanwei"].ToString();

                this.dataGridView4.Columns["wGongXiao1"].DataPropertyName = dtb.Columns["wyaowugongxiao"].ToString();
                this.dataGridView4.Columns["wZhuZhi1"].DataPropertyName = dtb.Columns["wyaowuzhuzhi"].ToString();

                this.dataGridView4.Columns["wShiYong1"].DataPropertyName = dtb.Columns["wshiyongshuoming"].ToString();
                this.dataGridView4.Columns["wKuCun1"].DataPropertyName = dtb.Columns["wkucun"].ToString();

                this.dataGridView4.Columns["wBeiZhu1"].DataPropertyName = dtb.Columns["wbeizhu"].ToString();

                this.dataGridView4.Columns["westmedid"].Visible = false;
                this.dataGridView4.Columns["wname"].Visible = false;
                this.dataGridView4.Columns["wguige"].Visible = false;
                this.dataGridView4.Columns["wyaojia"].Visible = false;
                this.dataGridView4.Columns["wdanwei"].Visible = false;
                this.dataGridView4.Columns["wyaowugongxiao"].Visible = false;
                this.dataGridView4.Columns["wyaowuzhuzhi"].Visible = false;
                this.dataGridView4.Columns["wshiyongshuoming"].Visible = false;
                this.dataGridView4.Columns["wkucun"].Visible = false;
                this.dataGridView4.Columns["wbeizhu"].Visible = false;

                sql.closeConnection();



                sql1.openConnetction();
                cmd1 = new MySqlCommand("SELECT * FROM chinesemed WHERE `cname` LIKE '%" + textBox1.Text + "%'", sql1.conn);
                dpt1 = new MySqlDataAdapter(cmd1);
                dpt1.Fill(ds1);
                dtb1 = ds1.Tables[0];
                dataGridView12.DataSource = dtb1;

                Console.WriteLine("secong fresh");

                this.dataGridView12.AutoGenerateColumns = false;

                this.dataGridView12.Columns["cNum1"].DataPropertyName = dtb1.Columns["chinesemedid"].ToString();

                this.dataGridView12.Columns["cName1"].DataPropertyName = dtb1.Columns["cname"].ToString();

                this.dataGridView12.Columns["cGuiGe1"].DataPropertyName = dtb1.Columns["cguige"].ToString();

                this.dataGridView12.Columns["cJiaGe1"].DataPropertyName = dtb1.Columns["cjiagedanwei"].ToString();

                this.dataGridView12.Columns["cDanWei1"].DataPropertyName = dtb1.Columns["cdanwei"].ToString();

                this.dataGridView12.Columns["cKuCun1"].DataPropertyName = dtb1.Columns["ckucun"].ToString();

                this.dataGridView12.Columns["cZhuJiao1"].DataPropertyName = dtb1.Columns["czhujiao"].ToString();

                this.dataGridView12.Columns["cBeiZhu1"].DataPropertyName = dtb1.Columns["cbeizhu"].ToString();



                this.dataGridView12.Columns["chinesemedid"].Visible = false;

                this.dataGridView12.Columns["cname"].Visible = false;


                this.dataGridView12.Columns["cguige"].Visible = false;


                this.dataGridView12.Columns["cjiagedanwei"].Visible = false;

                this.dataGridView12.Columns["cdanwei"].Visible = false;

                this.dataGridView12.Columns["ckucun"].Visible = false;

                this.dataGridView12.Columns["czhujiao"].Visible = false;


                this.dataGridView12.Columns["cbeizhu"].Visible = false;

                sql1.closeConnection();

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                string mednum = dataGridView1.Rows[i].Cells["Column3"].Value.ToString();
                string query = "";
                if (mednum.Substring(0, 1) == "c")
                {
                    query = "update chinesemed set ckucun = ckucun - 1 WHERE chinesemedid='"+mednum+"' ";

                }
                else
                {
                    query = "update westmed set wkucun = wkucun - 1 WHERE westmedid='" + mednum + "'";
                }
                mySql sql = new mySql();
                sql.update(query);

            }

            MessageBox.Show("发药成功");
            dataGridView1.DataSource = null;
           
            dataGridView1.Rows.Clear();
            textBox3.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new inputMed().Show();
        }
    }
}
