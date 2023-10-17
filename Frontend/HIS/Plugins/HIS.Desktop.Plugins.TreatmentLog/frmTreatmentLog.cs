using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.TreatmentLog
{
    public partial class frmTreatmentLog : HIS.Desktop.Utility.FormBase
    {
        long TreatmentId = 0;
        Inventec.Desktop.Common.Modules.Module module;
        long currentRoomId = 0;
        public frmTreatmentLog(Inventec.Desktop.Common.Modules.Module _Module, long _treatmentId, long _currentRoomId)
		:base(_Module)
        {
            InitializeComponent();
            TreatmentId = _treatmentId;
            module = _Module;
            this.currentRoomId = _currentRoomId;
            UCTreatmentProcessPartial UCtreatmentProcessPartial = new UCTreatmentProcessPartial(module, TreatmentId, currentRoomId);
            this.xtraUserControl1.Controls.Add(UCtreatmentProcessPartial);
            UCtreatmentProcessPartial.Dock = DockStyle.Fill;
            if (module != null)
            {
                this.Text = module.text;
            }
        }

        private void frmTreatmentLog_Load(object sender, EventArgs e)
        {
            SetIcon();
        }
        private void SetIcon()
        {
            try
            {
                string iconPath = System.IO.Path.Combine(HIS.Desktop.LocalStorage.Location.ApplicationStoreLocation.ApplicationStartupPath, System.Configuration.ConfigurationSettings.AppSettings["Inventec.Desktop.Icon"]);
                this.Icon = Icon.ExtractAssociatedIcon(iconPath);
            }
            catch (Exception ex)
            {
                LogSystem.Warn(ex);
            }
        }
    }
}
