using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisRestRetrType;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisDebateInviteUser
{
    partial class HisDebateInviteUserTruncate : BusinessBase
    {
        internal HisDebateInviteUserTruncate()
            : base()
        {

        }

        internal HisDebateInviteUserTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(long id)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisDebateInviteUserCheck checker = new HisDebateInviteUserCheck(param);
                HIS_DEBATE_INVITE_USER raw = null;
                valid = valid && checker.VerifyId(id, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.CheckConstraint(id);
                if (valid)
                {
                    result = DAOWorker.HisDebateInviteUserDAO.Truncate(raw);
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

        internal bool TruncateList(List<HIS_DEBATE_INVITE_USER> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisDebateInviteUserCheck checker = new HisDebateInviteUserCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data.ID);
					valid = valid && checker.CheckConstraint(data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisDebateInviteUserDAO.TruncateList(listData);
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
            List<HIS_DEBATE_INVITE_USER> hisDebateInviteUsers = new HisDebateInviteUserGet().GetByDebateId(debateId);
            if (IsNotNullOrEmpty(hisDebateInviteUsers))
            {
                return this.TruncateList(hisDebateInviteUsers);
            }
            return false;
        }
    }
}
