using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisRehaSum
{
    partial class HisRehaSumGet : EntityBase
    {
        public HIS_REHA_SUM GetByCode(string code, HisRehaSumSO search)
        {
            HIS_REHA_SUM result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_REHA_SUM.AsQueryable().Where(p => p.REHA_SUM_CODE == code);
                        if (search.listHisRehaSumExpression != null && search.listHisRehaSumExpression.Count > 0)
                        {
                            foreach (var item in search.listHisRehaSumExpression)
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