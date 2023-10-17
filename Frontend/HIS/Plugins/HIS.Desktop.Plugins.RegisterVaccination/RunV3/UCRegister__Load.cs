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
using MOS.SDO;
using MOS.EFMODEL.DataModels;
using HIS.Desktop.LocalStorage.LocalData;
using HIS.Desktop.LocalStorage.BackendData;
using Inventec.Common.Logging;
using Inventec.Common.QrCodeBHYT;
using Inventec.Core;
using MOS.Filter;
using Inventec.Common.Adapter;
using HIS.Desktop.ApiConsumer;
using HIS.Desktop.Plugins.Library.RegisterConfig;
using Inventec.Desktop.Common.Message;
using HIS.Desktop.ADO;

namespace HIS.Desktop.Plugins.RegisterVaccination.Run3
{
    public partial class UCRegister : UserControlBase
    {
        private List<HIS_CASHIER_ROOM> GetCashierRoomByUser()
        {
            List<HIS_CASHIER_ROOM> result = new List<HIS_CASHIER_ROOM>();
            try
            {
                //Ci hien thi phong thu ngan ma ng dung chon lam viec
                var roomIds = WorkPlace.GetRoomIds();
                if (roomIds == null || roomIds.Count == 0)
                    throw new ArgumentNullException("Nguoi dung khong chon phong thu ngan nao");
                result = BackendDataWorker.Get<MOS.EFMODEL.DataModels.HIS_CASHIER_ROOM>().Where(o => roomIds.Contains(o.ROOM_ID)).ToList();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
            return result;
        }

        private void GetPatientInfoFromResultData(ref AssignServiceADO assignServiceADO)
        {
            try
            {
                if (this.resultHisPatientProfileSDO != null)
                {
                    assignServiceADO.PatientName = this.resultHisPatientProfileSDO.HisPatient.VIR_PATIENT_NAME;
                    assignServiceADO.PatientDob = this.resultHisPatientProfileSDO.HisPatient.DOB;
                    assignServiceADO.GenderName = BackendDataWorker.Get<HIS_GENDER>().FirstOrDefault(o => o.ID == this.resultHisPatientProfileSDO.HisPatient.GENDER_ID).GENDER_NAME;
                }
                else if (this.currentHisExamServiceReqResultSDO != null)
                {
                    assignServiceADO.PatientName = this.currentHisExamServiceReqResultSDO.HisPatientProfile.HisPatient.VIR_PATIENT_NAME;
                    assignServiceADO.PatientDob = this.currentHisExamServiceReqResultSDO.HisPatientProfile.HisPatient.DOB;
                    assignServiceADO.GenderName = BackendDataWorker.Get<HIS_GENDER>().FirstOrDefault(o => o.ID == this.currentHisExamServiceReqResultSDO.HisPatientProfile.HisPatient.GENDER_ID).GENDER_NAME;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
