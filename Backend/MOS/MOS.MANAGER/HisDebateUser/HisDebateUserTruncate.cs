using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisDebateUser
{
    partial class HisDebateUserTruncate : BusinessBase
    {
        internal HisDebateUserTruncate()
            : base()
        {

        }

        internal HisDebateUserTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(HIS_DEBATE_USER data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisDebateUserCheck checker = new HisDebateUserCheck(param);
                valid = valid && IsNotNull(data);
                HIS_DEBATE_USER raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisDebateUserDAO.Truncate(data);
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

        internal bool TruncateList(List<HIS_DEBATE_USER> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisDebateUserCheck checker = new HisDebateUserCheck(param);
                List<HIS_DEBATE_USER> listRaw = new List<HIS_DEBATE_USER>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisDebateUserDAO.TruncateList(listData);
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

        internal bool TruncateByDebateId(long debateId)
        {
            List<HIS_DEBATE_USER> hisDebateUsers = new HisDebateUserGet().GetByDebateId(debateId);
            if (IsNotNullOrEmpty(hisDebateUsers))
            {
                return this.TruncateList(hisDebateUsers);
            }
            return false;
        }
    }
}
