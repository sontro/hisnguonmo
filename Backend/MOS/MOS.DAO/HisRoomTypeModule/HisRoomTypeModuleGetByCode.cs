using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisRoomTypeModule
{
    partial class HisRoomTypeModuleGet : EntityBase
    {
        public HIS_ROOM_TYPE_MODULE GetByCode(string code, HisRoomTypeModuleSO search)
        {
            HIS_ROOM_TYPE_MODULE result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_ROOM_TYPE_MODULE.AsQueryable().Where(p => p.ROOM_TYPE_MODULE_CODE == code);
                        if (search.listHisRoomTypeModuleExpression != null && search.listHisRoomTypeModuleExpression.Count > 0)
                        {
                            foreach (var item in search.listHisRoomTypeModuleExpression)
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
