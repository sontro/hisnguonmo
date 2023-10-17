using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisUnlimitType
{
    partial class HisUnlimitTypeGet : EntityBase
    {
        public HIS_UNLIMIT_TYPE GetByCode(string code, HisUnlimitTypeSO search)
        {
            HIS_UNLIMIT_TYPE result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_UNLIMIT_TYPE.AsQueryable().Where(p => p.UNLIMIT_TYPE_CODE == code);
                        if (search.listHisUnlimitTypeExpression != null && search.listHisUnlimitTypeExpression.Count > 0)
                        {
                            foreach (var item in search.listHisUnlimitTypeExpression)
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
