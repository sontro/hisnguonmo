using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisDepartmentTran
{
    partial class HisDepartmentTranGet : EntityBase
    {
        public HIS_DEPARTMENT_TRAN GetByCode(string code, HisDepartmentTranSO search)
        {
            HIS_DEPARTMENT_TRAN result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_DEPARTMENT_TRAN.AsQueryable().Where(p => p.DEPARTMENT_TRAN_CODE == code);
                        if (search.listHisDepartmentTranExpression != null && search.listHisDepartmentTranExpression.Count > 0)
                        {
                            foreach (var item in search.listHisDepartmentTranExpression)
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
