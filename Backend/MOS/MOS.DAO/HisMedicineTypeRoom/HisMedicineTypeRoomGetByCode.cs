using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMedicineTypeRoom
{
    partial class HisMedicineTypeRoomGet : EntityBase
    {
        public HIS_MEDICINE_TYPE_ROOM GetByCode(string code, HisMedicineTypeRoomSO search)
        {
            HIS_MEDICINE_TYPE_ROOM result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_MEDICINE_TYPE_ROOM.AsQueryable().Where(p => p.MEDICINE_TYPE_ROOM_CODE == code);
                        if (search.listHisMedicineTypeRoomExpression != null && search.listHisMedicineTypeRoomExpression.Count > 0)
                        {
                            foreach (var item in search.listHisMedicineTypeRoomExpression)
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
