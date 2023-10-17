using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisFormTypeCfg
{
    partial class HisFormTypeCfgGet : EntityBase
    {
        public HIS_FORM_TYPE_CFG GetByCode(string code, HisFormTypeCfgSO search)
        {
            HIS_FORM_TYPE_CFG result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_FORM_TYPE_CFG.AsQueryable().Where(p => p.FORM_TYPE_CFG_CODE == code);
                        if (search.listHisFormTypeCfgExpression != null && search.listHisFormTypeCfgExpression.Count > 0)
                        {
                            foreach (var item in search.listHisFormTypeCfgExpression)
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
