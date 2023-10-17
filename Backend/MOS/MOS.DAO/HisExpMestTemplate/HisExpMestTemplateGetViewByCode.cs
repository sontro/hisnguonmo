using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisExpMestTemplate
{
    partial class HisExpMestTemplateGet : EntityBase
    {
        public V_HIS_EXP_MEST_TEMPLATE GetViewByCode(string code, HisExpMestTemplateSO search)
        {
            V_HIS_EXP_MEST_TEMPLATE result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_EXP_MEST_TEMPLATE.AsQueryable().Where(p => p.EXP_MEST_TEMPLATE_CODE == code);
                        if (search.listVHisExpMestTemplateExpression != null && search.listVHisExpMestTemplateExpression.Count > 0)
                        {
                            foreach (var item in search.listVHisExpMestTemplateExpression)
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
