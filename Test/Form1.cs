//Apache2, 2017, WinterDev
using System;
using System.Collections;
using System.Text;
using System.Windows.Forms;

namespace Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string testHtml = "<html><head></head><body></body></html>";
            //1. tokenize the string buffer
            char[] strBuffer = testHtml.ToCharArray();
            //-----------------------------------------------------

        }
    }
}
