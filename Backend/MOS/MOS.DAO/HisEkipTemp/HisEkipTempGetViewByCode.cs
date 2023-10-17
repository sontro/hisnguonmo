using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisEkipTemp
{
    partial class HisEkipTempGet : EntityBase
    {
        public V_HIS_EKIP_TEMP GetViewByCode(string code, HisEkipTempSO search)
        {
            V_HIS_EKIP_TEMP result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_EKIP_TEMP.AsQueryable().Where(p => p.EKIP_TEMP_CODE == code);
                        if (search.listVHisEkipTempExpression != null && search.listVHisEkipTempExpression.Count > 0)
                        {
                            foreach (var item in search.listVHisEkipTempExpression)
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
