using SAR.DAO.Base;
using SAR.DAO.StagingObject;
using SAR.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SAR.DAO.SarUserReportType
{
    partial class SarUserReportTypeGet : EntityBase
    {
        public SAR_USER_REPORT_TYPE GetByCode(string code, SarUserReportTypeSO search)
        {
            SAR_USER_REPORT_TYPE result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.SAR_USER_REPORT_TYPE.AsQueryable().Where(p => p.USER_REPORT_TYPE_CODE == code);
                        if (search.listSarUserReportTypeExpression != null && search.listSarUserReportTypeExpression.Count > 0)
                        {
                            foreach (var item in search.listSarUserReportTypeExpression)
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
