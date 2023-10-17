using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisDebateEkipUser
{
    partial class HisDebateEkipUserDelete : BusinessBase
    {
        internal HisDebateEkipUserDelete()
            : base()
        {

        }

        internal HisDebateEkipUserDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_DEBATE_EKIP_USER data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisDebateEkipUserCheck checker = new HisDebateEkipUserCheck(param);
                valid = valid && IsNotNull(data);
                HIS_DEBATE_EKIP_USER raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisDebateEkipUserDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_DEBATE_EKIP_USER> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisDebateEkipUserCheck checker = new HisDebateEkipUserCheck(param);
                List<HIS_DEBATE_EKIP_USER> listRaw = new List<HIS_DEBATE_EKIP_USER>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisDebateEkipUserDAO.DeleteList(listData);
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
