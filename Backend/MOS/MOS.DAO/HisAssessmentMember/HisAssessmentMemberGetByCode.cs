using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisAssessmentMember
{
    partial class HisAssessmentMemberGet : EntityBase
    {
        public HIS_ASSESSMENT_MEMBER GetByCode(string code, HisAssessmentMemberSO search)
        {
            HIS_ASSESSMENT_MEMBER result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_ASSESSMENT_MEMBER.AsQueryable().Where(p => p.ASSESSMENT_MEMBER_CODE == code);
                        if (search.listHisAssessmentMemberExpression != null && search.listHisAssessmentMemberExpression.Count > 0)
                        {
                            foreach (var item in search.listHisAssessmentMemberExpression)
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
