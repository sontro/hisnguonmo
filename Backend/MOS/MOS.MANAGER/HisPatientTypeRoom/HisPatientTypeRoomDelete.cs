using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisPatientTypeRoom
{
    partial class HisPatientTypeRoomDelete : BusinessBase
    {
        internal HisPatientTypeRoomDelete()
            : base()
        {

        }

        internal HisPatientTypeRoomDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_PATIENT_TYPE_ROOM data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisPatientTypeRoomCheck checker = new HisPatientTypeRoomCheck(param);
                valid = valid && IsNotNull(data);
                HIS_PATIENT_TYPE_ROOM raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisPatientTypeRoomDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_PATIENT_TYPE_ROOM> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisPatientTypeRoomCheck checker = new HisPatientTypeRoomCheck(param);
                List<HIS_PATIENT_TYPE_ROOM> listRaw = new List<HIS_PATIENT_TYPE_ROOM>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisPatientTypeRoomDAO.DeleteList(listData);
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
