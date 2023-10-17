using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisBornPosition
{
    partial class HisBornPositionDelete : BusinessBase
    {
        internal HisBornPositionDelete()
            : base()
        {

        }

        internal HisBornPositionDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_BORN_POSITION data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBornPositionCheck checker = new HisBornPositionCheck(param);
                valid = valid && IsNotNull(data);
                HIS_BORN_POSITION raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisBornPositionDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_BORN_POSITION> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisBornPositionCheck checker = new HisBornPositionCheck(param);
                List<HIS_BORN_POSITION> listRaw = new List<HIS_BORN_POSITION>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisBornPositionDAO.DeleteList(listData);
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
