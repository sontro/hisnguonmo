using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMedicineService
{
    partial class HisMedicineServiceGet : EntityBase
    {
        public V_HIS_MEDICINE_SERVICE GetViewByCode(string code, HisMedicineServiceSO search)
        {
            V_HIS_MEDICINE_SERVICE result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_MEDICINE_SERVICE.AsQueryable().Where(p => p.MEDICINE_SERVICE_CODE == code);
                        if (search.listVHisMedicineServiceExpression != null && search.listVHisMedicineServiceExpression.Count > 0)
                        {
                            foreach (var item in search.listVHisMedicineServiceExpression)
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
