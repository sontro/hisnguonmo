using DevExpress.XtraEditors;
using HIS.Desktop.Common;
using HIS.Desktop.LocalStorage.BackendData.ADO;
using HIS.Desktop.Plugins.AssignNutrition.Config;
using HIS.Desktop.Plugins.AssignNutrition.Resources;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.AssignNutrition.AssignNutrition
{
    public partial class frmWaringConfigIcdService : Form
    {
        List<HIS_ICD> icdCodeFromUc;
        List<SereServADO> serviceCheckeds__Send;
        Inventec.Desktop.Common.Modules.Module currentModule;
        DelegateSelectData delegateSelectData;
        bool isYes = false;
        public System.Drawing.Size OldSize { get; set; }
        public frmWaringConfigIcdService(List<HIS_ICD> _icdCodeFromUc, List<SereServADO> _serviceCheckeds__Send, Inventec.Desktop.Common.Modules.Module _currentModule, DelegateSelectData _delegateSelectData)
        {
            InitializeComponent();
            this.icdCodeFromUc = _icdCodeFromUc;
            this.serviceCheckeds__Send = _serviceCheckeds__Send;
            this.currentModule = _currentModule;
            this.delegateSelectData = _delegateSelectData;
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

        private void frmWaringConfigIcdService_Load(object sender, EventArgs e)
        {
            try
            {
                if (HisConfigCFG.IcdServiceAllowUpdate == "1")
                {
                    btnServiceICD.Enabled = true;
                }
                else
                {
                    btnServiceICD.Enabled = false;
                }
                if (this.serviceCheckeds__Send != null && this.serviceCheckeds__Send.Count > 0)
                {
                    string serviceNames = "";
                    foreach (var item in this.serviceCheckeds__Send)
                    {
                        serviceNames += item.TDL_SERVICE_CODE + " - "+item.TDL_SERVICE_NAME+"; ";
                    }
                    txtWarning.Text = string.Format("ResourceMessage.DichVuChuaDuocCauHinhICDDichVu", serviceNames);
                }
                btnYes.Focus();
                btnYes.Select();
            }
            catch (Exception ex)
            {
                LogSystem.Warn(ex);
            }
        }

        private void btnServiceICD_Click(object sender, EventArgs e)
        {
            try
            {
                List<long> serviceNotConfigIds = new List<long>();
                if (this.icdCodeFromUc != null && this.icdCodeFromUc.Count > 0 && this.serviceCheckeds__Send != null && serviceCheckeds__Send.Count > 0)
                {
                    this.Close();
                    this.isYes = false;
                    if (this.delegateSelectData != null)
                    {
                        this.delegateSelectData(this.isYes);
                    }
                    serviceNotConfigIds = this.serviceCheckeds__Send.Select(o => o.SERVICE_ID).Distinct().ToList();
                    List<object> listObj = new List<object>();
                    listObj.Add(this.icdCodeFromUc);
                    listObj.Add(serviceNotConfigIds);
                    HIS.Desktop.ModuleExt.PluginInstanceBehavior.ShowModule("HIS.Desktop.Plugins.ServiceIcd", this.currentModule.RoomId, this.currentModule.RoomTypeId, listObj);
                }
                else
                {
                    Inventec.Common.Logging.LogSystem.Debug("icdCodeFromUc hoac serviceCheckeds__Send is null: " + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => this.icdCodeFromUc), this.icdCodeFromUc) + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => serviceNotConfigIds), serviceNotConfigIds));
                }
            }
            catch (Exception ex)
            {
                LogSystem.Warn(ex);
            }
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            try
            {
                this.isYes = false;
                if (this.delegateSelectData != null)
                {
                    this.delegateSelectData(this.isYes);
                }
                this.Close();
            }
            catch (Exception ex)
            {
                LogSystem.Warn(ex);
            }
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            try
            {
                this.isYes = true;
                if (this.delegateSelectData != null)
                {
                    this.delegateSelectData(this.isYes);
                }
                this.Close();
            }
            catch (Exception ex)
            {
                LogSystem.Warn(ex);
            }
        }
    }
}
