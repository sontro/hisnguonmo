using Inventec.Core;
using Inventec.Common.Logging;
using MOS.MANAGER.Base;
using MOS.EFMODEL.DataModels;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTreatment.Update.TranPatiBookInfo
{
    class HisTreatmentCheckTranPatiBookInfo : BusinessBase
    {
        internal HisTreatmentCheckTranPatiBookInfo()
            : base()
        {
        }

        internal HisTreatmentCheckTranPatiBookInfo(CommonParam paramCheck)
            : base(paramCheck)
        {
        }

        internal bool VerifyRequireField(HisTreatmentSetTranPatiBookSDO data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (data.TreatmentId <= 0) throw new ArgumentNullException("data.TreatmentId");
                if (data.TranPatiBookTime <= 0) throw new ArgumentNullException("data.TranPatiBookTime");
                if (string.IsNullOrWhiteSpace(data.TranPatiDoctorLoginname)) throw new ArgumentNullException("data.TranPatiDoctorLoginname");
                if (string.IsNullOrWhiteSpace(data.TranPatiDoctorUsername)) throw new ArgumentNullException("data.TranPatiDoctorUsername");
                if (string.IsNullOrWhiteSpace(data.TranPatiDepartmentLoginname)) throw new ArgumentNullException("data.TranPatiDepartmentLoginname");
                if (string.IsNullOrWhiteSpace(data.TranPatiDepartmentUsername)) throw new ArgumentNullException("data.TranPatiDepartmentUsername");
                if (string.IsNullOrWhiteSpace(data.TranPatiHospitalLoginname)) throw new ArgumentNullException("data.TranPatiHospitalLoginname");
                if (string.IsNullOrWhiteSpace(data.TranPatiHospitalUsername)) throw new ArgumentNullException("data.TranPatiHospitalUsername");
            }
            catch (ArgumentNullException ex)
            {
                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                LogSystem.Warn(ex);
                valid = false;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool HasOutTime(HIS_TREATMENT data)
        {
            bool valid = true;
            try
            {
                if (IsNotNull(data))
                {
                    if (!data.OUT_TIME.HasValue)
                    {
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTreatment_HoSoChuaCoThoiGianRaVien, data.TREATMENT_CODE);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool IsEndTransType(HIS_TREATMENT data)
        {
            bool valid = true;
            try
            {
                if (IsNotNull(data))
                {
                    if (!(data.TREATMENT_END_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_END_TYPE.ID__CHUYEN))
                    {
                        LogSystem.Warn("Loai giay ra vien khong phai la chuyen vien");
                        return false;
                    }
                    
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }
    }
}
