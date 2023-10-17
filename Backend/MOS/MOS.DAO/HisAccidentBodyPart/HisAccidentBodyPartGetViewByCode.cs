using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisAccidentBodyPart
{
    partial class HisAccidentBodyPartGet : EntityBase
    {
        public V_HIS_ACCIDENT_BODY_PART GetViewByCode(string code, HisAccidentBodyPartSO search)
        {
            V_HIS_ACCIDENT_BODY_PART result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_ACCIDENT_BODY_PART.AsQueryable().Where(p => p.ACCIDENT_BODY_PART_CODE == code);
                        if (search.listVHisAccidentBodyPartExpression != null && search.listVHisAccidentBodyPartExpression.Count > 0)
                        {
                            foreach (var item in search.listVHisAccidentBodyPartExpression)
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
