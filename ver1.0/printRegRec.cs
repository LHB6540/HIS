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
    public partial class printRegRec : Form
    {
        string regid, name, cardnum, age, phone, sex, address, classname, guomin, date, history, bodyhea, talk, now, check, withcheck, order, chufang = "";

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                /*printDocument.PrintPage += new PrintPageEventHandler(PrintDocument_PrintPage);
                printDocument.Print();
                */
                printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
                //printDocument1.PrinterSettings.
                printPreviewDialog1.Document = printDocument1;
                printDocument1.PrinterSettings.Copies = 0;
                DialogResult result = printPreviewDialog1.ShowDialog();

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

        //string regid; name=; cardnum, age, phone, sex, address, classname, guomin, date, history, bodyhea, talk, now, check, withcheck, order, chufang

        public printRegRec(string regid, string name, string cardnum, string age, string phone, string sex, string address, string classname, string guomin, string date, string history, string bodyhea, string talk, string now, string check, string withcheck, string order, string chufang)
        {
            InitializeComponent();

            textBox91.Text = regid;
            textBox93.Text = name;
            textBox92.Text = cardnum;
            textBox94.Text = age;
            textBox89.Text = phone;
            textBox1.Text = sex;
            textBox88.Text = address;
            textBox90.Text = guomin;
            textBox102.Text = history;
            textBox101.Text = bodyhea;
            textBox100.Text = talk;
            textBox99.Text = now;
            textBox98.Text = check;
            textBox97.Text = withcheck;
            textBox96.Text = order;
            textBox95.Text = chufang;
            textBox2.Text = classname;
            dateTimePicker13.Value = Convert.ToDateTime(date);


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


        private void button1_Click(object sender, EventArgs e)
        {
            
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
