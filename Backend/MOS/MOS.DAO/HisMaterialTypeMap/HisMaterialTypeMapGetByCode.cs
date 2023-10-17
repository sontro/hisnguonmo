using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMaterialTypeMap
{
    partial class HisMaterialTypeMapGet : EntityBase
    {
        public HIS_MATERIAL_TYPE_MAP GetByCode(string code, HisMaterialTypeMapSO search)
        {
            HIS_MATERIAL_TYPE_MAP result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_MATERIAL_TYPE_MAP.AsQueryable().Where(p => p.MATERIAL_TYPE_MAP_CODE == code);
                        if (search.listHisMaterialTypeMapExpression != null && search.listHisMaterialTypeMapExpression.Count > 0)
                        {
                            foreach (var item in search.listHisMaterialTypeMapExpression)
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
