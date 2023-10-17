using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisAnticipateMety
{
    partial class HisAnticipateMetyGet : EntityBase
    {
        public V_HIS_ANTICIPATE_METY GetViewByCode(string code, HisAnticipateMetySO search)
        {
            V_HIS_ANTICIPATE_METY result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_ANTICIPATE_METY.AsQueryable().Where(p => p.ANTICIPATE_METY_CODE == code);
                        if (search.listVHisAnticipateMetyExpression != null && search.listVHisAnticipateMetyExpression.Count > 0)
                        {
                            foreach (var item in search.listVHisAnticipateMetyExpression)
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
