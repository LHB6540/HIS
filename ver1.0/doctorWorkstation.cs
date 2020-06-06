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
    public partial class doctorWorkstation : Form
    {
        string doctorID = "";
        string today = System.DateTime.Now.Date.ToString();
        string regID = "";
        string paiID = "";
        string paName = "";
        string classname = "";
        string doctorname = "";
        public doctorWorkstation(string ID)
        {
            InitializeComponent();
            doctorID = ID;
            Console.WriteLine(doctorID);
        }

        void clearPaiInfo()
        {
            textBox91.Text = textBox93.Text = textBox92.Text = textBox94.Text = textBox89.Text = comboBox7.Text =
            textBox88.Text  = textBox90.Text = "";
        }
        void clearPaiReg()
        {
            textBox96.Text = textBox97.Text = textBox98.Text = textBox99.Text = textBox100.Text = textBox101.Text = textBox102.Text =textBox3.Text= "";
        }

        void inputPaiInfo()
        {
           
            clearPaiInfo();
            textBox91.Text = regID;
            textBox93.Text = paName;
            textBox92.Text = paiID;
            mySql sql = new mySql();
            string cardnum = paiID;
            string query = "SELECT COUNT(*) FROM `HIS`.`Patient` WHERE `CardNum`='" + cardnum + "'";
            int countResult = sql.count(query);
            if (countResult >= 1)
            {
                query = "SELECT * FROM `HIS`.`Patient` WHERE `CardNum`='" + cardnum + "'";
                MySqlDataReader searchResult = sql.searchData(query);
                while (searchResult.Read())
                {
                   
                    string paSexNum = searchResult["paSex"].ToString();
                    if (paSexNum == "1") comboBox7.SelectedIndex =1; else comboBox7.SelectedIndex = 0;
                    //paID.Text = searchResult["paID"].ToString();
                    textBox88.Text = searchResult["paAddress"].ToString();
                    textBox90.Text = searchResult["paAllergy"].ToString();
                    textBox94.Text = searchResult["paAge"].ToString();
                    textBox89.Text = searchResult["paPhone"].ToString();
                }
                sql.closeConnection();
            }
        }



        private void textBox18_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabPage7_Click(object sender, EventArgs e)
        {

        }

        private void panel11_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void label38_Click(object sender, EventArgs e)
        {

        }

        private void 电子报告_Click(object sender, EventArgs e)
        {

        }

        private void button21_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            int count = 0;
            int selectindex = 0;
           
            for (int i = 0; i < dataGridView3.Rows.Count; i++)
            {
                DataGridViewCheckBoxCell checkBoxCell = (DataGridViewCheckBoxCell)dataGridView3.Rows[i].Cells["select"];
                Boolean flag = Convert.ToBoolean(checkBoxCell.Value);
                if (flag == true)
                {
                    count++;
                    selectindex = i;
                }
                else
                {
                    
                }
            }
            //Console.WriteLine(count);
            if (count == 0)
            {
                MessageBox.Show("请选择要接诊的病人");
            }
            else if (count > 1)
            {
                MessageBox.Show("不能一次接诊多个病人");
            }
            else
            {
                toolStripReg.Text = regID = dataGridView3.Rows[selectindex].Cells[1].Value.ToString();
                toolStripCardNum.Text = paiID = dataGridView3.Rows[selectindex].Cells[2].Value.ToString();
                toolStripPaiName.Text = paName = dataGridView3.Rows[selectindex].Cells[3].Value.ToString();
                Console.WriteLine(regID+paiID+paName);

                mySql sql = new mySql();
                string query = "UPDATE regandduty SET treatstatus='1' WHERE regID='"+regID+"'";
                sql.update(query);
                fresh();
                tabControl1.SelectedTab = tabpageBingLi;
                clearPaiInfo();
                clearPaiReg();
                inputPaiInfo();
                
            }



        
        }

        


        void fresh()
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

            MySqlCommand cmd2 = null;
            MySqlDataAdapter dpt2 = null;
            mySql sql2 = new mySql();
            DataSet ds2 = new DataSet();
            DataTable dtb2 = null;

            if (doctorID == "")
            {
                MessageBox.Show("系统异常，未获取到当前工作站医生ID");
            }
            else
            {
                sql.openConnetction();
                cmd = new MySqlCommand("SELECT regandduty.regID,regandduty.paiID,patient.paName,regandduty.time FROM regandduty INNER JOIN patient ON regandduty.paiID=patient.CardNum WHERE regandduty.doctorID='" + doctorID + "' AND regandduty.`data`='" + today + "' AND regandduty.treatstatus='0' AND regandduty.ifUse='1' ORDER BY regandduty.time ", sql.conn);
                dpt = new MySqlDataAdapter(cmd);
                dpt.Fill(ds);
                dtb = ds.Tables[0];
                dataGridView3.DataSource = dtb;


                this.dataGridView3.AutoGenerateColumns = false;

                this.dataGridView3.Columns["jiuzhenhao"].DataPropertyName = dtb.Columns["regID"].ToString();

                this.dataGridView3.Columns["cardnum"].DataPropertyName = dtb.Columns["paiID"].ToString();

                this.dataGridView3.Columns["paiName"].DataPropertyName = dtb.Columns["paName"].ToString();

                this.dataGridView3.Columns["regTime"].DataPropertyName = dtb.Columns["time"].ToString();

                this.dataGridView3.Columns["regID"].Visible = false;

                this.dataGridView3.Columns["paiID"].Visible = false;

                this.dataGridView3.Columns["paName"].Visible = false;

                this.dataGridView3.Columns["time"].Visible = false;

                /*for (int i = 0; i < dataGridView3.Rows.Count-1; i++)
                {
                    dataGridView3.Rows[i].Cells[5].Value = "候诊中";
                }*/
                sql.closeConnection();

                

                sql.openConnetction();
                cmd1 = new MySqlCommand("SELECT regandduty.regID,regandduty.paiID,patient.paName,regandduty.time FROM regandduty INNER JOIN patient ON regandduty.paiID=patient.CardNum WHERE regandduty.doctorID='" + doctorID + "' AND regandduty.`data`='" + today + "' AND regandduty.treatstatus='1' AND regandduty.ifUse='1' ORDER BY regandduty.time ", sql.conn);
                dpt1 = new MySqlDataAdapter(cmd1);
                dpt1.Fill(ds1);
                dtb1 = ds1.Tables[0];
                dataGridView2.DataSource = dtb1;

                Console.WriteLine("secong fresh");

                this.dataGridView2.AutoGenerateColumns = false;

                this.dataGridView2.Columns["regIDIng"].DataPropertyName = dtb.Columns["regID"].ToString();

                this.dataGridView2.Columns["paiIDIng"].DataPropertyName = dtb.Columns["paiID"].ToString();

                this.dataGridView2.Columns["paiNameIng"].DataPropertyName = dtb.Columns["paName"].ToString();

                this.dataGridView2.Columns["timeIng"].DataPropertyName = dtb.Columns["time"].ToString();



                this.dataGridView2.Columns["regID"].Visible = false;

                this.dataGridView2.Columns["paiID"].Visible = false;

                this.dataGridView2.Columns["paName"].Visible = false;

                this.dataGridView2.Columns["time"].Visible = false;


                sql.closeConnection();


                sql.openConnetction();
                cmd2 = new MySqlCommand("SELECT regandduty.regID,regandduty.paiID,patient.paName,regandduty.time FROM regandduty INNER JOIN patient ON regandduty.paiID=patient.CardNum WHERE regandduty.doctorID='" + doctorID + "' AND regandduty.`data`='" + today + "' AND regandduty.treatstatus='2' AND regandduty.ifUse='1' ORDER BY regandduty.time ", sql.conn);
                dpt2 = new MySqlDataAdapter(cmd2);
                dpt2.Fill(ds2);
                dtb2 = ds2.Tables[0];
                dataGridView5.DataSource = dtb2;

                Console.WriteLine("secong fresh");

                this.dataGridView5.AutoGenerateColumns = false;

                this.dataGridView5.Columns["regIDEd"].DataPropertyName = dtb.Columns["regID"].ToString();

                this.dataGridView5.Columns["paiIDEd"].DataPropertyName = dtb.Columns["paiID"].ToString();

                this.dataGridView5.Columns["paiNameEd"].DataPropertyName = dtb.Columns["paName"].ToString();

                this.dataGridView5.Columns["timeEd"].DataPropertyName = dtb.Columns["time"].ToString();


                this.dataGridView5.Columns["regID"].Visible = false;

                this.dataGridView5.Columns["paiID"].Visible = false;

                this.dataGridView5.Columns["paName"].Visible = false;

                this.dataGridView5.Columns["time"].Visible = false;

                sql.closeConnection();


                if (dataGridView3.Rows.Count > 0)
                {
                    dataGridView3.Rows[0].Cells[0].Value = true;
                }













            }
        }

        private void doctorWorkstation_Load(object sender, EventArgs e)
        {
            fresh();

            dataGridView2.Enabled = true;dataGridView5.Enabled = true;dataGridView3.Enabled = true;

            string query = "SELECT class,name FROM classanddoctor WHERE ID='"+doctorID+"'";
            mySql sql = new mySql();
            
            MySqlDataReader mySqlDataReader= sql.searchData(query);

            while (mySqlDataReader.Read())
            {
                classname = mySqlDataReader["class"].ToString();
                doctorname=mySqlDataReader["name"].ToString();
            }
            toolStripStatusLabel2.Text = "科室:" + classname;
            textBox1.Text = classname;
            textBox71.Text = doctorname;
            textBox72.Text = doctorID;
            textBox73.Text = classname;
            textBox84.Text = classname;
            textBox68.Text = doctorID;
            textBox52.Text = doctorname;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int count = 0;
            int selectindex = 0;
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                DataGridViewCheckBoxCell checkBoxCell = (DataGridViewCheckBoxCell)dataGridView2.Rows[i].Cells["selectIng"];
                Boolean flag = Convert.ToBoolean(checkBoxCell.Value);
                if (flag == true)
                {
                    count++;
                    selectindex = i;
                }
                else
                {

                }
            }
            //Console.WriteLine(count);
            if (count == 0)
            {
                MessageBox.Show("请选择要继续诊断的病人");
            }
            else if (count > 1)
            {
                MessageBox.Show("不能一次接诊多个病人");
            }
            else
            {
                toolStripReg.Text=regID = dataGridView2.Rows[selectindex].Cells[1].Value.ToString();
                toolStripCardNum.Text=paiID = dataGridView2.Rows[selectindex].Cells[2].Value.ToString();
                toolStripPaiName.Text = paName = dataGridView2.Rows[selectindex].Cells[3].Value.ToString();
               
                Console.WriteLine(regID + paiID + paName);
                tabControl1.SelectedTab = tabpageBingLi;
                clearPaiInfo();
                clearPaiReg();
                inputPaiInfo();
            }
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void label53_Click(object sender, EventArgs e)
        {

        }

        private void textBox74_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {

        }

        private void button24_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button16_Click(object sender, EventArgs e)
        {
            MessageBox.Show("病历已保存");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPageChuFang;

        }

        private void button23_Click(object sender, EventArgs e)
        {
            if (textBox87.Text == "")
            {
                MessageBox.Show("请输入药品名称的全部或部分，支持模糊查询");
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
                cmd = new MySqlCommand("SELECT * FROM westmed WHERE `wname` LIKE '%"+textBox87.Text+"%'", sql.conn);
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
                cmd1 = new MySqlCommand("SELECT * FROM chinesemed WHERE `cname` LIKE '%"+textBox87.Text+"%'", sql1.conn);
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

        private void button25_Click(object sender, EventArgs e)
        {
            int count = 0;
            int selectindex = 0;
            for (int i = 0; i < dataGridView4.Rows.Count; i++)
            {
                DataGridViewCheckBoxCell checkBoxCell = (DataGridViewCheckBoxCell)dataGridView4.Rows[i].Cells["wSelect"];
                Boolean flag = Convert.ToBoolean(checkBoxCell.Value);
                if (flag == true)
                {
                    count++;
                    selectindex = i;
                }
                else
                {

                }
            }
            //Console.WriteLine(count);
            if (count == 0)
            {
                MessageBox.Show("请选择要使用的药物");
            }
            else if (count > 1)
            {
                MessageBox.Show("不能一次选择多种药物");
            }
            else
            {
                medName.Text = dataGridView4.Rows[selectindex].Cells["wName1"].Value.ToString();
                medJiaGe.Text = dataGridView4.Rows[selectindex].Cells["wYaoJia1"].Value.ToString();
                medNum.Text = dataGridView4.Rows[selectindex].Cells["wNum1"].Value.ToString();
                medDanwei.Text = dataGridView4.Rows[selectindex].Cells["wDanWei1"].Value.ToString();
                numericUpDown1.Maximum = Convert.ToDecimal(dataGridView4.Rows[selectindex].Cells["wKuCun"].Value.ToString());
            }
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void button26_Click(object sender, EventArgs e)
        {
            int count = 0;
            int selectindex = 0;
            for (int i = 0; i < dataGridView12.Rows.Count; i++)
            {
                DataGridViewCheckBoxCell checkBoxCell = (DataGridViewCheckBoxCell)dataGridView12.Rows[i].Cells["cSelect"];
                Boolean flag = Convert.ToBoolean(checkBoxCell.Value);
                if (flag == true)
                {
                    count++;
                    selectindex = i;
                }
                else
                {

                }
            }
            //Console.WriteLine(count);
            if (count == 0)
            {
                MessageBox.Show("请选择要使用的药物");
            }
            else if (count > 1)
            {
                MessageBox.Show("不能一次选择多种药物");
            }
            else
            {
                medName.Text = dataGridView12.Rows[selectindex].Cells["cName1"].Value.ToString();
                medJiaGe.Text = dataGridView12.Rows[selectindex].Cells["cJiaGe1"].Value.ToString();
                medNum.Text = dataGridView12.Rows[selectindex].Cells["cNum1"].Value.ToString();
                medDanwei.Text = dataGridView12.Rows[selectindex].Cells["cDanWei1"].Value.ToString();
                numericUpDown1.Maximum = Convert.ToDecimal(dataGridView12.Rows[selectindex].Cells["cKuCun"].Value.ToString());
            }
        }

        int numerFlag = 0;

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (numerFlag == 0)
            {
                if (medJiaGe.Text == "")
                {
                    MessageBox.Show("获取价格失败，请检查药品数据库");
                }
                else
                {
                    decimal zongjia = Convert.ToDecimal(medJiaGe.Text.Substring(0, medJiaGe.Text.IndexOf('元'))) * numericUpDown1.Value;
                    medCost.Text = zongjia.ToString();
                }
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (numericUpDown1.Value == 0)
            {
                MessageBox.Show("请选择药品数量");
                medUseType.Text = "";
            }
            else
            {
                dataGridView6.Rows.Add();
                int i = dataGridView6.Rows.Count;
                dataGridView6.Rows[i - 1].Cells["Column33"].Value = medNum.Text;
                dataGridView6.Rows[i - 1].Cells["Column23"].Value = medName.Text;
                dataGridView6.Rows[i - 1].Cells["Column24"].Value = medJiaGe.Text;
                dataGridView6.Rows[i - 1].Cells["Column25"].Value = medDanwei.Text;
                dataGridView6.Rows[i - 1].Cells["Column26"].Value = numericUpDown1.Value;
                dataGridView6.Rows[i - 1].Cells["Column30"].Value = medCost.Text + "元";
                dataGridView6.Rows[i - 1].Cells["Column27"].Value = medUseType.Text;
                dataGridView6.Rows[i - 1].Cells["Column28"].Value = medUseCount.Text;
                dataGridView6.Rows[i - 1].Cells["Column31"].Value = medClass.Text;
                dataGridView6.Rows[i - 1].Cells["Column32"].Value = medDoctor.Text;

                numerFlag = 1;


                medNum.Clear();
                medName.Clear();
                medJiaGe.Clear();
                medDanwei.Clear();
                numericUpDown1.Value = 0;
                medCost.Clear();
                medUseType.Clear();
                medUseCount.Clear();
                medClass.Clear();
                medDoctor.Clear();

                numerFlag = 0;
            }
        }

        private void button22_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabpageBingLi;
            textBox3.Clear();
            for (int i = 0; i < dataGridView6.Rows.Count; i++)
            {
                textBox3.Text = textBox3.Text + "[" + dataGridView6.Rows[i].Cells["Column23"].Value+","+dataGridView6.Rows[i].Cells["Column26"].Value +dataGridView6.Rows[i].Cells["Column25"].Value+","+dataGridView6.Rows[i].Cells["Column27"].Value+","+dataGridView6.Rows[i].Cells["Column28"].Value+ "]"+"    ";
            }
        }

        void reset() 
        {
            clearPaiInfo();
            clearPaiReg();
            dataGridView6.Rows.Clear();
            paName = "";
            paiID = "";
            toolStripCardNum.Text = "";
            toolStripPaiName.Text = "";
            regID = "";
            toolStripReg.Text = "";
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView6.Rows.Count == 0)
            {

                if (MessageBox.Show("未检测到处方，是否继续结束诊断", "无处方警告", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    //插入病历
                    string query = "INSERT INTO `regrec` (`regid`, `doctorid`, `doctorname`, `paiid`, `painame`, `guomin`, `history`, `bodyhea`, `talk`, `now`, `firstres`, `withres`, `class`, `yizhu`, `date`,`chufang`) VALUES ('" + regID + "', '" + doctorID + "', '" + doctorname + "', '" + paiID + "', '" + paName + "', '" + textBox90.Text + "', '" + textBox102.Text + "', '" + textBox101.Text + "', '" + textBox100.Text + "', '" + textBox99.Text + "', '" + textBox98.Text + "', '" + textBox97.Text + "', '" + textBox1.Text + "', '" + textBox96.Text + "', '" + dateTimePicker13.Value.Date.ToString() + "','"+textBox3.Text+"')";
                    mySql sql = new mySql();
                    sql.addDate(query);
                    //修改就诊状态
                    query = "UPDATE regandduty SET treatstatus='2' WHERE regID='" + regID + "'";
                    sql.update(query);

                    
                    fresh();
                    //清空多余内容
                    reset();
                    MessageBox.Show("已完成本次诊断");
                    //页面跳转
                    tabControl1.SelectedTab = tabpageDuiLie;
                }

            }
            else
            {
                //插入病历
                string query = "INSERT INTO `regrec` (`regid`, `doctorid`, `doctorname`, `paiid`, `painame`, `guomin`, `history`, `bodyhea`, `talk`, `now`, `firstres`, `withres`, `class`, `yizhu`, `date`,`chufang`) VALUES ('" + regID + "', '" + doctorID + "', '" + doctorname + "', '" + paiID + "', '" + paName + "', '" + textBox90.Text + "', '" + textBox102.Text + "', '" + textBox101.Text + "', '" + textBox100.Text + "', '" + textBox99.Text + "', '" + textBox98.Text + "', '" + textBox97.Text + "', '" + textBox1.Text + "', '" + textBox96.Text + "', '" + dateTimePicker13.Value.Date.ToString() + "' ,'"+textBox3.Text+"')";

                Console.WriteLine(query);
                mySql sql = new mySql();
                sql.addDate(query);

                //插入处方
                for (int i = 0; i < dataGridView6.Rows.Count; i++)
                {
                     

                    query = "INSERT INTO `chufang` (`regid`, `paid`, `painame`, `medid`, `medname`, `medjiage`, `meddanwei`, `medshuliang`, `medzongjia`, `medyongfa`, `medyongliang`, `medkaifang`, `meddate`, `medclass`, `meddoctor`) VALUES ('"+regID+"', '"+paiID+"', '"+paName+"', '"+dataGridView6.Rows[i].Cells["Column33"].Value.ToString()+ "', '" + dataGridView6.Rows[i].Cells["Column23"].Value.ToString() + "', '" + dataGridView6.Rows[i].Cells["Column24"].Value.ToString() + "', '" + dataGridView6.Rows[i].Cells["Column25"].Value.ToString() + "', '" + dataGridView6.Rows[i].Cells["Column26"].Value.ToString() + "', '" + dataGridView6.Rows[i].Cells["Column30"].Value.ToString() + "', '" + dataGridView6.Rows[i].Cells["Column27"].Value.ToString() + "', '" + dataGridView6.Rows[i].Cells["Column28"].Value.ToString() + "', '"+doctorname+"', '"+System.DateTime.Now.ToString()+ "', '" + dataGridView6.Rows[i].Cells["Column31"].Value.ToString() + "', '" + dataGridView6.Rows[i].Cells["Column32"].Value.ToString() + "')";
                    sql.addDate(query);
                }


                //修改就诊状态
                query = "UPDATE regandduty SET treatstatus='2' WHERE regID='" + regID + "'";
                sql.update(query);
                fresh();
                //清空多余内容
                reset();
                MessageBox.Show("已完成本次诊断");
                //页面跳转
                tabControl1.SelectedTab = tabpageDuiLie;




            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            if (regID == "")
            {
                MessageBox.Show("当前无就诊病人，无法自动填充");
            }
            else
            {
                textBox83.Text = "CS" + regID;
                textBox7.Text = paName;
                comboBox9.SelectedIndex = comboBox7.SelectedIndex;
                textBox8.Text = textBox94.Text;
                textBox2.Text = classname;
                textBox15.Text = regID;
                textBox14.Text = paiID;
                textBox75.Text = doctorname;
                
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (textBox16.Text == "")
            { MessageBox.Show("请输入病历号"); }
            else
            {
                string query = "INSERT INTO `check` (`appnum`, `cardnum`, `regid`, `paname`, `sex`, `age`, `class`, `zhenduan`, `feiyong`, `mudi`, `riqi`, `docname`, `address`, `orders`, `items`) VALUES ('" + textBox83.Text + "', '" + textBox14.Text + "', '" + textBox15.Text + "', '" + textBox7.Text + "', '" + comboBox9.Text + "', '" + textBox8.Text + "', '" + textBox2.Text + "', '" + textBox17.Text + "', '" + textBox16.Text + "元', '" + textBox18.Text + "', '" + dateTimePicker1.Value.Date.ToString() + "', '" + doctorname + "', '" + textBox23.Text + "', '" + textBox57.Text + "', '')";
                mySql sql = new mySql();
                sql.addDate(query);
                MessageBox.Show("申请成功");
            }
                 
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if (dateTimePicker14.Value.Date < dateTimePicker12.Value.Date)
            {
                MessageBox.Show("日期范围错误");
            }
            else if (dateTimePicker14.Value.Date == dateTimePicker12.Value.Date)
            {
                string riqi = dateTimePicker14.Value.Date.ToString();

                MySqlCommand cmd = null;
                MySqlDataAdapter dpt = null;
                mySql sql = new mySql();
                DataSet ds = new DataSet();
                DataTable dtb = null;


                sql.openConnetction();
                Console.WriteLine("SELECT * FROM `check` WHERE `paname` = '" + textBox51.Text + "' AND `cardnum` = '" + textBox54.Text + "' AND `riqi` = '" + riqi + "'");
                cmd = new MySqlCommand("SELECT appnum,paname,riqi FROM `check` WHERE `paname` = '" + textBox51.Text + "' AND `cardnum` = '" + textBox54.Text + "' AND `riqi` = '" + riqi + "'", sql.conn);
                dpt = new MySqlDataAdapter(cmd);
                dpt.Fill(ds);
                dtb = ds.Tables[0];
                dataGridView1.DataSource = dtb;


                this.dataGridView1.AutoGenerateColumns = false;

                this.dataGridView1.Columns["Column18"].DataPropertyName = dtb.Columns["appnum"].ToString();

                this.dataGridView1.Columns["Column2"].DataPropertyName = dtb.Columns["paname"].ToString();

                this.dataGridView1.Columns["Column1"].DataPropertyName = dtb.Columns["riqi"].ToString();


                this.dataGridView1.Columns["appnum"].Visible = false;
                this.dataGridView1.Columns["paname"].Visible = false;
                this.dataGridView1.Columns["riqi"].Visible = false;

            }
            else
            {
                DateTime start = dateTimePicker12.Value.Date;
                DateTime end = dateTimePicker14.Value.Date;

               
                    string riqi = start.ToString();
                    MySqlCommand cmd = null;
                    MySqlDataAdapter dpt = null;
                    mySql sql = new mySql();
                    DataSet ds = new DataSet();
                    DataTable dtb = null;


                    sql.openConnetction();
                    //Console.WriteLine("SELECT * FROM `check` WHERE `paname` = '" + textBox51.Text + "' AND `cardnum` = '" + textBox54.Text + "' AND `riqi` = '" + riqi + "'");
                    cmd = new MySqlCommand("SELECT appnum,paname,riqi FROM `check` WHERE `paname` = '" + textBox51.Text + "' AND `cardnum` = '" + textBox54.Text + "' AND `riqi` BETWEEN '"+start+"' AND '"+end+"'", sql.conn);
                Console.WriteLine("SELECT appnum,paname,riqi FROM `check` WHERE `paname` = '" + textBox51.Text + "' AND `cardnum` = '" + textBox54.Text + "' AND `riqi` BETWEEN '" + start + "' AND '" + end + "'");    
                dpt = new MySqlDataAdapter(cmd);
                    dpt.Fill(ds);
                    dtb = ds.Tables[0];
                    dataGridView1.DataSource = dtb;
                   

                    this.dataGridView1.AutoGenerateColumns = false;

                    this.dataGridView1.Columns["Column18"].DataPropertyName = dtb.Columns["appnum"].ToString();

                    this.dataGridView1.Columns["Column2"].DataPropertyName = dtb.Columns["paname"].ToString();

                    this.dataGridView1.Columns["Column1"].DataPropertyName = dtb.Columns["riqi"].ToString();


                    this.dataGridView1.Columns["appnum"].Visible = false;
                    this.dataGridView1.Columns["paname"].Visible = false;
                    this.dataGridView1.Columns["riqi"].Visible = false;
           

            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            DateTime start = dateTimePicker11.Value.Date;
            DateTime end = dateTimePicker10.Value.Date;

            
            dataGridView9.Rows.Clear();
            dataGridView9.Rows.Add();

            if (start == end)
            {
                
                mySql sql = new mySql();

                //统计总挂号数
                string query = "SELECT COUNT(*) FROM regandduty WHERE doctorID='" + textBox72.Text + "' AND `data`='" + end.ToString() + "' AND ifUse='1'";
                MySqlDataReader mdr = sql.searchData(query);
                while (mdr.Read())
                {
                    dataGridView9.Rows[0].Cells["Column16"].Value = mdr["COUNT(*)"];
                }
                sql.closeConnection();
                //统计超声检查单数
                query = "SELECT COUNT(*) FROM `check` WHERE docname='" + textBox71.Text + "' AND riqi='" + end.ToString() + "' AND appnum LIKE 'CS%' ";
                mdr = sql.searchData(query);
                while (mdr.Read())
                {
                    dataGridView9.Rows[0].Cells["Column12"].Value = mdr["COUNT(*)"];
                }
                sql.closeConnection();



                //12 13 14 15 17
                //统计CT检查单数
                query = "SELECT COUNT(*) FROM `check` WHERE docname='" + textBox71.Text + "' AND riqi='" + end.ToString() + "' AND appnum LIKE 'CT%' ";
                mdr = sql.searchData(query);
                while (mdr.Read())
                {
                    dataGridView9.Rows[0].Cells["Column13"].Value = mdr["COUNT(*)"];
                }
                sql.closeConnection();


                //统计MR检查单数
                query = "SELECT COUNT(*) FROM `check` WHERE docname='" + textBox71.Text + "' AND riqi='" + end.ToString() + "' AND appnum LIKE 'MR%' ";
                mdr = sql.searchData(query);
                while (mdr.Read())
                {
                    dataGridView9.Rows[0].Cells["Column14"].Value = mdr["COUNT(*)"];
                }
                sql.closeConnection();
                //统计化验检查单数
                query = "SELECT COUNT(*) FROM `check` WHERE docname='" + textBox71.Text + "' AND riqi='" + end.ToString() + "' AND appnum LIKE 'HY%' ";
                mdr = sql.searchData(query);
                while (mdr.Read())
                {
                    dataGridView9.Rows[0].Cells["Column15"].Value = mdr["COUNT(*)"];
                }
                sql.closeConnection();


                //统计开具病历数

                query = "SELECT COUNT(*) FROM regrec WHERE doctorid='" + textBox72.Text + "' AND date='" + end.ToString() + "'";
                mdr = sql.searchData(query);
                while (mdr.Read())
                {
                    dataGridView9.Rows[0].Cells["Column17"].Value = mdr["COUNT(*)"];
                }
                sql.closeConnection();
            }
            else if (start > end)
            {
                MessageBox.Show("日期范围错误");
            }
            else
            {
                
                mySql sql = new mySql();

                //统计总挂号数
                string query = "SELECT COUNT(*) FROM regandduty WHERE doctorID='" + textBox72.Text + "' AND `data` BETWEEN '"+start.ToString()+"' AND '"+end.ToString()+"'";
                MySqlDataReader mdr = sql.searchData(query);
                while (mdr.Read())
                {
                    dataGridView9.Rows[0].Cells["Column16"].Value = mdr["COUNT(*)"];
                }
                sql.closeConnection();
                //统计超声检查单数
                query = "SELECT COUNT(*) FROM `check` WHERE docname='" + textBox71.Text + "' AND riqi  BETWEEN '" + start.ToString() + "' AND '" + end.ToString() + "' AND appnum LIKE 'CS%' ";
                mdr = sql.searchData(query);
                while (mdr.Read())
                {
                    dataGridView9.Rows[0].Cells["Column12"].Value = mdr["COUNT(*)"];
                }
                sql.closeConnection();



                //12 13 14 15 17
                //统计CT检查单数
                query = "SELECT COUNT(*) FROM `check` WHERE docname='" + textBox71.Text + "' AND riqi BETWEEN '" + start.ToString() + "' AND '" + end.ToString() + "' AND appnum LIKE 'CT%' ";
                mdr = sql.searchData(query);
                while (mdr.Read())
                {
                    dataGridView9.Rows[0].Cells["Column13"].Value = mdr["COUNT(*)"];
                }
                sql.closeConnection();


                //统计MR检查单数
                query = "SELECT COUNT(*) FROM `check` WHERE docname='" + textBox71.Text + "' AND riqi  BETWEEN '" + start.ToString() + "' AND '" + end.ToString() + "' AND appnum LIKE 'MR%' ";
                mdr = sql.searchData(query);
                while (mdr.Read())
                {
                    dataGridView9.Rows[0].Cells["Column14"].Value = mdr["COUNT(*)"];
                }
                sql.closeConnection();
                //统计化验检查单数
                query = "SELECT COUNT(*) FROM `check` WHERE docname='" + textBox71.Text + "' AND riqi BETWEEN '" + start.ToString() + "' AND '" + end.ToString() + "' AND appnum LIKE 'HY%' ";
                mdr = sql.searchData(query);
                while (mdr.Read())
                {
                    dataGridView9.Rows[0].Cells["Column15"].Value = mdr["COUNT(*)"];
                }
                sql.closeConnection();


                //统计开具病历数

                query = "SELECT COUNT(*) FROM regrec WHERE doctorid='" + textBox72.Text + "' AND date BETWEEN '" + start.ToString() + "' AND '" + end.ToString() + "'";
                mdr = sql.searchData(query);
                while (mdr.Read())
                {
                    dataGridView9.Rows[0].Cells["Column17"].Value = mdr["COUNT(*)"];
                }
                sql.closeConnection();
            }

        }

        private void button18_Click(object sender, EventArgs e)
        {
            DateTime start = dateTimePicker7.Value.Date;
            DateTime end = dateTimePicker6.Value.Date;
            dataGridView7.DataSource = null;
            dataGridView7.Rows.Clear();
            if (start == end)
            {
                string riqi = start.ToString();
                MySqlCommand cmd = null;
                MySqlDataAdapter dpt = null;
                mySql sql = new mySql();
                DataSet ds = new DataSet();
                DataTable dtb = null;


                sql.openConnetction();

                cmd = new MySqlCommand("SELECT class,firstres,COUNT(firstres),date  FROM regrec WHERE class ='" + textBox84.Text + "' AND date='" + end.ToString() + "' GROUP BY firstres,date", sql.conn);

                dpt = new MySqlDataAdapter(cmd);
                dpt.Fill(ds);
                dtb = ds.Tables[0];
                dataGridView7.DataSource = dtb;


                this.dataGridView7.AutoGenerateColumns = false;

                this.dataGridView7.Columns["Columnclass"].DataPropertyName = dtb.Columns["class"].ToString();

                this.dataGridView7.Columns["Columnname"].DataPropertyName = dtb.Columns["firstres"].ToString();

                this.dataGridView7.Columns["Columnnum"].DataPropertyName = dtb.Columns["COUNT(firstres)"].ToString();

                this.dataGridView7.Columns["Columntime"].DataPropertyName = dtb.Columns["date"].ToString();

                this.dataGridView7.Columns["class"].Visible = false;
                this.dataGridView7.Columns["firstres"].Visible = false;
                this.dataGridView7.Columns["COUNT(firstres)"].Visible = false;
                this.dataGridView7.Columns["date"].Visible = false;

            }
            else if (start > end)
            {
                MessageBox.Show("时间范围出错");
            }
            else
            {
                string riqi = start.ToString();
                MySqlCommand cmd = null;
                MySqlDataAdapter dpt = null;
                mySql sql = new mySql();
                DataSet ds = new DataSet();
                DataTable dtb = null;


                sql.openConnetction();

                cmd = new MySqlCommand("SELECT class,firstres,COUNT(firstres),date  FROM regrec WHERE class ='" + textBox84.Text + "' AND date BETWEEN '"+start.ToString()+"' AND '"+end.ToString()+"' GROUP BY firstres,date", sql.conn);

                dpt = new MySqlDataAdapter(cmd);
                dpt.Fill(ds);
                dtb = ds.Tables[0];
                dataGridView7.DataSource = dtb;


                this.dataGridView7.AutoGenerateColumns = false;

                this.dataGridView7.Columns["Columnclass"].DataPropertyName = dtb.Columns["class"].ToString();

                this.dataGridView7.Columns["Columnname"].DataPropertyName = dtb.Columns["firstres"].ToString();

                this.dataGridView7.Columns["Columnnum"].DataPropertyName = dtb.Columns["COUNT(firstres)"].ToString();

                this.dataGridView7.Columns["Columntime"].DataPropertyName = dtb.Columns["date"].ToString();

                this.dataGridView7.Columns["class"].Visible = false;
                this.dataGridView7.Columns["firstres"].Visible = false;
                this.dataGridView7.Columns["COUNT(firstres)"].Visible = false;
                this.dataGridView7.Columns["date"].Visible = false;


            }



        }

        private void button19_Click(object sender, EventArgs e)
        {
            DateTime start = dateTimePicker9.Value.Date;
            DateTime end = dateTimePicker8.Value.Date;
            dataGridView8.DataSource = null;
            dataGridView8.Rows.Clear();
            if (start == end)
            {
                string riqi = start.ToString();
                MySqlCommand cmd = null;
                MySqlDataAdapter dpt = null;
                mySql sql = new mySql();
                DataSet ds = new DataSet();
                DataTable dtb = null;


                sql.openConnetction();

                cmd = new MySqlCommand("SELECT doctorid,firstres,COUNT(firstres),date  FROM regrec WHERE doctorid ='" + textBox68.Text + "' AND date='" + end.ToString() + "' GROUP BY firstres,date", sql.conn);

                dpt = new MySqlDataAdapter(cmd);
                dpt.Fill(ds);
                dtb = ds.Tables[0];
                dataGridView8.DataSource = dtb;


                this.dataGridView8.AutoGenerateColumns = false;

                this.dataGridView8.Columns["docColumn"].DataPropertyName = dtb.Columns["doctorid"].ToString();

                this.dataGridView8.Columns["nameColumn"].DataPropertyName = dtb.Columns["firstres"].ToString();

                this.dataGridView8.Columns["numColumn"].DataPropertyName = dtb.Columns["COUNT(firstres)"].ToString();

                this.dataGridView8.Columns["dateColumn"].DataPropertyName = dtb.Columns["date"].ToString();

                this.dataGridView8.Columns["doctorid"].Visible = false;
                this.dataGridView8.Columns["firstres"].Visible = false;
                this.dataGridView8.Columns["COUNT(firstres)"].Visible = false;
                this.dataGridView8.Columns["date"].Visible = false;

            }
            else if (start > end)
            {
                MessageBox.Show("时间范围出错");
            }
            else
            {
                string riqi = start.ToString();
                MySqlCommand cmd = null;
                MySqlDataAdapter dpt = null;
                mySql sql = new mySql();
                DataSet ds = new DataSet();
                DataTable dtb = null;


                sql.openConnetction();

                cmd = new MySqlCommand("SELECT doctorid,firstres,COUNT(firstres),date  FROM regrec WHERE doctorid ='" + textBox68.Text + "' AND date BETWEEN '" + start.ToString() + "' AND '" + end.ToString() + "' GROUP BY firstres,date", sql.conn);

                dpt = new MySqlDataAdapter(cmd);
                dpt.Fill(ds);
                dtb = ds.Tables[0];
                dataGridView8.DataSource = dtb;


                this.dataGridView8.AutoGenerateColumns = false;

                this.dataGridView8.Columns["docColumn"].DataPropertyName = dtb.Columns["doctorid"].ToString();

                this.dataGridView8.Columns["nameColumn"].DataPropertyName = dtb.Columns["firstres"].ToString();

                this.dataGridView8.Columns["numColumn"].DataPropertyName = dtb.Columns["COUNT(firstres)"].ToString();

                this.dataGridView8.Columns["dateColumn"].DataPropertyName = dtb.Columns["date"].ToString();

                this.dataGridView8.Columns["doctorid"].Visible = false;
                this.dataGridView8.Columns["firstres"].Visible = false;
                this.dataGridView8.Columns["COUNT(firstres)"].Visible = false;
                this.dataGridView8.Columns["date"].Visible = false;

            }






            }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            

            
        }

        private void button21_Click_1(object sender, EventArgs e)
        {
            dataGridView10.DataSource = null;
            dataGridView10.Rows.Clear();
            if (textBox4.Text == "")
            {
                MessageBox.Show("请输入诊疗卡号");
            }
            else
            {
                string query = "SELECT regid,date,class,firstres FROM regrec WHERE paiid='" + textBox4.Text + "'";

                MySqlCommand cmd = null;
                MySqlDataAdapter dpt = null;
                mySql sql = new mySql();
                DataSet ds = new DataSet();
                DataTable dtb = null;


                sql.openConnetction();

                cmd = new MySqlCommand(query, sql.conn);

                dpt = new MySqlDataAdapter(cmd);
                dpt.Fill(ds);
                dtb = ds.Tables[0];
                dataGridView10.DataSource = dtb;


                this.dataGridView10.AutoGenerateColumns = false;

                this.dataGridView10.Columns["liushui"].DataPropertyName = dtb.Columns["regid"].ToString();

                this.dataGridView10.Columns["date1"].DataPropertyName = dtb.Columns["date"].ToString();

                this.dataGridView10.Columns["keshi"].DataPropertyName = dtb.Columns["class"].ToString();

                this.dataGridView10.Columns["zhenduan"].DataPropertyName = dtb.Columns["firstres"].ToString();

                this.dataGridView10.Columns["regid"].Visible = false;
                this.dataGridView10.Columns["date"].Visible = false;
                this.dataGridView10.Columns["class"].Visible = false;
                this.dataGridView10.Columns["firstres"].Visible = false;

            }

            }

        private void button27_Click(object sender, EventArgs e)
        {

            int count = 0;
            int selectindex = 0;

            for (int i = 0; i < dataGridView10.Rows.Count; i++)
            {
                DataGridViewCheckBoxCell checkBoxCell = (DataGridViewCheckBoxCell)dataGridView10.Rows[i].Cells["selectColumn"];
                Boolean flag = Convert.ToBoolean(checkBoxCell.Value);
                if (flag == true)
                {
                    count++;
                    selectindex = i;
                }
                else
                {

                }
            }
            //Console.WriteLine(count);
            if (count == 0)
            {
                MessageBox.Show("请选择要打开的病历");
            }
            else if (count > 1)
            {
                MessageBox.Show("不能一次打开多个病历");
            }
            else
            {
                string regid = dataGridView10.Rows[selectindex].Cells["liushui"].Value.ToString();
                string query = "SELECT * FROM regrec WHERE regid='"+regid+"'";
                mySql sql = new mySql();
                MySqlDataReader mdr= sql.searchData(query);
                while (mdr.Read())
                {
                    textBox59.Text = mdr["regid"].ToString();
                    textBox61.Text = mdr["painame"].ToString();
                    textBox58.Text = mdr["guomin"].ToString();
                    textBox53.Text = mdr["history"].ToString();
                    textBox50.Text = mdr["bodyhea"].ToString();
                    textBox13.Text = mdr["talk"].ToString();
                    textBox12.Text = mdr["now"].ToString();
                    textBox11.Text = mdr["firstres"].ToString();
                    textBox10.Text = mdr["withres"].ToString();
                    textBox9.Text = mdr["yizhu"].ToString();
                    textBox5.Text = mdr["chufang"].ToString();
                    textBox6.Text = mdr["class"].ToString();
                    dateTimePicker2.Value = Convert.ToDateTime(mdr["date"].ToString());
                }
            


            }

        }

        private void button7_Click(object sender, EventArgs e) 
        {
            string danhao = textBox83.Text; string paname = textBox7.Text; string sex = comboBox9.Text; string age = textBox8.Text; string classname =textBox2.Text; string zhenduan=textBox17.Text; string regid =textBox15.Text; string mudi =textBox18.Text; string cardnum = textBox14.Text; string pay=textBox16.Text; string date = dateTimePicker1.Value.Date.ToString(); string order=textBox57.Text; string yishi = textBox75.Text;
            new printCS(danhao, paname, sex, age, classname, zhenduan, regid, mudi, cardnum, pay, date, order, yishi).Show();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            string regid = textBox91.Text; string name = textBox93.Text; string cardnum = textBox92.Text; string age = textBox94.Text; string phone = textBox89.Text; string sex = comboBox7.Text; string address = textBox88.Text; string classname = textBox1.Text; string guomin = textBox90.Text; string date = dateTimePicker13.Value.ToString(); string history = textBox102.Text; string bodyhea = textBox101.Text; string talk = textBox100.Text; string now = textBox99.Text; string check = textBox98.Text; string withcheck = textBox97.Text; string order = textBox96.Text; string chufang = textBox3.Text;
            new printRegRec(regid, name, cardnum, age, phone, sex, address, classname, guomin, date, history, bodyhea, talk, now, check, withcheck, order, chufang).Show();
        }

        private void Button12_Click(object sender, EventArgs e)
        {
            string danhao = textBox80.Text; string paname = textBox38.Text; string sex = comboBox24.Text; string age = textBox37.Text; string classname = textBox60.Text; string zhenduan = textBox33.Text; string regid = textBox36.Text; string mudi = textBox22.Text; string cardnum = textBox35.Text; string pay = textBox34.Text; string date = dateTimePicker5.Value.Date.ToString(); string order = textBox78.Text; string yishi = textBox79.Text;
            new printHY(danhao, paname, sex, age, classname, zhenduan, regid, mudi, cardnum, pay, date, order, yishi).Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string query = "INSERT INTO `check` (`appnum`, `cardnum`, `regid`, `paname`, `sex`, `age`, `class`, `zhenduan`, `feiyong`, `mudi`, `riqi`, `docname`, `address`, `orders`, `items`) VALUES ('" + textBox83.Text + "', '" + textBox14.Text + "', '" + textBox15.Text + "', '" + textBox7.Text + "', '" + comboBox9.Text + "', '" + textBox8.Text + "', '" + textBox2.Text + "', '" + textBox17.Text + "', '" + textBox16.Text + "', '" + textBox18.Text + "', '" + dateTimePicker1.Value.Date.ToString() + "', '" + doctorname + "', '" + textBox23.Text + "', '" + textBox57.Text + "', '')";
            mySql sql = new mySql();
            sql.addDate(query);
            MessageBox.Show("申请成功");
        }

        private void button11_Click(object sender, EventArgs e)
        {
            string query = "INSERT INTO `check` (`appnum`, `cardnum`, `regid`, `paname`, `sex`, `age`, `class`, `zhenduan`, `feiyong`, `mudi`, `riqi`, `docname`, `address`, `orders`, `items`) VALUES ('" + textBox83.Text + "', '" + textBox14.Text + "', '" + textBox15.Text + "', '" + textBox7.Text + "', '" + comboBox9.Text + "', '" + textBox8.Text + "', '" + textBox2.Text + "', '" + textBox17.Text + "', '" + textBox16.Text + "', '" + textBox18.Text + "', '" + dateTimePicker1.Value.Date.ToString() + "', '" + doctorname + "', '" + textBox23.Text + "', '" + textBox57.Text + "', '')";
            mySql sql = new mySql();
            sql.addDate(query);
            MessageBox.Show("申请成功");
        }

        private void button13_Click(object sender, EventArgs e)
        {
            string query = "INSERT INTO `check` (`appnum`, `cardnum`, `regid`, `paname`, `sex`, `age`, `class`, `zhenduan`, `feiyong`, `mudi`, `riqi`, `docname`, `address`, `orders`, `items`) VALUES ('" + textBox83.Text + "', '" + textBox14.Text + "', '" + textBox15.Text + "', '" + textBox7.Text + "', '" + comboBox9.Text + "', '" + textBox8.Text + "', '" + textBox2.Text + "', '" + textBox17.Text + "', '" + textBox16.Text + "', '" + textBox18.Text + "', '" + dateTimePicker1.Value.Date.ToString() + "', '" + doctorname + "', '" + textBox23.Text + "', '" + textBox57.Text + "', '')";
            mySql sql = new mySql();
            sql.addDate(query);
            MessageBox.Show("申请成功");
        }

        private void button28_Click(object sender, EventArgs e)
        {
            if (regID == "")
            {
                MessageBox.Show("当前无就诊病人，无法自动填充");
            }
            else
            {
                textBox82.Text = "CS" + regID;
                textBox30.Text = paName;
                comboBox21.SelectedIndex = comboBox7.SelectedIndex;
                textBox29.Text = textBox94.Text;
                textBox55.Text = classname;
                textBox28.Text = regID;
                textBox27.Text = paiID;
                textBox77.Text = doctorname;

            }
        }

        private void textBox28_TextChanged(object sender, EventArgs e)
        {

        }

        private void button29_Click(object sender, EventArgs e)
        {
            if (regID == "")
            {
                MessageBox.Show("当前无就诊病人，无法自动填充");
            }
            else
            {
                textBox81.Text = "CS" + regID;
                textBox45.Text = paName;
                comboBox23.SelectedIndex = comboBox7.SelectedIndex;
                textBox44.Text = textBox94.Text;
                textBox56.Text = classname;
                textBox43.Text = regID;
                textBox42.Text = paiID;
                textBox48.Text = doctorname;

            }
        }

        private void button30_Click(object sender, EventArgs e)
        {
            if (regID == "")
            {
                MessageBox.Show("当前无就诊病人，无法自动填充");
            }
            else
            {
                textBox80.Text = "CS" + regID;
                textBox38.Text = paName;
                comboBox24.SelectedIndex = comboBox7.SelectedIndex;
                textBox37.Text = textBox94.Text;
                textBox60.Text = classname;
                textBox36.Text = regID;
                textBox25.Text = paiID;
                textBox79.Text = doctorname;

            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            string danhao = textBox82.Text; string paname = textBox30.Text; string sex = comboBox21.Text; string age = textBox29.Text; string classname = textBox55.Text; string zhenduan = textBox25.Text; string regid = textBox28.Text;string mudi = textBox24.Text; string cardnum = textBox27.Text; string pay = textBox26.Text;string date = dateTimePicker3.Value.Date.ToString(); string order = textBox76.Text; string yishi = textBox77.Text;
            new printCT(danhao, paname, sex, age, classname, zhenduan, regid, mudi, cardnum, pay, date, order, yishi).Show();
        }

        private void button10_Click(object sender, EventArgs e) 
        {
            string danhao = textBox81.Text; string paname = textBox45.Text; string sex = comboBox23.Text;string age = textBox44.Text; string classname = textBox56.Text; string zhenduan = textBox40.Text;string regid = textBox53.Text; string mudi = textBox39.Text; string cardnum = textBox42.Text;string pay = textBox41.Text; string date = dateTimePicker4.Value.Date.ToString(); string order = textBox47.Text; string yishi = textBox48.Text;
            new printMR(danhao, paname, sex, age, classname, zhenduan, regid, mudi, cardnum, pay, date, order, yishi).Show();
        }
    }

    }

