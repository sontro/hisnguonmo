using Inventec.Common.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.HisServSegr
{
    public partial class frmServSegr : HIS.Desktop.Utility.FormBase
    {
        ucServSegr uCServSegr;
        public frmServSegr()
        {
            InitializeComponent();
        }

        public frmServSegr(Inventec.Desktop.Common.Modules.Module moduleData)
            : base(moduleData)
        {
            InitializeComponent();
            try
            {
                string iconPath = System.IO.Path.Combine(HIS.Desktop.LocalStorage.Location.ApplicationStoreLocation.ApplicationStartupPath, System.Configuration.ConfigurationSettings.AppSettings["Inventec.Desktop.Icon"]);
                this.Icon = Icon.ExtractAssociatedIcon(iconPath);

                if (moduleData != null)
                {
                    this.Text = moduleData.text;
                }
                uCServSegr = new ucServSegr(moduleData);
                this.panelControlServSegr.Controls.Add(uCServSegr);
                uCServSegr.Dock = DockStyle.Fill;
                this.TopMost = true;
            }
            catch (Exception ex)
            {
                LogSystem.Warn(ex);
            }
        }

        private void frmServSegr_Load(object sender, EventArgs e)
        {

        }

        private void bbtnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            uCServSegr.SaveShortcut();
        }

        private void bbtnAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            uCServSegr.SaveServiceGroup();
        }

        private void bbtnRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            uCServSegr.New();
        }

        private void bbtnEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            uCServSegr.EditServiceGroup();
        }

        private void bbtnSearch1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            uCServSegr.FindShortcut1();
        }

        private void bbtnSearch2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            uCServSegr.FindShortcut2();
        }
    }
}
