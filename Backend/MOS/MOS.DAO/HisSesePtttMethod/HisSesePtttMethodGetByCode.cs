using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisSesePtttMethod
{
    partial class HisSesePtttMethodGet : EntityBase
    {
        public HIS_SESE_PTTT_METHOD GetByCode(string code, HisSesePtttMethodSO search)
        {
            HIS_SESE_PTTT_METHOD result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_SESE_PTTT_METHOD.AsQueryable().Where(p => p.SESE_PTTT_METHOD_CODE == code);
                        if (search.listHisSesePtttMethodExpression != null && search.listHisSesePtttMethodExpression.Count > 0)
                        {
                            foreach (var item in search.listHisSesePtttMethodExpression)
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
