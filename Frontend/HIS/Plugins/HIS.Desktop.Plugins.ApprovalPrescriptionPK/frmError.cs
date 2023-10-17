using HIS.Desktop.Plugins.ApprovalPrescriptionPK.ADO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.ApprovalPrescriptionPK
{
    public partial class frmError : HIS.Desktop.Utility.FormBase
    {
        Inventec.Desktop.Common.Modules.Module currentModule = null;
        List<ErrorADO> lstErrorADO = new List<ErrorADO>();

        public frmError(Inventec.Desktop.Common.Modules.Module module, List<ErrorADO> _lstErrorADO)
            : base(module)
        {
            InitializeComponent();

            try
            {
                this.currentModule = module;
                this.lstErrorADO = _lstErrorADO;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);                
            }
            
        }

        private void frmError_Load(object sender, EventArgs e)
        {
            string Error = "";
            if (this.lstErrorADO != null && lstErrorADO.Count > 0)
            {
                foreach (var item in lstErrorADO)
                {
                    Error += item.ErrorCode + " - " + item.ErrorReason + "\r\n";
                }
            }

            txtError.Text = Error;
        }
    }
}
