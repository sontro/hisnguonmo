using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisHoreHandoverStt
{
    partial class HisHoreHandoverSttDelete : BusinessBase
    {
        internal HisHoreHandoverSttDelete()
            : base()
        {

        }

        internal HisHoreHandoverSttDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_HORE_HANDOVER_STT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisHoreHandoverSttCheck checker = new HisHoreHandoverSttCheck(param);
                valid = valid && IsNotNull(data);
                HIS_HORE_HANDOVER_STT raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisHoreHandoverSttDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_HORE_HANDOVER_STT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisHoreHandoverSttCheck checker = new HisHoreHandoverSttCheck(param);
                List<HIS_HORE_HANDOVER_STT> listRaw = new List<HIS_HORE_HANDOVER_STT>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisHoreHandoverSttDAO.DeleteList(listData);
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
