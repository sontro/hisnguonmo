using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisRoomSaro
{
    partial class HisRoomSaroDelete : BusinessBase
    {
        internal HisRoomSaroDelete()
            : base()
        {

        }

        internal HisRoomSaroDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_ROOM_SARO data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisRoomSaroCheck checker = new HisRoomSaroCheck(param);
                valid = valid && IsNotNull(data);
                HIS_ROOM_SARO raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisRoomSaroDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_ROOM_SARO> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisRoomSaroCheck checker = new HisRoomSaroCheck(param);
                List<HIS_ROOM_SARO> listRaw = new List<HIS_ROOM_SARO>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisRoomSaroDAO.DeleteList(listData);
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
