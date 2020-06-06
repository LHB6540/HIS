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
        mySql sql = new mySql();
        string doctorId = "";
        string[] regIDShuzu = new string[20];
        int signalToPrint = 0;
        //主窗体
        public registrationSystem()
        {
            InitializeComponent();
        }

        //清屏诊疗卡、冻结及不可视化按钮
        void clear()
        {
            status = 0;
            cardNum.Clear();
            paName.Clear();
            paSex.Clear();
            paBorth.Value = System.DateTime.Now;
            paAge.Clear();
            paPhone.Clear();
            paIDType.SelectedIndex = 0;
            paID.Clear();
            paAddress.Clear();
            paAllergy.Clear();
            buttonSearch.Visible = false;
            buttonSearch.Enabled = false;
            buttonChange.Visible = false;
            buttonChange.Enabled = false;
            buttonNew.Visible = false;
            buttonNew.Enabled = false;
        }

        //清屏挂号、冻结及不可视化按钮
        void clearReg()
        {
            status = 0;
            regClassType.Enabled = regDateTimePicker.Enabled = regClass.Enabled = regRemainNum.Enabled = regLevel.Enabled
           = regPayType.Enabled = regCostType.Enabled = regDoctor.Enabled = regID.Enabled = regIfWork.Enabled = false;
            regSearchButton.Enabled = cancelRegButton.Enabled = makeRegButton.Enabled =  false;
            regSearchButton.Visible = cancelRegButton.Visible = makeRegButton.Visible =  false;
            regClass.SelectedIndex = regClassType.SelectedIndex = regLevel.SelectedIndex = regDoctor.SelectedIndex = -1; timeChoose.SelectedIndex = -1;
            regClassType.Text = regClass.Text = regLevel.Text = regDoctor.Text = regCostType.Text = ""; regID.Text = "";
            timeChoose.Text = "";
        }


        //菜单栏查询项
        private void search_Click(object sender, EventArgs e)
        {
            clear();
            buttonSearch.Visible = true;
            buttonSearch.Enabled = true;
            buttonChange.Visible = true;
            buttonChange.Enabled = true;
        }

        //查询按钮
        private void buttonSearch_Click(object sender, EventArgs e)
        {
            if (cardNum.Text == "")
            {
                MessageBox.Show("请输入诊疗卡号");
            }
            else
            {
                string cardnum = cardNum.Text;
                string query = "SELECT COUNT(*) FROM `HIS`.`Patient` WHERE `CardNum`='" + cardnum + "'";
                int countResult = sql.count(query);
                if (countResult >= 1)
                {   
                    query = "SELECT * FROM `HIS`.`Patient` WHERE `CardNum`='" + cardnum + "'";
                    MySqlDataReader searchResult = sql.searchData(query);
                    while (searchResult.Read())
                    {
                        paName.Text = searchResult["paName"].ToString();
                        string paSexNum = searchResult["paSex"].ToString();
                        if (paSexNum == "1") paSex.Text = "男"; else paSex.Text = "女";
                        string paBorthStr = searchResult["paBorth"].ToString(); if (paBorthStr != "") paBorth.Value = Convert.ToDateTime(paBorthStr);
                        if (searchResult["paIDType"].ToString() != "") paIDType.SelectedIndex = Convert.ToInt32(searchResult["paIDType"].ToString());
                        paID.Text = searchResult["paID"].ToString();
                        paAddress.Text = searchResult["paAddress"].ToString();
                        paAllergy.Text = searchResult["paAllergy"].ToString();
                        paAge.Text = searchResult["paAge"].ToString();
                        paPhone.Text = searchResult["paPhone"].ToString();
                    }
                    sql.closeConnection();
                }
                else
                {
                    MessageBox.Show("未查询到此诊疗卡");
                }
            }
        }

        private void buttonNew_Click(object sender, EventArgs e)
        {
            int paSexInt = 1;
            if (paSex.Text == "男")
                paSexInt = 1;
            else
                paSexInt = 2;
            if (paID.Text == "")
            {
                MessageBox.Show("不允许证件号为空");
            }
            else
            {
                string cardNumber = System.DateTime.Now.ToString("yyyyMMddHHmmss") + paID.Text.Substring(paID.Text.Length - 4);
                cardNum.Text = cardNumber;
                string query = "INSERT INTO `patient` (`CardNum`, `paName`, `paSex`, `paBorth`, `paIDType`, `paID`, `paAge`, `paPhone`, `paAddress`, `paAllergy`) VALUES ('" + cardNum.Text + "', '" + paName.Text + "', '" + paSexInt + "', '" + paBorth.Value.ToString() + "', '" + paIDType.SelectedIndex + "', '" + paID.Text + "', '" + paAge.Text + "', '" + paPhone.Text + "', '" + paAddress.Text + "', '" + paAllergy.Text + "')";
                bool addResult = sql.addDate(query);
                if (addResult == true)
                {
                    buttonChange.Enabled = true;
                    buttonChange.Visible = true;
                    buttonNew.Enabled = false;
                    buttonNew.Visible = false;
                    buttonSearch.Enabled = true;
                    buttonSearch.Visible = true;
                    MessageBox.Show("新建诊疗卡成功");
                }
                else
                    MessageBox.Show("出现错误");
            }
        }

        private void newcard_Click(object sender, EventArgs e)
        {
            clear();
            buttonNew.Visible = true;
            buttonNew.Enabled = true;
        }

        private void buttonChange_Click(object sender, EventArgs e)
        {
            int paSexInt = 1;
            if (paSex.Text == "男")
                paSexInt = 1;
            else
                paSexInt = 2;

            string query = "UPDATE `patient` SET `paName`='" + paName.Text + "',`paSex`='" + paSexInt + "',`paBorth`='" + paBorth.Value.ToString() + "',`paIDType`='" + paIDType.SelectedIndex + "',`paID`='" + paID.Text + "',`paAge`='" + paAge.Text + "',`paPhone`='" + paPhone.Text + "',`paAllergy`='" + paAllergy.Text + "',`paAddress`='" + paAddress.Text + "' WHERE (`CardNum`='" + cardNum.Text + "') LIMIT 1";
            int updateResult = sql.update(query);
            switch (updateResult)
            {

                case -1:; break;
                case -2:; break;
                case 1: MessageBox.Show("更新成功"); break;

            }


        }

        private void searchRegToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clear();
            clearReg();
            regID.Enabled = true;
            regSearchButton.Enabled = true;
            regSearchButton.Visible = true;

        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
      



        }

        private void newRegToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clearReg();
            makeRegButton.Visible = true;
            makeRegButton.Enabled = true;
            regClassType.Items.Clear();
            regClassType.SelectedIndex = -1;
            string query = "SELECT DISTINCT classType FROM `classanddoctor` WHERE 1";
            MySqlDataReader Result = sql.searchData(query);
            while (Result.Read())
            {
                string cod = Result["classType"].ToString();
                regClassType.Items.Add(cod);
            }
            sql.closeConnection();
            regClassType.Enabled = true;
            regCostType.Enabled = true;
            regPayType.Enabled = true;
        }



        private void regClassType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (regClassType.SelectedIndex == -1) { }
            else
            {
                regClass.Items.Clear();
                regClass.SelectedIndex = -1;
                regClass.Enabled = false;
                regClass.Text = "";
                regDoctor.Items.Clear();
                regDoctor.SelectedIndex = -1;
                regDoctor.Enabled = false;
                regDoctor.Text = "";
                regLevel.SelectedIndex = -1;
                regLevel.Enabled = false;
                regLevel.Text = "";
                string classType = regClassType.SelectedItem.ToString();
                string query = "SELECT DISTINCT class FROM `ClassAndDoctor` WHERE `classType` = '" + classType + "'";
                MySqlDataReader mdr = sql.searchData(query);
                while (mdr.Read())
                {
                    string cate = mdr["class"].ToString();
                    regClass.Items.Add(cate);
                }
                sql.closeConnection();
                regClass.Enabled = true;
                if (regClass.Items.Count == 0)
                    regClass.SelectedIndex = -1;
                else
                {
                    regClass.SelectedIndex = 0;
                    regClass.Enabled = true;
                }
            }
        }

        private void regClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (regClass.SelectedIndex == -1)
            { }
            else
            {
                regDoctor.Items.Clear();
                regDoctor.Enabled = false;
                regDoctor.SelectedIndex = -1;
                regDoctor.Text = "";
                regLevel.SelectedIndex = -1;
                regLevel.Text = "";
                regLevel.Enabled = true;
                regLevel.SelectedIndex = 0;
            }
        }

        private void regLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (regLevel.SelectedIndex == -1 || status == 1) { }
            else
            {
                regDoctor.Enabled = false;
                regDoctor.Items.Clear();
                regDoctor.SelectedIndex = -1;
                regDoctor.Text = "";
                int levelIndex = regLevel.SelectedIndex;
                string regClassString = regClass.SelectedItem.ToString();
                string query = "";
              
                query = "SELECT DISTINCT ID,name FROM `ClassAndDoctor` WHERE `class` = '" + regClassString + "' AND `level` = '" + (levelIndex+1) + "'";
                Console.WriteLine(query);
                MySqlDataReader mdr = sql.searchData(query);
                if (mdr == null)
                { MessageBox.Show("该科室没有该级别的医师"); }
                else
                {
                    while (mdr.Read())
                    {
                        string id = mdr["ID"].ToString();
                        string name = mdr["name"].ToString();
                        string doctor = name + " " + id;
                        regDoctor.Items.Add(doctor);
                    }
                    sql.closeConnection();
                    //regLevel.Enabled = true;
                    regDoctor.Enabled = true;
                }
                Console.WriteLine("医生统计为" + regDoctor.Items.Count.ToString()
                    );
                if (regDoctor.Items.Count == 0)
                {
                    regDoctor.SelectedIndex = -1;
                    regCostType.SelectedIndex = -1;
                    //regCostType.Enabled = false;
                    regCostType.Text = "";
                    regDateTimePicker.Enabled = false;
                    regIfWork.Clear();
                    regIfWork.Enabled = false;
                    regRemainNum.Clear();
                    regRemainNum.Enabled = false;
                    timeChoose.Items.Clear();
                    timeChoose.SelectedIndex = -1;
                    timeChoose.Text = "";
                    timeChoose.Enabled = false;
                    checkBox1.Checked = checkBox3.Checked = checkBox1.Enabled = checkBox3.Enabled = timeChoose.Enabled = false;
                }
                else regDoctor.SelectedIndex = 0;
            }

        }

        private void regDoctor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (regDoctor.SelectedIndex == -1)
            {
                Console.WriteLine("触发了医生索引为-1");
                regCostType.SelectedIndex = -1;
                //regCostType.Enabled = false;
                regCostType.Text = "";
                regDateTimePicker.Enabled = false;
                regIfWork.Clear();
                regIfWork.Enabled = false;
                regRemainNum.Clear();
                regRemainNum.Enabled = false;
                timeChoose.Items.Clear();
                timeChoose.SelectedIndex = -1;
                timeChoose.Text = "";
                timeChoose.Enabled = false;
                checkBox1.Checked = checkBox3.Checked = checkBox1.Enabled = checkBox3.Enabled = timeChoose.Enabled = false;
                regID.Text = "";
            }
            else
            {
                regDateTimePicker.Value = new DateTime(2020, 01, 01);
                regDateTimePicker.Value = System.DateTime.Now;
                regDateTimePicker.Enabled = true;
            }

        }

        int status = 0;

        private void regDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            regIfWork.Clear();
            regIfWork.Enabled = false;
            regRemainNum.Clear();
            regRemainNum.Enabled = false;
            timeChoose.Items.Clear();
            timeChoose.SelectedIndex = -1;
            checkBox1.Checked = checkBox3.Checked = checkBox1.Enabled = checkBox3.Enabled = timeChoose.Enabled = false;

            if (regDateTimePicker.Value.Date < System.DateTime.Now.Date || status == 1)
            { }
            else
            {
                string query = "SELECT COUNT(*) FROM `regandduty`  WHERE `data`='" + regDateTimePicker.Value.Date.ToString() + "' AND `doctorID`='" + regDoctor.SelectedItem.ToString().Split(' ')[1] + "'";
                Console.WriteLine(query);
                int ifwork = sql.count(query);
                Console.WriteLine(ifwork);
                if (ifwork == 0)
                {
                    regIfWork.Text = "不开诊";
                    regRemainNum.Text = "-1";
                    timeChoose.Items.Clear();
                    timeChoose.Text = "";
                    checkBox1.Enabled = checkBox1.Checked = checkBox3.Checked = checkBox3.Enabled = false;
                    timeChoose.Items.Clear(); timeChoose.SelectedItem = -1; timeChoose.Enabled = false;
                    regID.Text = "";

                }
                else if (ifwork > 0)
                {
                    regIfWork.Text = "当日开诊";
                    query = "SELECT COUNT(*) FROM `regandduty`  WHERE `data`='" + regDateTimePicker.Value.Date.ToString() + "' AND `doctorID`='" + regDoctor.SelectedItem.ToString().Split(' ')[1] + "' AND `ifUse`='0' ";
                    Console.WriteLine(query);
                    int countNotUse = sql.count(query);
                    Console.WriteLine(countNotUse);
                    if (countNotUse == 0)
                    {
                        regRemainNum.Text = "0";
                        timeChoose.SelectedItem = -1;
                        timeChoose.Items.Clear();
                        timeChoose.Text = "";
                        timeChoose.Enabled = false;
                        checkBox1.Enabled = true;
                        checkBox3.Enabled = true;
                        regID.Text = "";
                    }
                    else
                    {
                        regRemainNum.Text = countNotUse.ToString();
                        query = "SELECT * FROM `regandduty`  WHERE `data`='" + regDateTimePicker.Value.Date.ToString() + "' AND `doctorID`='" + regDoctor.SelectedItem.ToString().Split(' ')[1] + "' AND `ifUse`='0' ";
                        MySqlDataReader mdr = sql.searchData(query);
                        int i = 0;

                        while (mdr.Read())
                        {
                            timeChoose.Items.Add(mdr["time"].ToString());
                            regIDShuzu[i] = mdr["regID"].ToString();
                            Console.WriteLine("from mdr read" + regIDShuzu[i]);
                            i++;
                        }
                        timeChoose.SelectedIndex = -1;
                        timeChoose.SelectedIndex = 0;
                        timeChoose.Enabled = true;
                    }
                }

            }
        }

        private void timeChoose_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (timeChoose.SelectedIndex == -1)
            { }
            else
            {

                regID.Text = regIDShuzu[timeChoose.SelectedIndex];

            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == false && status == 0)
            {
                regID.Text = "";
            }
            else
            { if (status == 1)
                { }
                else
                    regID.Text = cardNum.Text + regDoctor.Text.Split(' ')[1] + System.DateTime.Now.Date.ToString().Split(' ')[0];
            }
        }

        private void makeRegButton_Click(object sender, EventArgs e)
        {
            if (cardNum.Text == "")
            {
                MessageBox.Show("病历号不能为空");
            }
            else
            {
                if (regDoctor.SelectedItem.ToString() == "")
                {
                    MessageBox.Show("请选择医生");
                }
                else
                {
                    if (regIfWork.Text == "当日不开诊")
                    {
                        MessageBox.Show("医生当日不开诊");
                    }
                    else
                    {
                        if (regRemainNum.Text == "0" && checkBox1.Checked == false)
                        {
                            MessageBox.Show("该医生当日已经无余号，如需加号请勾选加号");
                        }
                        else if (regRemainNum.Text == "0" && checkBox1.Checked == true)
                        {
                            if (regID.Text == "")
                            { MessageBox.Show("系统异常，未生成就诊号"); }
                            else
                            {
                                if (regCostType.SelectedIndex == -1)
                                { MessageBox.Show("请选择费别"); }
                                else if (regPayType.SelectedIndex == -1)
                                {
                                    MessageBox.Show("请选择支付方式");

                                }
                                else
                                {
                                    regID.Text = cardNum.Text + regDoctor.Text.Split(' ')[1] + System.DateTime.Now.Date.ToString().Split(' ')[0];
                                    //加号
                                    string query = "INSERT INTO `regandduty` (`regID`, `doctorID`,`paiID`,`data`,`time`,`ifUse`,`treatstatus`,`regpay`,`level`,`regtype`,`paytype`) VALUES ('" + regID.Text + "', '" + regDoctor.Text.Split(' ')[1] + "','" + cardNum.Text + "','" + regDateTimePicker.Value.Date.ToString() + "','20:00:00','1','0','" + textBox2.Text + "','" + regLevel.Text + "','" + regCostType.Text + "','" + regPayType.Text + "');";
                                    if (sql.addDate(query) == true)
                                    {
                                        if (MessageBox.Show("加号成功，是否打印", "tip", MessageBoxButtons.OKCancel) == DialogResult.OK)
                                        {
                                            //regid,cardnum,classtype,classname,level,costtype,ifjiahao,paname,doname,sex,time,paytype,regpay
                                            string regid = regID.Text;
                                            string cardnum = cardNum.Text;
                                            string classtype = regClassType.Text;
                                            string classname = regClass.Text;
                                            string level = regLevel.Text;
                                            string costtype = regCostType.Text;
                                            string ifjiahao = "是";
                                            string paname = paName.Text;
                                            string doname = regDoctor.Text.Split(' ')[0];
                                            string sex = paSex.Text;
                                            string time = "加号";
                                            string paytype = regPayType.Text;
                                            string regpay = textBox2.Text;
                                            Console.WriteLine("开始打印");
                                            new printReg(regid, cardnum, classtype, classname, level, costtype, ifjiahao, paname, doname, sex, time, paytype, regpay).Show();
                                        }
                                        else { }
                                        clearReg();
                                        clear();
                                    }
                                }
                            }
                        }
                        else if (regRemainNum.Text != "0" && regRemainNum.Text != "-1")
                        {
                            if (timeChoose.SelectedIndex == -1)
                            {
                                MessageBox.Show("请选择挂号时间");
                            }
                            else
                            {

                                if (regCostType.SelectedIndex == -1)
                                {
                                    MessageBox.Show("请选择费别");
                                }
                                else 
                                {
                                    if (regPayType.SelectedIndex == -1)
                                    {
                                        MessageBox.Show("请选择支付方式");
                                    }
                                    else
                                    {
                                        string id = regID.Text.ToString();
                                        Console.WriteLine(id);
                                        //正常挂号
                                        string updateReg = "UPDATE `regandduty` set `ifUse`='1', `paiID`='" + cardNum.Text + "',`regpay`='"+textBox2.Text+"',`level`='"+regLevel.Text+"',`regtype`='"+regCostType.Text+"',`paytype`='"+regPayType.Text+"'  WHERE `regID`='" + id + "'";
                                        if (sql.update(updateReg) == 1)
                                        {
                                            if (MessageBox.Show("挂号成功，是否打印", "tip", MessageBoxButtons.OKCancel) == DialogResult.OK)
                                            {
                                                Console.WriteLine("开始打印");
                                                string regid = regID.Text;
                                                string cardnum = cardNum.Text;
                                                string classtype = regClassType.Text;
                                                string classname = regClass.Text;
                                                string level = regLevel.Text;
                                                string costtype = regCostType.Text;
                                                string ifjiahao = "否";
                                                string paname = paName.Text;
                                                string doname = regDoctor.Text.Split(' ')[0];
                                                string sex = paSex.Text;
                                                string time = timeChoose.Text;
                                                string paytype = regPayType.Text;
                                                string regpay = textBox2.Text;
                                                Console.WriteLine("开始打印");

                                                new printReg(regid, cardnum, classtype, classname, level, costtype, ifjiahao, paname, doname, sex, time, paytype, regpay).Show();



                                            }
                                            else { }
                                            clearReg();
                                            clear();
                                        }
                                    }
                                }

                            }
                        }
                    }
                }
            }
        }

        int howtocancel = -1;
        private void regSearchButton_Click(object sender, EventArgs e)
        {

            if (regID.Text == "")
            {
                MessageBox.Show("请输入就诊号");
            }
            else
            {
                status = 1;
                string query = "SELECT COUNT(*) FROM `regandduty`  WHERE `regID`='" + regID.Text + "' AND  `ifUse` ='1'";
                Console.WriteLine(sql.count(query));
                if (sql.count(query) == 0)
                {
                    MessageBox.Show("未查询到挂号记录");
                  /*  Console.WriteLine("第一次查询为0");
                    query = "SELECT COUNT(*) FROM `addReg`  WHERE `regID`='" + regID.Text + "'";
                    if (sql.count(query) == 0)
                    {
                        MessageBox.Show("未找到该就诊号记录");
                    }
                    else
                    {
                        howtocancel = 0;
                        checkBox1.Checked = true;

                        query = "SELECT DISTINCT doctorID,paiID,data FROM `addReg` WHERE `regID` = '" + regID.Text + "'";
                        MySqlDataReader mdr = sql.searchData(query);
                        string pai = ""; string doc = "";
                        while (mdr.Read())
                        {
                            regDateTimePicker.Value = Convert.ToDateTime(mdr["data"].ToString());
                            pai = mdr["paiID"].ToString();
                            doc = mdr["doctorID"].ToString();
                            regDateTimePicker.Value = Convert.ToDateTime(mdr["data"].ToString());
                        }

                        search.PerformClick();
                        cardNum.Text = pai;
                        buttonSearch.PerformClick();
                        status = 1;

                        query = "SELECT  * FROM `classanddoctor` WHERE `ID`='" + doc + "'";
                        mdr = sql.searchData(query);
                        while (mdr.Read())
                        {
                            regClassType.Text = mdr["classType"].ToString();
                            regClass.Text = mdr["class"].ToString();
                            regLevel.SelectedIndex = Convert.ToInt32(mdr["level"].ToString());
                        }
                        cancelRegButton.Visible = true;
                        cancelRegButton.Enabled = true;

                    }*/
                }
                else
                {
                    howtocancel = 1;
                    query = "SELECT DISTINCT doctorID,paiID,data,time FROM `regandduty` WHERE `regID` = '" + regID.Text + "'";
                    MySqlDataReader mdr = sql.searchData(query);
                    string pai = ""; string doc = "";
                    while (mdr.Read())
                    {
                        pai = mdr["paiID"].ToString();
                        doc = mdr["doctorID"].ToString();
                        regDateTimePicker.Value = Convert.ToDateTime(mdr["data"].ToString());
                        timeChoose.Text = mdr["time"].ToString();
                    }
                    sql.closeConnection();
                    search.PerformClick();
                    cardNum.Text = pai;
                    buttonSearch.PerformClick();
                    status = 1;

                    query = "SELECT  * FROM `classanddoctor` WHERE `ID`='" + doc + "'";
                    mdr = sql.searchData(query);
                    while (mdr.Read())
                    {
                        regClassType.Text = mdr["classType"].ToString();
                        regClass.Text = mdr["class"].ToString();
                        regLevel.SelectedIndex = Convert.ToInt32(mdr["level"].ToString());
                    }
                    cancelRegButton.Visible = true;
                    cancelRegButton.Enabled = true;


                }
            }
        }

        private void cancelRegButton_Click(object sender, EventArgs e)
        {
            if (howtocancel == -1)
            {
                MessageBox.Show("系统异常，在未查找到信息的情况下不能删除");
            }
            else if (howtocancel == 1)
            {
                string updateReg = "UPDATE `regandduty` set `ifUse`='0', `paiID`='' WHERE `regID`='" + regID.Text + "'";
                if (sql.update(updateReg) == 1)
                {
                    MessageBox.Show("退号成功");
                    clear();
                    clearReg();
                }
            }
            else if (howtocancel == 0)
            {
                string delquery = "DELETE FROM `addReg` WHERE `RegID`='" + regID.Text + "'";
                if (sql.del(delquery))
                {
                    MessageBox.Show("退号成功");
                    clearReg(); clear();
                }
            }
        }

        private void comboBox1_Click(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            comboBox1.SelectedIndex = -1;
            comboBox1.Text = "";
            string query = "SELECT DISTINCT classType FROM `ClassAndDoctor` WHERE 1";
            MySqlDataReader Result = sql.searchData(query);
            while (Result.Read())
            {
                string cod = Result["classType"].ToString();
                comboBox1.Items.Add(cod);
            }
            sql.closeConnection();
        }


        //重绘列值



        //查询科室信息
        private void buttonCountClass_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1 && comboBox1.Text == "")
            {
                MessageBox.Show("请选择科室类别");
            }
            else
            {
                string classtype = comboBox1.SelectedItem.ToString();

            
                MySqlCommand cmd = null;
                MySqlDataAdapter dpt = null;
                mySql sql = new mySql();
                DataSet ds = new DataSet();
                DataTable dtb = null;


                sql.openConnetction();

                cmd = new MySqlCommand("SELECT ID,name,classType,class,case when `level` = '1' then '专家'when `level` = '2' then '主任医师'when `level` = '3'then '副主任医师' WHEN  `level`= '4' THEN '医师'end as `level`  FROM classanddoctor WHERE classType='"+classtype+"'", sql.conn);

                dpt = new MySqlDataAdapter(cmd);
                dpt.Fill(ds);
                dtb = ds.Tables[0];
                dataGridView1.DataSource = dtb;


                this.dataGridView1.AutoGenerateColumns = false;

                this.dataGridView1.Columns["Column1"].DataPropertyName = dtb.Columns["classType"].ToString();

                this.dataGridView1.Columns["Column2"].DataPropertyName = dtb.Columns["class"].ToString();

                this.dataGridView1.Columns["Column3"].DataPropertyName = dtb.Columns["ID"].ToString();

                this.dataGridView1.Columns["Column4"].DataPropertyName = dtb.Columns["name"].ToString();

                this.dataGridView1.Columns["Column5"].DataPropertyName = dtb.Columns["level"].ToString();
               
                this.dataGridView1.Columns["classType"].Visible = false;
                this.dataGridView1.Columns["class"].Visible = false;
                this.dataGridView1.Columns["ID"].Visible = false;
                this.dataGridView1.Columns["name"].Visible = false;
                this.dataGridView1.Columns["level"].Visible = false;










            }
        }

        //查询医生排班信息
        private void button13_Click(object sender, EventArgs e)
        {
            {
                if (textBox12.Text == "")
                {
                    MessageBox.Show("请输入医生工号");
                }
                else
                {
                    dataGridView2.DataSource = null;
                    string query = "SELECT  doctorID,data,time,classanddoctor.class,classanddoctor.name,classanddoctor.phone FROM  `regandduty` INNER JOIN `classanddoctor` ON regandduty.doctorID=classanddoctor.ID WHERE `data`='" + dateTimePicker1.Value.Date.ToString() + "' AND `doctorID`='" + textBox12.Text + "'";

                    

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
                    dataGridView2.DataSource = dtb;


                    this.dataGridView2.AutoGenerateColumns = false;

                    this.dataGridView2.Columns["Column6"].DataPropertyName = dtb.Columns["data"].ToString();

                    this.dataGridView2.Columns["Column7"].DataPropertyName = dtb.Columns["time"].ToString();

                    this.dataGridView2.Columns["Column8"].DataPropertyName = dtb.Columns["class"].ToString();

                    this.dataGridView2.Columns["Column9"].DataPropertyName = dtb.Columns["doctorID"].ToString();

                    this.dataGridView2.Columns["Column10"].DataPropertyName = dtb.Columns["name"].ToString();
                    this.dataGridView2.Columns["Column11"].DataPropertyName = dtb.Columns["phone"].ToString();

                    
                    this.dataGridView2.Columns["data"].Visible = false;
                    this.dataGridView2.Columns["time"].Visible = false;
                    this.dataGridView2.Columns["class"].Visible = false;
                    this.dataGridView2.Columns["doctorID"].Visible = false;
                    this.dataGridView2.Columns["name"].Visible = false;
                    this.dataGridView2.Columns["phone"].Visible = false;
                    








                }
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {

            //string query = "SELECT  regID,doctorID,paiID，data,time FROM `regandduty` WHERE `data`='" + dateTimePicker2.Value.Date.ToString() + "' AND `ifUse`='1'";
            string query = "SELECT regandduty.regID,patient.paName,classanddoctor.class,classanddoctor.`name`,regandduty.`level`,regandduty.regtype,case WHEN time='20:00:00' THEN '是' WHEN time!='20:00:00' THEN '否' end AS `ifjiahao`,regandduty.time,regandduty.paytype,regandduty.regpay  FROM regandduty INNER JOIN patient ON regandduty.paiID=patient.CardNum INNER JOIN classanddoctor ON regandduty.doctorID=classanddoctor.ID WHERE regandduty.data='"+dateTimePicker2.Value.Date.ToString()+"' AND regandduty.ifUse='1'";
            Console.WriteLine(query);
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
            dataGridView4.DataSource = dtb;


            this.dataGridView4.AutoGenerateColumns = false;

            this.dataGridView4.Columns["Column12"].DataPropertyName = dtb.Columns["regID"].ToString();

            this.dataGridView4.Columns["Column14"].DataPropertyName = dtb.Columns["paName"].ToString();

            this.dataGridView4.Columns["Column15"].DataPropertyName = dtb.Columns["class"].ToString();

            this.dataGridView4.Columns["Column13"].DataPropertyName = dtb.Columns["name"].ToString();
            this.dataGridView4.Columns["Column17"].DataPropertyName = dtb.Columns["regtype"].ToString();
            this.dataGridView4.Columns["Column18"].DataPropertyName = dtb.Columns["level"].ToString();
            this.dataGridView4.Columns["Column19"].DataPropertyName = dtb.Columns["ifjiahao"].ToString();
            this.dataGridView4.Columns["Column20"].DataPropertyName = dtb.Columns["time"].ToString();
            this.dataGridView4.Columns["Column21"].DataPropertyName = dtb.Columns["paytype"].ToString();
            this.dataGridView4.Columns["Column22"].DataPropertyName = dtb.Columns["regpay"].ToString();
            //this.dataGridView4.Columns["Column19"].DataPropertyName = dtb.Columns["ifjiahao"].ToString();
            this.dataGridView4.Columns["regID"].Visible = false;
            this.dataGridView4.Columns["paName"].Visible = false;
            this.dataGridView4.Columns["class"].Visible = false;
            this.dataGridView4.Columns["name"].Visible = false;

            this.dataGridView4.Columns["level"].Visible = false;
            this.dataGridView4.Columns["regtype"].Visible = false;
            this.dataGridView4.Columns["ifjiahao"].Visible = false;
            this.dataGridView4.Columns["time"].Visible = false;
            this.dataGridView4.Columns["paytype"].Visible = false;
            this.dataGridView4.Columns["regpay"].Visible = false;
        }

        private void printRegButton_Click(object sender, EventArgs e)
        {

        }

        private void regCostType_SelectedIndexChanged(object sender, EventArgs e)
        {
            int levelpay = 0;
            if (regLevel.SelectedIndex == -1)
            {

            }
            else if (regLevel.SelectedIndex == 0)
            {
                levelpay = 10;
            }
            else if (regLevel.SelectedIndex == 1)
            {
                levelpay = 20;
            }
            else if (regLevel.SelectedIndex == 2)
            {
                levelpay = 15;
            }
            else if (regLevel.SelectedIndex == 3)
            {
                levelpay = 10;
            }
            else if (regLevel.SelectedIndex == 4)
            {
                levelpay = 5;
            }

            int typepay = 0;
            if (regCostType.SelectedIndex == 0)
            {
                typepay = 10;
            }
            else if (regCostType.SelectedIndex == 1)
            {
                typepay = 15;
            }
            else if (regCostType.SelectedIndex == 2)
            {
                typepay = 20;
            }

            int sum = levelpay + typepay;
            textBox2.Text = sum.ToString();


        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("请输入医生工号");
            }
            else
            {
                dataGridView3.Rows.Clear();
                string query = "SELECT regandduty.`data`,classanddoctor.class,classanddoctor.`name`,COUNT(*),sum(regpay) FROM regandduty INNER JOIN classanddoctor ON regandduty.doctorID=classanddoctor.ID WHERE regandduty.doctorID='" + textBox1.Text + "' AND regandduty.ifUse='1' AND regandduty.`data`='" + dateTimePicker3.Value.Date.ToString() +"'";
                mySql sql = new mySql();

                MySqlDataReader mdr = sql.searchData(query);

                int i = 0;
                while (mdr.Read())
                {
                    dataGridView3.Rows.Add();
                    dataGridView3.Rows[i].Cells["Column16"].Value = Convert.ToDateTime(mdr["data"].ToString()).Date.ToString("yyyy-MM-dd"); ;
                    dataGridView3.Rows[i].Cells["Column23"].Value = mdr["class"].ToString();
                    dataGridView3.Rows[i].Cells["Column24"].Value = mdr["name"].ToString();
                    dataGridView3.Rows[i].Cells["Column25"].Value = mdr["COUNT(*)"].ToString();
                    dataGridView3.Rows[i].Cells["Column26"].Value = mdr["sum(regpay)"].ToString();
                    i++;

                }
            }
        }
    }
}
