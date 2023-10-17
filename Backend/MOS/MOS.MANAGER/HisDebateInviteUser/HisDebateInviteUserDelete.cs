using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisDebateInviteUser
{
    partial class HisDebateInviteUserDelete : BusinessBase
    {
        internal HisDebateInviteUserDelete()
            : base()
        {

        }

        internal HisDebateInviteUserDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_DEBATE_INVITE_USER data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisDebateInviteUserCheck checker = new HisDebateInviteUserCheck(param);
                valid = valid && IsNotNull(data);
                HIS_DEBATE_INVITE_USER raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisDebateInviteUserDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_DEBATE_INVITE_USER> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisDebateInviteUserCheck checker = new HisDebateInviteUserCheck(param);
                List<HIS_DEBATE_INVITE_USER> listRaw = new List<HIS_DEBATE_INVITE_USER>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisDebateInviteUserDAO.DeleteList(listData);
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
