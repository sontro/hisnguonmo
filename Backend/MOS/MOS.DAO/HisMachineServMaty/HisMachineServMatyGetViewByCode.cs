using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMachineServMaty
{
    partial class HisMachineServMatyGet : EntityBase
    {
        public V_HIS_MACHINE_SERV_MATY GetViewByCode(string code, HisMachineServMatySO search)
        {
            V_HIS_MACHINE_SERV_MATY result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_MACHINE_SERV_MATY.AsQueryable().Where(p => p.MACHINE_SERV_MATY_CODE == code);
                        if (search.listVHisMachineServMatyExpression != null && search.listVHisMachineServMatyExpression.Count > 0)
                        {
                            foreach (var item in search.listVHisMachineServMatyExpression)
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
