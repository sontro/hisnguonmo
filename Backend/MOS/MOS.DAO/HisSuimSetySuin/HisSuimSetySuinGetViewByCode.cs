using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisSuimSetySuin
{
    partial class HisSuimSetySuinGet : EntityBase
    {
        public V_HIS_SUIM_SETY_SUIN GetViewByCode(string code, HisSuimSetySuinSO search)
        {
            V_HIS_SUIM_SETY_SUIN result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_SUIM_SETY_SUIN.AsQueryable().Where(p => p.SUIM_SETY_SUIN_CODE == code);
                        if (search.listVHisSuimSetySuinExpression != null && search.listVHisSuimSetySuinExpression.Count > 0)
                        {
                            foreach (var item in search.listVHisSuimSetySuinExpression)
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
