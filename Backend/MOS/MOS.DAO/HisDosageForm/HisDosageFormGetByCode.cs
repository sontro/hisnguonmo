using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisDosageForm
{
    partial class HisDosageFormGet : EntityBase
    {
        public HIS_DOSAGE_FORM GetByCode(string code, HisDosageFormSO search)
        {
            HIS_DOSAGE_FORM result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_DOSAGE_FORM.AsQueryable().Where(p => p.DOSAGE_FORM_CODE == code);
                        if (search.listHisDosageFormExpression != null && search.listHisDosageFormExpression.Count > 0)
                        {
                            foreach (var item in search.listHisDosageFormExpression)
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
