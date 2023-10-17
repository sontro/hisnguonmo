using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisEmteMaterialType
{
    partial class HisEmteMaterialTypeGet : EntityBase
    {
        public V_HIS_EMTE_MATERIAL_TYPE GetViewByCode(string code, HisEmteMaterialTypeSO search)
        {
            V_HIS_EMTE_MATERIAL_TYPE result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_EMTE_MATERIAL_TYPE.AsQueryable().Where(p => p.EMTE_MATERIAL_TYPE_CODE == code);
                        if (search.listVHisEmteMaterialTypeExpression != null && search.listVHisEmteMaterialTypeExpression.Count > 0)
                        {
                            foreach (var item in search.listVHisEmteMaterialTypeExpression)
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
