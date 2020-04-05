using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SpeechLib;
using System.Speech.Synthesis;
using System.Speech.Recognition;

    //10.24更新日志
    //强化医生选择逻辑，修复相关选项前提条件不足时崩溃
    //改进病历号识别逻辑，适配更多情景
    //优化打印前置条件判断逻辑
    //修复证件类型无法显示的问题

namespace ver1._0
{
    
    public partial class registrationSystem : Form
    {
        //主窗体
        public registrationSystem()
        {
            InitializeComponent();
        }
        
        //点击选项时查询并添加科室类别
      
        private void comboBox1_MouseClick(object sender, MouseEventArgs e)
        {
            comboBox1.Items.Clear();
            OC choosed = new OC();
            MySqlConnection conn = null;
            MySqlCommand command = null;
            MySqlDataReader mdr = null;
            string sql = "SELECT DISTINCT Cod FROM `DoctorInfo` WHERE 1";
            try
            {
                conn = choosed.OnConInf();
                command = new MySqlCommand(sql, conn);
                conn.Open();
                mdr = command.ExecuteReader();
                while (mdr.Read())
                {
                    string cod = mdr["Cod"].ToString();
                    comboBox1.Items.Add(cod);
                }
            }
            catch
            {
                MessageBox.Show("医生数据库连接异常");
            }
            finally
            { conn.Close(); }
        }

        //点击选项时查询并添加科室
        private void comboBox2_MouseClick(object sender, MouseEventArgs e)
        {
            comboBox2.Items.Clear();
            string cod = comboBox1.SelectedItem.ToString();
            OC chooseCate = new OC();
            MySqlConnection conn = null;
            MySqlCommand command = null;
            MySqlDataReader mdr = null;
            string sql = "SELECT DISTINCT Cate FROM `DoctorInfo` WHERE `Cod` = '" + cod + "'";
            try
            {
                conn = chooseCate.OnConInf();
                command = new MySqlCommand(sql, conn);
                conn.Open();
                mdr = command.ExecuteReader();
                while (mdr.Read())
                {
                    string cate = mdr["Cate"].ToString();
                    comboBox2.Items.Add(cate);
                }
            }
            catch
            {
                MessageBox.Show("医生数据库连接异常");
            }
            finally
            { conn.Close(); }

        }

        //点击选项时根据前两项添加医生
        private void comboBox3_MouseClick(object sender, MouseEventArgs e)
        {

            if (comboBox1.SelectedIndex == -1 || comboBox2.SelectedIndex == -1)
            { }
            else
            {
                comboBox3.Items.Clear();
                string cod = comboBox1.SelectedItem.ToString();
                string cate = comboBox2.SelectedItem.ToString();
                OC chooseDname = new OC();
                MySqlConnection conn = null;
                MySqlCommand command = null;
                MySqlDataReader mdr = null;
                string sql = "SELECT Dname FROM `DoctorInfo` WHERE `Cod` = '" + cod + "' AND `Cate` = '" + cate + "'";
                try
                {
                    conn = chooseDname.OnConInf();
                    command = new MySqlCommand(sql, conn);
                    conn.Open();
                    mdr = command.ExecuteReader();
                    while (mdr.Read())
                    {
                        string dnaem = mdr["Dname"].ToString();
                        comboBox3.Items.Add(dnaem);
                    }
                }
                catch
                {
                    MessageBox.Show("医生数据库连接异常");
                }
                finally
                { conn.Close(); }
            }
        }

