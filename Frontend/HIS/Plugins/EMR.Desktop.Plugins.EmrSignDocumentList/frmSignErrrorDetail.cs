using EMR.Desktop.Plugins.EmrSignDocumentList.ADO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EMR.Desktop.Plugins.EmrSignDocumentList
{
    public partial class frmSignErrrorDetail : Form
    {
        List<SignErrorADO> signErrorADOs;
        public frmSignErrrorDetail(List<SignErrorADO> _signErrorADOs)
        {
            InitializeComponent();
            this.signErrorADOs = _signErrorADOs;
        }

        private void frmSignErrrorDetail_Load(object sender, EventArgs e)
        {
            ShowErrorToControl();
        }

        internal void ShowErrorToControl()
        {
            try
            {
                Inventec.Common.Logging.LogSystem.Debug(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => signErrorADOs), signErrorADOs));
                if (signErrorADOs != null && signErrorADOs.Count > 0)
                {
                    foreach (var item in signErrorADOs)
                    {
                        txtErrorList.Text += item.NAME + "\r\n";
                    }
                }
                else
                {
                    txtErrorList.Text = "";
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
