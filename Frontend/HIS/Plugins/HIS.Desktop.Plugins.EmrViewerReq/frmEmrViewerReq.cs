using EMR.EFMODEL.DataModels;
using HIS.Desktop.ApiConsumer;
using HIS.Desktop.LocalStorage.BackendData;
using HIS.Desktop.Utility;
using Inventec.Common.Adapter;
using Inventec.Common.Logging;
using Inventec.Core;
using Inventec.Desktop.Common.Message;
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

namespace HIS.Desktop.Plugins.EmrViewerReq
{
    public partial class frmEmrViewerReq : FormBase
    {
        Inventec.Desktop.Common.Modules.Module currentModule;
        long _TreatmentId = 0;

        int positionHandle = -1;

        public frmEmrViewerReq()
        {
            InitializeComponent();
        }

        public frmEmrViewerReq(Inventec.Desktop.Common.Modules.Module module, long _treatmentId)
            : base(module)
        {

            InitializeComponent();
            currentModule = module;
            this._TreatmentId = _treatmentId;
            try
            {
                string iconPath = System.IO.Path.Combine(HIS.Desktop.LocalStorage.Location.ApplicationStoreLocation.ApplicationStartupPath, System.Configuration.ConfigurationSettings.AppSettings["Inventec.Desktop.Icon"]);
                this.Icon = Icon.ExtractAssociatedIcon(iconPath);
                if (currentModule != null)
                {
                    this.Text = currentModule.text;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Warn(ex);
            }
        }

        private void frmEmrViewerReq_Load(object sender, EventArgs e)
        {
            try
            {
                ValidateBedForm();
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
                if (this._TreatmentId > 0)
                {

                    this.positionHandle = -1;
                    if (!dxValidationProvider1.Validate())
                        return;

                    EMR.Filter.EmrTreatmentFilter treatmentFilter = new EMR.Filter.EmrTreatmentFilter();
                    treatmentFilter.ID = this._TreatmentId;
                    var rsTreatment = new BackendAdapter(new CommonParam()).Get<List<EMR_TREATMENT>>(EMR.URI.EmrTreatment.GET, ApiConsumers.EmrConsumer, treatmentFilter, null);
                    if (rsTreatment != null && rsTreatment.Count > 0)
                    {
                        DateTime reqTime = dtTime.DateTime;
                        DateTime inTimeTreatment = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(rsTreatment[0].IN_DATE) ?? DateTime.Now;
                        if (reqTime < inTimeTreatment)
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show("Ngày yêu cầu xem chi tiết bệnh án không được nhỏ hơn ngày nhập viện", "Thông báo");
                            dtTime.Focus();
                            dtTime.SelectAll();
                            return;
                        }
                    }


                    bool success = false;
                    CommonParam param = new CommonParam();

                    EMR.EFMODEL.DataModels.EMR_VIEWER ado = new EMR.EFMODEL.DataModels.EMR_VIEWER();
                    ado.TREATMENT_ID = this._TreatmentId;
                    ado.REQUEST_LOGINNAME = Inventec.UC.Login.Base.ClientTokenManagerStore.ClientTokenManager.GetLoginName();
                    DateTime? finishTime = dtTime.DateTime;
                    if (finishTime != null && finishTime.Value != DateTime.MinValue)
                    {
                        ado.REQUEST_FINISH_TIME = Inventec.Common.TypeConvert.Parse.ToInt64(finishTime.Value.ToString("yyyyMMdd") + "235959");// Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(dtTime.DateTime) ?? 0;
                    }
                    var room = BackendDataWorker.Get<V_HIS_ROOM>().FirstOrDefault(p => p.ID == currentModule.RoomId);
                    if (room != null)
                    {
                        //thaovtb yeu cau/ Ngay 20/12/2018
                        ado.DEPARTMENT_CODE = room.DEPARTMENT_CODE;
                        ado.DEPARTMENT_NAME = room.DEPARTMENT_NAME;
                    }
                    var outPut = new BackendAdapter(param).Post<EMR_VIEWER>(EMR.URI.EmrViewer.CREATE, ApiConsumer.ApiConsumers.EmrConsumer, ado, param);//"api/EmrViewer/Create"

                    if (outPut != null)
                    {
                        success = true;
                        this.Close();
                    }

                    #region Show message
                    MessageManager.Show(this, param, success);
                    #endregion
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void dxValidationProvider1_ValidationFailed(object sender, DevExpress.XtraEditors.DXErrorProvider.ValidationFailedEventArgs e)
        {
            try
            {
                DevExpress.XtraEditors.BaseEdit edit = e.InvalidControl as DevExpress.XtraEditors.BaseEdit;
                if (edit == null)
                    return;

                DevExpress.XtraEditors.ViewInfo.BaseEditViewInfo viewInfo = edit.GetViewInfo() as DevExpress.XtraEditors.ViewInfo.BaseEditViewInfo;
                if (viewInfo == null)
                    return;

                if (positionHandle == -1)
                {
                    positionHandle = edit.TabIndex;
                    if (edit.Visible)
                    {
                        edit.SelectAll();
                        edit.Focus();
                    }
                }
                if (positionHandle > edit.TabIndex)
                {
                    positionHandle = edit.TabIndex;
                    if (edit.Visible)
                    {
                        edit.SelectAll();
                        edit.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void ValidateBedForm()
        {
            try
            {
                DateFromValidationRule dateValidRule = new DateFromValidationRule();
                dateValidRule.dt = this.dtTime;
                dateValidRule.ErrorText = "Trường dữ liệu bắt buộc";
                dateValidRule.ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning;
                dxValidationProvider1.SetValidationRule(this.dtTime, dateValidRule);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void barButtonItem__Save_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            btnSave_Click(null, null);
        }
    }
}
