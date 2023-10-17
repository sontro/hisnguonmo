using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisPatientClassify
{
    partial class HisPatientClassifyGet : EntityBase
    {
        public HIS_PATIENT_CLASSIFY GetByCode(string code, HisPatientClassifySO search)
        {
            HIS_PATIENT_CLASSIFY result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_PATIENT_CLASSIFY.AsQueryable().Where(p => p.PATIENT_CLASSIFY_CODE == code);
                        if (search.listHisPatientClassifyExpression != null && search.listHisPatientClassifyExpression.Count > 0)
                        {
                            foreach (var item in search.listHisPatientClassifyExpression)
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
