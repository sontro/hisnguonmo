using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisEmrCoverConfig
{
    partial class HisEmrCoverConfigGet : EntityBase
    {
        public HIS_EMR_COVER_CONFIG GetByCode(string code, HisEmrCoverConfigSO search)
        {
            HIS_EMR_COVER_CONFIG result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_EMR_COVER_CONFIG.AsQueryable().Where(p => p.EMR_COVER_CONFIG_CODE == code);
                        if (search.listHisEmrCoverConfigExpression != null && search.listHisEmrCoverConfigExpression.Count > 0)
                        {
                            foreach (var item in search.listHisEmrCoverConfigExpression)
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
