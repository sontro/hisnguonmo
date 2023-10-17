using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisVaccineType
{
    partial class HisVaccineTypeGet : EntityBase
    {
        public V_HIS_VACCINE_TYPE GetViewByCode(string code, HisVaccineTypeSO search)
        {
            V_HIS_VACCINE_TYPE result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_VACCINE_TYPE.AsQueryable().Where(p => p.VACCINE_TYPE_CODE == code);
                        if (search.listVHisVaccineTypeExpression != null && search.listVHisVaccineTypeExpression.Count > 0)
                        {
                            foreach (var item in search.listVHisVaccineTypeExpression)
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
