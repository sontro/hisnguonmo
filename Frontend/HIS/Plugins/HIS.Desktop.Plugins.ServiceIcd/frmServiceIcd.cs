using HIS.Desktop.LocalStorage.Location;
using MOS.EFMODEL.DataModels;
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

namespace HIS.Desktop.Plugins.ServiceIcd
{
    public partial class frmServiceIcd : HIS.Desktop.Utility.FormBase
    {
        Inventec.Desktop.Common.Modules.Module currentModule;
        HIS_ICD currentIcd;
        List<HIS_ICD> currentListIcd;
        List<long> currentServiceIds;
        List<long> currentActiveIngredientIds;
        public frmServiceIcd()
        {
            InitializeComponent();
        }

        public frmServiceIcd(Inventec.Desktop.Common.Modules.Module _module, HIS_ICD _icd, List<long> _serviceIds, List<long> activeIngredientIds):base(_module)
        {
            InitializeComponent();
            try
            {
                this.currentIcd = _icd;
                this.currentModule = _module;
                this.currentServiceIds = _serviceIds;
                this.currentActiveIngredientIds = activeIngredientIds;

                SetIcon();
                if (this.currentModule != null)
                {
                    this.Text = this.currentModule.text;
                }

                UCServiceIcd uCServiceIcd = new UCServiceIcd(this.currentIcd, this.currentServiceIds, this.currentActiveIngredientIds);
                this.panelControlServiceIcd.Controls.Add(uCServiceIcd);
                uCServiceIcd.Dock = DockStyle.Fill;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }

        }

        public frmServiceIcd(Inventec.Desktop.Common.Modules.Module _module, List<HIS_ICD> _listIcd, List<long> _serviceIds, List<long> activeIngredientIds):base(_module)
        {
            InitializeComponent();
            try
            {
                this.currentListIcd = _listIcd;
                this.currentModule = _module;
                this.currentServiceIds = _serviceIds;
                this.currentActiveIngredientIds = activeIngredientIds;

                SetIcon();
                if (this.currentModule != null)
                {
                    this.Text = this.currentModule.text;
                }

                UCServiceIcd uCServiceIcd = new UCServiceIcd(this.currentListIcd, this.currentServiceIds, this.currentActiveIngredientIds);
                this.panelControlServiceIcd.Controls.Add(uCServiceIcd);
                uCServiceIcd.Dock = DockStyle.Fill;
                this.TopMost = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }

        }

        private void frmServiceIcd_Load(object sender, EventArgs e)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }

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
    }
}
