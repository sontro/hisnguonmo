using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisUserGroupTemp
{
    partial class HisUserGroupTempGet : EntityBase
    {
        public V_HIS_USER_GROUP_TEMP GetViewByCode(string code, HisUserGroupTempSO search)
        {
            V_HIS_USER_GROUP_TEMP result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_USER_GROUP_TEMP.AsQueryable().Where(p => p.USER_GROUP_TEMP_CODE == code);
                        if (search.listVHisUserGroupTempExpression != null && search.listVHisUserGroupTempExpression.Count > 0)
                        {
                            foreach (var item in search.listVHisUserGroupTempExpression)
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
                Logging(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => code), code) + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => search), search), LogType.Error);
                LogSystem.Error(ex);
                result = null;
            }
            return result;
        }
    }
}
