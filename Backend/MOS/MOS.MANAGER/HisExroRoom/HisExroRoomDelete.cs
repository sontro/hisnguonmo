using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisExroRoom
{
    partial class HisExroRoomDelete : BusinessBase
    {
        internal HisExroRoomDelete()
            : base()
        {

        }

        internal HisExroRoomDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_EXRO_ROOM data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisExroRoomCheck checker = new HisExroRoomCheck(param);
                valid = valid && IsNotNull(data);
                HIS_EXRO_ROOM raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisExroRoomDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_EXRO_ROOM> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisExroRoomCheck checker = new HisExroRoomCheck(param);
                List<HIS_EXRO_ROOM> listRaw = new List<HIS_EXRO_ROOM>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisExroRoomDAO.DeleteList(listData);
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
