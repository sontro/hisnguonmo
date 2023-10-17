using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisSereServSuin
{
    partial class HisSereServSuinGet : EntityBase
    {
        public HIS_SERE_SERV_SUIN GetByCode(string code, HisSereServSuinSO search)
        {
            HIS_SERE_SERV_SUIN result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_SERE_SERV_SUIN.AsQueryable().Where(p => p.SERE_SERV_SUIN_CODE == code);
                        if (search.listHisSereServSuinExpression != null && search.listHisSereServSuinExpression.Count > 0)
                        {
                            foreach (var item in search.listHisSereServSuinExpression)
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
