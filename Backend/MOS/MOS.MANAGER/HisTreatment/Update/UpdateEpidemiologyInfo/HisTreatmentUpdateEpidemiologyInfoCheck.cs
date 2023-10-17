using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisMedicineType;
using MOS.UTILITY;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTreatment.Update.UpdateEpidemiologyInfo
{
    class HisTreatmentUpdateEpidemiologyInfoCheck : BusinessBase
    {
        internal HisTreatmentUpdateEpidemiologyInfoCheck()
            : base()
        {
        }

        internal HisTreatmentUpdateEpidemiologyInfoCheck(CommonParam param)
            : base(param)
        {
        }

        internal bool VerifyRequireField(EpidemiologyInfoSDO data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (data.TreatmentId <= 0) throw new ArgumentNullException("data.TreatmentId");
                if (data.VaccineId.HasValue && data.VaccineId.Value <= 0) throw new ArgumentNullException("data.VaccineId");
                if (data.EpidemiologyContactType.HasValue && data.EpidemiologyContactType.Value < 0) throw new ArgumentNullException("data.EpidemiologyContactType");
                if (data.VaccinationOrder.HasValue && data.VaccinationOrder.Value < 1) throw new ArgumentNullException("data.VaccinationOrder");
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

        internal bool IsValid(EpidemiologyInfoSDO data)
        {
            bool valid = true;
            try
            {
                if (IsNotNull(data) && data.VaccineId.HasValue)
                {   
                    var medicineType = new HisMedicineTypeGet().GetById(data.VaccineId.Value);
                    if (medicineType == null || medicineType.IS_VACCINE != Constant.IS_TRUE)
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_KhongTimThayLoaiThuocHoacKhongPhaiVaccine);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }
    }
}
