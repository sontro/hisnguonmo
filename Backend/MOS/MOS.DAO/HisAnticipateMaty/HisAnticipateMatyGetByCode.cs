using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisAnticipateMaty
{
    partial class HisAnticipateMatyGet : EntityBase
    {
        public HIS_ANTICIPATE_MATY GetByCode(string code, HisAnticipateMatySO search)
        {
            HIS_ANTICIPATE_MATY result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_ANTICIPATE_MATY.AsQueryable().Where(p => p.ANTICIPATE_MATY_CODE == code);
                        if (search.listHisAnticipateMatyExpression != null && search.listHisAnticipateMatyExpression.Count > 0)
                        {
                            foreach (var item in search.listHisAnticipateMatyExpression)
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
