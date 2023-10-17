using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMestPatientType
{
    partial class HisMestPatientTypeGet : EntityBase
    {
        public HIS_MEST_PATIENT_TYPE GetByCode(string code, HisMestPatientTypeSO search)
        {
            HIS_MEST_PATIENT_TYPE result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_MEST_PATIENT_TYPE.AsQueryable().Where(p => p.MEST_PATIENT_TYPE_CODE == code);
                        if (search.listHisMestPatientTypeExpression != null && search.listHisMestPatientTypeExpression.Count > 0)
                        {
                            foreach (var item in search.listHisMestPatientTypeExpression)
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
