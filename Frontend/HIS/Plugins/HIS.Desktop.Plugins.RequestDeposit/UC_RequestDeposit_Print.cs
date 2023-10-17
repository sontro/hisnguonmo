using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MOS.EFMODEL.DataModels;
using Inventec.Desktop.Common.Message;
using HIS.Desktop.Print;

namespace HIS.Desktop.Plugins.RequestDeposit
{
    public partial class UC_RequestDeposit : UserControl
    {
        bool delegateRunPrinter(string printTypeCode, string fileName)
        {
            bool result = false;
            try
            {
                InYeuCauTamUng(printTypeCode, fileName, depositReqPrint);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
            return result;
        }

        private void InYeuCauTamUng(string printTypeCode, string fileName, V_HIS_DEPOSIT_REQ depositReq)
        {
            try
            {
                bool result = false;

                WaitingManager.Show();

                //Thông tin bệnh nhân
                var patient = PrintGlobalStore.getPatient(treatmentID);
                long instructionTime = Inventec.Common.DateTime.Get.Now() ?? 0;

                //BHYT
                var patyAlterBhyt = PrintGlobalStore.getPatyAlterBhyt(treatmentID, instructionTime);
                WaitingManager.Hide();
                MPS.Core.Mps000091.Mps000091RDO mps000091RDO = new MPS.Core.Mps000091.Mps000091RDO(
                                patient,
                                depositReq,
                                patyAlterBhyt
                                );

                result = MPS.Printer.Run(printTypeCode, fileName, mps000091RDO);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
    }
}
