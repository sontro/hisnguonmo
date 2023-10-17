using Inventec.Common.Logging;
using Inventec.Core;
using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using System;
using System.Linq;

namespace MOS.DAO.HisBedRoom
{
    partial class HisBedRoomGet : EntityBase
    {
        public V_HIS_BED_ROOM_1 GetView1ByCode(string code, HisBedRoomSO search)
        {
            V_HIS_BED_ROOM_1 result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_BED_ROOM_1.AsQueryable().Where(p => p.BED_ROOM_CODE == code);
                        if (search.listVHisBedRoom1Expression != null && search.listVHisBedRoom1Expression.Count > 0)
                        {
                            foreach (var item in search.listVHisBedRoom1Expression)
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
