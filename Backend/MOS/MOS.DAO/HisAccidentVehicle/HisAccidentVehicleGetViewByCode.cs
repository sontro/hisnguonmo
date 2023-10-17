using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisAccidentVehicle
{
    partial class HisAccidentVehicleGet : EntityBase
    {
        public V_HIS_ACCIDENT_VEHICLE GetViewByCode(string code, HisAccidentVehicleSO search)
        {
            V_HIS_ACCIDENT_VEHICLE result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_ACCIDENT_VEHICLE.AsQueryable().Where(p => p.ACCIDENT_VEHICLE_CODE == code);
                        if (search.listVHisAccidentVehicleExpression != null && search.listVHisAccidentVehicleExpression.Count > 0)
                        {
                            foreach (var item in search.listVHisAccidentVehicleExpression)
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
