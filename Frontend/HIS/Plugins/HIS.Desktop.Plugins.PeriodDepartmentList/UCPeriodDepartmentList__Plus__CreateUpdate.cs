using HIS.Desktop.ApiConsumer;
using HIS.Desktop.Controls.Session;
using HIS.Desktop.LibraryMessage;
using HIS.Desktop.LocalStorage.BackendData;
using HTC.EFMODEL.DataModels;
using Inventec.Core;
using Inventec.Desktop.Common.Message;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.PeriodDepartmentList
{
    public partial class UCPeriodDepartmentList
    {
        private void txtPeriodCode_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (!String.IsNullOrEmpty(txtPeriodCode.Text))
                    {
                        string key = txtPeriodCode.Text.Trim().ToLower();
                        var listData = BackendDataWorker.Get<HTC_PERIOD>().Where(o => o.PERIOD_CODE.ToLower().Contains(key) || o.PERIOD_NAME.ToLower().Contains(key)).ToList();
                        if (listData != null && listData.Count == 1)
                        {
                            txtPeriodCode.Text = listData.First().PERIOD_CODE;
                            cboPeriod.EditValue = listData.First().ID;
                            txtDepartmentCode.Focus();
                            txtDepartmentCode.SelectAll();
                        }
                        else
                        {
                            cboPeriod.Focus();
                            cboPeriod.ShowPopup();
                        }
                    }
                    else
                    {
                        cboPeriod.Focus();
                        cboPeriod.ShowPopup();
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void cboPeriod_Closed(object sender, DevExpress.XtraEditors.Controls.ClosedEventArgs e)
        {
            try
            {
                if (e.CloseMode == DevExpress.XtraEditors.PopupCloseMode.Normal)
                {
                    if (cboPeriod.EditValue != null)
                    {
                        var period = BackendDataWorker.Get<HTC_PERIOD>().FirstOrDefault(o => o.ID == Convert.ToInt64(cboPeriod.EditValue));
                        if (period != null)
                        {
                            txtPeriodCode.Text = period.PERIOD_CODE;
                        }
                    }
                    txtDepartmentCode.Focus();
                    txtDepartmentCode.SelectAll();
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void txtDepartmentCode_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (!String.IsNullOrEmpty(txtDepartmentCode.Text))
                    {
                        string key = txtDepartmentCode.Text.Trim().ToLower();
                        var listData = BackendDataWorker.Get<HIS_DEPARTMENT>().Where(o => o.DEPARTMENT_CODE.ToLower().Contains(key) || o.DEPARTMENT_NAME.ToLower().Contains(key)).ToList();
                        if (listData != null && listData.Count == 1)
                        {
                            txtDepartmentCode.Text = listData.First().DEPARTMENT_CODE;
                            cboDepartment.EditValue = listData.First().ID;
                            txtClinicalAmount.Focus();
                            txtClinicalAmount.SelectAll();
                        }
                        else
                        {
                            cboDepartment.Focus();
                            cboDepartment.ShowPopup();
                        }
                    }
                    else
                    {
                        cboDepartment.Focus();
                        cboDepartment.ShowPopup();
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void cboDepartment_Closed(object sender, DevExpress.XtraEditors.Controls.ClosedEventArgs e)
        {
            try
            {
                if (e.CloseMode == DevExpress.XtraEditors.PopupCloseMode.Normal)
                {
                    if (cboDepartment.EditValue != null)
                    {
                        var department = BackendDataWorker.Get<HIS_DEPARTMENT>().FirstOrDefault(o => o.ID == Convert.ToInt64(cboDepartment.EditValue));
                        if (department != null)
                        {
                            txtDepartmentCode.Text = department.DEPARTMENT_CODE;
                        }
                    }
                    txtClinicalAmount.Focus();
                    txtClinicalAmount.SelectAll();
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void txtEndTreatmentAmount_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtClinicalDayAmount.Focus();
                    txtClinicalDayAmount.SelectAll();
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void txtFromExamClinicalAmount_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtEndTreatmentAmount.Focus();
                    txtEndTreatmentAmount.SelectAll();
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void txtClinicalAmount_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtFromExamClinicalAmount.Focus();
                    txtFromExamClinicalAmount.SelectAll();
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void txtClinicalDayAmount_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtLaborAmount.Focus();
                    txtLaborAmount.SelectAll();
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void txtLaborAmount_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    SendKeys.Send("{TAB}");
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (!btnAdd.Enabled || !dxValidationProvider1.Validate())
                    return;
                WaitingManager.Show();
                CommonParam param = new CommonParam();
                bool success = false;
                HTC_PERIOD_DEPARTMENT data = new HTC_PERIOD_DEPARTMENT();
                var period = BackendDataWorker.Get<HTC_PERIOD>().FirstOrDefault(o => o.ID == Convert.ToInt64(cboPeriod.EditValue));
                if (period == null)
                {
                    goto End;
                }
                data.PERIOD_ID = period.ID;

                var department = BackendDataWorker.Get<HIS_DEPARTMENT>().FirstOrDefault(o => o.ID == Convert.ToInt64(cboDepartment.EditValue));
                if (department == null)
                {
                    goto End;
                }
                data.DEPARTMENT_CODE = department.DEPARTMENT_CODE;
                data.DEPARTMENT_NAME = department.DEPARTMENT_NAME;

                if (txtEndTreatmentAmount.Value > 0)
                {
                    data.END_TREATMENT_AMOUNT = Convert.ToInt64(txtEndTreatmentAmount.Text);
                }

                if (txtFromExamClinicalAmount.Value > 0)
                {
                    data.FROM_EXAM_CLINICAL_AMOUNT = Convert.ToInt64(txtFromExamClinicalAmount.Text);
                }

                if (txtClinicalAmount.Value > 0)
                {
                    data.CLINICAL_AMOUNT = Convert.ToInt64(txtClinicalAmount.Text);
                }

                if (txtClinicalDayAmount.Value > 0)
                {
                    data.CLINICAL_DAY_AMOUNT = Convert.ToInt64(txtClinicalDayAmount.Text);
                }

                if (txtLaborAmount.Value > 0)
                {
                    data.LABOR_AMOUNT = Convert.ToInt64(txtLaborAmount.Text);
                }

                var rs = new Inventec.Common.Adapter.BackendAdapter(param).Post<HTC_PERIOD_DEPARTMENT>(HtcRequestUriStore.HTC_PERIOD_DEPARTMENT__CREATE, ApiConsumers.HtcConsumer, data, param);
                if (rs != null)
                {
                    success = true;
                    FillDataToGrid();
                }

            End:
                WaitingManager.Hide();
                MessageManager.Show(param, success);
                SessionManager.ProcessTokenLost(param);
            }
            catch (Exception ex)
            {
                WaitingManager.Hide();
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (!btnEdit.Enabled || !dxValidationProvider1.Validate() || periodDepartment == null)
                    return;
                WaitingManager.Show();
                CommonParam param = new CommonParam();
                bool success = false;
                HTC_PERIOD_DEPARTMENT data = new HTC_PERIOD_DEPARTMENT();
                Inventec.Common.Mapper.DataObjectMapper.Map<HTC_PERIOD_DEPARTMENT>(data, periodDepartment);
                var period = BackendDataWorker.Get<HTC_PERIOD>().FirstOrDefault(o => o.ID == Convert.ToInt64(cboPeriod.EditValue));
                if (period == null)
                {
                    goto End;
                }
                data.PERIOD_ID = period.ID;

                var department = BackendDataWorker.Get<HIS_DEPARTMENT>().FirstOrDefault(o => o.ID == Convert.ToInt64(cboDepartment.EditValue));
                if (department == null)
                {
                    goto End;
                }
                data.DEPARTMENT_CODE = department.DEPARTMENT_CODE;
                data.DEPARTMENT_NAME = department.DEPARTMENT_NAME;

                if (txtEndTreatmentAmount.Value > 0)
                {
                    data.END_TREATMENT_AMOUNT = Convert.ToInt64(txtEndTreatmentAmount.Text);
                }

                if (txtFromExamClinicalAmount.Value > 0)
                {
                    data.FROM_EXAM_CLINICAL_AMOUNT = Convert.ToInt64(txtFromExamClinicalAmount.Text);
                }

                if (txtClinicalAmount.Value > 0)
                {
                    data.CLINICAL_AMOUNT = Convert.ToInt64(txtClinicalAmount.Text);
                }

                if (txtClinicalDayAmount.Value > 0)
                {
                    data.CLINICAL_DAY_AMOUNT = Convert.ToInt64(txtClinicalDayAmount.Text);
                }

                if (txtLaborAmount.Value > 0)
                {
                    data.LABOR_AMOUNT = Convert.ToInt64(txtLaborAmount.Text);
                }

                var rs = new Inventec.Common.Adapter.BackendAdapter(param).Post<HTC_PERIOD_DEPARTMENT>(HtcRequestUriStore.HTC_PERIOD_DEPARTMENT__UPDATE, ApiConsumers.HtcConsumer, data, param);
                if (rs != null)
                {
                    success = true;
                    FillDataToGrid();
                }

            End:
                WaitingManager.Hide();
                MessageManager.Show(param, success);
                SessionManager.ProcessTokenLost(param);
            }
            catch (Exception ex)
            {
                WaitingManager.Hide();
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                ResetValueControlCreate();
                SetEnableButtonCreate(true);
                txtPeriodCode.Focus();
                txtPeriodCode.SelectAll();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void repositoryItemBtnDelete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                var data = (V_HTC_PERIOD_DEPARTMENT)gridViewPeriodDepartmentList.GetFocusedRow();
                if (data != null)
                {
                    if (DevExpress.XtraEditors.XtraMessageBox.Show(MessageUtil.GetMessage(HIS.Desktop.LibraryMessage.Message.Enum.HeThongTBCuaSoThongBaoBanCoMuonXoaDuLieuKhong), MessageUtil.GetMessage(HIS.Desktop.LibraryMessage.Message.Enum.TieuDeCuaSoThongBaoLaThongBao), MessageBoxButtons.YesNo) != DialogResult.Yes)
                        return;
                    WaitingManager.Show();
                    CommonParam param = new CommonParam();
                    bool success = false;
                    success = new Inventec.Common.Adapter.BackendAdapter(param).Post<bool>(HtcRequestUriStore.HTC_PERIOD_DEPARTMENT__DELETE, ApiConsumers.HtcConsumer, data.ID, param);
                    if (success)
                    {
                        FillDataToGrid();
                    }
                    WaitingManager.Hide();
                    MessageManager.Show(param, success);
                    SessionManager.ProcessTokenLost(param);
                }
            }
            catch (Exception ex)
            {
                WaitingManager.Hide();
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void SetValueControlUpdate()
        {
            try
            {
                if (periodDepartment != null)
                {
                    txtPeriodCode.Text = periodDepartment.PERIOD_CODE;
                    cboPeriod.EditValue = periodDepartment.PERIOD_ID;

                    var department = BackendDataWorker.Get<HIS_DEPARTMENT>().FirstOrDefault(o => o.DEPARTMENT_CODE == periodDepartment.DEPARTMENT_CODE);
                    if (department != null)
                    {
                        txtDepartmentCode.Text = department.DEPARTMENT_CODE;
                        cboDepartment.EditValue = department.ID;
                    }
                    txtEndTreatmentAmount.Text = periodDepartment.END_TREATMENT_AMOUNT + "";
                    txtFromExamClinicalAmount.Text = periodDepartment.FROM_EXAM_CLINICAL_AMOUNT + "";
                    txtClinicalAmount.Text = periodDepartment.CLINICAL_AMOUNT + "";
                    txtClinicalDayAmount.Text = periodDepartment.CLINICAL_DAY_AMOUNT + "";
                    txtLaborAmount.Text = periodDepartment.LABOR_AMOUNT + "";
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
