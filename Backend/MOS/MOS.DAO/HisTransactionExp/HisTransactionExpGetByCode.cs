using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisTransactionExp
{
    partial class HisTransactionExpGet : EntityBase
    {
        public HIS_TRANSACTION_EXP GetByCode(string code, HisTransactionExpSO search)
        {
            HIS_TRANSACTION_EXP result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_TRANSACTION_EXP.AsQueryable().Where(p => p.TRANSACTION_EXP_CODE == code);
                        if (search.listHisTransactionExpExpression != null && search.listHisTransactionExpExpression.Count > 0)
                        {
                            foreach (var item in search.listHisTransactionExpExpression)
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