        //根据前三项信息确定唯一的医生ID和挂号级别,mysql未知bug，第三个筛选条件不能用=，只能使用like代替
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (clearint == 1)
            { }
            else
            {
                if (comboBox2.SelectedIndex == -1)
                {

                }
                else
                {
                    textBox2.Enabled = true;
                    textBox3.Enabled = true;
                    textBox2.Clear();
                    textBox3.Clear();
                    comboBox5.Enabled = true;
                    string cod = comboBox1.SelectedItem.ToString();
                    string cate = comboBox2.SelectedItem.ToString();
                    string name = comboBox3.SelectedItem.ToString();
                    OC chooseDname = new OC();
                    MySqlConnection conn = null;
                    MySqlCommand command = null;
                    MySqlDataReader mdr = null;
                    string sql = "SELECT * FROM `DoctorInfo` WHERE `Cod` = '" + cod + "' AND `Cate` = '" + cate + "' AND `Dname` LIKE '" + name + "'";
                    try
                    {

                        conn = chooseDname.OnConInf();
                        command = new MySqlCommand(sql, conn);
                        conn.Open();
                        mdr = command.ExecuteReader();
                        //textBox2.Text = "ceshi";
                        while (mdr.Read())
                        {
                            string did = mdr["Did"].ToString();
                            string lev = mdr["Dlev"].ToString();
                            textBox2.Text = did;
                            textBox3.Text = lev;
                        }
                    }
                    catch
                    {
                        MessageBox.Show("医生数据库连接异常");
                    }
                    finally
                    { conn.Close(); }
                    textBox2.Enabled = false;
                    textBox3.Enabled = false;
                }
            }
        }

