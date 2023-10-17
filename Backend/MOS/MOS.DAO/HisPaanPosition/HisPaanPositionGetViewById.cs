using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisPaanPosition
{
    partial class HisPaanPositionGet : EntityBase
    {
        public V_HIS_PAAN_POSITION GetViewById(long id, HisPaanPositionSO search)
        {
            V_HIS_PAAN_POSITION result = null;
            try
            {
                bool valid = true;
                valid = valid && IsGreaterThanZero(id);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_PAAN_POSITION.AsQueryable().Where(p => p.ID == id);
                        if (search.listVHisPaanPositionExpression != null && search.listVHisPaanPositionExpression.Count > 0)
                        {
                            foreach (var item in search.listVHisPaanPositionExpression)
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
