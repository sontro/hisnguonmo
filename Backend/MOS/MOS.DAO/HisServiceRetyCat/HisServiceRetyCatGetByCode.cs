using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisServiceRetyCat
{
    partial class HisServiceRetyCatGet : EntityBase
    {
        public HIS_SERVICE_RETY_CAT GetByCode(string code, HisServiceRetyCatSO search)
        {
            HIS_SERVICE_RETY_CAT result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_SERVICE_RETY_CAT.AsQueryable().Where(p => p.SERVICE_RETY_CAT_CODE == code);
                        if (search.listHisServiceRetyCatExpression != null && search.listHisServiceRetyCatExpression.Count > 0)
                        {
                            foreach (var item in search.listHisServiceRetyCatExpression)
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
