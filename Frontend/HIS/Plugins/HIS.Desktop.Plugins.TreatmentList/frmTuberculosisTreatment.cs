using HIS.Desktop.ApiConsumer;
using HIS.Desktop.LocalStorage.BackendData;
using HIS.Desktop.Utility;
using Inventec.Common.Adapter;
using Inventec.Common.Controls.EditorLoader;
using Inventec.Core;
using Inventec.Desktop.Common.Message;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.TreatmentList
{
    public partial class frmTuberculosisTreatment : FormBase
    {
        //List<HIS_MEDI_ORG> Mediorg = new List<HIS_MEDI_ORG>();
        public List<HIS_MEDI_ORG> listIssuedBy { get; set; }
        public HIS_TREATMENT treatment { get; set; }
        public long treamentId;
        public frmTuberculosisTreatment(long id)
        {

            InitializeComponent();
            try
            {
                this.treamentId = id;
                string iconPath = System.IO.Path.Combine(HIS.Desktop.LocalStorage.Location.ApplicationStoreLocation.ApplicationStartupPath, System.Configuration.ConfigurationSettings.AppSettings["Inventec.Desktop.Icon"]);
                this.Icon = Icon.ExtractAssociatedIcon(iconPath);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void frmTuberculosisTreatment_Load(object sender, EventArgs e)
        {
            try
            {
                LoadTreament();
                InitComboIssuedBy();
                FillData(treatment);
                Display();
            }

            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void LoadTreament()
        {
            try
            {
                CommonParam param = new CommonParam();
                HisTreatmentFilter filter = new HisTreatmentFilter();
                filter.ID = treamentId;
                treatment = new BackendAdapter(param).Get<List<HIS_TREATMENT>>(HisRequestUriStore.HIS_TREATMENT_GET, ApiConsumers.MosConsumer, filter, param).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
        private void Display()
        {
            try
            {
                btnSave.Enabled = treatment.IS_LOCK_HEIN != 1;
                btnSave.Select();
                btnSave.Focus();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
        private void InitComboIssuedBy()
        {
            try
            {
                var data = BackendDataWorker.Get<HIS_MEDI_ORG>().ToList();
                listIssuedBy = new List<HIS_MEDI_ORG>();
                listIssuedBy = data;
                List<ColumnInfo> columnInfos = new List<ColumnInfo>();
                columnInfos.Add(new ColumnInfo("MEDI_ORG_CODE", "", 100, 1));
                columnInfos.Add(new ColumnInfo("MEDI_ORG_NAME", "", 250, 2));
                ControlEditorADO controlEditorADO = new ControlEditorADO("MEDI_ORG_NAME", "MEDI_ORG_CODE", columnInfos, false, 350);
                controlEditorADO.ImmediatePopup = true;
                ControlEditorLoader.Load(cboIssuedBy, data, controlEditorADO);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void FillData(HIS_TREATMENT data)
        {
            try
            {
                if (data != null && !string.IsNullOrEmpty(data.TUBERCULOSIS_ISSUED_ORG_CODE))
                {
                    cboIssuedBy.EditValue = data.TUBERCULOSIS_ISSUED_ORG_CODE;
                }
                else
                {
                    cboIssuedBy.EditValue = BranchDataWorker.Branch.HEIN_MEDI_ORG_CODE;
                }

                if (data == null || data.TUBERCULOSIS_ISSUED_DATE == null)
                {
                    cboDateRange.DateTime = DateTime.Now;
                }
                else
                {
                    cboDateRange.DateTime = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(data.TUBERCULOSIS_ISSUED_DATE ?? 0) ?? DateTime.Now;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                SaveProcess();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void SaveProcess()
        {
            try
            {
                WaitingManager.Show();

                MOS.SDO.HisTreatmentTuberculosisIssuedInfoSDO sdo = new MOS.SDO.HisTreatmentTuberculosisIssuedInfoSDO();
                CommonParam param = new CommonParam();
                sdo.TreatmentId = treamentId;
                sdo.TuberculosisIssuedOrgCode = cboIssuedBy.EditValue != null ? cboIssuedBy.EditValue.ToString() : null;
                sdo.TuberculosisIssuedDate = cboDateRange.DateTime != null && cboDateRange.DateTime != DateTime.MinValue ? Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(cboDateRange.DateTime) : null;
                var dt = new BackendAdapter(param).Post<HisTreatmentTuberculosisIssuedInfoSDO>("api/HisTreatment/UpdateTuberculosisIssuedInfo", ApiConsumers.MosConsumer, sdo, param);
                WaitingManager.Hide();

                if (dt != null)
                {
                    this.Close();
                }
                else
                {
                    MessageManager.Show(this, param, false);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

    }
}
