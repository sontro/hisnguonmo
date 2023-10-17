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
        public V_HIS_DOSAGE_FORM GetViewByCode(string code, HisDosageFormSO search)
        {
            V_HIS_DOSAGE_FORM result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_DOSAGE_FORM.AsQueryable().Where(p => p.DOSAGE_FORM_CODE == code);
                        if (search.listVHisDosageFormExpression != null && search.listVHisDosageFormExpression.Count > 0)
                        {
                            foreach (var item in search.listVHisDosageFormExpression)
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