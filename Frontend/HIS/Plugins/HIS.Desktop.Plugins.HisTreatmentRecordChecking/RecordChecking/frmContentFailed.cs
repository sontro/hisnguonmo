using HIS.Desktop.ApiConsumer;
using HIS.Desktop.LocalStorage.Location;
using HIS.Desktop.Utility;
using Inventec.Common.Adapter;
using Inventec.Core;
using Inventec.Desktop.Common.Message;
using MOS.EFMODEL.DataModels;
using MOS.SDO;
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

namespace HIS.Desktop.Plugins.HisTreatmentRecordChecking.RecordChecking
{
    public partial class frmContentFailed : FormBase
    {

        private Inventec.Desktop.Common.Modules.Module moduleData;
        private long? treatmentId;
        private Action<bool> dlgIsSuccess;
        public frmContentFailed(Inventec.Desktop.Common.Modules.Module _moduleData, long? treatmentId, Action<bool> dlgIsSuccess)
            : base(_moduleData)
        {
            InitializeComponent();
            this.moduleData = _moduleData;
            this.treatmentId = treatmentId;
            this.dlgIsSuccess = dlgIsSuccess;
            SetIcon();
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

        private void frmContentFailed_Load(object sender, EventArgs e)
        {
            try
            {
                WaitingManager.Show();

                txtRejectReason.Text = "";

                WaitingManager.Hide();
            }
            catch (Exception ex)
            {
                WaitingManager.Hide();
                Inventec.Common.Logging.LogSystem.Error(ex);                
            }
        }


        private void btnAgree_Click(object sender, EventArgs e)
        {
            try
            {
                CommonParam param = new CommonParam();
                bool success = false;

                HisTreatmentRejectStoreSDO RejectStoreSDO = new HisTreatmentRejectStoreSDO();
                RejectStoreSDO.RejectReason = txtRejectReason.Text;
                RejectStoreSDO.TreatmentId = (long)this.treatmentId;

                var resultData = new BackendAdapter(param).Post<HIS_TREATMENT>("api/HisTreatment/RejectStore", ApiConsumers.MosConsumer, RejectStoreSDO, param);

                if (resultData != null)
                {
                    success = true;

                }
                if (dlgIsSuccess != null)
                {
                    dlgIsSuccess(success);
                }
                this.Close();

                MessageManager.Show(this, param, success);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void btnToIgnore_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);                
            }
        }

    }
}
