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
    public partial class choose : Form
    {
        string useID = "";
        public choose(string i, string ID)
        {
            InitializeComponent();
            if (i == "1")
            {
                button1.Enabled = true;
                button2.Enabled = false;
                button3.Enabled = button4.Enabled = false;
            }
            else if (i == "2")
            {
                button1.Enabled = button3.Enabled = button4.Enabled = false;
                button2.Enabled = true;
            }
            else if (i == "3")
            {
                button1.Enabled = button2.Enabled = button4.Enabled = false;
                button3.Enabled = true;
            }
            else if (i == "4")
            {
                button1.Enabled = button2.Enabled = button3.Enabled = false;
                button4.Enabled = true;
            }
            else
            {
                button1.Enabled = button2.Enabled = button3.Enabled = button4.Enabled = false;
                MessageBox.Show("当前账号没有任何权限，请联系管理员");
            }
            useID = ID;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new registrationSystem().Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new doctorWorkstation(useID).Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new outpatientPayment(useID).Show();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new pharmacyManagement().Show();
            this.Close();
        }

        private void choose_Load(object sender, EventArgs e)
        {

        }
    }
}
