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
        public HIS_OBEY_CONTRAINDI GetByCode(string code, HisObeyContraindiSO search)
        {
            HIS_OBEY_CONTRAINDI result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_OBEY_CONTRAINDI.AsQueryable().Where(p => p.OBEY_CONTRAINDI_CODE == code);
                        if (search.listHisObeyContraindiExpression != null && search.listHisObeyContraindiExpression.Count > 0)
                        {
                            foreach (var item in search.listHisObeyContraindiExpression)
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
