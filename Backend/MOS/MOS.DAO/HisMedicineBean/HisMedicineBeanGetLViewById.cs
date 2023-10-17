using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMedicineBean
{
    partial class HisMedicineBeanGet : EntityBase
    {
        public L_HIS_MEDICINE_BEAN GetLViewById(long id, HisMedicineBeanSO search)
        {
            L_HIS_MEDICINE_BEAN result = null;
            try
            {
                bool valid = true;
                valid = valid && IsGreaterThanZero(id);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.L_HIS_MEDICINE_BEAN.AsQueryable().Where(p => p.ID == id);
                        if (search.listLHisMedicineBeanExpression != null && search.listLHisMedicineBeanExpression.Count > 0)
                        {
                            foreach (var item in search.listLHisMedicineBeanExpression)
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
