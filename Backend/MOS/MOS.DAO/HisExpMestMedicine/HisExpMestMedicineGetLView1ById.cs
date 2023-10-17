using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisExpMestMedicine
{
    partial class HisExpMestMedicineGet : EntityBase
    {
        public L_HIS_EXP_MEST_MEDICINE_1 GetLView1ById(long id, HisExpMestMedicineSO search)
        {
            L_HIS_EXP_MEST_MEDICINE_1 result = null;
            try
            {
                bool valid = true;
                valid = valid && IsGreaterThanZero(id);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.L_HIS_EXP_MEST_MEDICINE_1.AsQueryable().Where(p => p.ID == id);
                        if (search.listLHisExpMestMedicine1Expression != null && search.listLHisExpMestMedicine1Expression.Count > 0)
                        {
                            foreach (var item in search.listLHisExpMestMedicine1Expression)
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
                Logging(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => id), id) + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => search), search), LogType.Error);
                LogSystem.Error(ex);
                result = null;
            }
            return result;
        }
    }
}
