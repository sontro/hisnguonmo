using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMedicineInteractive
{
    partial class HisMedicineInteractiveGet : EntityBase
    {
        public V_HIS_MEDICINE_INTERACTIVE GetViewByCode(string code, HisMedicineInteractiveSO search)
        {
            V_HIS_MEDICINE_INTERACTIVE result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_MEDICINE_INTERACTIVE.AsQueryable().Where(p => p.MEDICINE_INTERACTIVE_CODE == code);
                        if (search.listVHisMedicineInteractiveExpression != null && search.listVHisMedicineInteractiveExpression.Count > 0)
                        {
                            foreach (var item in search.listVHisMedicineInteractiveExpression)
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
