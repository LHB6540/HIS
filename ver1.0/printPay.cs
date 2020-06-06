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
    public partial class printPay : Form
    {
        DataGridView gridView = null;
        string doctor, shoufeiyuan, zongfeiyong, paytype = "";
        public printPay(string doctor,string shoufeiyuan,string zongfeiyong,string paytype,DataTable gridView)
        {
            InitializeComponent();
            dataGridView1.DataSource = gridView;
            textBox6.Text = doctor;
            textBox7.Text = shoufeiyuan;
            textBox3.Text = zongfeiyong;
            textBox1.Text = paytype;
        }

        private void 打印发票_Load(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

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
