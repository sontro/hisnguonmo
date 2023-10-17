using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisUserGroupTempDt
{
    partial class HisUserGroupTempDtGet : EntityBase
    {
        public HIS_USER_GROUP_TEMP_DT GetByCode(string code, HisUserGroupTempDtSO search)
        {
            HIS_USER_GROUP_TEMP_DT result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_USER_GROUP_TEMP_DT.AsQueryable().Where(p => p.USER_GROUP_TEMP_DT_CODE == code);
                        if (search.listHisUserGroupTempDtExpression != null && search.listHisUserGroupTempDtExpression.Count > 0)
                        {
                            foreach (var item in search.listHisUserGroupTempDtExpression)
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
