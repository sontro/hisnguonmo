using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisBloodAbo
{
    partial class HisBloodAboGet : EntityBase
    {
        public V_HIS_BLOOD_ABO GetViewByCode(string code, HisBloodAboSO search)
        {
            V_HIS_BLOOD_ABO result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_BLOOD_ABO.AsQueryable().Where(p => p.BLOOD_ABO_CODE == code);
                        if (search.listVHisBloodAboExpression != null && search.listVHisBloodAboExpression.Count > 0)
                        {
                            foreach (var item in search.listVHisBloodAboExpression)
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
