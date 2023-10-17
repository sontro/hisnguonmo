using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMaterialMaterial
{
    partial class HisMaterialMaterialGet : EntityBase
    {
        public HIS_MATERIAL_MATERIAL GetByCode(string code, HisMaterialMaterialSO search)
        {
            HIS_MATERIAL_MATERIAL result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_MATERIAL_MATERIAL.AsQueryable().Where(p => p.MATERIAL_MATERIAL_CODE == code);
                        if (search.listHisMaterialMaterialExpression != null && search.listHisMaterialMaterialExpression.Count > 0)
                        {
                            foreach (var item in search.listHisMaterialMaterialExpression)
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
