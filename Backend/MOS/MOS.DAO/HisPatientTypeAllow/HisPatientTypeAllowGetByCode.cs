using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisPatientTypeAllow
{
    partial class HisPatientTypeAllowGet : EntityBase
    {
        public HIS_PATIENT_TYPE_ALLOW GetByCode(string code, HisPatientTypeAllowSO search)
        {
            HIS_PATIENT_TYPE_ALLOW result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_PATIENT_TYPE_ALLOW.AsQueryable().Where(p => p.PATIENT_TYPE_ALLOW_CODE == code);
                        if (search.listHisPatientTypeAllowExpression != null && search.listHisPatientTypeAllowExpression.Count > 0)
                        {
                            foreach (var item in search.listHisPatientTypeAllowExpression)
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
