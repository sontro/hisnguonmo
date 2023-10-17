using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisAntigenMety
{
    partial class HisAntigenMetyGet : EntityBase
    {
        public HIS_ANTIGEN_METY GetByCode(string code, HisAntigenMetySO search)
        {
            HIS_ANTIGEN_METY result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_ANTIGEN_METY.AsQueryable().Where(p => p.ANTIGEN_METY_CODE == code);
                        if (search.listHisAntigenMetyExpression != null && search.listHisAntigenMetyExpression.Count > 0)
                        {
                            foreach (var item in search.listHisAntigenMetyExpression)
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
