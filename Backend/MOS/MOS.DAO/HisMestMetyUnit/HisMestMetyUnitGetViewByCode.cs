using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMestMetyUnit
{
    partial class HisMestMetyUnitGet : EntityBase
    {
        public V_HIS_MEST_METY_UNIT GetViewByCode(string code, HisMestMetyUnitSO search)
        {
            V_HIS_MEST_METY_UNIT result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_MEST_METY_UNIT.AsQueryable().Where(p => p.MEST_METY_UNIT_CODE == code);
                        if (search.listVHisMestMetyUnitExpression != null && search.listVHisMestMetyUnitExpression.Count > 0)
                        {
                            foreach (var item in search.listVHisMestMetyUnitExpression)
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