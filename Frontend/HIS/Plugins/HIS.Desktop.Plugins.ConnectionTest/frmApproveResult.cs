using HIS.Desktop.Plugins.ConnectionTest.ADO;
using LIS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Inventec.Common.Adapter;
using Inventec.Core;
using HIS.Desktop.Controls.Session;
using Inventec.Desktop.Common.Message;
using LIS.SDO;

namespace HIS.Desktop.Plugins.ConnectionTest
{
    public partial class frmApproveResult : Form
    {
        LisSampleADO ado;
        Action<LIS_SAMPLE> action;
        Action<bool> IsShowMessage;
        bool IsClickSave;
        public frmApproveResult(LisSampleADO ado, Action<LIS_SAMPLE> action, Action<bool> IsShowMessage = default(Action<bool>))
        {
            InitializeComponent();
            try
            {
                this.ado = ado;
                this.action = action;
                this.IsShowMessage = IsShowMessage;
                SetIconFrm();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
        void SetIconFrm()
        {
            try
            {
                string iconPath = System.IO.Path.Combine(HIS.Desktop.LocalStorage.Location.ApplicationStoreLocation.ApplicationStartupPath, System.Configuration.ConfigurationSettings.AppSettings["Inventec.Desktop.Icon"]);
                this.Icon = Icon.ExtractAssociatedIcon(iconPath);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void frmApproveResult_Load(object sender, EventArgs e)
        {
            try
            {
                dteApprove.DateTime = DateTime.Now;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                WaitingManager.Show();
                CommonParam param = new CommonParam();
                LisSampleApproveResultSDO sdo = new LisSampleApproveResultSDO();
                sdo.SampleId = ado.ID;
                sdo.ApproveTime = Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(dteApprove.DateTime);
                var curentSTT = new BackendAdapter(param).Post<LIS_SAMPLE>("api/LisSample/ApproveResult", ApiConsumer.ApiConsumers.LisConsumer, sdo, param);
                this.action(curentSTT);
                IsClickSave = true;
                WaitingManager.Hide();
                #region Show message

                MessageManager.Show(this.ParentForm, param, curentSTT != null);
                #endregion
                #region Process has exception
                SessionManager.ProcessTokenLost(param);
                #endregion
                if (curentSTT != null)
                    this.Close();
            }
            catch (Exception ex)
            {
                WaitingManager.Hide();
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            btnSave_Click(null, null);
        }

        private void frmApproveResult_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                if(IsShowMessage != null)
                    IsShowMessage(IsClickSave);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
