using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.DXErrorProvider;
using DevExpress.XtraEditors.ViewInfo;
using HIS.Desktop.LibraryMessage;
using HIS.Desktop.Plugins.TransDepartment.Validate.ValidationRule;
using Inventec.Common.Adapter;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.TransDepartment
{
    public partial class frmDepartmentTran : HIS.Desktop.Utility.FormBase
    {
        private void ValidControl()
        {
            try
            {
                ValidDepartment();
                //ValidLogTime();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void ValidLogTime()
        {
            DepartmentTran__TreatmentLogTimeValidationRule oDateRule = null;
            if (this.departmentTran.PREVIOUS_ID != null)
            {
                CommonParam param = new CommonParam();
                HisDepartmentTranFilter filter = new HisDepartmentTranFilter();
                filter.ID = this.departmentTran.PREVIOUS_ID;
                var preDepartmentTran = new BackendAdapter(param).Get<List<HIS_DEPARTMENT_TRAN>>("api/HisDepartmentTran/Get", ApiConsumer.ApiConsumers.MosConsumer, filter, param);

                if (preDepartmentTran != null && preDepartmentTran.Count > 0)
                {
                    oDateRule = new DepartmentTran__TreatmentLogTimeValidationRule(preDepartmentTran.FirstOrDefault().DEPARTMENT_IN_TIME);
                }
            }
            else
            {
                oDateRule = new DepartmentTran__TreatmentLogTimeValidationRule(null);
            }

            oDateRule.dtLogTime = dtLogTime;
            //oDateRule.ErrorText = MessageUtil.GetMessage(HIS.Desktop.LibraryMessage.Message.Enum.ThieuTruongDuLieuBatBuoc);
            oDateRule.ErrorType = ErrorType.Warning;
            this.dxValidationProvider1.SetValidationRule(dtLogTime, oDateRule);
        }

        private void ValidDepartment()
        {
            DepartmentTran__BedRoomValidationRule oDateRule = new DepartmentTran__BedRoomValidationRule();
            oDateRule.txtDepartmentCode = txtDepartmentCode;
            oDateRule.cboDepartment = cboDepartment;
            oDateRule.ErrorText = MessageUtil.GetMessage(HIS.Desktop.LibraryMessage.Message.Enum.ThieuTruongDuLieuBatBuoc);
            oDateRule.ErrorType = ErrorType.Warning;
            this.dxValidationProvider1.SetValidationRule(txtDepartmentCode, oDateRule);
        }

        private void dxValidationProvider1_ValidationFailed(object sender, DevExpress.XtraEditors.DXErrorProvider.ValidationFailedEventArgs e)
        {
            try
            {
                BaseEdit edit = e.InvalidControl as BaseEdit;
                if (edit == null)
                    return;

                BaseEditViewInfo viewInfo = edit.GetViewInfo() as BaseEditViewInfo;
                if (viewInfo == null)
                    return;

                if (positionHandleControl == -1)
                {
                    positionHandleControl = edit.TabIndex;
                    if (edit.Visible)
                    {
                        edit.SelectAll();
                        edit.Focus();
                    }
                }
                if (positionHandleControl > edit.TabIndex)
                {
                    positionHandleControl = edit.TabIndex;
                    if (edit.Visible)
                    {
                        edit.SelectAll();
                        edit.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
    }
}
