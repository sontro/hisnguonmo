using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisEinvoiceType
{
    partial class HisEinvoiceTypeGet : EntityBase
    {
        public HIS_EINVOICE_TYPE GetByCode(string code, HisEinvoiceTypeSO search)
        {
            HIS_EINVOICE_TYPE result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_EINVOICE_TYPE.AsQueryable().Where(p => p.EINVOICE_TYPE_CODE == code);
                        if (search.listHisEinvoiceTypeExpression != null && search.listHisEinvoiceTypeExpression.Count > 0)
                        {
                            foreach (var item in search.listHisEinvoiceTypeExpression)
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
