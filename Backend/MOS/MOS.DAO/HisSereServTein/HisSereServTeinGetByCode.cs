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
        public HIS_SERE_SERV_TEIN GetByCode(string code, HisSereServTeinSO search)
        {
            HIS_SERE_SERV_TEIN result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_SERE_SERV_TEIN.AsQueryable().Where(p => p.SERE_SERV_TEIN_CODE == code);
                        if (search.listHisSereServTeinExpression != null && search.listHisSereServTeinExpression.Count > 0)
                        {
                            foreach (var item in search.listHisSereServTeinExpression)
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
