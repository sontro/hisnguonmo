using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisDocHoldType
{
    partial class HisDocHoldTypeGet : EntityBase
    {
        public HIS_DOC_HOLD_TYPE GetByCode(string code, HisDocHoldTypeSO search)
        {
            HIS_DOC_HOLD_TYPE result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_DOC_HOLD_TYPE.AsQueryable().Where(p => p.DOC_HOLD_TYPE_CODE == code);
                        if (search.listHisDocHoldTypeExpression != null && search.listHisDocHoldTypeExpression.Count > 0)
                        {
                            foreach (var item in search.listHisDocHoldTypeExpression)
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
