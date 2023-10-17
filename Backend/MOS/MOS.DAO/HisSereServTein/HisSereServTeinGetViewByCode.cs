using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisSereServTein
{
    partial class HisSereServTeinGet : EntityBase
    {
        public V_HIS_SERE_SERV_TEIN GetViewByCode(string code, HisSereServTeinSO search)
        {
            V_HIS_SERE_SERV_TEIN result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_SERE_SERV_TEIN.AsQueryable().Where(p => p.SERE_SERV_TEIN_CODE == code);
                        if (search.listVHisSereServTeinExpression != null && search.listVHisSereServTeinExpression.Count > 0)
                        {
                            foreach (var item in search.listVHisSereServTeinExpression)
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
