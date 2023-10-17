using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisEyeSurgryDesc
{
    partial class HisEyeSurgryDescGet : EntityBase
    {
        public HIS_EYE_SURGRY_DESC GetByCode(string code, HisEyeSurgryDescSO search)
        {
            HIS_EYE_SURGRY_DESC result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_EYE_SURGRY_DESC.AsQueryable().Where(p => p.EYE_SURGRY_DESC_CODE == code);
                        if (search.listHisEyeSurgryDescExpression != null && search.listHisEyeSurgryDescExpression.Count > 0)
                        {
                            foreach (var item in search.listHisEyeSurgryDescExpression)
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
