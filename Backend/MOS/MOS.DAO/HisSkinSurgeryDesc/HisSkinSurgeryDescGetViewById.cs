using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisSkinSurgeryDesc
{
    partial class HisSkinSurgeryDescGet : EntityBase
    {
        public V_HIS_SKIN_SURGERY_DESC GetViewById(long id, HisSkinSurgeryDescSO search)
        {
            V_HIS_SKIN_SURGERY_DESC result = null;
            try
            {
                bool valid = true;
                valid = valid && IsGreaterThanZero(id);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_SKIN_SURGERY_DESC.AsQueryable().Where(p => p.ID == id);
                        if (search.listVHisSkinSurgeryDescExpression != null && search.listVHisSkinSurgeryDescExpression.Count > 0)
                        {
                            foreach (var item in search.listVHisSkinSurgeryDescExpression)
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
                Logging(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => id), id) + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => search), search), LogType.Error);
                LogSystem.Error(ex);
                result = null;
            }
            return result;
        }
    }
}
