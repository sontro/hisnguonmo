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

namespace HIS.Desktop.Plugins.RegisterV2.Run2
{
    public partial class UCRegister : UserControlBase
    {
        public void Save()
        {
            try
            {
                if (this.actionType == GlobalVariables.ActionAdd && this.btnSave.Enabled == true)
                    this.btnSave_Click(null, null);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        public void SaveAndPrint()
        {
            try
            {
                if (!btnSaveAndPrint.Enabled)
                    return;
                if (this.actionType == GlobalVariables.ActionAdd)
                    this.btnSaveAndPrint_Click(null, null);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        public void New()
        {
            try
            {
                this.btnNewContinue_Click(null, null);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        public void PatientNew()
        {
            try
            {
                if (this.actionType == GlobalVariables.ActionAdd && this.lcibtnPatientNewInfo.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
                    this.btnPatientNew_Click(null, null);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        public void AssignService()
        {
            try
            {
                if (this.actionType == GlobalVariables.ActionView)
                    this.btnSaveAndAssain_Click(null, null);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        public void InBed()
        {
            try
            {
                if (this.actionType == GlobalVariables.ActionView)
                {
                    this.btnTreatmentBedRoom_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        public void BillKeyboard()
        {
            try
            {

            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        public void Deposit()
        {
            try
            {
                if (this.actionType == GlobalVariables.ActionView && this.btnDepositDetail.Enabled && lcibtnDepositDetail.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
                {
                    this.btnDepositDetail_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        public void DepositRequest()
        {
            try
            {
                if (this.actionType == GlobalVariables.ActionView && this.btnDepositRequest.Enabled && lcibtnDepositRequest.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
                {
                    this.btnDepositRequest_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        public void PrintKeyboard()
        {
            try
            {
                if (this.actionType == GlobalVariables.ActionView)
                {
                    this.btnPrint_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        public void ClickF1()
        {
            try
            {
                btnNewContinue_Click(null, null);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        public void ClickF2()
        {
            try
            {
                if (this.actionType == GlobalVariables.ActionAdd)
                {
                    // focus thông tin bệnh nhân
                    this.ucPatientRaw1.FocusUserControl();
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        public void ClickF3()
        {
            try
            {
                if (this.actionType == GlobalVariables.ActionAdd)
                {
                    long patientTypeId = GetPatientTypeId();
                    if (patientTypeId == HIS.Desktop.Plugins.Library.RegisterConfig.HisConfigCFG.PatientTypeId__BHYT)
                    {
                        this.ucHeinInfo1.FocusUserControl();
                    }
                    else if (patientTypeId == HIS.Desktop.Plugins.Library.RegisterConfig.HisConfigCFG.PatientTypeId__KSK)
                    {
                        if (this.ucKskContract != null && this.kskContractProcessor != null)
                        {
                            kskContractProcessor.InFocus(this.ucKskContract);
                        }
                    }
                    else
                    {
                        this.ucServiceRoomInfo1.FocusUserControl();
                    }

                    // focus thông tin thẻ

                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        public void ClickF4()
        {
            try
            {
                if (this.actionType == GlobalVariables.ActionAdd)
                {
                    // focus phòng khám
                    this.ucServiceRoomInfo1.FocusUserControl();
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        public void ClickF5()
        {
            try
            {
                this.btnCallPatient_Click(null, null);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        public void ClickF6()
        {
            try
            {
                this.btnRecallPatient_Click(null, null);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        public void ClickF7()
        {
            try
            {
                this.ucPlusInfo1.FocusUserControl();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        public void ClickF8()
        {
            try
            {
                if (!btnSaveAndPrint.Enabled) return;
                btnSaveAndPrint_Click(null, null);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        public void ClickF9()
        {
            try
            {
                this.ucRelativeInfo1.FocusUserControl();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        public void ClickF10()
        {
            try
            {
                if (this.actionType == GlobalVariables.ActionView)
                {
                    Inventec.Common.RichEditor.RichEditorStore richEditorMain = new Inventec.Common.RichEditor.RichEditorStore(HIS.Desktop.ApiConsumer.ApiConsumers.SarConsumer, HIS.Desktop.LocalStorage.ConfigSystem.ConfigSystems.URI_API_SAR, Inventec.Desktop.Common.LanguageManager.LanguageManager.GetLanguage(), GlobalVariables.TemnplatePathFolder);
                    richEditorMain.RunPrintTemplate("Mps000178", DelegateRunPrinterInTheBenhNhanPrintNow);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
