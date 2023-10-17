using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisHoreHoha
{
    partial class HisHoreHohaGet : EntityBase
    {
        public HIS_HORE_HOHA GetByCode(string code, HisHoreHohaSO search)
        {
            HIS_HORE_HOHA result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_HORE_HOHA.AsQueryable().Where(p => p.HORE_HOHA_CODE == code);
                        if (search.listHisHoreHohaExpression != null && search.listHisHoreHohaExpression.Count > 0)
                        {
                            foreach (var item in search.listHisHoreHohaExpression)
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