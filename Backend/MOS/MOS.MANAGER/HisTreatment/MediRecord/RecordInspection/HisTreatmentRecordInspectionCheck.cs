using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisMediRecord;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTreatment.MediRecord.RecordInspection
{
    class HisTreatmentRecordInspectionCheck : BusinessBase
    {
        internal HisTreatmentRecordInspectionCheck()
            : base()
        {

        }

        internal HisTreatmentRecordInspectionCheck(CommonParam param)
            : base(param)
        {

        }

        internal bool HasMediRecordId(HIS_TREATMENT treatment)
        {
            return this.HasMediRecordId(new List<HIS_TREATMENT>() { treatment });
        }

        internal bool HasMediRecordId(List<HIS_TREATMENT> treatments)
        {
            bool valid = true;
            try
            {
                List<string> hasMediRecordIds = treatments != null ? treatments.Where(o => !o.MEDI_RECORD_ID.HasValue).Select(o => o.TREATMENT_CODE).ToList() : null;
                if (IsNotNullOrEmpty(hasMediRecordIds))
                {
                    string codeStr = string.Join(",", hasMediRecordIds);
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTreatment_HoSoChuaDuocChoVaoBenhAn, codeStr);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool IsNotRecordInspectioned(HIS_TREATMENT treatment)
        {
            return this.IsNotRecordInspectioned(new List<HIS_TREATMENT>() { treatment });
        }

        internal bool IsNotRecordInspectioned(List<HIS_TREATMENT> treatments)
        {
            bool valid = true;
            try
            {
                List<string> recordInspectioned = treatments != null ? treatments.Where(o => o.RECORD_INSPECTION_STT_ID.HasValue).Select(o => o.TREATMENT_CODE).ToList() : null;
                if (IsNotNullOrEmpty(recordInspectioned))
                {
                    string codeStr = string.Join(",", recordInspectioned);
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTreatment_HoSoDaDuocGiamDinh, codeStr);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool IsRecordInspectionApproved(HIS_TREATMENT treatment)
        {
            bool valid = true;
            try
            {
                if (treatment != null && treatment.RECORD_INSPECTION_STT_ID != 1)
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTreatment_HoSoChuaDuocDuyetGiamDinh, treatment.TREATMENT_CODE);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool IsRecordInspectionRejected(HIS_TREATMENT treatment)
        {
            bool valid = true;
            try
            {
                if (treatment != null && treatment.RECORD_INSPECTION_STT_ID != 2)
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTreatment_HoSoChuaBiTuChoiDuyetGiamDinh, treatment.TREATMENT_CODE);
                    return false;
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
