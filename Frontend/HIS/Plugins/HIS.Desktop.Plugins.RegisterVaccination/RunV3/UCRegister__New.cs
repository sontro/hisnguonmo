using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HIS.Desktop.Utility;
using HIS.Desktop.LocalStorage.LocalData;
using HIS.Desktop.Plugins.Library.RegisterConfig;
using MOS.EFMODEL.DataModels;

namespace HIS.Desktop.Plugins.RegisterVaccination.Run3
{
    public partial class UCRegister : UserControlBase
    {
        private void RefreshUserControl()
        {
            try
            {
                this.currentHisExamServiceReqResultSDO = null;
                this.resultHisPatientProfileSDO = null;
                this.ucPatientRaw1.RefreshUserControl();
                this.ucAddressCombo1.RefreshUserControl();
                this.SetPatientSearchPanel(false);
                this.EnableControl(true);
                this.actionType = GlobalVariables.ActionAdd;
                this.ResetVariableUCAddress(false);
                this._TreatmnetIdByAppointmentCode = 0;
                this.cardSearch = null;

                txtDanToc.Text = "";
                cboDanToc.EditValue = null;
                txtNguoiNha.Text = "";
                txtQuanHe.Text = "";
                txtCMND.Text = "";
                txtDiaChi.Text = "";

                dtThoiGian.DateTime = DateTime.Now;

                InitDanhSachThuocTheoPhongTiem();

                this.ucPatientRaw1.FocusUserControl();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void ResetVariableUCAddress(bool isTrue)
        {
            try
            {
                this.ucAddressCombo1.isReadCard = isTrue;
                this.ucAddressCombo1.isPatientBHYT = isTrue;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void SetPatientSearchPanel(bool isFinded)
        {
            try
            {
                if (isFinded)
                {
                    this.lcibtnPatientNewInfo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                }
                else
                {
                    this.currentPatientSDO = null;
                    this.lcibtnPatientNewInfo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void EnableControl(bool _isEnable)
        {
            try
            {
                this.btnSave.Enabled = this.btnSaveAndPrint.Enabled = _isEnable;
                this.btnDepositDetail.Enabled = this.btnPrint.Enabled = !_isEnable;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
    }
}
