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
    public partial class FormAbout : Form
    {
        public FormAbout() {
            InitializeComponent();
            using (var sweph = new SwissEphNet.SwissEph())
            {
                lblVersion.Text = $"Version {sweph.swe_version()}";
                lblDotnetVersion.Text = $".Net Version {sweph.swe_dotnet_version()}";
            }
        }
    }
}
