using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisObeyContraindi
{
    partial class HisObeyContraindiGet : EntityBase
    {
        public V_HIS_OBEY_CONTRAINDI GetViewByCode(string code, HisObeyContraindiSO search)
        {
            V_HIS_OBEY_CONTRAINDI result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_OBEY_CONTRAINDI.AsQueryable().Where(p => p.OBEY_CONTRAINDI_CODE == code);
                        if (search.listVHisObeyContraindiExpression != null && search.listVHisObeyContraindiExpression.Count > 0)
                        {
                            foreach (var item in search.listVHisObeyContraindiExpression)
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
