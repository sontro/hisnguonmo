using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisCaroDepartment
{
    partial class HisCaroDepartmentGet : EntityBase
    {
        public HIS_CARO_DEPARTMENT GetByCode(string code, HisCaroDepartmentSO search)
        {
            HIS_CARO_DEPARTMENT result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_CARO_DEPARTMENT.AsQueryable().Where(p => p.CARO_DEPARTMENT_CODE == code);
                        if (search.listHisCaroDepartmentExpression != null && search.listHisCaroDepartmentExpression.Count > 0)
                        {
                            foreach (var item in search.listHisCaroDepartmentExpression)
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
