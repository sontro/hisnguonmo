using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisSereServBill
{
    partial class HisSereServBillGet : EntityBase
    {
        public HIS_SERE_SERV_BILL GetByCode(string code, HisSereServBillSO search)
        {
            HIS_SERE_SERV_BILL result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_SERE_SERV_BILL.AsQueryable().Where(p => p.SERE_SERV_BILL_CODE == code);
                        if (search.listHisSereServBillExpression != null && search.listHisSereServBillExpression.Count > 0)
                        {
                            foreach (var item in search.listHisSereServBillExpression)
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
