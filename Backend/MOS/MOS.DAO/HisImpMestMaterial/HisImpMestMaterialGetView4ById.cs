using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisImpMestMaterial
{
    partial class HisImpMestMaterialGet : EntityBase
    {
        public V_HIS_IMP_MEST_MATERIAL_4 GetView4ById(long id, HisImpMestMaterialSO search)
        {
            V_HIS_IMP_MEST_MATERIAL_4 result = null;
            try
            {
                bool valid = true;
                valid = valid && IsGreaterThanZero(id);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_IMP_MEST_MATERIAL_4.AsQueryable().Where(p => p.ID == id);
                        if (search.listVHisImpMestMaterial4Expression != null && search.listVHisImpMestMaterial4Expression.Count > 0)
                        {
                            foreach (var item in search.listVHisImpMestMaterial4Expression)
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
                Logging(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => id), id) + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => search), search), LogType.Error);
                LogSystem.Error(ex);
                result = null;
            }
            return result;
        }
    }
}
