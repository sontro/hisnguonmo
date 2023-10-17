using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisSourceMedicine
{
    partial class HisSourceMedicineGet : EntityBase
    {
        public V_HIS_SOURCE_MEDICINE GetViewByCode(string code, HisSourceMedicineSO search)
        {
            V_HIS_SOURCE_MEDICINE result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_SOURCE_MEDICINE.AsQueryable().Where(p => p.SOURCE_MEDICINE_CODE == code);
                        if (search.listVHisSourceMedicineExpression != null && search.listVHisSourceMedicineExpression.Count > 0)
                        {
                            foreach (var item in search.listVHisSourceMedicineExpression)
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
