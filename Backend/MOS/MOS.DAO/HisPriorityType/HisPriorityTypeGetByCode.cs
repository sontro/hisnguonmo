using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisPriorityType
{
    partial class HisPriorityTypeGet : EntityBase
    {
        public HIS_PRIORITY_TYPE GetByCode(string code, HisPriorityTypeSO search)
        {
            HIS_PRIORITY_TYPE result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_PRIORITY_TYPE.AsQueryable().Where(p => p.PRIORITY_TYPE_CODE == code);
                        if (search.listHisPriorityTypeExpression != null && search.listHisPriorityTypeExpression.Count > 0)
                        {
                            foreach (var item in search.listHisPriorityTypeExpression)
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
