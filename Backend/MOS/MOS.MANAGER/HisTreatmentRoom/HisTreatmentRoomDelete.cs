using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisTreatmentRoom
{
    partial class HisTreatmentRoomDelete : BusinessBase
    {
        internal HisTreatmentRoomDelete()
            : base()
        {

        }

        internal HisTreatmentRoomDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_TREATMENT_ROOM data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisTreatmentRoomCheck checker = new HisTreatmentRoomCheck(param);
                valid = valid && IsNotNull(data);
                HIS_TREATMENT_ROOM raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisTreatmentRoomDAO.Delete(data);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        internal bool DeleteList(List<HIS_TREATMENT_ROOM> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisTreatmentRoomCheck checker = new HisTreatmentRoomCheck(param);
                List<HIS_TREATMENT_ROOM> listRaw = new List<HIS_TREATMENT_ROOM>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisTreatmentRoomDAO.DeleteList(listData);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }
    }
}
