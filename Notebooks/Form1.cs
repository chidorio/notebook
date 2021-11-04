using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Notebooks
{
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = ColorTranslator.FromHtml("#0F3460");
            textBox1.BackColor = ColorTranslator.FromHtml("#0F3460");
            textBox1.ReadOnly = true;
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aboutProg aboutProg = new aboutProg();
            aboutProg.Show();
        }
    }
}
