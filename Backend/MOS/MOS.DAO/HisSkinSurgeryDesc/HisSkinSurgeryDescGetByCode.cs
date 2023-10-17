using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisSkinSurgeryDesc
{
    partial class HisSkinSurgeryDescGet : EntityBase
    {
        public HIS_SKIN_SURGERY_DESC GetByCode(string code, HisSkinSurgeryDescSO search)
        {
            HIS_SKIN_SURGERY_DESC result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_SKIN_SURGERY_DESC.AsQueryable().Where(p => p.SKIN_SURGERY_DESC_CODE == code);
                        if (search.listHisSkinSurgeryDescExpression != null && search.listHisSkinSurgeryDescExpression.Count > 0)
                        {
                            foreach (var item in search.listHisSkinSurgeryDescExpression)
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
