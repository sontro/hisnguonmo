using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisImpMestMedicine
{
    partial class HisImpMestMedicineGet : EntityBase
    {
        public HIS_IMP_MEST_MEDICINE GetByCode(string code, HisImpMestMedicineSO search)
        {
            HIS_IMP_MEST_MEDICINE result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_IMP_MEST_MEDICINE.AsQueryable().Where(p => p.IMP_MEST_MEDICINE_CODE == code);
                        if (search.listHisImpMestMedicineExpression != null && search.listHisImpMestMedicineExpression.Count > 0)
                        {
                            foreach (var item in search.listHisImpMestMedicineExpression)
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
