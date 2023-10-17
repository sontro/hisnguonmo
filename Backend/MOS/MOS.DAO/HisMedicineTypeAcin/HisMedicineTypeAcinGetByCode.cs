using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMedicineTypeAcin
{
    partial class HisMedicineTypeAcinGet : EntityBase
    {
        public HIS_MEDICINE_TYPE_ACIN GetByCode(string code, HisMedicineTypeAcinSO search)
        {
            HIS_MEDICINE_TYPE_ACIN result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_MEDICINE_TYPE_ACIN.AsQueryable().Where(p => p.MEDICINE_TYPE_ACIN_CODE == code);
                        if (search.listHisMedicineTypeAcinExpression != null && search.listHisMedicineTypeAcinExpression.Count > 0)
                        {
                            foreach (var item in search.listHisMedicineTypeAcinExpression)
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
