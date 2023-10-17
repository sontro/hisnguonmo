using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisSereServReha
{
    partial class HisSereServRehaGet : EntityBase
    {
        public V_HIS_SERE_SERV_REHA GetViewByCode(string code, HisSereServRehaSO search)
        {
            V_HIS_SERE_SERV_REHA result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_SERE_SERV_REHA.AsQueryable().Where(p => p.SERE_SERV_REHA_CODE == code);
                        if (search.listVHisSereServRehaExpression != null && search.listVHisSereServRehaExpression.Count > 0)
                        {
                            foreach (var item in search.listVHisSereServRehaExpression)
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
