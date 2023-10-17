using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisHoreHandover
{
    partial class HisHoreHandoverDelete : BusinessBase
    {
        internal HisHoreHandoverDelete()
            : base()
        {

        }

        internal HisHoreHandoverDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_HORE_HANDOVER data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisHoreHandoverCheck checker = new HisHoreHandoverCheck(param);
                valid = valid && IsNotNull(data);
                HIS_HORE_HANDOVER raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisHoreHandoverDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_HORE_HANDOVER> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisHoreHandoverCheck checker = new HisHoreHandoverCheck(param);
                List<HIS_HORE_HANDOVER> listRaw = new List<HIS_HORE_HANDOVER>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisHoreHandoverDAO.DeleteList(listData);
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
