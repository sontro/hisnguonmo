using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MOS.SDO;
using MOS.EFMODEL.DataModels;
using HIS.Desktop.ApiConsumer;
using Inventec.Core;
using Inventec.Common.Adapter;
using Inventec.Desktop.Common.Message;

namespace HIS.Desktop.Plugins.TreatmentInspection
{
    public partial class frmRefuseApprove : Form
    {
        long Treatmentid = 0;

        public frmRefuseApprove(long Treatmentid)
        {
            InitializeComponent();
            this.Treatmentid = Treatmentid;
            try
            {
                string iconPath = System.IO.Path.Combine(HIS.Desktop.LocalStorage.Location.ApplicationStoreLocation.ApplicationStartupPath, System.Configuration.ConfigurationSettings.AppSettings["Inventec.Desktop.Icon"]);
                this.Icon = Icon.ExtractAssociatedIcon(iconPath);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void frmRefuseApprove_Load(object sender, EventArgs e)
        {
            try
            {
                txtReason.Text = "";
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);                
            }
        }

        private void btnIgnore_Click(object sender, EventArgs e)
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                bool Success = false;
                CommonParam param = new CommonParam();
                RecordInspectionRejectSdo input = new RecordInspectionRejectSdo();
                input.RecordInspectionRejectNote = txtReason.Text.Trim();
                input.TreatmentId = Treatmentid;

                var result = new BackendAdapter(param).Post<HIS_TREATMENT>(HisRequestUriStore.HIS_TREATMENT_RECORD_INSPECTION_REJECT, ApiConsumers.MosConsumer, input, param);

                if (result != null)
                {
                    Success = true;
                    this.Close();
                }
                MessageManager.Show(this, param, Success);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);                
            }
        }

        private void bbtnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                btnSave_Click(null, null);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);                
            }
        }


    }
}
