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

namespace ver1._0
{
    public partial class printCS : Form
    {
        string danhao, paname, sex, age, classname, zhenduan, regid, mudi, cardnum, pay, date, order, yishi = "";

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                /*printDocument.PrintPage += new PrintPageEventHandler(PrintDocument_PrintPage);
                printDocument.Print();
                */
                printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
                //printDocument1.PrinterSettings.
                printDocument1.Print();

                /*StandardPrintController spc = new StandardPrintController();
                this.printDocument1.PrintController = spc;
                //this.printDialog1.PrinterSettings.PrinterName = "Microsoft XPS Document Writer";
                this.printDocument1.Print();
                */
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "打印出错");
            }
        }

        public printCS(string danhao, string paname, string sex, string age, string classname, string zhenduan, string regid, string mudi, string cardnum, string pay, string date, string order, string yishi)
        {
            InitializeComponent();
            textBox80.Text = danhao;
            textBox7.Text = paname;
            textBox2.Text = sex;
            textBox8.Text = age;
            textBox3.Text = classname;
            textBox17.Text = zhenduan;
            textBox15.Text = regid;
            textBox18.Text = mudi;
            textBox14.Text = cardnum;
            textBox16.Text = pay;
            textBox31.Text = yishi;
            textBox1.Text = order;
            dateTimePicker1.Value = Convert.ToDateTime(date);



        }


        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            /* Image myFormImage;
             myFormImage = new Bitmap(this.Width, this.Height);
             Graphics g = Graphics.FromImage(myFormImage);
             g.CopyFromScreen(this.Location.X, this.Location.Y,0,0,this.Size);
             e.Graphics.DrawImage(myFormImage, 0, 0);
             */
            Bitmap _NewBitmap = new Bitmap(groupBox1.Width, groupBox1.Height);
            groupBox1.DrawToBitmap(_NewBitmap, new Rectangle(0, 0, _NewBitmap.Width, _NewBitmap.Height));
            e.Graphics.DrawImage(_NewBitmap, 0, 0, _NewBitmap.Width, _NewBitmap.Height);
        }


 

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
                printPreviewDialog1.Document = printDocument1;
                this.printPreviewDialog1.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "打印出错");
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            pageSetupDialog1.Document = printDocument1;
            try
            {
                this.pageSetupDialog1.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "打印出错");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
