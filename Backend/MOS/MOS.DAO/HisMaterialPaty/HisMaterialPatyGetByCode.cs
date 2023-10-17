using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMaterialPaty
{
    partial class HisMaterialPatyGet : EntityBase
    {
        public HIS_MATERIAL_PATY GetByCode(string code, HisMaterialPatySO search)
        {
            HIS_MATERIAL_PATY result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_MATERIAL_PATY.AsQueryable().Where(p => p.MATERIAL_PATY_CODE == code);
                        if (search.listHisMaterialPatyExpression != null && search.listHisMaterialPatyExpression.Count > 0)
                        {
                            foreach (var item in search.listHisMaterialPatyExpression)
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
