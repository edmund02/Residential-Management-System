using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Residential_Management
{
    public partial class Report : Form
    {
        public Report()
        {
            InitializeComponent();
        }

        private void Report_Load(object sender, EventArgs e)
        {
            axAcroPDF1.LoadFile(fileName: "D:\\MMU\\MMU BACHELOR OF COMPUTER SCIENCE\\SE Fundamental\\Residential Management FinalVersion2\\Residential Management\\bin\\Debug\\Report.pdf");
        }
    }
}
