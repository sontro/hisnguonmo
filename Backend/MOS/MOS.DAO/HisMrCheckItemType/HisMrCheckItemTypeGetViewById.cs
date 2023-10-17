using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMrCheckItemType
{
    partial class HisMrCheckItemTypeGet : EntityBase
    {
        public V_HIS_MR_CHECK_ITEM_TYPE GetViewById(long id, HisMrCheckItemTypeSO search)
        {
            V_HIS_MR_CHECK_ITEM_TYPE result = null;
            try
            {
                bool valid = true;
                valid = valid && IsGreaterThanZero(id);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_MR_CHECK_ITEM_TYPE.AsQueryable().Where(p => p.ID == id);
                        if (search.listVHisMrCheckItemTypeExpression != null && search.listVHisMrCheckItemTypeExpression.Count > 0)
                        {
                            foreach (var item in search.listVHisMrCheckItemTypeExpression)
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
                Logging(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => id), id) + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => search), search), LogType.Error);
                LogSystem.Error(ex);
                result = null;
            }
            return result;
        }
    }
}
