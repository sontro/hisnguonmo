using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisTranPatiTemp
{
    partial class HisTranPatiTempGet : EntityBase
    {
        public V_HIS_TRAN_PATI_TEMP GetViewByCode(string code, HisTranPatiTempSO search)
        {
            V_HIS_TRAN_PATI_TEMP result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_TRAN_PATI_TEMP.AsQueryable().Where(p => p.TRAN_PATI_TEMP_CODE == code);
                        if (search.listVHisTranPatiTempExpression != null && search.listVHisTranPatiTempExpression.Count > 0)
                        {
                            foreach (var item in search.listVHisTranPatiTempExpression)
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
