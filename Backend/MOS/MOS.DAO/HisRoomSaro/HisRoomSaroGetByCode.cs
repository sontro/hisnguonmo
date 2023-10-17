using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisRoomSaro
{
    partial class HisRoomSaroGet : EntityBase
    {
        public HIS_ROOM_SARO GetByCode(string code, HisRoomSaroSO search)
        {
            HIS_ROOM_SARO result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_ROOM_SARO.AsQueryable().Where(p => p.ROOM_SARO_CODE == code);
                        if (search.listHisRoomSaroExpression != null && search.listHisRoomSaroExpression.Count > 0)
                        {
                            foreach (var item in search.listHisRoomSaroExpression)
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
