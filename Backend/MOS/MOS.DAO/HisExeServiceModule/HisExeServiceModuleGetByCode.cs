using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisExeServiceModule
{
    partial class HisExeServiceModuleGet : EntityBase
    {
        public HIS_EXE_SERVICE_MODULE GetByCode(string code, HisExeServiceModuleSO search)
        {
            HIS_EXE_SERVICE_MODULE result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_EXE_SERVICE_MODULE.AsQueryable().Where(p => p.EXE_SERVICE_MODULE_CODE == code);
                        if (search.listHisExeServiceModuleExpression != null && search.listHisExeServiceModuleExpression.Count > 0)
                        {
                            foreach (var item in search.listHisExeServiceModuleExpression)
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
