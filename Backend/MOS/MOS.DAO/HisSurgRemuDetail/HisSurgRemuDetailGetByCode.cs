using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisSurgRemuDetail
{
    partial class HisSurgRemuDetailGet : EntityBase
    {
        public HIS_SURG_REMU_DETAIL GetByCode(string code, HisSurgRemuDetailSO search)
        {
            HIS_SURG_REMU_DETAIL result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_SURG_REMU_DETAIL.AsQueryable().Where(p => p.SURG_REMU_DETAIL_CODE == code);
                        if (search.listHisSurgRemuDetailExpression != null && search.listHisSurgRemuDetailExpression.Count > 0)
                        {
                            foreach (var item in search.listHisSurgRemuDetailExpression)
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
