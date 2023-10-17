using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisDebateEkipUser;
using MOS.MANAGER.HisDebateUser;
using MOS.MANAGER.HisDebateInviteUser;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisDebate
{
    partial class HisDebateTruncate : BusinessBase
    {
        internal HisDebateTruncate()
            : base()
        {

        }

        internal HisDebateTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(HIS_DEBATE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisDebateCheck checker = new HisDebateCheck(param);
                valid = valid && IsNotNull(data);
                HIS_DEBATE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    new HisDebateUserTruncate(param).TruncateByDebateId(data.ID);
                    new HisDebateInviteUserTruncate(param).TruncateByDebateId(data.ID);
                    new HisDebateEkipUserTruncate(param).TruncateByDebateId(data.ID);
                    result = DAOWorker.HisDebateDAO.Truncate(data);
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
