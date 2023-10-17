using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisRestRetrType
{
    partial class HisRestRetrTypeGet : EntityBase
    {
        public HIS_REST_RETR_TYPE GetByCode(string code, HisRestRetrTypeSO search)
        {
            HIS_REST_RETR_TYPE result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_REST_RETR_TYPE.AsQueryable().Where(p => p.REST_RETR_TYPE_CODE == code);
                        if (search.listHisRestRetrTypeExpression != null && search.listHisRestRetrTypeExpression.Count > 0)
                        {
                            foreach (var item in search.listHisRestRetrTypeExpression)
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
