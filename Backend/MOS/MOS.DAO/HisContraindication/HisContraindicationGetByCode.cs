using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisContraindication
{
    partial class HisContraindicationGet : EntityBase
    {
        public HIS_CONTRAINDICATION GetByCode(string code, HisContraindicationSO search)
        {
            HIS_CONTRAINDICATION result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_CONTRAINDICATION.AsQueryable().Where(p => p.CONTRAINDICATION_CODE == code);
                        if (search.listHisContraindicationExpression != null && search.listHisContraindicationExpression.Count > 0)
                        {
                            foreach (var item in search.listHisContraindicationExpression)
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
