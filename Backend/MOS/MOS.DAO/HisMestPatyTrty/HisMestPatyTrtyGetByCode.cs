using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMestPatyTrty
{
    partial class HisMestPatyTrtyGet : EntityBase
    {
        public HIS_MEST_PATY_TRTY GetByCode(string code, HisMestPatyTrtySO search)
        {
            HIS_MEST_PATY_TRTY result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_MEST_PATY_TRTY.AsQueryable().Where(p => p.MEST_PATY_TRTY_CODE == code);
                        if (search.listHisMestPatyTrtyExpression != null && search.listHisMestPatyTrtyExpression.Count > 0)
                        {
                            foreach (var item in search.listHisMestPatyTrtyExpression)
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