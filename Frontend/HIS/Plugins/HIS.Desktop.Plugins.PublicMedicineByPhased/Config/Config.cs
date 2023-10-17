using HIS.Desktop.LocalStorage.BackendData;
using HIS.Desktop.LocalStorage.HisConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.PublicMedicineByPhased.Config
{
   internal class Config
    {
        private const string CONFIG_KEY__PATIENT_TYPE_CODE__BHYT = "MOS.HIS_PATIENT_TYPE.PATIENT_TYPE_CODE.BHYT";//Doi tuong BHYT
        private const string CONFIG_KEY__PHIEU_CONG_KHAI_THUOC__DAY_SIZE = "HIS.PHIEU_CONG_KHAI_THUOC.DAY_SIZE";
        internal static long PatientTypeId__BHYT;
        internal static int congKhaiThuoc_DaySize;

        internal static void LoadConfig()
        {
            try
            {
                PatientTypeId__BHYT = GetPatientTypeByCode(HisConfigs.Get<string>(CONFIG_KEY__PATIENT_TYPE_CODE__BHYT)).ID;
                congKhaiThuoc_DaySize = HisConfigs.Get<int>(CONFIG_KEY__PHIEU_CONG_KHAI_THUOC__DAY_SIZE) == 0 ? 10 : HisConfigs.Get<int>(CONFIG_KEY__PHIEU_CONG_KHAI_THUOC__DAY_SIZE);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        static MOS.EFMODEL.DataModels.HIS_PATIENT_TYPE GetPatientTypeByCode(string code)
        {
            MOS.EFMODEL.DataModels.HIS_PATIENT_TYPE result = new MOS.EFMODEL.DataModels.HIS_PATIENT_TYPE();
            try
            {
                result = BackendDataWorker.Get<MOS.EFMODEL.DataModels.HIS_PATIENT_TYPE>().FirstOrDefault(o => o.PATIENT_TYPE_CODE == code);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }

            return result ?? new MOS.EFMODEL.DataModels.HIS_PATIENT_TYPE();
        }
    }
}
