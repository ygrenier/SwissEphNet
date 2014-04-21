using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SweWin
{
    public partial class FormMain : Form
    {
        public FormMain() {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e) {
            using (var frm = new FormAbout()) {
                frm.ShowDialog();
            }
        }

        private void calculateToolStripMenuItem_Click(object sender, EventArgs e) {
            using (var frm = new FormData()) {
                frm.ShowDialog();
            }
        }
    }
}
