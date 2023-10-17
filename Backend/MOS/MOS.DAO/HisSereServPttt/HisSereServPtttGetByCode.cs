using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisSereServPttt
{
    partial class HisSereServPtttGet : EntityBase
    {
        public HIS_SERE_SERV_PTTT GetByCode(string code, HisSereServPtttSO search)
        {
            HIS_SERE_SERV_PTTT result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_SERE_SERV_PTTT.AsQueryable().Where(p => p.SERE_SERV_PTTT_CODE == code);
                        if (search.listHisSereServPtttExpression != null && search.listHisSereServPtttExpression.Count > 0)
                        {
                            foreach (var item in search.listHisSereServPtttExpression)
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
