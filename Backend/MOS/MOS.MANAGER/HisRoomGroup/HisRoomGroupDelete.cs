using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisRoomGroup
{
    partial class HisRoomGroupDelete : BusinessBase
    {
        internal HisRoomGroupDelete()
            : base()
        {

        }

        internal HisRoomGroupDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_ROOM_GROUP data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisRoomGroupCheck checker = new HisRoomGroupCheck(param);
                valid = valid && IsNotNull(data);
                HIS_ROOM_GROUP raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisRoomGroupDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_ROOM_GROUP> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisRoomGroupCheck checker = new HisRoomGroupCheck(param);
                List<HIS_ROOM_GROUP> listRaw = new List<HIS_ROOM_GROUP>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisRoomGroupDAO.DeleteList(listData);
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
