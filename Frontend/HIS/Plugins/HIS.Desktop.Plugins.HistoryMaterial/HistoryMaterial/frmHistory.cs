using HIS.Desktop.LocalStorage.Location;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.HistoryMaterial.HistoryMaterial
{
    public partial class frmHistory : HIS.Desktop.Utility.FormBase
    {
        UC_HistoryMaterial uc;

        public frmHistory(Inventec.Desktop.Common.Modules.Module _module, long materialTypeId)
            : base(_module)
        {
            InitializeComponent();
            SetIcon();
            if (_module != null)
            {
                this.Text = _module.text;
            }

            uc = new UC_HistoryMaterial(_module, materialTypeId);
            uc.Dock = DockStyle.Fill;
            uc.layoutControlItem2.Size = new Size(110, uc.layoutControlItem2.Height);

            panelControl1.Controls.Add(uc);
        }
        public frmHistory(Inventec.Desktop.Common.Modules.Module _module, long materialTypeId, string _pakage_number)
            : base(_module)
        {
            InitializeComponent();
            SetIcon();
            if (_module != null)
            {
                this.Text = _module.text;
            }

            uc = new UC_HistoryMaterial(_module, materialTypeId, _pakage_number);
            uc.Dock = DockStyle.Fill;
            uc.layoutControlItem2.Size = new Size(110, uc.layoutControlItem2.Height);

            panelControl1.Controls.Add(uc);
        }

        public frmHistory()
        {
            InitializeComponent();
        }

        private void frmHistory_Load(object sender, EventArgs e)
        {

        }

        private void SetIcon()
        {
            try
            {
                this.Icon = Icon.ExtractAssociatedIcon(System.IO.Path.Combine(ApplicationStoreLocation.ApplicationDirectory, ConfigurationSettings.AppSettings["Inventec.Desktop.Icon"]));
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                uc.btnSearch_Click(null, null);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                uc.btnExport_Click(null, null);
            }
            catch (Exception ex)
            {
                
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
    }
}
