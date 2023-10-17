using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisDebateInviteUser
{
    partial class HisDebateInviteUserGet : EntityBase
    {
        public V_HIS_DEBATE_INVITE_USER GetViewById(long id, HisDebateInviteUserSO search)
        {
            V_HIS_DEBATE_INVITE_USER result = null;
            try
            {
                bool valid = true;
                valid = valid && IsGreaterThanZero(id);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_DEBATE_INVITE_USER.AsQueryable().Where(p => p.ID == id);
                        if (search.listVHisDebateInviteUserExpression != null && search.listVHisDebateInviteUserExpression.Count > 0)
                        {
                            foreach (var item in search.listVHisDebateInviteUserExpression)
                            {
                                query = query.Where(item);
                            }
                        }
                        result = query.SingleOrDefault();
                    }
                }
            }
            catch (Exception ex)
            {
                Logging(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => id), id) + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => search), search), LogType.Error);
                LogSystem.Error(ex);
                result = null;
            }
            return result;
        }
    }
}
