using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisSampleRoom
{
    partial class HisSampleRoomGet : EntityBase
    {
        public V_HIS_SAMPLE_ROOM GetViewByCode(string code, HisSampleRoomSO search)
        {
            V_HIS_SAMPLE_ROOM result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_SAMPLE_ROOM.AsQueryable().Where(p => p.SAMPLE_ROOM_CODE == code);
                        if (search.listVHisSampleRoomExpression != null && search.listVHisSampleRoomExpression.Count > 0)
                        {
                            foreach (var item in search.listVHisSampleRoomExpression)
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
