using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace HIS.Desktop.Plugins.ServiceReqPatient.Printf
{
    public partial class XtraForm1 : DevExpress.XtraEditors.XtraForm
    {
        ADO.Printf printf;
        public XtraForm1(ADO.Printf _printf)
        { 
            InitializeComponent();
            printf = _printf;
        }

        private void XtraForm1_Load(object sender, EventArgs e)
        {
            OpenFileDialog open1 = new OpenFileDialog();
            open1.
        }
    }
}