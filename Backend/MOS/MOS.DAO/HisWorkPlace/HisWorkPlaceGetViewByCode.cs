using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisWorkPlace
{
    partial class HisWorkPlaceGet : EntityBase
    {
        public V_HIS_WORK_PLACE GetViewByCode(string code, HisWorkPlaceSO search)
        {
            V_HIS_WORK_PLACE result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_WORK_PLACE.AsQueryable().Where(p => p.WORK_PLACE_CODE == code);
                        if (search.listVHisWorkPlaceExpression != null && search.listVHisWorkPlaceExpression.Count > 0)
                        {
                            foreach (var item in search.listVHisWorkPlaceExpression)
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
