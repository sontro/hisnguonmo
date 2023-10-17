using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMrCheckItem
{
    partial class HisMrCheckItemGet : EntityBase
    {
        public HIS_MR_CHECK_ITEM GetByCode(string code, HisMrCheckItemSO search)
        {
            HIS_MR_CHECK_ITEM result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_MR_CHECK_ITEM.AsQueryable().Where(p => p.MR_CHECK_ITEM_CODE == code);
                        if (search.listHisMrCheckItemExpression != null && search.listHisMrCheckItemExpression.Count > 0)
                        {
                            foreach (var item in search.listHisMrCheckItemExpression)
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
