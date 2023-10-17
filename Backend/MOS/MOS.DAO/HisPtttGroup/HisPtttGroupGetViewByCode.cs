using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisPtttGroup
{
    partial class HisPtttGroupGet : EntityBase
    {
        public V_HIS_PTTT_GROUP GetViewByCode(string code, HisPtttGroupSO search)
        {
            V_HIS_PTTT_GROUP result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_PTTT_GROUP.AsQueryable().Where(p => p.PTTT_GROUP_CODE == code);
                        if (search.listVHisPtttGroupExpression != null && search.listVHisPtttGroupExpression.Count > 0)
                        {
                            foreach (var item in search.listVHisPtttGroupExpression)
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
