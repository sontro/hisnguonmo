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

namespace HIS.Desktop.Plugins.RegisterV3.Run3
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
                //if (this.actionType == GlobalVariables.ActionView && this.btnDepositDetail.Enabled && lcibtnDepositDetail.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
                //{
                //    this.btnDepositDetail_Click(null, null);
                //}
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
                //this.ucPlusInfo1.FocusUserControl();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
