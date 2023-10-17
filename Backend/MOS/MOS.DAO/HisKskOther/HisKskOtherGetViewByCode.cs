using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisKskOther
{
    partial class HisKskOtherGet : EntityBase
    {
        public V_HIS_KSK_OTHER GetViewByCode(string code, HisKskOtherSO search)
        {
            V_HIS_KSK_OTHER result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_KSK_OTHER.AsQueryable().Where(p => p.KSK_OTHER_CODE == code);
                        if (search.listVHisKskOtherExpression != null && search.listVHisKskOtherExpression.Count > 0)
                        {
                            foreach (var item in search.listVHisKskOtherExpression)
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