        //根据当前医生信息和日期选择进行挂号情况判断
        private void comboBox5_MouseClick(object sender, MouseEventArgs e)
        {
            comboBox5.Items.Clear();
            //string cod = comboBox1.SelectedItem.ToString();
            //string cate = comboBox2.SelectedItem.ToString();
            //string name = comboBox3.SelectedItem.ToString();
            string id = textBox2.Text;
            OC getduty = new OC();
            MySqlConnection conn = null;
            MySqlCommand command = null;
            MySqlDataReader mdr = null;
            string sql = "SELECT COUNT(*) FROM `Duty` WHERE `Did` = '" + id + "' AND `Date`='" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "'";
            string sqlcancount = "SELECT COUNT(*) FROM `Duty` WHERE `Did` = '" + id + "' AND  `Statu` ='0'  AND `Date`='" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "'";
            string sqlcan = "SELECT Time FROM `Duty` WHERE `Did` = '" + id + "' AND  `Statu` ='0'  AND `Date`='" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "'";
            try
            {
                textBox1.Clear();
                conn = getduty.OnConInf();
                command = new MySqlCommand(sql, conn);
                conn.Open();
                string num = command.ExecuteScalar().ToString();
                //textBox1.Text = num;
                conn.Close();
                if (num == "0")
                {
                    comboBox5.Items.Clear();
                    textBox11.Text = "不开诊";
                    comboBox5.Enabled = false;
                    checkBox1.Enabled = false;
                    textBox1.Enabled = false;
                }
                else
                {
                    textBox5.Clear();
                    textBox11.Text = "开诊";
                    checkBox1.Enabled = true;
                    //textBox1.Enabled = true;
                    comboBox5.Enabled = true;
                    comboBox5.Items.Clear();
                    command = new MySqlCommand(sqlcancount, conn);
                    conn.Open();
                    if (command.ExecuteScalar().ToString() == "0")
                    {
                        comboBox5.Items.Clear();
                        comboBox5.Text = "当前医生当天号已满"; textBox13.Clear(); textBox13.Text = "0";
                        comboBox5.Enabled = false;
                        conn.Close();
                        sqlcancount = "SELECT COUNT(*) FROM `Duty` WHERE `Did` = '" + id + "' AND  `Statu` ='1'  AND `Date`='" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "'";
                        command = new MySqlCommand(sqlcancount, conn);
                        conn.Open();
                        textBox12.Clear();
                        textBox12.Text = command.ExecuteScalar().ToString();
                    }
                    else
                    {
                        textBox13.Clear();
                        textBox13.Text = command.ExecuteScalar().ToString();
                        conn.Close();

                        command = new MySqlCommand(sqlcan, conn);
                        conn.Open();
                        comboBox5.Text = "";

                        mdr = command.ExecuteReader();
                        while (mdr.Read())
                        {
                            string time = mdr["Time"].ToString();
                            comboBox5.Items.Add(time);
                        }

                        conn.Close();
                        sqlcancount = "SELECT COUNT(*) FROM `Duty` WHERE `Did` = '" + id + "' AND  `Statu` ='1'  AND `Date`='" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "'";
                        command = new MySqlCommand(sqlcancount, conn);
                        conn.Open();
                        textBox12.Clear();
                        textBox12.Text = command.ExecuteScalar().ToString();

                    }
                }

            }
            catch
            { }
            finally
            { conn.Close(); }

        }
        //加号选项
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
                textBox1.Enabled = true;
            else
                textBox1.Enabled = false;
        }

        //修复直接选科室崩溃的bug
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1)
                comboBox2.Enabled = false;
            else
                comboBox2.Enabled = true;

        }












        //病历号自动查询以及修改
        //病历号当前状态判断标识符
        public static int IsMakeup = 0;
        //生成病历号
        private void button1_Click(object sender, EventArgs e)
        {
            IsMakeup = 1;
            string ID = System.DateTime.Now.ToString("yyyyMMddHHmmss");
            textBox4.Text = ID;
            //IsMakeup = 0;

        }
        //自动查询已存在的病历号信息
        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            string idnum = textBox4.Text;
            int len = idnum.Length;
            if (len <= 13)
            {
                Console.WriteLine("donothing");
                textBox5.Clear(); textBox6.Clear(); textBox7.Clear();
                textBox8.Clear(); textBox9.Clear(); textBox10.Clear();
            }
            else
            {
                if (IsMakeup == 0)
                {
                    textBox5.Clear(); textBox6.Clear(); textBox7.Clear();
                    textBox8.Clear(); textBox9.Clear(); textBox10.Clear();

                    string Pid = textBox4.Text;
                    OC getPinfo = new OC();
                    MySqlConnection conn = null;
                    MySqlCommand command = null;
                    MySqlDataReader mdr = null;
                    string sql = "SELECT COUNT(*) FROM `Pinfo` WHERE `Pid` = '" + Pid + "'";
                    string sqlget = "SELECT * FROM `Pinfo` WHERE `Pid` = '" + Pid + "'";
                    try
                    {
                        conn = getPinfo.OnConInf();
                        command = new MySqlCommand(sql, conn);
                        conn.Open();
                        string num = command.ExecuteScalar().ToString();
                        if (num == "0")
                        {
                            if (IsMakeup == 0)
                            {
                                MessageBox.Show("未查询到此病历号，请确认病历号是否填写正确");
                            }
                        }

                        else
                        {
                            conn.Close();
                            command = new MySqlCommand(sqlget, conn);
                            conn.Open();
                            mdr = command.ExecuteReader();

                            while (mdr.Read())
                            {
                                string pname = mdr["Pname"].ToString(); textBox5.Text = pname;
                                string ps = mdr["Ps"].ToString(); textBox7.Text = ps;
                                string pbo = mdr["PBo"].ToString(); dateTimePicker2.Value = Convert.ToDateTime(pbo);
                                string page = mdr["Page"].ToString(); textBox8.Text = page;
                                string pcall = mdr["Pcall"].ToString(); textBox9.Text = pcall;
                                string ptype = mdr["Ptype"].ToString(); 
                                if (ptype == "1") { comboBox4.SelectedIndex = 0; }
                                else if (ptype == "2") { comboBox4.SelectedIndex = 1; }
                                else if (ptype == "3") { comboBox4.SelectedIndex = 2; }
                                else if (ptype == "4") { comboBox4.SelectedIndex = 3; }

                                string pnum = mdr["Pnum"].ToString(); textBox6.Text = pnum;
                                string paddress = mdr["Paddress"].ToString(); textBox10.Text = paddress;
                                IsMakeup = 0;

                            }

                        }
                    }
                    catch
                    { }
                    finally
                    {
                        conn.Close();

                    }


                }
                else if (IsMakeup == 1)
                {
                    textBox5.Clear(); textBox6.Clear(); textBox7.Clear();
                    textBox8.Clear(); textBox9.Clear(); textBox10.Clear();
                    //IsMakeup = 0;
                }

            }
        }
        //病历号信息更新或保存
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox4.Text.Length < 13)
                MessageBox.Show("病历号不符合要求，您可能（1）修改了生成的病历号（2）手动输入了错误的病历号");
            else
            {
                if (IsMakeup == 0)
                {
                    string Pid = textBox4.Text;
                    OC getPinfo = new OC();
                    MySqlConnection conn = null;
                    MySqlCommand command = null;
                    //MySqlDataReader mdr = null;
                    int type = comboBox4.SelectedIndex + 1;
                    string sql = "SELECT COUNT(*) FROM `Pinfo` WHERE `Pid` = '" + Pid + "'";
                    string sqlupdata = "UPDATE `Pinfo` SET `Pname`='" + textBox5.Text + "',`Ps`='" + textBox7.Text + "',`PBo`='" + dateTimePicker2.Value.ToString() + "',`Page`='" + textBox8.Text + "',`Pcall`='" + textBox9.Text + "',`Ptype`='" +type+ "',`Pnum`='" + textBox6.Text + "',`Paddress`='" + textBox10.Text + "' WHERE `Pid`='" + textBox4.Text + "'";
                    try
                    {
                        conn = getPinfo.OnConInf();
                        command = new MySqlCommand(sql, conn);
                        conn.Open();
                        string num = command.ExecuteScalar().ToString();
                        if (num == "0")
                        { MessageBox.Show("未查询到此病历号，请确认病历号是否填写正确或者确认是否需要生成需要新的病历号"); }
                        else
                        {
                            conn.Close();
                            command = new MySqlCommand(sqlupdata, conn);
                            conn.Open();
                            int sta = command.ExecuteNonQuery();
                            if (sta == 1)
                                MessageBox.Show("此病历号相关信息更新成功");
                                

                        }
                    }
                    catch
                    {

                        MessageBox.Show("更新失败，请联系管理员");
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                else if (IsMakeup == 1)
                {
                    string Pid = textBox4.Text;
                    OC getPinfo = new OC();
                    MySqlConnection conn = null;
                    MySqlCommand command = null;
                    string sql = "SELECT COUNT(*) FROM `Pinfo` WHERE `Pid` = '" + Pid + "'";
                    string sqlupdata = "UPDATE `Pinfo` SET `Pname`='" + textBox5.Text + "',`Ps`='" + textBox7.Text + "',`PBo`='" + dateTimePicker2.Value.ToString() + "',`Page`='" + textBox8.Text + "',`Pcall`='" + textBox9.Text + "',`Ptype`='" + comboBox4.SelectedIndex + 1 + "',`Pnum`='" + textBox6.Text + "',`Paddress`='" + textBox10.Text + "' WHERE `Pid`='" + textBox4.Text + "'";
                    string sqlins = " INSERT INTO `Pinfo`(`Pid`, `Pname`, `Ps`, `PBo`, `Page`, `Pcall`, `Ptype`, `Pnum`, `Paddress`) VALUES('" + Pid + "','" + textBox5.Text + "','" + textBox7.Text + "','" + dateTimePicker2.Value.ToString() + "','" + textBox8.Text + "','" + textBox9.Text + "','" + comboBox4.SelectedIndex + 1 + "','" + textBox6.Text + "','" + textBox10.Text + "')";
                    try
                    {
                        conn = getPinfo.OnConInf();
                        command = new MySqlCommand(sql, conn);
                        conn.Open();
                        string num = command.ExecuteScalar().ToString();
                        if (num != "0")
                        { MessageBox.Show("此病历号已存在，可能存在并发生成病历号的情况，请重新生成病历号，如仍未解决，请联系管理员"); }
                        else
                        {
                            conn.Close();
                            command = new MySqlCommand(sqlins, conn);
                            conn.Open();
                            int sta = command.ExecuteNonQuery();
                            if (sta == 1)
                                MessageBox.Show("新的病历号信息保存成功");


                        }
                    }
                    catch { }
                    finally
                    {
                        conn.Close();
                        IsMakeup = 0;
                    }
                    IsMakeup = 0;
                }
            }
        }


        //结算挂号类别自动选取
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox3.Text == "普通")
                comboBox7.SelectedIndex = 0;
            else if (textBox3.Text == "主任医师")
                comboBox7.SelectedIndex = 1;
            else if (textBox3.Text == "教授")
                comboBox7.SelectedIndex = 2;
            else if (textBox3.Text == "副教授")
                comboBox7.SelectedIndex = 3;

        }

        public static double get = 0;
        public static double guahao = 0;
        public static double feibie = 0;
        public static double idbuy = 0;
        //计费相关项更新自动更新计费
        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox7.SelectedIndex == 0)
                guahao = 2;
            else
                guahao = 5;

            get = guahao + feibie + idbuy;
            textBox14.Text = get.ToString();
        }

        private void comboBox8_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox8.SelectedIndex == 0)
                feibie = 2;
            else if (comboBox8.SelectedIndex == 1)
                feibie = 3;
            else
                feibie = 10;

            get = guahao + feibie + idbuy;
            textBox14.Text = get.ToString();



        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
                idbuy = 1;
            else idbuy = 0;
            get = guahao + feibie + idbuy;
            textBox14.Text = get.ToString();

        }
        //缴费复查
        private void textBox15_TextChanged(object sender, EventArgs e)
        {
            if (comboBox6.SelectedIndex != -1 & comboBox7.SelectedIndex != -1 & comboBox8.SelectedIndex != -1)
            {
                if (textBox14.Text != "" & textBox15.Text != "")
                    textBox16.Text = (-(Convert.ToDouble(textBox14.Text) - Convert.ToDouble(textBox15.Text))).ToString();
                if (textBox15.Text == "")
                {
                    textBox16.Text = "";
                }
            }
            else
            {
                textBox16.Text = "";
                if (clearint ==0)
                MessageBox.Show("请填写完整的缴费信息");
            }
        }
        //不开诊状态对计费项的更改
        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            if (textBox11.Text == "不开诊")
            {
                comboBox7.SelectedIndex = -1;
            }
        }







      
  

       

        


        //打印方法
        public void print()
        {
            //(string No, string Pid, string Pname, string Ps, string type, string level, string dname, string daddress)
            //this.printDocument1.DefaultPageSettings.PaperSize = new PaperSize("Custum", 210, 297);

            PaperSize p = null;
            foreach (PaperSize ps in this.printDocument1.PrinterSettings.PaperSizes)
            {
                if (ps.PaperName.Equals("A4"))
                    p = ps;
            }
            this.printDocument1.DefaultPageSettings.PaperSize = p;
            //this.printDocument1.Print();




            this.printDocument1.PrintPage += new PrintPageEventHandler(this.MyPrintDocument_PrintPage);

            /*PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();
               printPreviewDialog.Document = printDocument1;
                lineReader = new StringReader(textBox.Text);
                
    
                        printPreviewDialog.ShowDialog();*/
               


            printPreviewDialog1.Document = printDocument1; 
            printDocument1.PrinterSettings.Copies = 0;
            //printDocument1.Print();
            DialogResult result = printPreviewDialog1.ShowDialog();
           


        }
        //打印公共参数
        public static string NoToPrint;

        //打印参数
        private void MyPrintDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            /*如果需要改变自己 可以在new Font(new FontFamily("黑体"),11）中的“黑体”改成自己要的字体就行了，黑体 后面的数字代表字体的大小
             System.Drawing.Brushes.Blue , 170, 10 中的 System.Drawing.Brushes.Blue 为颜色，后面的为输出的位置 */
            e.Graphics.DrawString("挂号单", new Font(new FontFamily("黑体"), 20), System.Drawing.Brushes.Black, 400, 30);
            e.Graphics.DrawString("流水号:" + NoToPrint, new Font(new FontFamily("黑体"), 10), System.Drawing.Brushes.Blue, 650, 45);
            //
            e.Graphics.DrawLine(Pens.Black, 8, 100, 820, 100);
            e.Graphics.DrawString("ID：" + textBox4.Text, new Font(new FontFamily("黑体"), 10), System.Drawing.Brushes.Black, 10, 150);
            e.Graphics.DrawString("姓名：" + textBox5.Text, new Font(new FontFamily("黑体"), 10), System.Drawing.Brushes.Black, 200, 150);
            e.Graphics.DrawString("性别：" + textBox7.Text, new Font(new FontFamily("黑体"), 10), System.Drawing.Brushes.Black, 400, 150);
            e.Graphics.DrawString("挂号级别：" + textBox3.Text, new Font(new FontFamily("黑体"), 10), System.Drawing.Brushes.Black, 10, 200);
            e.Graphics.DrawString("挂号科室：" + comboBox2.SelectedItem.ToString(), new Font(new FontFamily("黑体"), 10), System.Drawing.Brushes.Black, 10, 250);
            e.Graphics.DrawString("医生：" + comboBox3.SelectedItem.ToString(), new Font(new FontFamily("黑体"), 10), System.Drawing.Brushes.Black, 200, 250);
            
            e.Graphics.DrawString("诊室位置：", new Font(new FontFamily("黑体"), 10), System.Drawing.Brushes.Black, 400, 250);
            if (checkBox1.Checked == false)
            { e.Graphics.DrawString("预约就诊日期" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "  " + comboBox5.SelectedItem.ToString(), new Font(new FontFamily("黑体"), 10), System.Drawing.Brushes.Black, 10, 300); }
            else
                e.Graphics.DrawString("预约就诊日期" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "  " + textBox1.Text, new Font(new FontFamily("黑体"), 10), System.Drawing.Brushes.Black, 10, 300);

            e.Graphics.DrawLine(Pens.Black, 8, 900, 820, 900);
            //
            e.Graphics.DrawString("挂号日期：" + System.DateTime.Now.ToString(), new Font(new FontFamily("黑体"), 10), System.Drawing.Brushes.Black, 10, 950);
            e.Graphics.DrawString("挂号费：" + textBox14.Text, new Font(new FontFamily("黑体"), 10), System.Drawing.Brushes.Black, 10, 1000);
            e.Graphics.DrawString("实收：" + textBox15.Text, new Font(new FontFamily("黑体"), 10), System.Drawing.Brushes.Black, 200, 1000);
            e.Graphics.DrawString("找零：" + textBox16.Text, new Font(new FontFamily("黑体"), 10), System.Drawing.Brushes.Black, 400, 1000);




        }

        //打印
     
        //打印所需病历号参数，窗体载入时更改
        public  void reflashNo()
        { 
            string No ="No." +System.DateTime.Now.ToString("yyyyMMddHHmmss");
            label28.Text = No;
            NoToPrint = No;
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            reflashNo();
        }





        SpeechSynthesizer speech = new SpeechSynthesizer();
        public void SpeechVideo_Read(int rate, int volume, string speektext)  //读
        {
        speech.Rate = rate;
        speech.Volume = volume;
        speech.SpeakAsync(speektext);
        }

        //挂号事件
         private void button3_Click(object sender, EventArgs e)
        {
            string num = System.DateTime.Now.ToString("yyyyMMddHHmmss");
            string did = textBox2.Text; string dname = comboBox3.SelectedItem.ToString();
            string pid = textBox4.Text; string pname = textBox5.Text;
            string Ps = textBox7.Text;
            string date = dateTimePicker1.Value.ToString("yyyy-MM-dd");

            string type = comboBox1.SelectedItem.ToString() + comboBox2.SelectedItem.ToString();
            string addtime = textBox1.Text;
            int isadd = 0;
            //流水记在另一个流水表中，题目未要求，故未实现



            if (textBox11.Text == "不开诊")
                MessageBox.Show("该医生当天不开诊，请更换其他医生");
            else
            {
                if (textBox13.Text != "0")
                {
                    if (comboBox5.SelectedIndex == -1)
                        MessageBox.Show("请选择挂号时段");
                    else
                    {
                        string time = comboBox5.SelectedItem.ToString();
                        if (textBox15.Text == "")
                            MessageBox.Show("No pay");
                        else
                        {
                            OC Addreg = new OC();
                            MySqlConnection conn = null;
                            MySqlCommand command = null;

                            try
                            {


                                string sqlins = " INSERT INTO `RegInput`(`RegID`, `Did`, `Dname`, `Pid`, `Pname`, `DateTime`, `Type`, `Isadd`) VALUES('" + num + "','" + did + "','" + dname + "','" + pid + "','" + pname + "','" + date + time + "','" + type + "','" + isadd + "' )";

                                conn = Addreg.OnConInf();
                                command = new MySqlCommand(sqlins, conn);
                                conn.Open();
                                int statu = command.ExecuteNonQuery();
                                if (statu == 1)
                                {
                                    MessageBox.Show("挂号成功,如需打印请点击打印按钮");
                                    tabControl1.SelectedTab = tabPage2;
                                    showduty();
                                }
                            }
                            catch
                            {
                                MessageBox.Show("挂号失败");

                            }

                            finally
                            {
                                conn.Close();
                            }

                            try
                            {
                                conn.Close();

                                string sqlupdata = "UPDATE `Duty` SET `Statu`='1' WHERE `Did`='" + did + "' AND `Date`='" + date + "' AND `Time`='" + time + "'";
                                command = new MySqlCommand(sqlupdata, conn);
                                conn.Open();
                                int statu = command.ExecuteNonQuery();
                                if (statu == 1)
                                {
                                    //MessageBox.Show("医生门诊信息更新成功");
                                    string bobao = "请" + textBox5.Text + "到" + comboBox2.SelectedItem.ToString() + "就诊";
                                    SpeechVideo_Read(1, 100, bobao);
                                    button5.Enabled = true;

                                }

                            }
                            catch
                            { }
                            finally
                            { }




                        }

                    }
                }
                else
                {
                    if (checkBox1.Checked == false)
                        MessageBox.Show("当前医生号已满，如有需要请加号");
                    else
                    {
                        isadd = 1;
                        if (textBox15.Text == "")
                            MessageBox.Show("未缴纳挂号费用");
                        else
                        {
                            OC Addreg = new OC();
                            MySqlConnection conn = null;
                            MySqlCommand command = null;

                            try
                            {


                                string sqlins = " INSERT INTO `RegInput`(`RegID`, `Did`, `Dname`, `Pid`, `Pname`, `DateTime`, `Type`, `Isadd`) VALUES('" + num + "','" + did + "','" + dname + "','" + pid + "','" + pname + "','" + date + addtime + "','" + type + "','" + isadd + "' )";

                                conn = Addreg.OnConInf();
                                command = new MySqlCommand(sqlins, conn);
                                conn.Open();
                                int statu = command.ExecuteNonQuery();
                                if (statu == 1)
                                {
                                    MessageBox.Show("挂号成功，如需打印请点击打印按钮");
                                    tabControl1.SelectedTab = tabPage2;
                                    showduty();
                                }
                            }
                            catch
                            {
                                MessageBox.Show("挂号失败");
                            }
                            finally
                            {
                                conn.Close();
                            }
                            try
                            {
                                conn.Close();

                                string sqlupdata = "INSERT INTO `Duty`(`Did`, `Date`, `Time`, `Statu`) VALUES ('" + did + "','" + date + "','" + addtime + "','1')";
                                command = new MySqlCommand(sqlupdata, conn);
                                conn.Open();
                                int statu = command.ExecuteNonQuery();
                                if (statu == 1)
                                {
                                    //MessageBox.Show("医生门诊信息更新成功");
                                    string bobao = "请" + textBox5.Text + "到" + comboBox2.SelectedItem.ToString() + "就诊";
                                    SpeechVideo_Read(1, 100, bobao);
                                    button5.Enabled = true;

                                }

                            }
                            catch
                            { }
                            finally
                            { }


                        }
                    }


                }

            }

            /*private void textBox11_TextChanged(object sender, EventArgs e)
            {
               
            }*/
        }



        //tabpage切换事件
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPage1)
            {
                showduty();
            }
            else
            {
                showReg();
            }
        }
        //datagridview关联控件的事件
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            comboBox5.Enabled = true;
            showduty();
        }
        //datagridview 值班
        public void showduty()
        {
            if (textBox2.Text.Length < 10)
            { }
            else
            {
                tabControl1.SelectedTab = tabPage1;
                OC oc = new OC();

                MySqlCommand command = null;
                MySqlDataReader mdr = null;
                string sql = "SELECT * FROM `Duty` WHERE `Did` = '" + textBox2.Text + "' AND `Date` = '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "'";


                try
                {
                    MySqlConnection show = oc.OnConInf();
                    command = new MySqlCommand(sql, show);
                    show.Open();
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = command;

                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    dataGridView1.DataSource = table;
                    dataGridView1.Columns[0].HeaderCell.Value = "医生ID";
                    dataGridView1.Columns[1].HeaderCell.Value = "开诊日期";
                    dataGridView1.Columns[2].HeaderCell.Value = "预约时段";
                    dataGridView1.Columns[3].HeaderCell.Value = "是否已挂号";

                }
                catch { }
                finally
                { }
            }
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            showduty();
        }
        //datagridview 挂号记录
        public void showReg()
        {
           
            {
                //tabControl1.SelectedTab = tabPage1;
                OC oc = new OC();

                MySqlCommand command = null;
                MySqlDataReader mdr = null;
                string sql = "SELECT * FROM `RegInput` ORDER BY `RegID` DESC"; 


                try
                {
                    MySqlConnection show = oc.OnConInf();
                    command = new MySqlCommand(sql, show);
                    show.Open();
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = command;

                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    dataGridView2.DataSource = table;
                    dataGridView2.Columns[0].HeaderCell.Value = "流水号";
                    dataGridView2.Columns[1].HeaderCell.Value = "医生ID";
                    dataGridView2.Columns[2].HeaderCell.Value = "医生姓名";
                    dataGridView2.Columns[3].HeaderCell.Value = "病人ID";
                    dataGridView2.Columns[4].HeaderCell.Value = "病人姓名姓名";
                    dataGridView2.Columns[5].HeaderCell.Value = "就诊时间";
                    dataGridView2.Columns[6].HeaderCell.Value = "就诊科室";
                    dataGridView2.Columns[7].HeaderCell.Value = "是否加号";

                }
                catch { }
                finally
                { }
            }
        }



        //清屏方法
        public static int clearint = 0;
        public void clear()
        {
            clearint = 1;
            textBox15.Clear();
            comboBox1.SelectedIndex =-1;
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
            comboBox4.SelectedIndex = -1;
            comboBox5.SelectedIndex = -1;
            comboBox6.SelectedIndex = -1;
            comboBox7.SelectedIndex = -1;
            comboBox8.SelectedIndex = -1;
            textBox1.Clear();textBox1.Enabled = false;
            textBox2.Clear(); textBox2.Enabled = false;
            textBox3.Clear(); textBox3.Enabled = false;
            textBox11.Clear(); textBox11.Enabled = false;
            textBox12.Clear(); textBox12.Enabled = false;
            textBox13.Clear(); textBox13.Enabled = false;
            checkBox1.Checked = false;
            checkBox1.Enabled = false;

            IsMakeup = 0;
            textBox4.Clear();
            textBox5.Clear();
            textBox7.Clear();
            textBox8.Clear();
            textBox9.Clear();
            textBox6.Clear();
            textBox10.Clear();
            textBox14.Clear();

            checkBox2.Checked = false;
            clearint = 0;

            button5.Enabled = false;

        }

        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void 新建挂号ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clear();
            reflashNo();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1 || comboBox2.SelectedIndex == -1 || comboBox3.SelectedIndex == -1 || (comboBox5.SelectedIndex == -1 & checkBox1.Checked == false))
            {
                MessageBox.Show("关键信息不完整，暂不能打印");
            }
            else
                print();
        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void label27_Click(object sender, EventArgs e)
        {

        }

        

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label28_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged_1(object sender, EventArgs e)
        {

        }
   
    }

  
}
