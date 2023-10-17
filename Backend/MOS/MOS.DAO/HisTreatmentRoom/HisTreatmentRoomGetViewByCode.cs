using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisTreatmentRoom
{
    partial class HisTreatmentRoomGet : EntityBase
    {
        public V_HIS_TREATMENT_ROOM GetViewByCode(string code, HisTreatmentRoomSO search)
        {
            V_HIS_TREATMENT_ROOM result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_TREATMENT_ROOM.AsQueryable().Where(p => p.TREATMENT_ROOM_CODE == code);
                        if (search.listVHisTreatmentRoomExpression != null && search.listVHisTreatmentRoomExpression.Count > 0)
                        {
                            foreach (var item in search.listVHisTreatmentRoomExpression)
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
