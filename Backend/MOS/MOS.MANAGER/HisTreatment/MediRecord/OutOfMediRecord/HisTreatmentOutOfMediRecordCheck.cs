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

namespace MOS.MANAGER.HisTreatment.MediRecord.OutOfMediRecord
{
    class HisTreatmentOutOfMediRecordCheck : BusinessBase
    {
        internal HisTreatmentOutOfMediRecordCheck()
            : base()
        {

        }

        internal HisTreatmentOutOfMediRecordCheck(CommonParam param)
            : base(param)
        {

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

        internal bool IsNotRecordInspectionApproved(List<HIS_TREATMENT> treatments)
        {
            bool valid = true;
            try
            {
                List<string> approved = treatments != null ? treatments.Where(o => o.RECORD_INSPECTION_STT_ID == 1).Select(o => o.TREATMENT_CODE).ToList() : null;
                if (IsNotNullOrEmpty(approved))
                {
                    string codeStr = string.Join(",", approved);
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTreatment_HoSoDaDuocDuyetGiamDinh, codeStr);
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

        internal bool HasPermission(List<HIS_TREATMENT> treatments, ref List<HIS_MEDI_RECORD> mediRecords)
        {
            bool valid = true;
            try
            {
                List<long> mediRecordIds = treatments != null ? treatments.Select(o => o.MEDI_RECORD_ID.Value).ToList() : null;
                if (IsNotNullOrEmpty(mediRecordIds))
                {
                    mediRecords = new HisMediRecordGet().GetByIds(mediRecordIds);
                    List<long> dataStoreIds = IsNotNullOrEmpty(mediRecords) ? mediRecords.Where(o => o.DATA_STORE_ID.HasValue).Select(o => o.DATA_STORE_ID.Value).ToList() : null;
                    string loginName = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();

                    if (IsNotNullOrEmpty(dataStoreIds))
                    {
                        List<string> dataStoreNames = HisDataStoreCFG.DATA.Where(o => dataStoreIds.Contains(o.ID) && !HisUserRoomCFG.DATA.Exists(t => t.ROOM_ID == o.ROOM_ID && t.LOGINNAME == loginName)).Select(o => o.DATA_STORE_NAME).ToList();

                        if (IsNotNullOrEmpty(dataStoreNames))
                        {
                            string dataStoreNameStr = string.Join(",", dataStoreNames);
                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_KhongCoQuyenTaiTuBenhAn, dataStoreNameStr);
                            return false;
                        }
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
