using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisNumOrderBlock
{
    partial class HisNumOrderBlockGet : EntityBase
    {
        public HIS_NUM_ORDER_BLOCK GetByCode(string code, HisNumOrderBlockSO search)
        {
            HIS_NUM_ORDER_BLOCK result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_NUM_ORDER_BLOCK.AsQueryable().Where(p => p.NUM_ORDER_BLOCK_CODE == code);
                        if (search.listHisNumOrderBlockExpression != null && search.listHisNumOrderBlockExpression.Count > 0)
                        {
                            foreach (var item in search.listHisNumOrderBlockExpression)
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
