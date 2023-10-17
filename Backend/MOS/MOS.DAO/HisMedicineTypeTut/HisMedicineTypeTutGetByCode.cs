using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMedicineTypeTut
{
    partial class HisMedicineTypeTutGet : EntityBase
    {
        public HIS_MEDICINE_TYPE_TUT GetByCode(string code, HisMedicineTypeTutSO search)
        {
            HIS_MEDICINE_TYPE_TUT result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_MEDICINE_TYPE_TUT.AsQueryable().Where(p => p.MEDICINE_TYPE_TUT_CODE == code);
                        if (search.listHisMedicineTypeTutExpression != null && search.listHisMedicineTypeTutExpression.Count > 0)
                        {
                            foreach (var item in search.listHisMedicineTypeTutExpression)
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
