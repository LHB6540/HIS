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
    public partial class printReg : Form
    {
        string regid,cardnum,classtype,classname,level,costtype,ifjiahao,paname,doname,sex,time,paytype,regpay = "";

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
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

        public printReg(string regid, string cardnum, string classtype, string classname, string level, string costtype, string ifjiahao, string paname, string doname, string sex, string time, string paytype, string regpay)
        {
            InitializeComponent();
            label13.Text = regid;
            label14.Text = paname;
            label24.Text = cardnum;
            label15.Text = sex;
            label16.Text = classtype;
            label32.Text = time;
            label17.Text = classname;
            label19.Text = doname;
            label26.Text = level;
            label21.Text = paytype;
            label18.Text = costtype;
            label23.Text = regpay;
            label20.Text = ifjiahao;



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


        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
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
    }
}
